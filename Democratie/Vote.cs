using Ninject;

namespace Democratie;

[Automatic]
[SynapseCommand(
    CommandName = "Vote",
    Aliases = new string[0] { },
    Description = "Vote (Yes/No)",
    Platforms = new CommandPlatform [] { CommandPlatform.PlayerConsole }
    )]
public class Vote : SynapseCommand
{
    [Inject]
    public Democratie Plugin { get; set; }
    
    public override void Execute(SynapseContext context, ref CommandResult result)
    {
        if (context.Arguments.Length != 1)
        {
            result.StatusCode = CommandStatusCode.BadSyntax;
            result.Response = context.Player.GetTranslation(Plugin.Translation).InvalideVote;
            return;
        }

        if (!Plugin.IsActivePool)
        {
            result.StatusCode = CommandStatusCode.Forbidden;
            result.Response = context.Player.GetTranslation(Plugin.Translation).NoActiveVote;
            return;
        }

        if (Plugin.VotedPlayer.Contains(context.Player))
        {
            result.StatusCode = CommandStatusCode.Forbidden;
            result.Response = context.Player.GetTranslation(Plugin.Translation).OnlyOneVote;
            return;
        }


        switch (context.Arguments[0].ToLower())
        {
            case "yes":
                Plugin.VoteYes++;
                result.StatusCode = CommandStatusCode.BadSyntax;
                result.Response = context.Player.GetTranslation(Plugin.Translation).VotedYes;
                Plugin.VotedPlayer.Add(context.Player);
                break;

            case "no":
                Plugin.VoteNo++;
                result.StatusCode = CommandStatusCode.BadSyntax;
                result.Response = context.Player.GetTranslation(Plugin.Translation).VotedNo;
                Plugin.VotedPlayer.Add(context.Player);
                break;

            default:
                result.StatusCode = CommandStatusCode.BadSyntax;
                result.Response = context.Player.GetTranslation(Plugin.Translation).InvalideVote;
                break;
        }
    }
}
