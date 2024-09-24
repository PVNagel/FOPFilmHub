using FOPFilmHub.Library;
using System.Net.Http.Json;

namespace FOPFilmHub.Client.Services.User
{
    public class UserClientService : IUserClientService
    {
        private readonly HttpClient _httpClient;

        public UserClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserInfo> GetUserAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<UserInfo>($"api/user/id/{userId}");
        }
        public async Task<UserInfo> GetUserByUsernameAsync(string userName)
        {
            return await _httpClient.GetFromJsonAsync<UserInfo>($"api/user/username/{userName}");
        }
    }
}
