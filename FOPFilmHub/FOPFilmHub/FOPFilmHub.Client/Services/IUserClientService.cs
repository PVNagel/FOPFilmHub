using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services
{
    public interface IUserClientService
    {
        Task<UserInfo> GetUserAsync(string userId);
        Task<UserInfo> GetUserByUsernameAsync(string userName);
        Task<List<UserInfo>> GetAllUsersAsync();
    }
}