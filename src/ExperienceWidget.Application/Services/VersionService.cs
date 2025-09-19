using System.Reflection;

namespace ExperienceWidget.Application.Services
{
    public static class VersionService
    {
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "desconhecida";
        }
    }
}
