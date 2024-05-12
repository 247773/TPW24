using Data;
using System.Numerics;

namespace Logic
{
    internal class Ball : IBall
    {
        private Vector2 _position;

        public override Vector2 Position { get => _position; }

        public override event EventHandler<LogicEventArgs>? ChangedPosition;

        internal Ball(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void UpdateBall(Object s, DataEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            _position.X = ball.Position.X;
            _position.Y = ball.Position.Y;
            LogicEventArgs args = new LogicEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }
    }
}