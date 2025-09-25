using ExpForge.Application.Commands.Widget;
using FluentValidation;

namespace ExpForge.Application.Validators.Widget
{
    public class NewWidgetValidator : AbstractValidator<NewWidgetCommand>
    {
        public NewWidgetValidator()
        {
            RuleFor(x => x.WidgetName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        }
    }
}
