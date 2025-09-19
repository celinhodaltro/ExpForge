using ExperienceWidget.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperienceWidget.Application.Validators
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
