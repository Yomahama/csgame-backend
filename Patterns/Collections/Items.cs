using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns.Collections
{
    public class Items : IterableCollection
    {
        // Some data structure
        // Binary tree
        private SortedSet<Item> items;

        public Items()
        {
            items = new SortedSet<Item>();
        }
        
        public void addItem(Item item)
        {
            items.Add(item);
        }

        public void removeItem(Item item)
        {
            items.Remove(item);
        }

        public Iterator getIterator()
        {
            return new ItemIterator(ref items);
        }

        private class ItemIterator : Iterator
        {
            private SortedSet<Item> items;
            private int index;
            private IEnumerator<Item> enumerator;

            public ItemIterator(ref SortedSet<Item> tree)
            {
                this.items = tree;
                index = 0;
                enumerator = items.GetEnumerator();
            }


            public int getIndex()
            {
                return index;
            }

            public object? getItem()
            {
                return enumerator.Current;
            }

            public bool hasNext()
            {
                if (enumerator.MoveNext()) return true;
                return false;
            }

            public object? next()
            {
                index++;
                if (this.hasNext()) return enumerator.Current;
                return null;
            }
        }
    }
}
