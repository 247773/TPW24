using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class DataBall : IDataBall, IDisposable
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

        private bool _continueMoving;

        private Object _locker = new Object();
        private Object _positionLocker = new Object();
        private Object _velocityLocker = new Object();

        public DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            _position = new Vector2(x, y);
            _velocity = new Vector2(vX, vY);
            _continueMoving = true;
            Task.Run(StartSimulation);
        }

        private async void StartSimulation()
        {
            Stopwatch stopwatch = new Stopwatch();
            int movementTime = 10;
            while (_continueMoving)
            {
                stopwatch.Start();
                MoveBall();
                stopwatch.Stop();
                if (movementTime > (int)stopwatch.ElapsedMilliseconds)
                {
                    await Task.Delay(movementTime - (int)stopwatch.ElapsedMilliseconds);
                }
                stopwatch.Reset();
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

        public override void Dispose()
        {
            _continueMoving = false;
        }
    }
}