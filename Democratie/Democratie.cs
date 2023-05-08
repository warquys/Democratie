namespace Democratie;

[Plugin(
    Name = "Democratie",
    Description = "A plugin to open vote pool",
    Version = "1.0.0",
    Author = "VT",
    Repository = "https://github.com/warquys/Democratie"
)]
public class Democratie : ReloadablePlugin<DemocratieConfig, DemocratieTranalation>
{
    public HashSet<SynapsePlayer> VotedPlayer { get; } = new HashSet<SynapsePlayer>();
    public bool IsActivePool { get; set; }
    public int VoteYes { get; set; }
    public int VoteNo { get; set; }
    public string CurentVoteSubject { get; set; }

}