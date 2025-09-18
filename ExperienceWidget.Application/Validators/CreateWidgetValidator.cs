using ExperienceWidget.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperienceWidget.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateWidgetCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.TemplateName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.WidgetName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
