using ExperienceWidget.Application.Services;
using McMaster.Extensions.CommandLineUtils;
using System;

namespace ExperienceWidget.CLI.Actions;

[Command(Name = "experience-widget", Description = "CLI Experience Widget Builder")]
[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
[Subcommand(typeof(CreateAction))]
public class MainAction
{
    public static string GetVersion => VersionService.GetVersion();

    private int OnExecute()
    {
        Console.WriteLine("Use --help to see the available commands");
        return 0;
    }
}
