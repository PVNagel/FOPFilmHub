using FOPFilmHub.Data;
using FOPFilmHub.Library;
using FOPFilmHub.Services;
using Newtonsoft.Json;

public class FilmService : IFilmService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ApplicationDbContext _dbContext;

    public FilmService(HttpClient httpClient, IConfiguration configuration, ApplicationDbContext dbContext)
    {
        _httpClient = httpClient;
        _apiKey = configuration["ApiSettings:TMDB:ApiKey"];
        _dbContext = dbContext;

        // Set the Authorization header
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<Film> GetFilmByIdAsync(int id)
    {
        var apiUrl = $"https://api.themoviedb.org/3/movie/{id}";

        // Send the request to the TMDb API
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
            return null;

        // Get the response content as a string
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON response into the FilmResponse.Root class
        var filmResponse = JsonConvert.DeserializeObject<FilmResponse.Root>(jsonResponse);

        if (filmResponse == null)
            return null;

        // Map FilmResponse.Root to Film
        var film = new Film
        {
            Id = filmResponse.Id,
            Title = filmResponse.Title,
            OriginalTitle = filmResponse.OriginalTitle,
            Overview = filmResponse.Overview,
            PosterPath = filmResponse.PosterPath,
            BackdropPath = filmResponse.BackdropPath,
            Popularity = filmResponse.Popularity,
            VoteAverage = filmResponse.VoteAverage,
            VoteCount = filmResponse.VoteCount,
            Adult = filmResponse.Adult,
            ReleaseDate = filmResponse.ReleaseDate,
            Budget = filmResponse.Budget,
            Revenue = filmResponse.Revenue,
            Runtime = filmResponse.Runtime,
            Status = filmResponse.Status,
            Tagline = filmResponse.Tagline,
            Homepage = filmResponse.Homepage,
            OriginalLanguage = filmResponse.OriginalLanguage,
            ImdbId = filmResponse.ImdbId,

            // Map Genres
            Genres = filmResponse.Genres?.Select(g => new Film.Genre
            {
                Id = g.Id,
                Name = g.Name
            }).ToList(),

            // Map Production Companies
            ProductionCompanies = filmResponse.ProductionCompanies?.Select(pc => new Film.ProductionCompany
            {
                Id = pc.Id,
                Name = pc.Name,
                LogoPath = pc.LogoPath,
                OriginCountry = pc.OriginCountry
            }).ToList(),

            // Map Production Countries
            ProductionCountries = filmResponse.ProductionCountries?.Select(pc => new Film.ProductionCountry
            {
                Iso31661 = pc.Iso31661,
                Name = pc.Name
            }).ToList(),

            // Map Spoken Languages
            SpokenLanguages = filmResponse.SpokenLanguages?.Select(sl => new Film.SpokenLanguage
            {
                EnglishName = sl.EnglishName,
                Iso6391 = sl.Iso6391,
                Name = sl.Name
            }).ToList(),
        };

        return film;
    }

}
