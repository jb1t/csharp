namespace PaperAffiliations
{
    class ResultPaperCount
    {
        public string ConferenceId { get; set; }
        public int Year { get; set; }
        public string AffiliationId { get; set; }
        public int CountOfPapers { get; set; }
        
        public ResultPaperCount()
        {}
    }
}