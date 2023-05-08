using System.ComponentModel;
using Syml;

namespace Democratie;

[Automatic]
[Serializable]
[DocumentSection("Democratie")]
public class DemocratieConfig : IDocumentSection
{
    [Description("Time window in seconde to vote")]
    public ushort SecondToVote { get; set; } = 10;



}