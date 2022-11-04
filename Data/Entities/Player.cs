using System;
using System.Reflection.Metadata.Ecma335;
using csgame_backend.Patterns;
using Newtonsoft.Json;

namespace csgame_backend.Data.Entities
{
    public class Player : Prototype<Player>
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("x")]
        public double PositionX { get; set; }
        [JsonProperty("y")]
        public double PositionY { get; set; }

        public List<Gun> guns;
        public float Health { get; set; }

        public Collision collision;
        // radius of detection zone around the player position
        public double CollisionRadius { get; private set; }

        public Player(string username, int positionX, int positionY)
        {
            Username = username;
            PositionX = positionX;
            PositionY = positionY;
            collision = new Collision(Resources.Collision_Type.CIRCLE, 40, 40);
            CollisionRadius = 20;
            guns = new List<Gun>();
        }


        public void AddGun(Gun gun)
        {
            guns.Add(gun);
        }

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   Username == player.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username);
        }

        public override Player? Clone()
        {
            try
            {
                Player? clone = MemberwiseClone() as Player;
                if (clone == null) return null;
                clone.Username = "Cloned_Player" + DateTime.Now.ToString();
                return clone;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public override Player? DeepClone()
        {
            Player p = (Player)MemberwiseClone();

            p.collision = collision.GetClone();
            return p;
        }

        /// <summary>
        /// Damage function for the Command pattern
        /// </summary>
        public void TakeDamage(float amount)
        {
            Health -= amount;
        }
        public void UndoDamage(float amount)
        {
            Health += amount;
        }
    }
}

