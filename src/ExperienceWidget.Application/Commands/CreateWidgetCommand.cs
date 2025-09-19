using MediatR;


namespace ExperienceWidget.Application.Commands
{
    public class CreateWidgetCommand : IRequest<bool>
    {
        public CreateWidgetCommand(string widgetName, string templateName, string? destinationPath = null)
        {
            WidgetName = widgetName;
            TemplateName = templateName;
            DestinationPath = destinationPath;
        }

        public string WidgetName { get; set; }
        public string TemplateName { get; set; }
        public string? DestinationPath { get; set; }
    }
}
