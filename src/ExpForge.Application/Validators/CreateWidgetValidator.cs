using ExpForge.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpForge.Application.Validators
{
    public class CreateWidgetValidator : AbstractValidator<CreateWidgetCommand>
    {
        public CreateWidgetValidator()
        {
            RuleFor(x => x.WidgetName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
