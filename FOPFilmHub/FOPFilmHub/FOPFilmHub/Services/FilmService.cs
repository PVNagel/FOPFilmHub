﻿using FOPFilmHub.Data;
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
        var requestUrl = $"https://api.themoviedb.org/3/movie/{id}";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve film with id {id}. Status code: {response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var filmResponse = JsonConvert.DeserializeObject<FilmResponse.Root>(responseContent);

        Film film = new Film
        {
            TmdbFilmId = filmResponse.Id,
            Adult = filmResponse.Adult,
            BackdropPath = filmResponse.BackdropPath,
            Budget = filmResponse.Budget,
            Homepage = filmResponse.Homepage,
            ImdbId = filmResponse.ImdbId,
            OriginCountry = string.Join(", ", filmResponse.OriginCountry),
            OriginalLanguage = filmResponse.OriginalLanguage,
            OriginalTitle = filmResponse.OriginalTitle,
            Overview = filmResponse.Overview,
            Popularity = filmResponse.Popularity,
            PosterPath = filmResponse.PosterPath,
            ReleaseDate = filmResponse.ReleaseDate,
            Revenue = filmResponse.Revenue,
            Runtime = filmResponse.Runtime,
            Status = filmResponse.Status,
            Tagline = filmResponse.Tagline,
            Title = filmResponse.Title,
            Video = filmResponse.Video,
            VoteAverage = filmResponse.VoteAverage,
            VoteCount = filmResponse.VoteCount,

            Genres = filmResponse.Genres?.Select(g => new Genre
            {
                TmdbGenreId = g.Id,
                Name = g.Name
            }).ToList(),

            ProductionCompanies = filmResponse.ProductionCompanies?.Select(pc => new ProductionCompany
            {
                TmdbProductionCompanyId = pc.Id,
                Name = pc.Name,
                LogoPath = pc.LogoPath,
                OriginCountry = pc.OriginCountry
            }).ToList(),

            ProductionCountries = filmResponse.ProductionCountries?.Select(pc => new ProductionCountry
            {
                Iso31661 = pc.Iso31661,
                Name = pc.Name
            }).ToList()
        };

        return film;
    }

    public async Task<Credits> GetFilmCredits(int id)
    {
        var requestUrl = $"https://api.themoviedb.org/3/movie/{id}/credits";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to retrieve credits of film with id {id}. Status code: {response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var creditResponse = JsonConvert.DeserializeObject<Credits>(responseContent);

        return creditResponse;
    }

    public async Task<List<Film>> GetPopularFilms()
    {
        var requestUrl = $"https://api.themoviedb.org/3/movie/popular";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var popularFilmsResponse = JsonConvert.DeserializeObject<PopularFilmsResponse>(responseContent);
        List<Film> popularFilms = new List<Film>();

        foreach(var film in popularFilmsResponse.Films )
        {
            Film popularFilm = new Film
            {
                TmdbFilmId = film.Id,
                Adult = film.Adult,
                BackdropPath = film.BackdropPath,
                PosterPath = film.PosterPath,
                Popularity = film.Popularity,
                OriginalLanguage = film.OriginalLanguage,
                OriginalTitle = film.OriginalTitle,
                Overview = film.Overview,
                ReleaseDate = film.ReleaseDate,
                Title = film.Title,
                Video = film.Video,
                VoteAverage = film.VoteAverage,
                VoteCount = film.VoteCount
            };

            popularFilms.Add(popularFilm);
        }

        return popularFilms;
    }
}
