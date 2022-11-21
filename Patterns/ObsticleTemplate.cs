using csgame_backend.Data.Entities;

namespace csgame_backend.Patterns
{
    public abstract class ObstacleTemplate
    {
        public Obstacle TemplateMethod(double position_x, double postion_y)
        {
            Obstacle obstacle = this.BuildBaseObstacle();
            obstacle = this.SetCornerPoints(obstacle);
            obstacle = this.OptionalHook(obstacle);
            obstacle = this.SetPosition(obstacle, position_x, postion_y);
            return obstacle;
        }

        // base stuff all obstacles have
        protected Obstacle BuildBaseObstacle()
        {
            return new Obstacle();
        }

        protected Obstacle SetPosition(Obstacle obs, double x, double y)
        {
            obs.PositionX = x;
            obs.PositionY = y;

            return obs;
        }

        protected abstract Obstacle SetCornerPoints(Obstacle obs);

        protected virtual Obstacle OptionalHook(Obstacle obs)
        {
            return obs;
        } 
    }

    sealed class ObstacleWithDoorwayHorizontal : ObstacleTemplate
    {
        protected override Obstacle OptionalHook(Obstacle obs)
        {
            obs.collision.gap = 30; // px size

            return obs;
        }

        protected override Obstacle SetCornerPoints(Obstacle obs)
        {
            // Column 1
            obs.collision.wallsList.Add(new Tuple<double, double>(25, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 25));
            obs.collision.wallsList.Add(new Tuple<double, double>(25, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 25));

            // Column 2
            obs.collision.wallsList.Add(new Tuple<double, double>(25, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 25));
            obs.collision.wallsList.Add(new Tuple<double, double>(25, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 25));


            return obs;
        }

        
    }

    sealed class ObstacleWallHorizontal : ObstacleTemplate
    {
        protected override Obstacle SetCornerPoints(Obstacle obs)
        {
            obs.collision.wallsList.Add(new Tuple<double, double>(80, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(80, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 20));

            return obs;
        }
    }

    sealed class ObstacleWallVertical : ObstacleTemplate
    {
        protected override Obstacle SetCornerPoints(Obstacle obs)
        {
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(80, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(20, 20));
            obs.collision.wallsList.Add(new Tuple<double, double>(80, 20));

            return obs;
        }
    }

    class TemplatedBuilder
    {
        public static Obstacle Build(ObstacleTemplate template, double position_x, double position_y)
        {
            return template.TemplateMethod(position_x, position_y);
        }
    }
}
