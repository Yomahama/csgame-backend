using System;
namespace csgame_backend.player_websocket
{
    public static class TeamSingleton
    {
        private static TeamSingletonInstance? _team;
        private static readonly object _lock = new object();

        public static TeamSingletonInstance Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_team == null)
                    {
                        _team = new TeamSingletonInstance();
                    }

                    return _team;
                }
            }
        }
       
    }

    public class TeamSingletonInstance
    {
        public Team Teamm { get; set; } = new Team();
    }
}

