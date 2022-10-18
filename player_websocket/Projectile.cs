using csgame_backend.Data.Entities;
using csgame_backend.Patterns;
using Newtonsoft.Json;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class Projectile : WebSocketBehavior
    {
        private IStrategy? _strategy;

        protected override void OnOpen()
        {
            IStrategy strategy = new Strategy();
            _strategy = strategy;

            base.OnOpen();
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

            _strategy.Trajectory(bullet.PositionX, bullet.PositionY, bullet.TargetX, bullet.TargetY, out double m, out double b);
            double? x_hit = null;
            double? y_hit = null;


            // check if hit player // later can expand and check also for other objects!
            foreach (Player player in players)
            {
                // Cannot Hit yourself!
                if(bullet.Username.Equals(player.Username))
                {
                    continue;
                }

                // Found someone !
                if (_strategy.CheckForHit(bullet, player, m, b, out x_hit, out y_hit))// this one can check more than players
                {
                    unlucky_one = player;
                    break;
                }
            }
            if (unlucky_one is not null) // Add additional information about hitted person
            {
                bullet.HitTargetUsername = unlucky_one.Username;
                // here we can change the target position to hit position
                // as we currently don't need to know where mouse clicked
                if(x_hit is not null && y_hit is not null)
                {
                    bullet.TargetX = Math.Round((double) x_hit, 2);
                    bullet.TargetY = Math.Round((double) y_hit, 2);
                }
            }

            var json = JsonConvert.SerializeObject(bullet);
            
            Sessions.Broadcast(json);
        }
    }
}
