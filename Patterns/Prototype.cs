using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;

namespace csgame_backend.Patterns
{
    public interface IPrototype<T>
    {
        T? Clone();
        T? DeepClone();
    }

    public abstract class Prototype<T> : IPrototype<T>
    {
        public abstract T? Clone();

        public abstract T? DeepClone();
    }
}
