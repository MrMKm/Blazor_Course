using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Client.Services.Interfaces
{
    public interface IMovieAPI
    {
        Task<string> AddMovie(MovieDto MovieDto);
        Task<string> EditMovie(MovieDto MovieDto);
        Task<List<MovieDto>> GetAll();
        Task<MovieDto> GetByID(int ID);
        List<Movie> GetMovies();
    }
}
