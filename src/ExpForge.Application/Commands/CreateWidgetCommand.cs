using MediatR;


namespace ExpForge.Application.Commands
{
    public class CreateWidgetCommand : IRequest<bool>
    {
        public CreateWidgetCommand(string widgetName, string templateName, string? destinationPath = null, string templatePath = null)
        {
            WidgetName = widgetName;
            TemplateName = templateName;
            DestinationPath = destinationPath;
            TemplatePath = templatePath;
        }

        public string WidgetName { get; set; }
        public string TemplateName { get; set; }
        public string? DestinationPath { get; set; }
        public string TemplatePath { get; set; }
    }
}
