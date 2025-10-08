using System.IO;

namespace ExpForge.Domain.Extensions
{
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

                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }

                    File.Move(file, destFile);
                }

                Directory.Delete(subDir, true);
            }
        }
    }
}
