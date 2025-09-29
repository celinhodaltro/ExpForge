using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

public static class TemplatePathExtensions
{
    private static string _cachedPath;

    public static string GetTemplatePath(this CommandLineApplication app, Func<string> resolver)
    {
        if (!string.IsNullOrEmpty(_cachedPath))
            return _cachedPath;

        var path = resolver();
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
        {
            app.Error.WriteLine("❌ 'templates' folder not found.");
            return null;
        }

        _cachedPath = path;
        return _cachedPath;
    }
}
