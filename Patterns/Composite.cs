using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns
{
    public class Composite : Item
    {
        public List<Item> weaponsList = new List<Item>();

        public override int Id { get; set; }

        public void addWeapon(Item item)
        {
            weaponsList.Add(item);
        }
        public void removeWeapon(Item item)
        {
            weaponsList.Remove(item);
        }
    }

    
}
