using System.Collections.Generic;
using System.Threading.Tasks;
using CountryDashboard.Models;

namespace CountryDashboard.Services
{
    public interface ICountriesService
    {
        public bool AddCountry(Countries country);
        public Task<List<CountryViewModel>> GetAllCountries();
        public Countries GetCountry(int id);
        public bool DeleteCountry(int id);
    }
}