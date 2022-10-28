using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns
{
    public abstract class AbstractFactory
    {
        private Player player;

        public abstract void hlthUp(Player player);
        public abstract void dmgUp(Player player);

    }
    public abstract class Utility : AbstractFactory
    {
        public override void hlthUp(Player player)
        {
            player.Health += 150;
        }
    }
    public abstract class Power : AbstractFactory
    {
        public override void dmgUp(Player player)
        {
            foreach (Gun gun in player.guns)
            {
                gun.Damage += 5;
            }
        }
    }
}
