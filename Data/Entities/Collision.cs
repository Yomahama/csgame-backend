using csgame_backend.Data.Resources;

namespace csgame_backend.Data.Entities
{
    public class Collision
    {
        public Collision_Type type { get; set; }

        public double coll_height { get; set; }
        public double coll_length { get; set; }

        // Contains list of walls size for the obstacle 
        //  
        //  1-------2
        //  |       |
        //  |       |
        //  4-------3
        //  IF 4 walls - square
        //  IF more then:
        //
        //  1-------2   5-------6
        //  |       |   |       |
        //  |       |   |       |
        //  4-------3   8-------7
        //
        //  Everything is desided as non rotatable objects and we count as 20 width, 80 height
        //  is not same as 80 width and 20 height

        public List<Tuple<double, double>> wallsList { get; set; }
        // gap is between points 2 5 and 3 8
        public double gap { get; set; }
        public Collision(Collision_Type type, double height, double length)
        {
            this.type = type;
            coll_height = height;
            coll_length = length;
        }

        public Collision GetClone()
        {
            return (Collision)this.MemberwiseClone();
        }
    }
}
