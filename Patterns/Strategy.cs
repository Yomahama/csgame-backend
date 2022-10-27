using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{

    public interface IStrategy
    {
        bool CheckForHit(Bullet bullet, Obsticle? obj, double m, double b, out double? x_target, out double? y_target, Collision_Type collision_Type = Collision_Type.SQUARE);
        bool CheckForHit(Bullet bullet, Player obj, double m, double b, out double? x_target, out double? y_target, Collision_Type collision_Type = Collision_Type.CIRCLE);
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
        public bool CheckForHit(Bullet bullet, Player obj, double m, double b, out double? x_target, out double? y_target, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            x_target = null;
            y_target = null;

            if (obj == null) return false;
            Player? player_obj = obj;
            if (player_obj == null) return false; // incase we fail to take player from obj

            float x = (float)player_obj.PositionX;
            float y = (float)player_obj.PositionY;
            double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float)m * (float)m))); // check for shortest distance between dot and linear function

            //lets get coordinates of hitted targetpoint!
            if (collision_Type == Collision_Type.CIRCLE)
            {
                double aprim = 1 + (m * m);
                double bprim = 2 * m * (b - y) - 2 * x;
                double cprim = (x * x) + Math.Pow((b - y), 2) - Math.Pow((player_obj.collision.coll_length / 2), 2);
                double delta = (bprim * bprim) - (4 * aprim * cprim);
                double x_hit = (-bprim + Math.Sqrt(delta)) / (2 * aprim);
                double y_hit = m * x_hit + b;

                double x_hit1 = (-bprim - Math.Sqrt(delta)) / (2 * aprim);
                double y_hit1 = m * x_hit1 + b;

                Console.WriteLine("x_hit: " + x_hit.ToString() + "  y_hit: " + y_hit.ToString());
                Console.WriteLine("x_hit1: " + x_hit1.ToString() + "  y_hit1: " + y_hit1.ToString());

                //lets find which point in circle is closer to us just to know where we hit in that circle and lets return hitpoint coordinates
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
            }
           

            if (d < (player_obj.collision.coll_length / 2)) return true; // if distance to dot is smaller than its radius then we HIT it ! else miss

            return false;
        }

        public bool CheckForHit(Bullet bullet, Obsticle? obj, double m, double b, out double? x_target, out double? y_target, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            x_target = null;
            y_target = null;

            if (obj == null) return false; // incase we fail to take player from obj

            float x = (float)obj.positionX;
            float y = (float)obj.positionY;
            double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float)m * (float)m))); // check for shortest distance between dot and linear function

            //lets get coordinates of hitted targetpoint!
            if(collision_Type == Collision_Type.SQUARE)
            {

                
                /*     
                 *     A ------------- D
                 *       |           |
                 *       |           |
                 *       |           |
                 *     B ------------- C
                 * 
                 */

                Position A = new Position();
                A.X = obj.positionX - (obj.collision.coll_length / 2);
                A.Y = obj.positionY + (obj.collision.coll_height / 2);
                Position B = new Position();
                B.X = obj.positionX - (obj.collision.coll_length / 2);
                B.Y = obj.positionY - (obj.collision.coll_height / 2);
                Position C = new Position();
                C.X = obj.positionX + (obj.collision.coll_length / 2);
                C.Y = obj.positionY - (obj.collision.coll_height / 2);
                Position D = new Position();
                D.X = obj.positionX + (obj.collision.coll_length / 2);
                D.Y = obj.positionY + (obj.collision.coll_height / 2);

                var t = 0;

                // the bullet goes thro the object               
                if (m * B.X + b <= D.Y && m * B.X + b >= B.Y && (B.Y + b)/ m >= B.X && (C.Y + b)/ m <= C.X)
                {
                    double? x_temp, y_temp;
                    // calculating intersect of each line in square
                    Position line_AB = new Position();
                    List<Position> intersect_points = new List<Position>();
                    Trajectory(A.X, A.Y, B.X, B.Y,out double m_line, out double b_line);
                    double det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_AB.X = (m_line * b - m * b_line) / det;
                        line_AB.Y = (1 * b - 1 * b_line) / det;

                        if (line_AB.Y >= B.Y && line_AB.Y <= A.Y) intersect_points.Add(line_AB);
                    }
                    Position line_AD = new Position();
                    Trajectory(A.X, A.Y, D.X, D.Y, out m_line, out b_line);
                    det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_AD.X = (m_line * b - m * b_line) / det;
                        line_AD.Y = (1 * b - 1 * b_line) / det;

                        if (line_AD.X >= A.X && line_AD.X <= D.X) intersect_points.Add(line_AD);
                    }
                    Position line_CB = new Position();
                    Trajectory(C.X, C.Y, B.X, B.Y, out m_line, out b_line);
                    det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_CB.X = (m_line * b - m * b_line) / det;
                        line_CB.Y = (1 * b - 1 * b_line) / det;
                        if (line_CB.X >= B.X && line_CB.X <= C.X) intersect_points.Add(line_CB);
                    }
                    Position line_CD = new Position();
                    Trajectory(C.X, C.Y, D.X, D.Y, out m_line, out b_line);
                    //pointIntersect(m, b, m_line, b_line, out x_temp, out y_temp);

                    det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_CD.X = (m_line * b - m * b_line) / det;
                        line_CD.Y = (1 * b - 1 * b_line) / det;

                        if (line_CD.Y >= C.Y && line_CD.Y <= D.Y) intersect_points.Add(line_CD);
                    }
                    double min = -1;
                    foreach (Position pos in intersect_points)
                    {
                        if(min == -1)
                        {
                            min = GetDistance(bullet.PositionX, bullet.PositionY, pos.X, pos.Y);
                        }else
                        {
                            if (min > GetDistance(bullet.PositionX, bullet.PositionY, pos.X, pos.Y))
                            {
                                min = GetDistance(bullet.PositionX, bullet.PositionY, pos.X, pos.Y);
                                x_target = pos.X;
                                y_target = pos.Y;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }  

            return false;
        }

        private void pointIntersect(double m, double b, double m_1, double b_1, out double? x, out double? y)
        {
            double det = m * 1 - m_1 * 1;
            if (det == 0)
            {
                x = null;
                y = null;
            }
            else
            {
                x = (m_1 * b - m * b_1) / det;
                y = (1 * b - 1 * b_1) / det;
            }
        }
    }
}
