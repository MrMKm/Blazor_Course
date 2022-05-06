using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Shared.DataObjectTransfer
{
    public class ActorDto
    { 
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Photo { get; set; }
        public string Bio { get; set; }
    }

    public class ActorDtoValidator : AbstractValidator<ActorDto>
    {
        public ActorDtoValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Actor");

            RuleFor(a => a.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Actor");

            RuleFor(a => a.BirthDate)
                .NotEmpty()
                .WithName("Actor");

            RuleFor(a => a.Bio)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Actor");
        }
    }
}
