namespace csgame_backend.Data.Entities
{
    public class Gun : Item
    {   
        public string? Name { get; set; }
        public float FireRate { get; set; } // rounds per minute
        public float Damage { get; set; }
        public override int Id { get; set; }
    }
}
