using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services.User
{
    public interface IUserClientService
    {
        Task<UserInfo> GetUserAsync(string userId);
        Task<UserInfo> GetUserByUsernameAsync(string userName);
        Task<List<UserInfo>> GetAllUsersAsync();
    }
}