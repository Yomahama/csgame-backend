using csgame_backend.Patterns;
using csgame_backend.player_websocket;

namespace csgame_backend.Data.Entities
{
    public class Obstacle : Prototype<Obstacle>
    {
        public int Id { get; set; }

        public Collision? collision { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public Obstacle()
        {
            this.Id = int.Parse(DateTime.Now.ToString());
            collision = new Collision(Resources.Collision_Type.SQUARE, 20, 40);
            PositionX = 0;
            PositionY = 0;
        }

        public Obstacle(int id, Collision? collision, double positionX, double positionY)
        {
            Id = id;
            this.collision = collision;
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public override Obstacle? Clone()
        {
            try
            {
                Obstacle? clone = this.MemberwiseClone() as Obstacle;
                if (clone == null) return null;
                clone.Id = int.Parse(DateTime.Now.ToString());
                return clone;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Obstacle? DeepClone()
        {
            Obstacle clone = (Obstacle)this.MemberwiseClone();

            clone.collision = this.collision.GetClone();
            return clone;
        }
    }
}
