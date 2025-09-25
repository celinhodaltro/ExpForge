using MediatR;


namespace ExperienceWidget.Application.Commands
{
    public class RenameWidgetCommand : IRequest<bool>
    {
        public RenameWidgetCommand(string widgetPath, string newWidgetName)
        {
            WidgetPath = widgetPath;
            NewWidgetName = newWidgetName;
        }

        public string WidgetPath { get; set; }
        public string NewWidgetName { get; set; }
    }
}
