using ExperienceWidget.Application.Services;
using ExperienceWidget.CLI.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Reflection;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "experience-widget", Description = "CLI Experience Widget Builder")]
[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
[Subcommand(typeof(CreateAction), typeof(RenameAction))]
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
        return $"Experience-Widget Version: ({version})";
    }
}
