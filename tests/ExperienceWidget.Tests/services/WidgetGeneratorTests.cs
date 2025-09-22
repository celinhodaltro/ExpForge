using System;
using System.IO;
using System.Linq;
using Xunit;
using ExperienceWidgetCli.Services;

namespace ExperienceWidgetCli.Tests
{
    public class WidgetGeneratorTests : IDisposable
    {
        private readonly string _templatesPath;
        private readonly string _tempRoot;
        private readonly string _widgetName = "TestWidget";
        private string _widgetPath => Path.Combine(_tempRoot, _widgetName);

        public WidgetGeneratorTests()
        {
            var solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
            _templatesPath = Path.Combine(solutionRoot, "npm-package", "templates");
            _tempRoot = Path.Combine(Path.GetTempPath(), "ExperienceWidgetCliTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempRoot);
        }

        [Fact]
        public void Generate_WhenCalledWithEmptyTemplate_CreatesExpectedWidgetStructure()
        {
            // Arrange
            var generator = new WidgetGeneratorService(_templatesPath);

            // Act
            generator.Generate(_widgetName, "empty", _tempRoot);

            // Assert
            Assert.True(Directory.Exists(_widgetPath), $"Widget folder was not created. {_widgetPath}");

            var expectedFiles = new[]
            {
                "manifest.json",
                "src/runtime/widget.tsx",
                "src/setting/setting.tsx"
            };

            foreach (var relativeFile in expectedFiles)
            {
                var fullPath = Path.Combine(_widgetPath, relativeFile.Replace('/', Path.DirectorySeparatorChar));
                Assert.True(File.Exists(fullPath), $"Expected file not found: {relativeFile}");
            }

            // Extra: ensure no ".template" file leaked
            var leakedTemplates = Directory.GetFiles(_widgetPath, "*.template", SearchOption.AllDirectories);
            Assert.Empty(leakedTemplates);
        }

        public void Dispose()
        {
            // Cleanup
            if (Directory.Exists(_tempRoot))
                Directory.Delete(_tempRoot, true);
        }
    }
}
