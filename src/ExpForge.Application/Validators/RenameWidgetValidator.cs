using ExpForge.Application.Commands;
using FluentValidation;

namespace ExpForge.Application.Validators
{
    public class RenameWidgetValidator : AbstractValidator<RenameWidgetCommand>
    {
        public RenameWidgetValidator()
        {


            RuleFor(x => x.NewWidgetName)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");


            RuleFor(x => x.WidgetPath)
                .NotEmpty().WithMessage("WidgetPath is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.WidgetPath)
                .Must(path =>
                {
                    var currentPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                    System.IO.Directory.Exists(currentPath);
                    return true;
                })
                .WithMessage("The specified widget path does not exist.");
        }
    }
}
