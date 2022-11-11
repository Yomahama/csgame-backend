using csgame_backend.Helpers;

namespace csgame_backend.Patterns
{
    public class Adapter : Utils
    {
        SpecialUtils _adaptee = new SpecialUtils();

        public void Trajectory(double x, double y, out double m, out double b)
        {
            _adaptee.PlaceMine(x,y, out m, out b);
        }
    }
}
