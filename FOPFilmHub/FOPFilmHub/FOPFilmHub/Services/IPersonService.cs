using FOPFilmHub.Library;

namespace FOPFilmHub.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonByIdAsync(int id);

    }
}
