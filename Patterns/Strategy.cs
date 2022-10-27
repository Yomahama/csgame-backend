﻿using csgame_backend.Data.Entities;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{
    public interface IStrategy
    {
        bool CheckForHit(Bullet bullet, object? obj, double m, double b, out double? x_target, out double? y_target);
        double GetDistance(double x, double y, double x1, double y1);
        void Trajectory(double x, double y, double x1, double y1, out double m, out double b);
    }

    public class Strategy : IStrategy
    {
        public void Trajectory(double x, double y, double x1, double y1, out double m, out double b)
        {
            m = 0;
            b = 0;

            m = (y1 - y) / (x1 - x);
            b = y1 - m * x1;
        }

        public double GetDistance(double x, double y, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2));
        }
        public bool CheckForHit(Bullet bullet, object? obj, double m, double b, out double? x_target, out double? y_target)
        {
            x_target = null;
            y_target = null;

            if (obj == null) return false;
            if (obj is Player)
            {
                Player? player_obj = obj as Player;
                if (player_obj == null) return false; // incase we fail to take player from obj

                float x = (float)player_obj.PositionX;
                float y = (float)player_obj.PositionY;
                double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float)m * (float)m))); // check for shortest distance between dot and linear function

                // lets get coordinates of hitted targetpoint!

                double aprim = 1 + (m * m);
                double bprim = 2 * m * (b - y) - 2 * x;
                double cprim = (x * x) + Math.Pow((b - y), 2) - Math.Pow(player_obj.CollisionRadius, 2);
                double delta = (bprim * bprim) - (4 * aprim * cprim);
                double x_hit = (-bprim + Math.Sqrt(delta)) / (2 * aprim);
                double y_hit = m * x_hit + b;

                double x_hit1 = (-bprim - Math.Sqrt(delta)) / (2 * aprim);
                double y_hit1 = m * x_hit1 + b;

                //Console.WriteLine("x_hit: "+ x_hit.ToString() + "  y_hit: " + y_hit.ToString() );
                //Console.WriteLine("x_hit1: " + x_hit1.ToString() + "  y_hit1: " + y_hit1.ToString());

                // lets find which point in circle is closer to us just to know where we hit in that circle and lets return hitpoint coordinates
                if (GetDistance(bullet.PositionX, bullet.PositionY, x_hit, y_hit) < GetDistance(bullet.PositionX, bullet.PositionY, x_hit1, y_hit1))
                {
                    x_target = x_hit;
                    y_target = y_hit;
                }
                else
                {
                    x_target = x_hit1;
                    y_target = y_hit1;
                }

                if (d < player_obj.CollisionRadius) return true; // if distance to dot is smaller than its radius then we HIT it ! else miss
            }

            return false;
        }
    }
}
