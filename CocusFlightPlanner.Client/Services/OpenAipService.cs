using AutoMapper;
using CocusFlightPlanner.Common.DTO;
using System.Text.Json;

namespace CocusFlightPlanner.Application.Services
{
    public class OpenAipService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;
        private readonly IMapper _mapper;

        public OpenAipService(HttpClient httpClient, IConfiguration config, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _apiToken = config["OpenAip:ApiToken"];
        }

        public async Task<AirportDto> GetAirportsAsync(string iataCode, string icaoCode)
        {
            try
            {
                var code = iataCode ?? icaoCode;
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.core.openaip.net/api/airports?search={code}");
                request.Headers.Add("x-openaip-api-key", _apiToken);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"OpenAIP API error: {response.StatusCode}");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                var airports = JsonSerializer.Deserialize<AipAirportListDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var airport = airports?.items?.Where(x => x.IcaoCode == code || x.IataCode == code).FirstOrDefault() ?? throw new Exception();

                return _mapper.Map<AirportDto>(airport);
            }
            catch (Exception e)
            {

               throw;
            }

        }
    }

}