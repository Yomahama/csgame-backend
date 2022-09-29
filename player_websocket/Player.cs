﻿using System;
using Newtonsoft.Json;

namespace csgame_backend.player_websocket
{
    public class Player
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("x")]
        public double PositionX { get; set; }
        [JsonProperty("y")]
        public double PositionY { get; set; }

        public Player(string username, int positionX, int positionY)
        {
            Username = username;
            PositionX = positionX;
            PositionY = positionY;
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
    }
}

