using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns.Collections
{
    public class Players : IterableCollection
    {
        // List
        private List<Player> players;

        public Players()
        {
            players = new List<Player>();
        }

        public void addPlayer(Player player)
        {
            players.Add(player);
        }

        public void removePlayer(Player player)
        {
            players.Remove(player);
        }

        public Iterator getIterator()
        {
            return new PlayersIterator(ref players);
        }

        private class PlayersIterator : Iterator
        {
            private List<Player> players;
            private IEnumerator<Player> iter;
            int index;
            public PlayersIterator(ref List<Player> players)
            {
                this.players = players;
                iter = players.GetEnumerator();
                index = 0;
            }

            public int getIndex()
            {
                return index;
            }

            public object? getItem()
            {
                return iter.Current;
            }

            public bool hasNext()
            {
                if (iter.MoveNext()) return true; // next item
                return false;
            }
            public object? next()
            {
                index++;
                if (this.hasNext()) return iter.Current;
                return null;
            }
       }
    }
}
