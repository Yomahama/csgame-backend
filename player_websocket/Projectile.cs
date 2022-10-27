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
            
            List<Object> objects = new List<Object>();

            objects.Add(TeamSingleton.Instance.Teamm.Players);

            Object? unlucky_one = null;

            _strategy.Trajectory(bullet.PositionX, bullet.PositionY, bullet.TargetX, bullet.TargetY, out double m, out double b);
            double? x_hit = null;
            double? y_hit = null;


            // check if hit player // later can expand and check also for other objects!
            foreach (Object obj in objects)
            {
                if (obj is Player)
                {
                    // Cannot Hit yourself!
                    if (bullet.Username.Equals((obj as Player).Username))
                    {
                        continue;
                    }
                }
                    // Found Player!
                if (_strategy.CheckForHit(bullet, obj as Player, m, b, out x_hit, out y_hit))// this one can check more than players
                {
                    unlucky_one = obj as Player;
                    break;
                } // found obsticle
                else if (_strategy.CheckForHit(bullet, obj as Obsticle, m, b, out x_hit, out y_hit))// this one can check more than players
                {
                    unlucky_one = obj;
                    break;
                }
            }
            if (unlucky_one is not null) // Add additional information about hitted person
            {
                if(unlucky_one is Player) bullet.HitTargetUsername = (unlucky_one as Player).Username;
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
