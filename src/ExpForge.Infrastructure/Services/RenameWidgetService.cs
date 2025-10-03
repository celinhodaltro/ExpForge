using ExpForge.Application.Interfaces.Services;
using ExpForge.Infrastructure.Services;
using System.Text.Json;

namespace ExpForgeCli.Services
{
    public class RenameWidgetService : IRenameWidgetService
    {
        private readonly ITerminalMessageService _terminalMessageService;

        public RenameWidgetService(ITerminalMessageService terminalMessageService)
        {
            _terminalMessageService = terminalMessageService;
        }

        public bool Rename(string currentWidgetPath, string newWidgetName)
        {
            currentWidgetPath = Path.Combine(Directory.GetCurrentDirectory(), currentWidgetPath);
            var manifestPath = Path.Combine(currentWidgetPath, "manifest.json");

            if (File.Exists(manifestPath))
            {
                try
                {
                    var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                        File.ReadAllText(manifestPath)
                    )!;
                    jsonDict["name"] = newWidgetName;
                    File.WriteAllText(
                        manifestPath,
                        JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions { WriteIndented = true })
                    );
                }
                catch (Exception ex)
                {
                    _terminalMessageService.WriteLine(
                        $"Error updating manifest.json: {ex.Message}",
                        MessageStatus.Error
                    );
                    return false;
                }
            }
            else
            {
                _terminalMessageService.WriteLine(
                    $"Warning: 'manifest.json' not found in '{currentWidgetPath}'.",
                    MessageStatus.Warning
                );
            }

            try
            {
                var parentDir = Path.GetDirectoryName(currentWidgetPath)!;
                var newPath = Path.Combine(parentDir, newWidgetName);

                if (Directory.Exists(newPath))
                {
                    _terminalMessageService.WriteLine(
                        $"Error: A folder named '{newWidgetName}' already exists.",
                        MessageStatus.Error
                    );
                    return false;
                }
                Directory.Move(currentWidgetPath, newPath);
                return true;
            }
            catch (Exception ex)
            {
                _terminalMessageService.WriteLine(
                    $"Error renaming widget folder: {ex.Message}",
                    MessageStatus.Error
                );
                return false;
            }
        }
    }
}
