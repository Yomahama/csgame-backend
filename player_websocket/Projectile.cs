using csgame_backend.Data.Entities;
using Newtonsoft.Json;
using System.Numerics;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.player_websocket
{
    public class Projectile : WebSocketBehavior
    {

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

            Trajectory(bullet.PositionX, bullet.PositionY, bullet.TargetX, bullet.TargetY, out double m, out double b);

            // check if hit player // later can expand and check also for other objects!
            foreach (Player player in players)
            {
                // Cannot Hit yourself!
                if(bullet.Username.Equals(player.Username))
                {
                    continue;
                }

                // Found someone !
                if(CheckForHit(player, m, b))// this one can check more than players
                {
                    unlucky_one = player;
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

        private void Trajectory(double x, double y,double x1, double y1, out double m, out double b)
        {
            m = 0;
            b = 0;

            m = (y1 - y) / (x1 - x);
            b = y1 - m * x1;
        }
        private bool CheckForHit(object? obj, double m, double b)
        {
            if (obj == null) return false;
            if (obj is Player) 
            { 
                Player? player_obj = obj as Player;
                if (player_obj == null) return false; // incase we fail to take player from obj

                float x = (float) player_obj.PositionX;
                float y = (float) player_obj.PositionY;
                double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float) m * (float) m))); // check for shortest distance between dot and linear function

                if (d < player_obj.CollisionRadius) return true; // if distance to dot is smaller than its radius then we HIT it ! else miss
            }

            return false;
        }
    }
}
