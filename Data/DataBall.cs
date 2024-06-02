using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class DataBall : IDataBall, IDisposable
    {
        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        private Vector2 _velocity;
        public override BallPosition Position
        {
            get
            {
                lock (_positionLocker)
                {
                    return new BallPosition(_position.X, _position.Y);
                }
            }
        }

        public override BallVelocity Velocity
        {
            get
            {
                lock (_velocityLocker)
                {
                    return new BallVelocity(_velocity.X, _velocity.Y);
                }
            }
            set
            {
                lock (_velocityLocker)
                {
                    _velocity = new Vector2(value.X, value.Y);
                }
            }
        }

        public override int ID { get; }

        private DataLogger _logger;
        private bool _continueMoving;

        private Object _locker = new Object();
        private Object _positionLocker = new Object();
        private Object _velocityLocker = new Object();

        public DataBall(float x, float y, int r, int m, float vX, float vY, DataLogger logger, int id)
        {
            _position = new Vector2(x, y);
            _velocity = new Vector2(vX, vY);
            _continueMoving = true;
            Task.Run(StartSimulation);
            ID = id;
            _logger = logger;
        }

        private async void StartSimulation()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            float startTime = 0f;
            while (_continueMoving)
            {
                float currentTime = stopwatch.ElapsedMilliseconds;
                float elapsedTime = currentTime - startTime;
                if (elapsedTime >= 1f / 60f)
                {
                    MoveBall();
                    _logger.AddBall(this);
                    startTime = currentTime;
                    await Task.Delay((int)elapsedTime / 1000);
                }
            }
        }

        private void MoveBall()
        {
            lock (_locker)
            {
                _position += _velocity;
                ChangedPosition?.Invoke(this, new DataEventArgs(this.Position));
            }
        }

        public override void Dispose()
        {
            _continueMoving = false;
        }
    }
}