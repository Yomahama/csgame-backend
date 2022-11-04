using System;
using csgame_backend.Data.Entities;
using csgame_backend.Patterns;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class Game : WebSocketBehavior
    {
        private IBuilder _builder;

        protected override void OnOpen()
        {
            _builder = new Builder();
            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs args)
        {
            //Send("testas");
            var unkown_data = JsonConvert.DeserializeObject<Player>(args.Data);

            if(unkown_data == null)
            {
                Sessions.Broadcast("Error");
                return;
            }

            int index = TeamSingleton.Instance.Teamm.Players.IndexOf(unkown_data);

            if (index == -1) // never seen this players' data before
            {
                _builder.AddGunPistol();
                Player new_player = _builder.Build(unkown_data.Username, unkown_data.PositionX, unkown_data.PositionY);
                _builder.Reset();
                TeamSingleton.Instance.Teamm.Players.Add(unkown_data);
            }
            else // recognized as the player
            {
                TeamSingleton.Instance.Teamm.Players[index] = unkown_data;
            }


            var json = JsonConvert.SerializeObject(TeamSingleton.Instance.Teamm);

            Sessions.Broadcast(json);
        }
    }
}



