using FOPFilmHub.Library;
using System.Net.Http;
using System.Net.Http.Json;

namespace FOPFilmHub.Client.Services
{
    public class PersonClientService : IPersonClientService
    {
        private readonly HttpClient _httpClient;

        public PersonClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/person/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Person>();
            }

            return null;
        }
    }
}
