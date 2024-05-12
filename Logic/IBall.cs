using System.Numerics;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int x, int y)
        {
            return new Ball(x, y);
        }

        public abstract Vector2 Position { get; }

        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;
    }
}