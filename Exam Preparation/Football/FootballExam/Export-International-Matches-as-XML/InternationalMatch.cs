namespace Export_International_Matches_as_XML
{
    using System;

    public class InternationalMatch
    {
        public string LeagueName { get; set; }

        public string HomeCountryName { get; set; }

        public string AwayCountryName { get; set; }

        public string HomeCountryCode { get; set; }
        
        public string AwayCountryCode { get; set; }

        public int? HomeCountryGoals { get; set; }

        public int? AwayCountryGoals { get; set; }

        public DateTime? MatchDate { get; set; }
    }
}
