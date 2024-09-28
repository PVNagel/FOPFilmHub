using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FOPFilmHub.Library;
using Newtonsoft.Json;
using FOPFilmHub.Services;

namespace FOPFilmHub.Controllers
{
    /// <summary>
    /// Handles incoming HTTP requests related to movie data.
    /// Acts as an API controller that the client-side MovieClientService will interact with 
    /// to fetch data from the server-side application.    
    /// /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);

            if (film != null)
            {
                return Ok(film);
            }
            return NotFound();
        }
    }
}
