using ExperienceWidget.Application.Interfaces;
using ExperienceWidget.CLI.Services;
using System.Text.Json;

namespace ExperienceWidgetCli.Services
{
    public class WidgetRenameService : IWidgetRenameService
    {
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
                    TerminalMessageService.WriteLine($"Error updating manifest.json: {ex.Message}", MessageStatus.Error);
                    return false;
                }
            }
            else
            {
                TerminalMessageService.WriteLine($"Warning: 'manifest.json' not found in '{currentWidgetPath}'.", MessageStatus.Warning);
            }

            try
            {
                var parentDir = Path.GetDirectoryName(currentWidgetPath)!;
                var newPath = Path.Combine(parentDir, newWidgetName);

                if (Directory.Exists(newPath))
                {
                    TerminalMessageService.WriteLine($"Error: A folder named '{newWidgetName}' already exists.", MessageStatus.Error);
                    return false;
                }
                Directory.Move(currentWidgetPath, newPath);
                return true;
            }
            catch (Exception ex)
            {
                TerminalMessageService.WriteLine($"Error renaming widget folder: {ex.Message}", MessageStatus.Error);
                return false;
            }
        }
    }
}
