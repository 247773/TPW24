using Data;
using System.Numerics;

namespace Logic
{
    internal class LogicBall : ILogicBall
    {
        private Vector2 _position;

        public override Vector2 Position { get => _position; }

        public override event EventHandler<LogicEventArgs>? ChangedPosition;

        internal LogicBall(float x, float y)
        {
            _position = new Vector2(x, y);
        }

        public void UpdateBall(Object s, DataEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            _position = new Vector2(e.ballPosition.X, e.ballPosition.Y);
            ChangedPosition?.Invoke(this, new LogicEventArgs(this));
        }
    }
}