using System;
using System.Linq;

namespace PaperAffiliations
{
    public class Affiliation : IPopulateRecord<Affiliation>
    {
        public Affiliation()
        {}

        public string Id { get; set; }
        public string Name { get; set; }

        public Affiliation PopulateRecord(string line)
        {
            var fields = line.Split("\t");

            this.Id = fields[0];
            this.Name = fields[1];

            return this;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}";
        }
    }
}