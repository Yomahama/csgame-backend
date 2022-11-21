using csgame_backend.Data.Entities;
using csgame_backend.Patterns;

namespace csgame_backend.Helpers
{
    public class Map
    {
        public double width { get; set; }
        public double height { get; set; }

        public List<Obstacle> obstacle { get; private set; }
        public List<Player> players { get; private set; }

        public Map(double width, double height, List<Obstacle> obstacle, List<Player> players)
        {
            this.width = width;
            this.height = height;
            this.obstacle = obstacle;
            this.players = players;
        }
    }


    public class MapCreator
    {
        double MapWidth { get; set; }
        double MapHeight { get; set; }

        public Map map { get; private set; }

        public MapCreator(double width = 500, double height = 500)
        {
            this.MapWidth = width;
            this.MapHeight = height;
        }

        public void createMap()
        {
            /* MAP walls
                           (30,10)      (110, 10)
                (10,10) |--||--|    |--||--|
                        |  ||--|    |--||  |
                        |--|            |--|
                (10,80) |--|            |--|
                        |  |            |  |
                        |--|            |--|
               (10,160) |--|            |--|
                        |  ||----------||  |
                        |--||----------||--|
             */

            List<Obstacle> obstacles = new List<Obstacle>();

            List<Player> players = new List<Player>();
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 10, 10));
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWithDoorwayHorizontal(), 30, 10));
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 110, 10));
            
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 10, 80));
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 110, 80));

            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 10, 160));
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallHorizontal(), 30, 220));
            obstacles.Add(TemplatedBuilder.Build(new ObstacleWallVertical(), 110, 160));

            this.map = new Map(MapWidth, MapHeight, obstacles, players);
        }
    }
}
