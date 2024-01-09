using BusinessServiceTemplate.Api.Services.Interfaces;
using BusinessServiceTemplate.Core.Cache;
using BusinessServiceTemplate.Shared.Common;
using System.Text;

namespace BusinessServiceTemplate.Api.Services
{
    public class LocationService : ILocationService
    {
        private HttpClient _httpClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _category = "Location";
        public LocationService(IHttpClientFactory httpClientFactory, ICacheManager cacheManager)
        {
            this._httpClient = httpClientFactory.CreateClient("LocationServiceHttpClient");
            this._cacheManager = cacheManager;
        }

        public async Task<List<string>> GetAllLocations()
        {
            var response = await _httpClient.GetAsync("Locations/All");

            return null;
        }

        public async Task<List<string>> SearchLocations(string postcode)
        {
            var paymentConfigResult = _cacheManager[_category, key: postcode];

            if (paymentConfigResult == null)
            {
                var response = await _httpClient.PostAsync($"eorders/{postcode}/ipsi-payment", new StringContent(postcode, Encoding.UTF8, "application/json"));

                if (response == null) throw new InvalidDataException(ConstantStrings.INVALID_REQUEST_DATA);

                _cacheManager[_category, postcode] = response;
            }

            return null;
        }
    }
}
