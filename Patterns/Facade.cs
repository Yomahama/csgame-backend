using csgame_backend.Data.Entities;
using csgame_backend.Data.Resources;

namespace csgame_backend.Patterns
{
    public class Facade
    {
        private IBuilder _Builder;
        private Adapter _Adapter;
        public IStrategy _IStrategy;
        public IPrototype<Player> _IPrototype;
        public Facade()
        {
            _Adapter = new Adapter();
            _Builder = new Builder();

        }

        public void Trajectory(double x, double y, out double m, out double b)
        {
            _Adapter.Trajectory(x, y, out m, out b);
        }
        public bool CheckForHit(Object obj, Object obj_1, out double? x_target, out double? y_target, double? m = null, double? b = null, Collision_Type collision_Type = Collision_Type.SQUARE)
        {
            return _IStrategy.CheckForHit(obj, obj_1, out x_target, out y_target, m = null, b = null, collision_Type = Collision_Type.SQUARE);
        }
        public Player? Clone()
        {
           return _IPrototype.Clone();
        }
        public void Reset()
        {
            _Builder.Reset();
        }
        public Player Build()
        {
            return _Builder.Build();
        }
        public void AddGunPistol()
        {
            _Builder.AddGunPistol();
        }
    }
}
