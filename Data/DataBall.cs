using System.Numerics;

namespace Data
{
    internal class DataBall : IDataBall
    {
        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        public override Vector2 Position
        {
            get => _position;
        }

        public override Vector2 Velocity { get; set; }

        public override bool HasCollided { get; set; }
        public override bool ContinueMoving { get; set; }

        public DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            _position = new Vector2(x, y);
            Velocity = new Vector2(vX, vY);
            ContinueMoving = true;
            Task.Run(StartSimulation);
            HasCollided = false;
        }

        public async void StartSimulation()
        {
            while (ContinueMoving)
            {
                MoveBall();
                HasCollided = false;
                await Task.Delay(10);
            }
        }

        public override void MoveBall()
        {
            _position += Velocity;
            ChangedPosition?.Invoke(this, new DataEventArgs(this));
        }
    }
}