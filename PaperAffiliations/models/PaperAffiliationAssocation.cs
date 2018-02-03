namespace PaperAffiliations
{
    public class PAAssociation : IPopulateRecord<PAAssociation>
    {
        public PAAssociation()
        {}

        public string PaperId { get; set; }
        public string AffiliationId { get; set; }
        public string AuthorId { get; set; }

        public PAAssociation PopulateRecord(string line)
        {
            var fields = line.Split("\t");

            this.PaperId = fields[0];
            this.AffiliationId = fields[1];
            this.AuthorId = fields[2];
            
            return this;
        }

        public override string ToString()
        {
            return $"PaperId: {this.PaperId}, AffiliationId: {this.AffiliationId}, AuthorId: {this.AuthorId}";
        }
    }
}