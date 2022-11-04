using csgame_backend.Data.Entities;
using csgame_backend.csgame_backend.Patterns;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{
    public abstract class AbstractFactory
    {
        private Player player;
    }
    public abstract class Utility : AbstractFactory
    {
        public abstract void hlthUp(Player player);
        public abstract void hlthDown(Player player);
        public abstract void removeDmgInstance(Shoot shot);
    }
    public abstract class Power : AbstractFactory
    {
        public abstract void dmgUp(Player player);
        public abstract void dmgSuperUp(Player player);
        public abstract void fireUp(Player player);

    }
    abstract class Health : Utility
    {
        public override void hlthUp(Player player)
        {
            player.Health += 150;
        }
        public override void hlthDown(Player player)
        {
            player.Health -= 30;
        }
    }
    abstract class Other : Utility
    {
        public override void removeDmgInstance(Shoot shot)
        {
            shot.UnExecuteCommand();
        }
    }
    abstract class Firerate : Power
    {
        public override void fireUp(Player player)
        {
            foreach (Gun gun in player.guns)
            {
                gun.FireRate += 5;
            }
        }
    }
    abstract class Damage : Power 
    {
        public override void dmgUp(Player player)
        {
            foreach (Gun gun in player.guns)
            {
                gun.Damage += 5;
            }
        }
        public override void dmgSuperUp(Player player)
        {
            foreach (Gun gun in player.guns)
            {
                gun.Damage *= 2;
            }
        }
    }

}