using csgame_backend.Patterns;
using csgame_backend.Data.Entities;
using Newtonsoft.Json;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class SpecialProjectile : WebSocketBehavior
    {
        private IStrategy? _strategy;

        protected override void OnOpen()
        {
            IStrategy strategy = new Strategy();
            _strategy = strategy;

            base.OnOpen();
        }
        public string DropBomb()
        {
            return "bomb droped";
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            var bullet = JsonConvert.DeserializeObject<Bullet>(e.Data);

            if (bullet == null)
            {
                Sessions.Broadcast("Bullet Error");
                return;
            }

            List<Player> players = TeamSingleton.Instance.Teamm.Players;
            Player? unlucky_one = null;


            // check if hit player
            foreach (Player player in players)
            {
                // Found someone !
                if (_strategy.DropBomb())
                {
                    break;
                }
            }
            if (unlucky_one is not null) // Add additional information about hitted person
            {
                bullet.HitTargetUsername = unlucky_one.Username;
            }

            var json = JsonConvert.SerializeObject(bullet);

            Sessions.Broadcast(json);
        }
    }
}
