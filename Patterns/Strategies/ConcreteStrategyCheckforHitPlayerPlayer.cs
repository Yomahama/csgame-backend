using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.Helpers;
using System.Numerics;

namespace csgame_backend.Patterns.Strategies
{
    public class ConcreteStrategyCheckforHitPlayerPlayer : IStrategy
    {
        public bool CheckForHit(object obj, object obj_1, out double? x_target, out double? y_target, 
            double? m = null, double? b = null,
            Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            Player player = obj as Player;
            Player player_1 = obj_1 as Player;
            x_target = null;
            y_target = null;
            if (player == null || obj == null) return false;

            if (collision_Type == Collision_Type.CIRCLE)
                return Utils.GetDistance(player.PositionX, player.PositionY, player_1.PositionX, player_1.PositionY) < (player.collision.coll_length + player_1.collision.coll_length);
            return false;
        }
    }
}
