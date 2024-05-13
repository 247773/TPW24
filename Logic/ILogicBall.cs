using System.Numerics;

namespace Logic
{
    public abstract class ILogicBall
    {
        public static ILogicBall CreateBall(int x, int y)
        {
            return new LogicBall(x, y);
        }

        public abstract Vector2 Position { get; }

        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;
    }
}