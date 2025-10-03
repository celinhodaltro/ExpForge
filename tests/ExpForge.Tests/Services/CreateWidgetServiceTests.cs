
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
        private string _widgetPath => Path.Combine(_tempRoot, _widgetName);

        public CreateWidgetServiceTests()
        {
            // Use TemplatePathProvider from Infrastructure
            TemplatePathProvider templatePathProvider = new TemplatePathProvider();
            _templatesPath = templatePathProvider.GetTemplatesPath();

            // Temp folder for widget generation
            _tempRoot = Path.Combine(Path.GetTempPath(), "ExpForgeCliTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempRoot);
        }

        [Fact]
        public void Generate_WhenCalledWithEmptyTemplate_CreatesExpectedWidgetStructure()
        {
            // Arrange
            var terminalMessageService = new TerminalMessageService();
            var generator = new TemplateGeneratorService(terminalMessageService);

            // Act
            generator.Generate(_widgetName, templatePath: _templatesPath, templateName: "empty", outputRoot: _tempRoot, type: TemplateType.Widget);

            // Assert
            Assert.True(Directory.Exists(_widgetPath), $"Widget folder was not created: {_widgetPath}");

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

            // Ensure no ".template" files leaked
            var leakedTemplates = Directory.GetFiles(_widgetPath, "*.template", SearchOption.AllDirectories);
            Assert.Empty(leakedTemplates);
        }

        public void Dispose()
        {
            // Cleanup temp folder
            if (Directory.Exists(_tempRoot))
                Directory.Delete(_tempRoot, true);
        }
    }
}
