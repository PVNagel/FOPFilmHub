namespace FOPFilmHub.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetPopularFilmsAsync();
        Task<Film?> GetFilmByIdAsync(int id);
        Task<IEnumerable<Film>> SearchFilmsAsync(string query);
    }
}
