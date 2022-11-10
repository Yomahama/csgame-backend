using csgame_backend.Data.Entities;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace csgame_backend.Helpers
{
    public class TestHelpers
    {
        Player? player;
        Player? clone;
        Player? deep_clone;
        public void testPrototype()
        {
            player = new Player("Test", 264, -590);

            clone = player.Clone();

            deep_clone = player.DeepClone();
            Console.WriteLine();
        }
    }
}
