using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace PaperAffiliations
{
    class Program
    {
        static void Main(string[] args)
        {

            // Adding JSON file into IConfiguration.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
          
            Console.WriteLine("Hello World!");
            var AffiliationProvider = new PaperRepository<Affiliation>(config);
            var PaperProvider = new PaperRepository<Paper>(config);
            var PAAssocProvider = new PaperRepository<PAAssociation>(config);
            
            var affiliations = AffiliationProvider.GetRecords(); //"https://s3.amazonaws.com/kddcup2016-ghen/Affiliations.tsv");
            var papers = PaperProvider.GetRecords(); //"https://s3.amazonaws.com/kddcup2016-ghen/Papers.tsv");
            var associations = PAAssocProvider.GetRecords(); //"https://s3.amazonaws.com/kddcup2016-ghen/PapersAuthorsAffiliations.tsv");

            // Get Results for Problem #1
            var results = GetRankedAffiliationsByPapers(papers, affiliations, associations).OrderBy(c => c.ConferenceId).ThenBy(c => c.Year).ThenByDescending(c => c.CountOfPapers).ToList();
            
            Console.WriteLine("Conference\tYear\tAffiliation\tCountOfPapers");
            foreach(var result in results)
            {
                Console.WriteLine($"{result.ConferenceId}\t{result.Year}\t{result.AffiliationId}\t{result.CountOfPapers}");
            }

            // Get Results for Problem #2
            var results2 = GetRankedAffiliationsByAuthors(papers, affiliations, associations).OrderBy(c => c.ConferenceId).ThenBy(c => c.Year).ThenByDescending(c => c.CountOfAuthors).ToList();

            Console.WriteLine("Conference\tYear\tAffiliation\tCountOfAuthors"); 
            foreach(var result in results2)
            {
                Console.WriteLine($"{result.ConferenceId}\t{result.Year}\t{result.AffiliationId}\t{result.CountOfAuthors}");
            }

            Console.WriteLine("Press any key to continue");
            Console.Read();
        }

        private static IEnumerable<ResultPaperCount> GetRankedAffiliationsByPapers(List<Paper> papers, List<Affiliation> affiliations, List<PAAssociation> associations)
        {
            var distinctResults = (from p in papers
                            join a in associations on p.Id equals a.PaperId
                            join af in affiliations on a.AffiliationId equals af.Id
                        select new { Conference = p.ConferenceId, Year = p.PublishYear, Affiliation = a.AffiliationId, PaperId = p.Id })
                        .GroupBy(g => new {g.Conference, g.Year, g.Affiliation, g.PaperId})
                        .Select(s => s.FirstOrDefault());
            
            return distinctResults.GroupBy(g => new {g.Conference, g.Year, g.Affiliation})
                        .Select(g => new ResultPaperCount() { AffiliationId = g.Key.Affiliation, ConferenceId = g.Key.Conference, Year = g.Key.Year, CountOfPapers = g.Count()});
        }

        private static IEnumerable<ResultAuthorCount> GetRankedAffiliationsByAuthors(List<Paper> papers, List<Affiliation> affiliations, List<PAAssociation> associations)
        {
            var distinctResults = (from p in papers
                            join a in associations on p.Id equals a.PaperId
                            join af in affiliations on a.AffiliationId equals af.Id
                        select new { Conference = p.ConferenceId, Year = p.PublishYear, Affiliation = a.AffiliationId, AuthorId = a.AuthorId })
                        .GroupBy(g => new {g.Conference, g.Year, g.Affiliation, g.AuthorId})
                        .Select(s => s.FirstOrDefault());
            
            return distinctResults.GroupBy(g => new {g.Conference, g.Year, g.Affiliation})
                        .Select(g => new ResultAuthorCount() { AffiliationId = g.Key.Affiliation, ConferenceId = g.Key.Conference, Year = g.Key.Year, CountOfAuthors = g.Count()});
        }
    }
}
