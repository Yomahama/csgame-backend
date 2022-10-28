using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.Helpers;

namespace csgame_backend.Patterns.Strategies
{
    public class ConcreteStrategyCheckforHitBulletObstacle : IStrategy
    {
        public bool CheckForHit(object obj, object obj_1, 
            out double? x_target, out double? y_target, 
            double? m = null, double? b = null, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            x_target = null;
            y_target = null;

            Bullet bullet = obj as Bullet;
            Obstacle obs = obj_1 as Obstacle;

            if (obj == null) return false; // incase we fail to take player from obj

            float x = (float)obs.PositionX;
            float y = (float)obs.PositionY;
            double d = MathF.Abs((float)b + ((float)m * x) - y) / (MathF.Sqrt(1.0f + ((float)m * (float)m))); // check for shortest distance between dot and linear function

            //lets get coordinates of hitted targetpoint!
            if (collision_Type == Collision_Type.SQUARE)
            {

                Position A = new Position();
                A.X = obs.PositionX - (obs.collision.coll_length / 2);
                A.Y = obs.PositionY + (obs.collision.coll_height / 2);
                Position B = new Position();
                B.X = obs.PositionX - (obs.collision.coll_length / 2);
                B.Y = obs.PositionY - (obs.collision.coll_height / 2);
                Position C = new Position();
                C.X = obs.PositionX + (obs.collision.coll_length / 2);
                C.Y = obs.PositionY - (obs.collision.coll_height / 2);
                Position D = new Position();
                D.X = obs.PositionX + (obs.collision.coll_length / 2);
                D.Y = obs.PositionY + (obs.collision.coll_height / 2);

                var t = 0;

                // the bullet goes thro the object               
                if (m * B.X + b <= D.Y && m * B.X + b >= B.Y && (B.Y + b) / m >= B.X && (C.Y + b) / m <= C.X)
                {
                    double? x_temp, y_temp;
                    // calculating intersect of each line in square
                    Position line_AB = new Position();
                    List<Position> intersect_points = new List<Position>();
                    Utils.Trajectory((double)A.X, (double)A.Y, (double)B.X, (double)B.Y, out double m_line, out double b_line);
                    double? det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_AB.X = (m_line * b - m * b_line) / det;
                        line_AB.Y = (1 * b - 1 * b_line) / det;

                        if (line_AB.Y >= B.Y && line_AB.Y <= A.Y) intersect_points.Add(line_AB);
                    }
                    Position line_AD = new Position();
                    Utils.Trajectory((double)A.X, (double)A.Y, (double)D.X, (double)D.Y, out m_line, out b_line);
                    det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_AD.X = (m_line * b - m * b_line) / det;
                        line_AD.Y = (1 * b - 1 * b_line) / det;

                        if (line_AD.X >= A.X && line_AD.X <= D.X) intersect_points.Add(line_AD);
                    }
                    Position line_CB = new Position();
                    Utils.Trajectory((double)C.X, (double)C.Y, (double)B.X, (double)B.Y, out m_line, out b_line);
                    det = m * 1 - m_line * 1;
                    if (det == 0) { }
                    else
                    {
                        line_CB.X = (m_line * b - m * b_line) / det;
                        line_CB.Y = (1 * b - 1 * b_line) / det;
                        if (line_CB.X >= B.X && line_CB.X <= C.X) intersect_points.Add(line_CB);
                    }
                    Position line_CD = new Position();
                    Utils.Trajectory((double) C.X,(double) C.Y,(double) D.X, (double) D.Y, out m_line, out b_line);

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
                        if (min == -1)
                        {
                            min = Utils.GetDistance(bullet.PositionX, bullet.PositionY, (double)pos.X, (double)pos.Y);
                        }
                        else
                        {
                            if (min > Utils.GetDistance(bullet.PositionX, bullet.PositionY, (double)pos.X, (double)pos.Y))
                            {
                                min = Utils.GetDistance(bullet.PositionX, bullet.PositionY, (double)pos.X, (double)pos.Y);
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
    }
}
