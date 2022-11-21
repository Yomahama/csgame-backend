using csgame_backend.Data.Entities;
using csgame_backend.Helpers;
using csgame_backend.Patterns;
using csgame_backend.Patterns.Strategies;
using Newtonsoft.Json;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class Projectile : WebSocketBehavior
    {
        private Context _context = new Context();
        
        protected override void OnOpen()
        {
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

            objects.AddRange(players);
            
            Utils.Trajectory(bullet.PositionX, bullet.PositionY, 
                bullet.TargetX, bullet.TargetY, out double m, out double b);
            double? x_hit = null;
            double? y_hit = null;

            Object unlucky_one = null;

            // check if hit player // later can expand and check also for other objects!
            foreach (Object obj in objects)
            {
                if (obj is Player && obj != null)
                {
                    // Cannot Hit yourself!
                    if (bullet.Username.Equals((obj as Player).Username))
                    {
                        continue;
                    }
                }
                // Found Player!
                if (obj is Player)// this one can check more than players
                {

                    _context.setStrategy(new ConcreteStrategyCheckforHitBulletPlayer());
                    if (_context.executeSrategy(bullet, obj as Player, out x_hit, out y_hit, m, b, Data.Resources.Collision_Type.CIRCLE))
                    {

                        unlucky_one = obj as Player;
                        break;
                    }

                } // found obsticle
                else if (obj is Obstacle)// this one can check more than players
                {
                    _context.setStrategy(new ConcreteStrategyCheckforHitBulletObstacle());
                    if (_context.executeSrategy(bullet, obj as Obstacle, out x_hit, out y_hit, m, b, Data.Resources.Collision_Type.SQUARE ))
                    {
                        unlucky_one = obj as Obstacle;
                        break;
                    }
  
                }
            }
            if (unlucky_one is not null) // Add additional information about hitted person
            {
                if (unlucky_one is Player) bullet.HitTargetUsername = (unlucky_one as Player).Username;
                if (unlucky_one is Obstacle) bullet.HitTargetUsername = (unlucky_one as Obstacle).Id.ToString();
                // here we can change the target position to hit position
                // as we currently don't need to know where mouse clicked
                if (x_hit is not null && y_hit is not null)
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
