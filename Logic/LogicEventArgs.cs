using System.Numerics;

namespace Logic
{
    public class LogicEventArgs
    {
        public Vector2 ballPosition;
        public LogicEventArgs(Vector2 position)
        {
            ballPosition = position;
        }
    }
}
