using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services
{
    public interface IFilmClientService
    {
        Task<Film> GetFilmByIdAsync(int id);
    }
}
