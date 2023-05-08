using System.Text.RegularExpressions;
using CommandSystem.Commands.RemoteAdmin;
using Neuron.Core.Events;
using Neuron.Core.Plugins;

namespace Democratie;

[Automatic]
public class EventHandler : Listener
{

    private readonly Democratie _plugin;

    public EventHandler(Democratie plguin)
    {
        _plugin = plguin;
    }

    [EventHandler]
    public void OnJoin(JoinEvent ev)
    {
        if (_plugin.IsActivePool)
        {

            var message = ev.Player.GetTranslation(_plugin.Translation).NewVoteMessage;
            message = Regex.Replace(message, "%Subject%", _plugin.CurentVoteSubject, RegexOptions.IgnoreCase);
            ev.Player.SendBroadcast(message, _plugin.Config.SecondToVote, true);
        }
    }
}
