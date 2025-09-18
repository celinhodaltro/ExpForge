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
            _templatesPath = Path.Combine(AppContext.BaseDirectory, "../../src/templates");
            _tempRoot = Path.Combine(Path.GetTempPath(), "ExperienceWidgetCliTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempRoot);
        }

        [Fact]
        public void CreateWidget_ShouldGenerateExpectedStructure()
        {
            // Arrange
            var generator = new WidgetGenerator(_templatesPath);

            // Act
            generator.Generate(_widgetName, "empty");

            // Assert
            Assert.True(Directory.Exists(_widgetPath), "Widget folder was not created.");

            var expectedFiles = new[]
            {
                "manifest.json",
                "runtime/widget.tsx",
                "setting/setting.tsx"
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
