using Microsoft.AspNetCore.Http;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Shared.DataObjectTransfer;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public ActorController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(ActorDto ActorDto)
        {
            try
            {
                var Actor = new Actor
                    (ActorDto.FirstName, ActorDto.LastName, (DateTime)ActorDto.BirthDate, ActorDto.Photo, ActorDto.Bio);

                await _repositoryContext.Actor.AddAsync(Actor);
                await _repositoryContext.SaveChangesAsync();

                return Ok(Actor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ActorDto>> GetAllActors()
        {
            var Actors = await _repositoryContext.Actor.Select(g => new ActorDto
            {
                ID = g.ID,
                FirstName = g.FirstName,
                LastName = g.LastName,
                BirthDate = g.BirthDate,
                Bio = g.Bio,
                Photo = g.Photo
            }).ToListAsync();

            return Actors;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActorDto> GetActorByID(int id)
        {
            return await _repositoryContext.Actor
                .Select(g => new ActorDto
                {
                    ID = g.ID,
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    BirthDate = g.BirthDate,
                    Bio = g.Bio,
                    Photo = g.Photo
                }).FirstOrDefaultAsync(g => g.ID.Equals(id));
        }

        [HttpPut]
        public async Task<IActionResult> EditActor(ActorDto ActorDto)
        {
            try
            {
                var DBActor = await _repositoryContext.Actor.FirstOrDefaultAsync(g => g.ID.Equals(ActorDto.ID));

                DBActor.FirstName = ActorDto.FirstName;
                DBActor.LastName = ActorDto.LastName;
                DBActor.BirthDate = (DateTime)ActorDto.BirthDate;
                DBActor.Bio = ActorDto.Bio;
                DBActor.Photo = ActorDto.Photo;

                await _repositoryContext.SaveChangesAsync();

                return Ok(DBActor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
