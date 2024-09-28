using System.Net.Http.Json;
using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services
{
    /// <summary>
    /// Service for getting internal movie data from the Server.
    /// </summary>
    public class FilmClientService : IFilmClientService
    {
        private readonly HttpClient _httpClient;

        public FilmClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets a film by its ID from the server.
        /// </summary>
        /// <param name="id">The ID of the film.</param>
        /// <returns>The film object or null if not found.</returns>
        public async Task<Film> GetFilmByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/film/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Film>();
            }

            // Handle the case when the film is not found or other errors
            return null;
        }
    }
}
