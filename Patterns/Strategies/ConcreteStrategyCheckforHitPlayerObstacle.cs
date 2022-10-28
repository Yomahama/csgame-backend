using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.Helpers;
using csgame_backend.player_websocket;
using System.Numerics;

namespace csgame_backend.Patterns.Strategies
{
    public class ConcreteStrategyCheckforHitPlayerObstacle : IStrategy
    {
        public bool CheckForHit(object obj, object obj_1, out double? x_target, out double? y_target, double? m = null, double? b = null, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            Player player = obj as Player;
            Obstacle obs = obj_1 as Obstacle;
            x_target = null;
            y_target = null;

            if (player == null || obj == null) return false;
            return Utils.GetDistance(player.PositionX, player.PositionY, obs.PositionX, obs.PositionY) < (player.collision.coll_length + obs.collision.coll_length)
                && Utils.GetDistance(player.PositionX, player.PositionY, obs.PositionX, obs.PositionY) < (player.collision.coll_height + obs.collision.coll_height);
        }
    }
}
