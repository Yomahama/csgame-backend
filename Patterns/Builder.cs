﻿using csgame_backend.Data.Entities;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{
    public interface IBuilder
    {
        void AddGunPistol();
        void AddGunSniper();
        void AddGunSubmachine();
        Player Build(string username = "default", double x = 0, double y = 0);
        void Reset(string username, double x, double y);
    }

    public class Builder : IBuilder
    {
        private Player player;


        public Builder()
        {
            player = new Player("default", 0, 0);
        }

        public void Reset(string username, double x, double y)
        {
            this.player = new Player(username,(int) x, (int) y);
            
        }
        public Player Build(string username = "default", double x = 0, double y = 0)
        {
            Player result = this.player;

            this.Reset("default_player", 0, 0); // needs to be ready for next build

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