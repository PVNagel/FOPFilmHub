using FOPFilmHub.Library;

namespace FOPFilmHub.Services
{
    public interface IFilmService
    {
        Task<Film> GetFilmByIdAsync(int id);
        //Task SaveFilmAsync(Film film);
    }
}
