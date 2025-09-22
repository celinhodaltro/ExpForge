using ExperienceWidget.Application.Commands;
using ExperienceWidget.CLI.Services;
using ExperienceWidgetCli.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExperienceWidget.Application.Handlers
{
    public class CreateWidgetHandler : IRequestHandler<CreateWidgetCommand, bool>
    {
        public Task<bool> Handle(CreateWidgetCommand request, CancellationToken cancellationToken)
        {
            string templatesPath;

            var publishPath = Path.Combine(AppContext.BaseDirectory, "..", "templates");

            var solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
            var devPath = Path.Combine(solutionRoot, "npm-package", "templates");

            if (Directory.Exists(publishPath))
                templatesPath = publishPath;   
            else if (Directory.Exists(devPath))
                templatesPath = devPath;   
            else
                throw new DirectoryNotFoundException("Templates folder not found!");
            var generator = new WidgetGeneratorService(templatesPath);
            if (generator.Generate(request.WidgetName, request.TemplateName))
            {
                TerminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
