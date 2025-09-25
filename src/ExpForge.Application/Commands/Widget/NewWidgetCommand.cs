using MediatR;


namespace ExpForge.Application.Commands.Widget
{
    public class NewWidgetCommand : IRequest<bool>
    {
        public NewWidgetCommand(string widgetName, string templateName, string? destinationPath = null, string templatePath = null)
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
