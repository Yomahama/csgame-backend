using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns
{
    public interface IBuilder
    {
        void AddGunPistol();
        void AddGunSniper();
        void AddGunSubmachine();
        Player Build(string username = "default", double x = 0, double y = 0);
        void Reset();
    }

    public class Builder : IBuilder
    {
        private Player? player;


        public Builder()
        {
            player = new Player("default", 0, 0);
        }

        public void Reset()
        {
            this.player = new Player("default", 0, 0);

        }
        public Player Build(string username = "default", double x = 0, double y = 0)
        {
            Player result = this.player;

            this.Reset(); // needs to be ready for next build

            return result;
        }

        public void AddGunSniper()
        {
            Gun sniper = new Gun()
            {
                Damage = 30f,
                FireRate = 20.0f,
                Name = "Sniper"
            };

            this.player.AddGun(sniper);
        }
        public void AddGunPistol()
        {
            Gun pistol = new Gun()
            {
                Damage = 5.0f,
                FireRate = 60.0f,
                Name = "Pistol"
            };

            this.player.AddGun(pistol);
        }
        public void AddGunSubmachine()
        {
            Gun submachine = new Gun()
            {
                Damage = 15.0f,
                FireRate = 300.0f,
                Name = "Submachine"
            };

            this.player.AddGun(submachine);
        }
    }
}
