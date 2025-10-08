using System.IO;

namespace ExpForge.Domain.Extensions;
public static class FileExtension
{
    public static void FlattenFolder(string rootPath)
    {
        if (!Directory.Exists(rootPath))
            return;

        foreach (var subDir in Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories))
        {
            foreach (var file in Directory.GetFiles(subDir))
            {
                var fileName = Path.GetFileName(file);
                var destFile = Path.Combine(rootPath, fileName);

                int counter = 1;
                while (File.Exists(destFile))
                {
                    var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    var ext = Path.GetExtension(fileName);
                    destFile = Path.Combine(rootPath, $"{nameWithoutExt}_{counter}{ext}");
                    counter++;
                }

                File.Move(file, destFile);
            }

            Directory.Delete(subDir, true); 
        }
    }
}
