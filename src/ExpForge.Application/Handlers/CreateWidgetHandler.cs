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

        public CreateWidgetHandler(ICreateWidgetService service)
        {
            _service = service;
        }

        public readonly ICreateWidgetService _service;

        public Task<bool> Handle(CreateWidgetCommand request, CancellationToken cancellationToken)
        {
            string templatesPath;

            if (_service.Generate(request.WidgetName, request.TemplatePath, request.TemplateName))
            {
                TerminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
