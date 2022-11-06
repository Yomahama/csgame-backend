using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csgame_backend.Data.Entities;
using csgame_backend.player_websocket;

namespace csgame_backend.csgame_backend.Patterns
{
    public abstract class Command
    {
        //Receiver
        protected Player player;

        public Command(Player player)
        {
            this.player = player;
        }
        public abstract void Execute(float damage);
        public abstract void UnExecute(float damage);
    }

    public class TakeDamage : Command
    {

        public TakeDamage(Player player) :
            base(player)
        {
        }
        public override void Execute(float damage)
        {
            player.TakeDamage(damage);
        }
        public override void UnExecute(float damage)
        {
            player.UndoDamage(damage);
        }
    }
    public class Shoot
    {
        Command command;
        public List<float> damageDealt = new List<float>();
        public void ExecuteCommand(float damage)
        {
            command.Execute(damage);
            damageDealt.Add(damage);
        }
        public void UnExecuteCommand()
        {
            command.UnExecute(damageDealt.Last());
            damageDealt.RemoveAt(damageDealt.Count - 1);
        }
    }
}
