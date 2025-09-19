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
            var templatesPath = Path.Combine(AppContext.BaseDirectory, "templates");
            var generator = new WidgetGeneratorService(templatesPath);
            generator.Generate(request.WidgetName, request.TemplateName);

            TerminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
            return Task.FromResult(true);
        }
    }
}
