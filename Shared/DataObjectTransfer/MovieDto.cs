using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Shared.DataObjectTransfer
{
    public class MovieDto
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        public string Poster { get; set; }

        public List<ActorDto> Actors { get; set; } = new List<ActorDto>();

        public List<GenderDto> Genders { get; set; } = new List<GenderDto>();

        public List<int> actorsID { get; set; } = new List<int>();
        public List<int> gendersID { get; set; } = new List<int>();
    }

    public class MovieDtoValidator : AbstractValidator<MovieDto>
    {
        public MovieDtoValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Movie");

            RuleFor(a => a.ReleaseDate)
                .NotEmpty()
                .WithName("Movie");

        }
    }
}
