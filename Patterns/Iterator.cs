using Microsoft.OpenApi.Services;
using System.Collections.Generic;

namespace csgame_backend.Patterns
{
    public interface Iterator
    {
        public object? next();
        public bool hasNext();

        public int getIndex();

        public object? getItem();
    }

    public interface IterableCollection
    {
        public Iterator getIterator();
    }
}
