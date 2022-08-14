using System.Collections.Generic;
using System.Threading.Tasks;
using CountryDashboard.Models;
using CountryDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountryDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet("getCountries")]
        public async Task<List<CountryViewModel>> GetCountries()
        {
            return await _countriesService.GetAllCountries();
        }

        [HttpGet("getCountry/{id}")]
        public async Task<Countries> GetCountry(int id)
        {
            return _countriesService.GetCountry(id);
        }

        [HttpPost("addCountry")]
        public async Task<bool> AddCountry(Countries countryViewModel)
        {
            return _countriesService.AddCountry(countryViewModel);
        }

        [HttpDelete("deleteCountry/{id}")]
        public async Task<bool> DeleteCountry(int id)
        {
            return _countriesService.DeleteCountry(id);
        }
    }
}