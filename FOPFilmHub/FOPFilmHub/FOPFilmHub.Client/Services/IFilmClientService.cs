using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services
{
    public interface IFilmClientService
    {
        Task<Film> GetFilmByIdAsync(int id);
        Task<Credits> GetFilmCreditsAsync(int id);
        Task<List<Film>> GetPopularFilmsAsync();

    }
}
