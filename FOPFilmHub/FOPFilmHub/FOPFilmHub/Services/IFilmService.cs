using FOPFilmHub.Library;

namespace FOPFilmHub.Services
{
    public interface IFilmService
    {
        public Task<Film> GetFilmByIdAsync(int id);
    }
}
