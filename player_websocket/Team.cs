using System;
using csgame_backend.Data.Entities;
using Newtonsoft.Json;

namespace csgame_backend.player_websocket
{
    public class Team
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; } = new List<Player>();

        public Team()
        {
            Players = new List<Player>();
        }
    }
}

