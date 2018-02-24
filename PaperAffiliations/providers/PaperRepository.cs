using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PaperAffiliations
{
    public class PaperRepository<T> where T : IPopulateRecord<T>, new()
    {
        private IConfiguration _Configuration;
        public PaperRepository(IConfiguration config) => this._Configuration = config;

        public List<T> GetRecords() 
        {
            var url = this.GetUrl();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MS-RTC LM 8; .NET4.0C; .NET4.0E; InfoPath.3) chromeframe/6.0.472.63";
            var content = ReadContent(request);
            var lines = content.Split("\n");

            List<T> records = new List<T>();
            foreach(var line in lines)
            {
                var newRecord = new T();
                if(!string.IsNullOrWhiteSpace(line))
                {
                    records.Add(newRecord.PopulateRecord(line));
                }
            }
            return records;
        }

        private string GetUrl()
        {
            var urls = this._Configuration.GetSection("Urls");
            return urls[typeof(T).ToString()];   
        }

        private string ReadContent(HttpWebRequest request)
        {
            try
			{
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					return reader.ReadToEnd();
				}
			}
			catch (WebException ex)
			{
                return ex.Message;
				//string responseBody = ex.GetResponseBody();
				//throw new ServiceCommunicationException(request.RequestUri.AbsoluteUri, responseBody, ex);
			}
			catch (Exception ex)
			{
                return ex.Message;
				//throw new ServiceCommunicationException(request.RequestUri.AbsoluteUri, null, ex);
			}
        }
    }
}