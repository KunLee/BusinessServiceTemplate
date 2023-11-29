using BusinessServiceTemplate.Api.Services.Interfaces;

namespace BusinessServiceTemplate.Api.Services
{
    public class LocationService : ILocationService
    {
        private HttpClient _httpClient;
        public LocationService(IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient("LocationServiceHttpClient"); ;
        }

        public async Task<List<string>> GetAllLocations()
        {
            var response = await _httpClient.GetAsync("Locations/All");

            return null;
        }

        public async Task<List<string>> SearchLocations(string postcode)
        {
            var response = await _httpClient.GetAsync("Locations/Search");

            return null;
        }
    }
}
