using csgame_backend.Data.Entities;
using csgame_backend.Patterns;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace csgame_backend.GameEnvironment_websocket
{
    public class GameEnvironment : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs e)
        {

            base.OnMessage(e);
        }
    }
}
