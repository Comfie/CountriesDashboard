namespace CountryDashboard.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Iso2Code { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string AdminRegion { get; set; }
        public string IncomeLevel { get; set; }
        public string LendingType { get; set; }
        public string CapitalCity { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}