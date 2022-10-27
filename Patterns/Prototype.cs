using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;

namespace csgame_backend.Patterns
{
    public abstract class Prototype<T>
    {
        public abstract T? Clone();

        public abstract T? DeepClone();
    }
}
