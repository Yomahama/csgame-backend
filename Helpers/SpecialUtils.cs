namespace csgame_backend.Helpers
{
    public class SpecialUtils
    {
        public void PlaceMine(double x, double y, out double m, out double b)
        {
            m = 0;
            b = 0;
        }

        public static double GetDistance(double x, double y, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y - y1, 2));
        }
    }
}
