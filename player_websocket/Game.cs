using System;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class Game : WebSocketBehavior
    {

        protected override void OnMessage(MessageEventArgs args)
        {
            //Send("testas");
            var player = JsonConvert.DeserializeObject<Player>(args.Data);

            if(player == null)
            {
                Sessions.Broadcast("Error");
                return;
            }

            int index = TeamSingleton.Instance.Teamm.Players.IndexOf(player);

            if (index == -1)
            {
                TeamSingleton.Instance.Teamm.Players.Add(player);
            }
            else
            {
                TeamSingleton.Instance.Teamm.Players[index] = player;
            }


            var json = JsonConvert.SerializeObject(TeamSingleton.Instance.Teamm);

            Sessions.Broadcast(json);
        }
    }
}



