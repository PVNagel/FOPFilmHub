using FOPFilmHub.Library;

namespace FOPFilmHub.Services
{
    public interface IFilmService
    {
        Task<Film> GetFilmByIdAsync(int id);
        Task<Credits> GetFilmCredits(int id);
        Task<List<Film>> GetPopularFilms();

    }
}
