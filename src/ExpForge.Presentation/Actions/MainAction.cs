using ExpForge.CLI.Actions.Widget;
using ExpForge.CLI.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Reflection;

namespace ExpForge.CLI.Actions;

[Command(Name = "expforge", Description = "CLI Experience Widget Builder")]
[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
[Subcommand(typeof(NewAction), typeof(RenameAction))]
public class MainAction
{
    private int OnExecute()
    {
        TerminalMessageService.WriteLine("Use --help to see the available commands", MessageStatus.Warning);
        return 0;
    }

    public string GetVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "...";
        return $"exo-forge Version: ({version})";
    }
}
