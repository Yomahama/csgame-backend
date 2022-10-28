using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.Helpers;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns.Strategies
{
    public class ConcreteStrategyCheckforHitBulletPlayer : IStrategy
    {
       
        public bool CheckForHit(object obj, object obj_1, out double? x_target, out double? y_target, double? m = null, double? b = null, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            Console.WriteLine("Using strategy Bullet_player");
            x_target = null;
            y_target = null;

            Bullet? bullet = (Bullet)obj;
            Player? player_obj = (Player)obj_1;

            if (player_obj == null || bullet == null) return false; // incase we fail to take player from obj
            float x = (float)player_obj.PositionX;
            float y = (float)player_obj.PositionY;
            double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float)m * (float)m))); // check for shortest distance between dot and linear function

            //lets get coordinates of hitted targetpoint!
            if (collision_Type == Collision_Type.CIRCLE)
            {
                double? aprim = 1 + (m * m);
                double? bprim = 2 * m * (b - y) - 2 * x;
                double? cprim = (x * x) + Math.Pow((double)(b - y), 2) - Math.Pow((player_obj.collision.coll_length / 2), 2);
                double? delta = (bprim * bprim) - (4 * aprim * cprim);
                double? x_hit = (-bprim + Math.Sqrt((double)delta)) / (2 * aprim);
                double? y_hit = m * x_hit + b;

                double? x_hit1 = (-bprim - Math.Sqrt((double)delta)) / (2 * aprim);
                double? y_hit1 = m * x_hit1 + b;

                Console.WriteLine("x_hit: " + x_hit.ToString() + "  y_hit: " + y_hit.ToString());
                Console.WriteLine("x_hit1: " + x_hit1.ToString() + "  y_hit1: " + y_hit1.ToString());
                //lets find which point in circle is closer to us just to know where we hit in that circle and lets return hitpoint coordinates
                if (Utils.GetDistance(bullet.PositionX, bullet.PositionY, (double)x_hit, (double)y_hit) < Utils.GetDistance(bullet.PositionX, bullet.PositionY, (double)x_hit1, (double)y_hit1))
                {
                    x_target = x_hit;
                    y_target = y_hit;
                }
                else
                {
                    x_target = x_hit1;
                    y_target = y_hit1;
                }
            }
            if (d < (player_obj.collision.coll_length / 2)) return true; // if distance to dot is smaller than its radius then we HIT it ! else miss

            return false;
        }
    }
}
