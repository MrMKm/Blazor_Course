using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Shared.DataObjectTransfer;

namespace TestProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public GenderController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddGender(GenderDto genderDto)
        {
            try
            {
                var gender = new Gender(genderDto.Name);

                await _repositoryContext.Gender.AddAsync(gender);
                await _repositoryContext.SaveChangesAsync();

                return Ok(gender);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<GenderDto>> GetAllGenders()
        {
            var genders = await _repositoryContext.Gender.Select(g => new GenderDto
            {
                ID = g.ID,
                Name = g.Name
            }).ToListAsync();

            return genders;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<GenderDto> GetGenderByID(int id)
        {
            return await _repositoryContext.Gender
                .Select(g => new GenderDto
                {
                    ID = g.ID,
                    Name = g.Name
                }).FirstOrDefaultAsync(g => g.ID.Equals(id));
        }

        [HttpPut]
        public async Task<IActionResult> EditGender(GenderDto genderDto)
        {
            try
            {
                var DBGender = await _repositoryContext.Gender.FirstOrDefaultAsync(g => g.ID.Equals(genderDto.ID));

                DBGender.Name = genderDto.Name;
                await _repositoryContext.SaveChangesAsync();

                return Ok(DBGender);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
