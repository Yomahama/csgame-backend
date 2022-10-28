using csgame_backend.Data.Resources;

namespace csgame_backend.Patterns.Strategies
{
    public class Context
    {
        private IStrategy strategy;

        public void setStrategy(IStrategy strategy)
        {
            Console.WriteLine(strategy.ToString());
            this.strategy = strategy;
        }

        public bool executeSrategy(Object obj, Object obj_1, out double? x_target,out double? y_target, double? m = null, double? b = null, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            bool result = this.strategy.CheckForHit(obj, obj_1, out x_target, out y_target, m, b, collision_Type);
            Console.WriteLine(this.strategy.ToString());
            return result;
        }
    }
}
