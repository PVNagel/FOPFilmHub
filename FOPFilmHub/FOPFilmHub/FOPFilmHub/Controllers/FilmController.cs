using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FOPFilmHub.Library;
using Newtonsoft.Json;
using FOPFilmHub.Services;
using Microsoft.EntityFrameworkCore;
using FOPFilmHub.Data;

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
        private readonly ApplicationDbContext _dbContext;


        public FilmController(IFilmService filmService, ApplicationDbContext dbContext)
        {
            _filmService = filmService;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            try
            {
                var filmInDb = await _dbContext.Films
                    .Include(f => f.Genres)
                    .Include(f => f.ProductionCompanies)
                    .Include(f => f.ProductionCountries)
                    .FirstOrDefaultAsync(f => f.TmdbFilmId == id);

                if (filmInDb != null)
                {
                    return Ok(filmInDb);
                }

                var filmFromApi = await _filmService.GetFilmByIdAsync(id);

                if (filmFromApi != null)
                {
                    // Handle genres
                    var genreIds = filmFromApi.Genres.Select(g => g.TmdbGenreId).ToList();
                    var existingGenres = await _dbContext.Genres
                        .Where(g => genreIds.Contains(g.TmdbGenreId))
                        .ToListAsync();

                    filmFromApi.Genres = filmFromApi.Genres.Select(g =>
                        existingGenres.FirstOrDefault(eg => eg.TmdbGenreId == g.TmdbGenreId) ?? g
                    ).ToList();

                    // Handle production companies
                    var companyIds = filmFromApi.ProductionCompanies.Select(pc => pc.TmdbProductionCompanyId).ToList();
                    var existingCompanies = await _dbContext.ProductionCompanies
                        .Where(pc => companyIds.Contains(pc.TmdbProductionCompanyId))
                        .ToListAsync();

                    filmFromApi.ProductionCompanies = filmFromApi.ProductionCompanies.Select(pc =>
                        existingCompanies.FirstOrDefault(ec => ec.TmdbProductionCompanyId == pc.TmdbProductionCompanyId) ?? pc
                    ).ToList();

                    // Handle production countries
                    var countryCodes = filmFromApi.ProductionCountries.Select(pc => pc.Iso31661).ToList();
                    var existingCountries = await _dbContext.ProductionCountries
                        .Where(pc => countryCodes.Contains(pc.Iso31661))
                        .ToListAsync();

                    filmFromApi.ProductionCountries = filmFromApi.ProductionCountries.Select(pc =>
                        existingCountries.FirstOrDefault(ec => ec.Iso31661 == pc.Iso31661) ?? pc
                    ).ToList();

                    // Add film to the database
                    _dbContext.Films.Add(filmFromApi);
                    await _dbContext.SaveChangesAsync();

                    return Ok(filmFromApi);
                }

                return NotFound($"Film with id {id} was not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving film: {ex.Message}");
            }
        }

        [HttpGet("{id}/credits")]
        public async Task<IActionResult> GetFilmCredits(int id)
        {
            try
            {
                var credits = await _filmService.GetFilmCredits(id);

                if (credits != null)
                {
                    return Ok(credits);
                }

                return NotFound($"Credits for film with id {id} were not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving film credits: {ex.Message}");
            }
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularFilms()
        {
            try
            {
                var popularFilms = await _filmService.GetPopularFilms();

                if (popularFilms != null)
                {
                    return Ok(popularFilms);
                }

                return NotFound($"Popular films were not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving popular films: {ex.Message}");
            }
        }
    }
}
