using McMaster.Extensions.CommandLineUtils;
using System;

public static class CommandLineExtensions
{
    public static string GetValueOrPrompt(
        this CommandLineApplication app,
        string currentValue,
        string promptMessage,
        Func<string> validateOrResolve,
        string errorMessageIfEmpty = "❌ Value cannot be empty.")
    {
        if (!string.IsNullOrWhiteSpace(currentValue))
            return currentValue;

        var resolved = validateOrResolve?.Invoke();
        if (!string.IsNullOrWhiteSpace(resolved))
            return resolved;

        var input = Prompt.GetString(promptMessage);
        if (string.IsNullOrWhiteSpace(input))
        {
            app.Error.WriteLine(errorMessageIfEmpty);
            return null;
        }

        return input;
    }



}
