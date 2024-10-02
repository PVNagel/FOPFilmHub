using FOPFilmHub.Library;

namespace FOPFilmHub.Client.Services
{
    public interface IPersonClientService
    {
        Task<Person> GetPersonByIdAsync(int id);
    }
}
