using System.Text.RegularExpressions;
using MEC;
using Ninject;

namespace Democratie;

[Automatic]
[SynapseCommand(
    CommandName = "NewVote",
    Aliases = new string[0] { },
    Description = "Do a new pool for a vote",
    Platforms = new CommandPlatform [] { CommandPlatform.RemoteAdmin },
    Permission = "Democratie"
    )]
public class NewVote : SynapseCommand
{
    [Inject]
    public Democratie Plugin { get; set; }

    [Inject]
    public PlayerService PlayerService { get; set; }

    public override void Execute(SynapseContext context, ref CommandResult result)
    {
        if (context.Arguments.Length == 0)
        {
            result.StatusCode = CommandStatusCode.BadSyntax;
            result.Response = "You need to put after the command the subject of the vote";
            return;
        }

        var subject = string.Join(" ", context.Arguments);
        Plugin.CurentVoteSubject = subject;
        Plugin.IsActivePool = true;

        foreach (var player in PlayerService.Players)
        {
            var message = player.GetTranslation(Plugin.Translation).NewVoteMessage;
            message = Regex.Replace(message, "%Subject%", subject, RegexOptions.IgnoreCase);
            player.SendBroadcast(message, Plugin.Config.SecondToVote, true);
        }

        Plugin.VotedPlayer.Clear();
        result.Response = "New vote start";
        Timing.RunCoroutine(TimeOut());
    }

    public IEnumerator<float> TimeOut()
    {
        yield return Timing.WaitForSeconds(Plugin.Config.SecondToVote);

        Plugin.IsActivePool = false;

        foreach (var player in PlayerService.Players)
        {
            var message = player.GetTranslation(Plugin.Translation).VoteResult;
            message = Regex.Replace(message, "%Subject%", Plugin.CurentVoteSubject, RegexOptions.IgnoreCase);
            message = Regex.Replace(message, "%No%", Plugin.VoteNo.ToString(), RegexOptions.IgnoreCase);
            message = Regex.Replace(message, "%Yes%", Plugin.VoteYes.ToString(), RegexOptions.IgnoreCase);
            player.SendBroadcast(message, Plugin.Config.SecondToVote, true);
        }
    }
}
