
using ExpForge.Domain.Enums;
using ExpForge.Infrastructure.Providers;
using ExpForge.Infrastructure.Services;

namespace ExpForge.Tests
{
    public class CreateWidgetServiceTests : IDisposable
    {
        private readonly string _templatesPath;
        private readonly string _tempRoot;
        private readonly string _widgetName = "TestWidget";
        private string WidgetPath => Path.Combine(_tempRoot, _widgetName);

        public CreateWidgetServiceTests()
        {
            // Use TemplatePathProvider from Infrastructure
            TemplatePathProvider templatePathProvider = new();
            _templatesPath = templatePathProvider.GetTemplatesPath();

            // Temp folder for widget generation
            _tempRoot = Path.Combine(Path.GetTempPath(), "ExpForgeCliTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempRoot);
        }

        [Fact]
        public void Generate_WhenCalledWithEmptyTemplate_CreatesExpectedWidgetStructure()
        {
            // Arrange
            TerminalMessageService terminalMessageService = new ();
            TemplateGeneratorService generator = new(terminalMessageService);

            // Act
            generator.Generate(_widgetName, templatePath: _templatesPath, templateName: "empty", outputRoot: _tempRoot, type: TemplateType.Widget);

            // Assert
            Assert.True(Directory.Exists(WidgetPath), $"Widget folder was not created: {WidgetPath}");

            string[] expectedFiles =
            [
                "manifest.json",
                "src/runtime/widget.tsx",
                "src/setting/setting.tsx"
            ];

            foreach (string? relativeFile in expectedFiles)
            {
                string fullPath = Path.Combine(WidgetPath, relativeFile.Replace('/', Path.DirectorySeparatorChar));
                Assert.True(File.Exists(fullPath), $"Expected file not found: {relativeFile}");
            }

            // Ensure no ".template" files leaked
            string[] leakedTemplates = Directory.GetFiles(WidgetPath, "*.template", SearchOption.AllDirectories);
            Assert.Empty(leakedTemplates);
        }

        void IDisposable.Dispose()
        {
            // Cleanup temp folder
            if (Directory.Exists(_tempRoot))
            {
                Directory.Delete(_tempRoot, true);
            }
        }
    }
}
