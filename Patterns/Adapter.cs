using csgame_backend.player_websocket;

namespace csgame_backend.Patterns
{
    public class Adapter
    { 

        SpecialProjectile e = new SpecialProjectile();
        public string CheckForHit()
        {
            return e.DropBomb();
        }
    }
}
