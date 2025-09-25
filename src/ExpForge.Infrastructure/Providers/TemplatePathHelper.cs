using ExpForge.Application.Services.IServices;

namespace ExpForge.Infrastructure.Providers
{
    public class TemplatePathProvider : ITemplatePathProvider
    {
        public string GetTemplatesPath()
        {
            var publishPath = Path.Combine(AppContext.BaseDirectory, "..", "templates");

            var solutionRoot = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..")
            );
            var devPath = Path.Combine(solutionRoot, "npm-package", "templates");

            if (Directory.Exists(publishPath))
                return publishPath;

            if (Directory.Exists(devPath))
                return devPath;

            throw new DirectoryNotFoundException("Templates folder not found!");
        }
    }
}
