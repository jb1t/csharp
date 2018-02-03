using System;

namespace PaperAffiliations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var AffiliationProvider = new PaperRepository<Affiliation>();
            var affiliations = AffiliationProvider.GetRecords("https://s3.amazonaws.com/kddcup2016-ghen/Affiliations.tsv");
            var PaperProvider = new PaperRepository<Paper>();
            var papers = PaperProvider.GetRecords("https://s3.amazonaws.com/kddcup2016-ghen/Papers.tsv");
            var PAAssocProvider = new PaperRepository<PAAssociation>();
            var associations = PAAssocProvider.GetRecords("https://s3.amazonaws.com/kddcup2016-ghen/PapersAuthorsAffiliations.tsv");
            
            affiliations.ForEach(x => Console.WriteLine(x.ToString()));
            papers.ForEach(x => Console.WriteLine(x.ToString()));
            associations.ForEach(x => Console.WriteLine(x.ToString()));

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
