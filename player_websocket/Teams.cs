using System;
namespace csgame_backend.player_websocket
{
    public class Teams
    {
        public Team TeamOne { get; set; }

        public Team TeamTwo { get; set; }

        public List<int> Score { get; set; }

        public Teams(Team teamOne, Team teamTwo, List<int> score)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
            Score = score;
        }
    }
}

