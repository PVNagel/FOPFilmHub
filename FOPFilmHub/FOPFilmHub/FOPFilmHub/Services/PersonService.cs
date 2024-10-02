using FOPFilmHub.Data;
using FOPFilmHub.Library;
using Newtonsoft.Json;

namespace FOPFilmHub.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PersonService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:TMDB:ApiKey"];

            // Set the Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var requestUrl = $"https://api.themoviedb.org/3/person/{id}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve film with id {id}. Status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseContent);

            return person;
        }

    }
}
