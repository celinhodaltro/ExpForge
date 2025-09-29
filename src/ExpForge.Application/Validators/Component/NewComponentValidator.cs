using ExpForge.Application.Commands;
using ExpForge.Application.Commands.Component;
using FluentValidation;

namespace ExpForge.Application.Validators.Component
{
    public class NewComponentValidator : AbstractValidator<NewComponentCommand>
    {
        public NewComponentValidator()
        {
            RuleFor(x => x.ComponentName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
