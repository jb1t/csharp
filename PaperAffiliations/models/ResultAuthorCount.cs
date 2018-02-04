namespace PaperAffiliations
{
    class ResultAuthorCount
    {
        public string ConferenceId { get; set; }
        public int Year { get; set; }
        public string AffiliationId { get; set; }
        public int CountOfAuthors { get; set; }
        
        public ResultAuthorCount()
        {}
    }
}