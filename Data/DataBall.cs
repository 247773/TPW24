using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class DataBall : IDataBall, IDisposable
    {
        private readonly DataLogger _logger = DataLogger.getInstance();

        public override event EventHandler<DataEventArgs>? ChangedPosition;

        private Vector2 _position;
        private Vector2 _velocity;
        private float _elapsedTime;
        private const float TIME_INTERVAL = 1f / 60f; // 60 FPS

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

        public override float Time
        {
            get => _elapsedTime;
            set => _elapsedTime = value;
        }

        private bool _continueMoving;

        private Object _locker = new Object();
        private Object _positionLocker = new Object();
        private Object _velocityLocker = new Object();

        public DataBall(float x, float y, int r, int m, float vX, float vY, int id)
        {
            _position = new Vector2(x, y);
            _velocity = new Vector2(vX, vY);
            _continueMoving = true;
            Task.Run(StartSimulation);
            ID = id;
        }

        private async void StartSimulation()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long previousTime = stopwatch.ElapsedMilliseconds;
            while (_continueMoving)
            {
                long currentTime = stopwatch.ElapsedMilliseconds;
                float elapsedTime = (currentTime - previousTime);
                if (elapsedTime >= TIME_INTERVAL)
                {
                    Time = elapsedTime;
                    MoveBall(elapsedTime);
                    _logger.AddBall(new LogBall(Position, Velocity, DateTime.Now, ID));
                    previousTime = currentTime;
                }
                await Task.Delay(TimeSpan.FromSeconds(TIME_INTERVAL));
            }
        }

        private void MoveBall(float elapsedTime)
        {
            lock (_locker)
            {
                _position += _velocity * elapsedTime;
                ChangedPosition?.Invoke(this, new DataEventArgs(this.Position));
            }
        }

        public override void Dispose()
        {
            _continueMoving = false;
        }
    }
}
