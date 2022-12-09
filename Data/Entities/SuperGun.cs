namespace csgame_backend.Data.Entities
{
    public class SuperGun : Item
    {   
        public string? Name { get; set; }
        public float FireRate { get; set; }
        public float Damage { get; set; }
        public override int Id { get; set; }
    }
}
