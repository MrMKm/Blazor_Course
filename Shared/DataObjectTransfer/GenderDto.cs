using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Shared.DataObjectTransfer
{
    public class GenderDto
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

    public class GenderDtoValidator : AbstractValidator<GenderDto>
    {
        public GenderDtoValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .MaximumLength(20)
                .WithName("Gender");
        }
    }
}
