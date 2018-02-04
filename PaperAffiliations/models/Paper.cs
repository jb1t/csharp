using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace PaperAffiliations
{
    public class Paper : IPopulateRecord<Paper>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public string ConferenceId { get; set; }
        public string ConferenceAbrv { get; set; }
        public Paper(){}

        public Paper PopulateRecord(string line)
        {
            var fields = line.Split("\t");

            this.Id = fields[0];
            this.Title = fields[1];
            this.PublishYear = int.Parse(fields[2]);
            this.ConferenceId = fields[3];
            this.ConferenceAbrv = fields[4];
            
            return this;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}, Title: {this.Title}, PublishYear: {this.PublishYear}, ConferenceId: {this.ConferenceId}, ConferenceAbrv: {this.ConferenceAbrv}";
        }

        public string GetUrl(IConfiguration config)
        {
            var urls = config.GetSection("Urls");
            return urls["Paper"];
        }
    }
}