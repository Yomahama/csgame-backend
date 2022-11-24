using csgame_backend.Data.Entities;
using System;

namespace csgame_backend.Patterns.Collections
{
    public class Obstacles : IterableCollection
    {
        private Obstacle[] obstacles;

        public void addObstacle(Obstacle obj)
        {
            Obstacle[] temp = new Obstacle[obstacles.Length + 1];
            for(Iterator iter = this.getIterator(); iter.hasNext();)
            {
                temp[iter.getIndex()] = (Obstacle) iter.getItem();
                iter.next();
            }

            temp[obstacles.Length] = obj;
            this.obstacles = temp;
        }

        public void removeObstacle(Obstacle obj)
        {
            Obstacle[] temp = new Obstacle[obstacles.Length - 1];
            bool flag = false;
            for(Iterator iter = this.getIterator(); iter.hasNext();)
            {
                if(iter.getItem() == obj)
                {
                    flag = true;
                }

                if (flag)
                {
                    temp[iter.getIndex()] = (Obstacle) iter.next();
                }
                else
                {
                    temp[iter.getIndex()] = (Obstacle)iter.getItem();
                    iter.next();
                }
            }

            this.obstacles = temp;
        }

        public Obstacles(Obstacle[] array)
        {
            this.obstacles = array;
        }

        public Iterator getIterator()
        {
            return new ObstaclesIterator(ref obstacles);
        }


        private class ObstaclesIterator : Iterator
        {
            Obstacle[] array;

            public ObstaclesIterator(ref Obstacle[] array)
            {
                this.array = array;
                index = 0;
            }

            int index;
            public bool hasNext()
            {
                if(index < array.Length)
                    return true;
                return false;
            }

            public object? next()
            {
                if(this.hasNext())
                    return array[index++];
                return null;
            }

            public int getIndex()
            {
                return this.index;
            }

            public object? getItem()
            {
                return array[index];
            }
        }
    }
}
