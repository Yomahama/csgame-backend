using csgame_backend.Data.Entities;
using static csgame_backend.Helpers.Utils;

namespace csgame_backend.Helpers
{

    public class Utils
    {
        
        public static void Trajectory(double x, double y, double x1, double y1, out double m, out double b)
        {
            m = 0;
            b = 0;

            m = (y1 - y) / (x1 - x);
            b = y1 - m * x1;
        }

        public static double GetDistance(double x, double y, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2));
        }
    }
}
