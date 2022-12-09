using System.Numerics;

namespace csgame_backend.Patterns
{
    public class Memento
    {
        private Vector2 state;
        public Memento(Vector2 state)
        {
            this.state = state;
        }
        public Vector2 getState()
        {
            return state;
        }
    }
}
