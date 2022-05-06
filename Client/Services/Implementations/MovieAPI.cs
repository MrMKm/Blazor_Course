using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestProject.Client.Services.Implementations;
using TestProject.Client.Services.Interfaces;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Client.Services.Implementations
{
    public class MovieAPI : IMovieAPI
    {
        private readonly string _endpoint = "api/movie";
        private readonly HttpClient _httpClient;

        public MovieAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Movie> GetMovies()
        {
            //return new List<Movie>
            //{
            //    new Movie{Title = "Spider-Man 4", Poster = " ", ReleaseDate = new DateTime(2024, 06, 30)},
            //    new Movie{Title = "Spider-Man 5", Poster = " ", ReleaseDate = new DateTime(2025, 06, 30)},
            //    new Movie{Title = "Spider-Man 6", Poster = " ", ReleaseDate = new DateTime(2026, 06, 30)}
            //};

            return null;
        }

        public async Task<string> AddMovie(MovieDto MovieDto)
        {
            var result = await _httpClient.PostAsJsonAsync(_endpoint, MovieDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }

        public async Task<MovieDto> GetByID(int ID)
        {
            return await _httpClient.GetFromJsonAsync<MovieDto>($"{_endpoint}/{ID}");
        }

        public async Task<List<MovieDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<MovieDto>>($"{_endpoint}/all");
        }

        public async Task<string> EditMovie(MovieDto MovieDto)
        {
            var result = await _httpClient.PutAsJsonAsync(_endpoint, MovieDto);

            if (!result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            return null;
        }
    }
}
