using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;
using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{

    public interface IStrategy
    {
        bool CheckForHit(Object obj, Object obj_1, out double? x_target, out double? y_target, double? m = null, double? b = null,  Collision_Type collision_Type = Collision_Type.SQUARE);
    }
}
