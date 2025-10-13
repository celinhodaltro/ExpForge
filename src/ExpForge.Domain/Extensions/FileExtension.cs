using System;
using System.IO;

namespace ExpForge.Domain.Extensions
{
    public static class FileExtension
    {
        public static void OrganizeByLastNameSegment(string rootPath)
        {
            if (!Directory.Exists(rootPath))
                return;

            foreach (var file in Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(file);

                var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var segments = nameWithoutExtension.Split('-', StringSplitOptions.RemoveEmptyEntries);
                var lastSegment = segments.Length > 0 ? segments[^1] : nameWithoutExtension;

                var destFolder = Path.Combine(rootPath, lastSegment);
                Directory.CreateDirectory(destFolder);

                var destFile = Path.Combine(destFolder, fileName);

                if (File.Exists(destFile))
                    File.Delete(destFile);
                

                File.Move(file, destFile);
            }

            foreach (var dir in Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories))
            {
                if (Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
                {
                    Directory.Delete(dir, true);
                }
            }
        }
    }
}
