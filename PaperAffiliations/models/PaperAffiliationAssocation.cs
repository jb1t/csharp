using Microsoft.Extensions.Configuration;


namespace PaperAffiliations
{
    public class PAAssociation : IPopulateRecord<PAAssociation>
    {
        public string PaperId { get; set; }
        public string AffiliationId { get; set; }
        public string AuthorId { get; set; }
           
        public PAAssociation() {}

        public PAAssociation PopulateRecord(string line)
        {
            var fields = line.Split("\t");

            this.PaperId = fields[0];
            this.AuthorId = fields[1];
            this.AffiliationId = fields[2];
            
            return this;
        }

        public override string ToString()
        {
            return $"PaperId: {this.PaperId}, AffiliationId: {this.AffiliationId}, AuthorId: {this.AuthorId}";
        }

    }
}