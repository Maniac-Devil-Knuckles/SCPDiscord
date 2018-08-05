﻿using SCPDiscord.Properties;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Text;

namespace SCPDiscord
{
    class RoundEventListener : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerConnect, IEventHandlerDisconnect, IEventHandlerWaitingForPlayers, IEventHandlerCheckRoundEnd, IEventHandlerRoundRestart, IEventHandlerSetServerName
    {
        private SCPDiscordPlugin plugin;
        bool roundHasStarted = false;

        public RoundEventListener(SCPDiscordPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            /// <summary>  
            ///  This is the event handler for Round start events (before people are spawned in)
            /// </summary> 
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onroundstart"), "round.onroundstart");
            roundHasStarted = true;
        }

        public void OnConnect(ConnectEvent ev)
        {
            /// <summary>  
            ///  This is the event handler for connection events, before players have been created, so names and what not are available. See PlayerJoin if you need that information
            /// </summary>
            string[][] variables = new string[1][]
            {
                new string[] { "ipaddress", ev.Connection.IpAddress.ToString().Substring(7) }
            };
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onconnect"), "round.onconnect", variables);
        }

        public void OnDisconnect(DisconnectEvent ev)
        {
            /// <summary>  
            ///  This is the event handler for disconnection events.
            /// </summary>
            ///             
            string[][] variables = new string[1][]
            {
                new string[] { "ipaddress", ev.Connection.IpAddress.ToString().Substring(7) }
            };
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_ondisconnect"), "round.ondisconnect", variables);
        }

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            /// <summary>  
            ///  This event handler will call everytime the game checks for a round end
            /// </summary> 

            //Protip, don't turn this on.
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_oncheckroundend"), "round.oncheckroundend");
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            /// <summary>  
            ///  This is the event handler for Round end events (when the stats appear on screen)
            /// </summary>
            if(roundHasStarted && ev.Round.Duration > 60)
            {
                string[][] variables = new string[16][]
                {
                    new string[] { "duration", (ev.Round.Duration/60).ToString() },
                    new string[] { "dclassalive", ev.Round.Stats.ClassDAlive.ToString() },
                    new string[] { "dclassdead", ev.Round.Stats.ClassDDead.ToString() },
                    new string[] { "dclassescaped", ev.Round.Stats.ClassDEscaped.ToString() },
                    new string[] { "dclassstart", ev.Round.Stats.ClassDStart.ToString() },
                    new string[] { "mtfalive", ev.Round.Stats.NTFAlive.ToString() },
                    new string[] { "scientistsalive", ev.Round.Stats.ScientistsAlive.ToString() },
                    new string[] { "scientistsdead", ev.Round.Stats.ScientistsDead.ToString() },
                    new string[] { "scientistsescaped", ev.Round.Stats.ScientistsEscaped.ToString() },
                    new string[] { "scientistsstart", ev.Round.Stats.ScientistsStart.ToString() },
                    new string[] { "scpalive", ev.Round.Stats.SCPAlive.ToString() },
                    new string[] { "scpdead", ev.Round.Stats.SCPDead.ToString() },
                    new string[] { "scpkills", ev.Round.Stats.SCPKills.ToString() },
                    new string[] { "scpstart", ev.Round.Stats.SCPStart.ToString() },
                    new string[] { "warheaddetonated", ev.Round.Stats.WarheadDetonated.ToString() },
                    new string[] { "zombies", ev.Round.Stats.Zombies.ToString() }
                };
                plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onroundend"), "round.onroundend", variables);
                roundHasStarted = false;
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            /// <summary>  
            ///  This event handler will call when the server is waiting for players
            /// </summary> 
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onwaitingforplayers"), "round.onwaitingforplayers");
        }

        public void OnRoundRestart(RoundRestartEvent ev)
        {
            /// <summary>  
            ///  This event handler will call when the server is about to restart
            /// </summary> 
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onroundrestart"), "round.onroundrestart");
        }

        public void OnSetServerName(SetServerNameEvent ev)
        {
            /// <summary>  
            ///  This event handler will call when the server name is set
            /// </summary>
            string[][] variables = new string[1][]
            {
                new string[] { "servername", ev.ServerName }
            };
            plugin.SendParsedMessageAsync(plugin.GetConfigString("discord_channel_onsetservername"), "round.onsetservername", variables);
        }


    }
}
