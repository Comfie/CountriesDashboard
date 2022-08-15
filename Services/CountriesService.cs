using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CountryDashboard.Models;
using Newtonsoft.Json.Linq;

namespace CountryDashboard.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly CountryDashboardContext _countryDashboardContext;

        public CountriesService(CountryDashboardContext countryDashboardContext)
        {
            _countryDashboardContext = countryDashboardContext;
        }

        public bool AddCountry(Countries country)
        {
            //Check if country already exists
            var checkCountry =
                _countryDashboardContext.Countries.FirstOrDefault(c => c.CountryCode == country.CountryCode);

            //Check if the number of countries is less than 10
            var numberOfCountries = _countryDashboardContext.Countries.Count();

            if (checkCountry == null && numberOfCountries < 10)
            {
                _countryDashboardContext.Countries.Add(country);
                _countryDashboardContext.SaveChanges();
                return true;
            }

            //Country with same country code already exists or you have reached the limit
            return false;
        }

        public async Task<List<CountryViewModel>> GetAllCountries()
        {
            try
            {
                var countriesList = new List<CountryViewModel>();

                var countries = _countryDashboardContext.Countries.ToList();

                foreach (var country in countries)
                {
                    HttpClient client = new HttpClient();
                    var url = "https://api.worldbank.org/v2/country/" + country.CountryCode + "?format=json";
                    var request = GetMessage(url);
                    var responseMessage = await client.SendAsync(request);
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse("{ \"Data\":" + response + "}");
                    var countryData = obj["Data"][1][0];

                    countriesList.Add(new CountryViewModel()
                    {
                        Id = (int)country.Id,
                        Name = country.Name,
                        Iso2Code = country.CountryCode,
                        AdminRegion = (string)countryData["adminregion"]["value"],
                        Region = (string)countryData["region"]["value"],
                        IncomeLevel = (string)countryData["incomeLevel"]["value"],
                        LendingType = (string)countryData["lendingType"]["value"],
                        CapitalCity = (string)countryData["capitalCity"],
                        Longitude = (string)countryData["longitude"],
                        Latitude = (string)countryData["latitude"]
                    });
                }

                return countriesList;
            }
            catch
            {
                throw;
            }
        }

        public Countries GetCountry(int id)
        {
            try
            {
                return _countryDashboardContext.Countries.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteCountry(int id)
        {
            try
            {
                var country = _countryDashboardContext.Countries.FirstOrDefault(x => x.Id == id);

                if (country != null)
                {
                    _countryDashboardContext.Countries.Remove(country);
                    _countryDashboardContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        private HttpRequestMessage GetMessage(string apiUrl)
        {
            return new HttpRequestMessage(HttpMethod.Get, apiUrl);
        }
    }
}
