using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Model;
using TestProject.Shared.DataObjectTransfer;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public MovieController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDto MovieDto)
        {
            try
            {
                if (!MovieDto.actorsID.Any() || !MovieDto.gendersID.Any())
                    throw new ApplicationException("At least one actor and one gender must be selected");

                var Movie = new Movie(MovieDto.Title, (DateTime)MovieDto.ReleaseDate, MovieDto.Poster);

                Movie.Actors = (await _repositoryContext.Actor
                .Where(a => MovieDto.actorsID.Contains(a.ID)).ToListAsync());

                Movie.Genders = (await _repositoryContext.Gender
                .Where(a => MovieDto.gendersID.Contains(a.ID)).ToListAsync());

                await _repositoryContext.Movie.AddAsync(Movie);
                await _repositoryContext.SaveChangesAsync();

                return Ok(Movie);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<MovieDto>> GetAllMovies()
        {
            var Movies = await _repositoryContext.Movie.Select(g => new MovieDto
            {
                ID = g.ID,
                Title = g.Title,
                ReleaseDate = g.ReleaseDate,
                Poster = g.Poster,
                Actors = g.Actors.Select(a => new ActorDto
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    BirthDate = a.BirthDate,
                    Bio = a.Bio,
                    Photo = a.Photo
                }).ToList(),
                Genders = g.Genders.Select(a => new GenderDto
                {
                    ID = a.ID,
                    Name = a.Name
                }).ToList()
            }).ToListAsync();

            return Movies;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MovieDto> GetMovieByID(int id)
        {
            return await _repositoryContext.Movie
                .Select(g => new MovieDto
                {
                    ID = g.ID,
                    Title = g.Title,
                    ReleaseDate = g.ReleaseDate,
                    Poster = g.Poster,
                    Actors = g.Actors.Select(a => new ActorDto
                    {
                        ID = a.ID,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        BirthDate = a.BirthDate,
                        Bio = a.Bio,
                        Photo = a.Photo
                    }).ToList(),
                    actorsID = g.Actors.Select(a => a.ID).ToList(),
                    Genders = g.Genders.Select(a => new GenderDto
                    {
                        ID = a.ID,
                        Name = a.Name
                    }).ToList(),
                    gendersID = g.Genders.Select(g => g.ID).ToList()
                }).FirstOrDefaultAsync(g => g.ID.Equals(id));
        }

        [HttpPut]
        public async Task<IActionResult> EditMovie(MovieDto MovieDto)
        {
            try
            {
                if (!MovieDto.actorsID.Any() || !MovieDto.gendersID.Any())
                    throw new ApplicationException("At least one actor and one gender must be selected");

                MovieDto.actorsID = MovieDto.actorsID.Distinct().ToList();
                MovieDto.gendersID = MovieDto.gendersID.Distinct().ToList();

                var DBMovie = await _repositoryContext.Movie
                    .Include(a => a.Actors)
                    .Include(g => g.Genders)
                    .FirstOrDefaultAsync(g => g.ID.Equals(MovieDto.ID));

                DBMovie.Title = MovieDto.Title;
                DBMovie.ReleaseDate = (DateTime)MovieDto.ReleaseDate;
                DBMovie.Poster = MovieDto.Poster;

                DBMovie.Actors.Clear();
                DBMovie.Genders.Clear();
                await _repositoryContext.SaveChangesAsync();

                foreach (var actorID in MovieDto.actorsID)
                {
                    var actor = _repositoryContext.Actor.FirstOrDefault(a => a.ID.Equals(actorID));
                    if(!DBMovie.Actors.Contains(actor))
                        DBMovie.Actors.Add(actor);
                }

                foreach (var genderID in MovieDto.gendersID)
                {
                    var gender = _repositoryContext.Gender.FirstOrDefault(g => g.ID.Equals(genderID));
                    if (!DBMovie.Genders.Contains(gender))
                        DBMovie.Genders.Add(gender);
                }

                await _repositoryContext.SaveChangesAsync();

                return Ok(DBMovie);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
