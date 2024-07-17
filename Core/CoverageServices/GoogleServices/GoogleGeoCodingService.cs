using Data.ipNXContext;
using GoogleApi.Entities.Places.AutoComplete.Request;
using API.Data;
using API.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using GoogleApi.Entities.Common.Enums;
namespace Core.CoverageServices.GoogleServices
{
    public class GoogleGeoCodingService : IGoogleGeoCodingService
    {
        private readonly IpNxDbContext _coverageDbContext;
        private string _GoogleApiKey;

        public GoogleGeoCodingService(IpNxDbContext coverageDbContext)
        {
            _coverageDbContext = coverageDbContext;
        }

        public async Task<LatitudeAndLongitudeDto> GetCoordinatesAsync(string location)
        {
            var record = await _coverageDbContext.GcpGeoCodingApiKeys.FirstOrDefaultAsync();
            _GoogleApiKey = record.Key;
            using (var client = new HttpClient())
            {
                //var encodedLocation = Uri.EscapeDataString(location);
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={location}&key={_GoogleApiKey}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(responseBody);

                    if (data.results.Count == 0)
                    {
                        //if google is not giving us the lat and long, then we check our ipNxDb to see if that location name is present with the lga or city
                        location = location.ToLower();
                        var locationWords = location.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var LocationsFoundInipNXDb = await _coverageDbContext.CoverageLocations
                                                                .Where(coveragelocation =>
                                                                locationWords.Any(word =>
                                                                coveragelocation.Lga.ToLower().Contains(word.ToLower()) ||
                                                                coveragelocation.CoverageName.ToLower().Contains(word.ToLower()))).ToListAsync();
                        if (LocationsFoundInipNXDb == null)
                        {
                            return new LatitudeAndLongitudeDto();
                        }
                        return new LatitudeAndLongitudeDto() { CloseCoverageLocations = LocationsFoundInipNXDb };

                    }
                    var latitude = (double)data.results[0].geometry.location.lat;
                    var longitude = (double)data.results[0].geometry.location.lng;

                    var networkCoverageDto = new LatitudeAndLongitudeDto
                    {
                        UserLatitude = latitude,
                        UserLongitude = longitude
                    };

                    return networkCoverageDto;
                }
                else
                {
                    throw new Exception("Failed to retrieve coordinates from Google Geocoding API.");

                }
            }
        }



        public async Task<IEnumerable<string>> GetSuggestionsFromGooglePlacesAsync(string userInput)
        {
            var record = await _coverageDbContext.GcpGeoCodingApiKeys.FirstOrDefaultAsync();
            var request = new PlacesAutoCompleteRequest
            {
                Input = userInput,
                Key = record.Key,
                Components = new List<KeyValuePair<Component, string>>
                {
                    new KeyValuePair<Component, string>(Component.Country, "ng") //We are restricting the search to Nigeria only
                }
            };

            var response = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(request);

            if (response.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
            {
                // Map predictions to strings and return
                var suggestions = response.Predictions.Select(prediction => prediction.Description);
                return suggestions;
            }
            else
            {
                // Handle failure
                return new List<string> { "Failed to retrieve autocomplete suggestions" };
            }
        }



    }


}
