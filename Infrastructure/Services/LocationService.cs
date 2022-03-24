using Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <inheritdoc/>
    public class LocationService : Application.Common.Services.ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public LocationService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration.GetConnectionString("MapQuestApiKey");
        }

        /// <inheritdoc/>
        public async Task<Location> AddressToLocation(string address, string city, string province, string postalCode, string country)
        {
            // Postal codes don't play nice with the api so it is unused.
            var loc = string.Join(",", address, city, province, country);
            var url = $"http://open.mapquestapi.com/geocoding/v1/address?key={_apiKey}&location={loc}";
            var result = await _httpClient.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await result.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(json);
            var test = obj.results[0].locations[0].displayLatLng.lng;
            double longitude = Convert.ToDouble(obj.results[0].locations[0].displayLatLng.lng);
            double latitude = Convert.ToDouble(obj.results[0].locations[0].displayLatLng.lat);
            if(longitude == -100.445882 && latitude == 39.78373)
            {
                // These coordinates mean the location could not be resolved.
                return null;
            }
            return new Location(longitude, latitude);
        }
    }
}
