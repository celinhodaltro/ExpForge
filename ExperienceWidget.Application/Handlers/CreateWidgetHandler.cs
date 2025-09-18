using ExperienceWidget.Application.Commands;
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

            return Task.FromResult(true);
        }
    }
}
