namespace csgame_backend.Data.Entities
{
    public class Obsticle
    {
        public int Id { get; set; }

        public Collision? collision { get; set; }

        public double positionX { get; set; }
        public double positionY { get; set; }

    }
}
