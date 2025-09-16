using System;
using System.IO;
using Xunit;
using ExperienceWidgetCli.Services;

namespace ExperienceWidgetCli.Tests
{
    public class WidgetGeneratorTests
    {
        private readonly string _templatesPath = Path.Combine(AppContext.BaseDirectory, "../../src/templates");

        [Fact]
        public void CreateWidget_ShouldGenerateFiles_AndCleanup()
        {
            // Arrange
            var widgetName = "TestWidget";
            var generator = new WidgetGenerator(_templatesPath);
            var widgetPath = Path.Combine(Directory.GetCurrentDirectory(), widgetName);

            try
            {
                // Act
                generator.Generate(widgetName);

                // Assert
                Assert.True(Directory.Exists(widgetPath), "A pasta do widget não foi criada.");
                Assert.True(File.Exists(Path.Combine(widgetPath, "manifest.json")), "manifest.json não existe.");
                Assert.True(File.Exists(Path.Combine(widgetPath, "widget.tsx")), "widget.tsx não existe.");
                Assert.True(File.Exists(Path.Combine(widgetPath, "setting.tsx")), "setting.tsx não existe.");
            }
            finally
            {
                // Cleanup
                if (Directory.Exists(widgetPath))
                    Directory.Delete(widgetPath, true);
            }
        }
    }
}
