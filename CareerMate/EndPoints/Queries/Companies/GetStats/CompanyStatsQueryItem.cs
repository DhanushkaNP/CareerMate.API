namespace CareerMate.EndPoints.Queries.Companies.GetStats
{
    public class CompanyStatsQueryItem
    {
        public int ApprovedCompaniesCount { get; set; }

        public int PendingCompaniesCount { get; set; }

        public int BlockedCompaniesCount { get; set; }
    }
}
