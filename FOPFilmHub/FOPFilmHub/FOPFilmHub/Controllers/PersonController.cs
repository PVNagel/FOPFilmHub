using FOPFilmHub.Data;
using FOPFilmHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace FOPFilmHub.Controllers
{
    /// <summary>
    /// Handles incoming HTTP requests related to movie data.
    /// Acts as an API controller that the client-side MovieClientService will interact with 
    /// to fetch data from the server-side application.    
    /// /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ApplicationDbContext _dbContext;


        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAsync(int id)
        {
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                if (person != null)
                {
                    return Ok(person);
                }

                return NotFound($"Person with id {id} were not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving person: {ex.Message}");
            }
        }
    }

    
}
