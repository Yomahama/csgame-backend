using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace csgame_backend.Data.Entities
{
    public class Bullet
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("x")]
        public double PositionX { get; set; }
        [JsonProperty("y")]
        public double PositionY { get; set; }
        [JsonProperty("target_x")]
        public double TargetX { get; set; }
        [JsonProperty("target_y")]
        public double TargetY { get; set; }

        [JsonProperty("hit_target_username")] // only to return // not null if hit
        public string HitTargetUsername { get; set; }

        public Bullet(string username, double positionX, double positionY, double targetX, double targetY)
        {
            Username = username;
            PositionX = positionX;
            PositionY = positionY;
            TargetX = targetX;
            TargetY = targetY;
        }

        public override bool Equals(object? obj)
        {
            return obj is Bullet bullet &&
                   Username == bullet.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username);
        }
    }
}
