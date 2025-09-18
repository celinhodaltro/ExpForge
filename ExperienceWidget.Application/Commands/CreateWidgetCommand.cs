using MediatR;


namespace ExperienceWidget.Application.Commands
{
    public class CreateWidgetCommand : IRequest<bool>
    {
        public string WidgetName { get; set; }
        public string TemplateName { get; set; }
        public string DestinationPath { get; set; }
    }
}
