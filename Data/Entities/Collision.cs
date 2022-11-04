using csgame_backend.Data.Resources;

namespace csgame_backend.Data.Entities
{
    public class Collision
    {
        public Collision_Type type { get; set; }

        public double coll_height { get; set; }
        public double coll_length { get; set; }
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
