using System.Numerics;

namespace Data
{
    internal class DataBall : IDataBall
    {
        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        private Vector2 _velocity;
        public override Vector2 Position
        {
            get
            {
                lock (_positionLocker)
                {
                    return _position;
                }
            }
        }

        public override Vector2 Velocity
        {
            get
            {
                lock (_velocityLocker)
                {
                    return _velocity;
                }
            }
            set
            {
                lock (_velocityLocker)
                {
                    _velocity = value;
                }
            }
        }

        public override bool HasCollided { get; set; }
        public override bool ContinueMoving { get; set; }

        private Object _locker = new Object();
        private Object _positionLocker = new Object();
        private Object _velocityLocker = new Object();

        public DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            _position = new Vector2(x, y);
            Velocity = new Vector2(vX, vY);
            ContinueMoving = true;
            Task.Run(StartSimulation);
            HasCollided = false;
        }

        private async void StartSimulation()
        {
            while (ContinueMoving)
            {
                MoveBall();
                HasCollided = false;
                await Task.Delay(10);
            }
        }

        private void MoveBall()
        {
            lock (_locker)
            {
                _position += Velocity;
                ChangedPosition?.Invoke(this, new DataEventArgs(this));
            }
        }
    }
}