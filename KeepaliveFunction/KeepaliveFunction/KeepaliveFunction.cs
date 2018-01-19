using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace KeepaliveFunction
{ 
    public static class KeepaliveFunction
    {
        [FunctionName("KeepaliveFunction")]
        public static void Run([TimerTrigger("* 4 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            string url = "http://thefullstacknerd.com";
            log.Info($"{DateTime.Now} KeepaliveFunction started.");

            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var response = Get(url);
                log.Info($"Called {url}, Status Code: {response.StatusCode}, Elapsed Time: {sw.Elapsed.TotalSeconds} secs.");
                sw.Stop();
            }
            catch(Exception ex)
            {
                log.Error($"Error calling {url}. {ex.Message}");
            }


            log.Info($"{DateTime.Now} KeepaliveFunction ended.");
        }

        private static ResponseWrapper Get(string url)
        {
            HttpWebRequest request = CreateRequest(url);
            return ReadResponseString(request);
        }

        private static HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MS-RTC LM 8; .NET4.0C; .NET4.0E; InfoPath.3) chromeframe/6.0.472.63";
            return request;
        }

        private static ResponseWrapper ReadResponseString(HttpWebRequest request)
        {
            string body = string.Empty;
            string statusCode = string.Empty;
            HttpWebResponse response = null;
            try
            {
                using (response = request.GetResponse() as HttpWebResponse)
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    body = reader.ReadToEnd();
                    statusCode = response.StatusCode.ToString();
                }
            }
            catch (WebException ex)
            {
                string responseBody = ex.GetResponseBody();
                throw new ServiceCommunicationException(request.RequestUri.AbsoluteUri, responseBody, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceCommunicationException(request.RequestUri.AbsoluteUri, null, ex);
            }
            return new ResponseWrapper() { ResponseBody = body, StatusCode = statusCode };
        }

    }

    [Serializable]
    public class ServiceCommunicationException : Exception
    {
        public const string ExceptionMessageFormat = "Service Communication Exception. Url=[{0}], ResponseBody=[{1}]";

        public string Url { get; private set; }

        public ServiceCommunicationException() { }

        public ServiceCommunicationException(string url, string responseBody, Exception inner)
            : base(string.Format(ExceptionMessageFormat, url, responseBody), inner)
        {
            Url = url;
        }

        protected ServiceCommunicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    public class ResponseWrapper
    {
        public string ResponseBody { get; set; }
        public string StatusCode { get; set; }
    }

    public static class WebExceptionExtensions
    {
        public static string GetResponseBody(this WebException ex)
        {
            if (ex.Response == null)
            {
                return null;
            }

            var responseStream = ex.Response.GetResponseStream();

            if (responseStream == null)
            {
                return null;
            }

            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        public static bool IsRateLimitException(this WebException ex)
        {
            var httpResponse = ex.Response as HttpWebResponse;

            if (httpResponse == null)
                return false;

            return httpResponse.StatusCode == (HttpStatusCode)HttpStatusCodeAdditional.TooManyRequests;
        }

        public enum HttpStatusCodeAdditional
        {
            //
            // Summary:
            //     Equivalent to HTTP status 429. System.Net.HttpStatusCode.TooManyRequests indicates
            //     that the user has sent too manyrequests in a given amount of time ("rate limiting").
            TooManyRequests = 429,
        }
    }
}
