using Neuron.Modules.Configs.Localization;

namespace Democratie;


[Automatic]
[Serializable]
public class DemocratieTranalation : Translations<DemocratieTranalation>
{
    
    public string NewVoteMessage { get; set; } = "<b>New Vote :</b>\\n<color=red>%Subject%";
    
    public string NoActiveVote { get; set; } = "You can only vote in vote time";

    public string VotedYes { get; set; } = "You voted \"Yes\"";
    
    public string VotedNo { get; set; } = "You voted \"No\"";

    public string OnlyOneVote { get; set; } = "You can only vote one time";

    public string InvalideVote { get; set; } = "Invalide vote you can do \"vote Yes\" or \"vote No\"";

    public string VoteResult { get; set; } = "No: %No% // Yes: %Yes%\\n%Subject%";

}