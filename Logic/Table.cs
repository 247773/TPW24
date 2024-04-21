using Data;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        private DataAbstractAPI _dataLayer;
        public int _length { get; set; }
        public int _width { get; set; }
        public List<IBall> _balls { get; set; }
        public List<Task> _tasks { get; set; }

        private bool _stopTasks;

        public Table(int length, int width, DataAbstractAPI data)
        {
            _length = length;
            _width = width;
            _tasks = new List<Task>();
            _balls = new List<IBall>();
            _dataLayer = data;

        }

        public override void CreateBalls(int numOfBalls, int r)
        {
            Random random = new Random();
            for (int i = 0; i < numOfBalls; i++)
            {
                int x = random.Next(r, _length - r);
                int y = random.Next(r, _width - r);
                IBall ball = IBall.CreateBall(x, y, r);
                _balls.Add(ball);
                _tasks.Add(new Task(() =>
                {
                    while (!_stopTasks)
                    {
                        ball.RandomVelocity(-7, 7);
                        if (ball.IsWithinBounds(_length, _width))
                        {
                            ball.MoveBall();
                            Thread.Sleep(50);
                        }
                    }
                }));
            }
        }

        public override void StartSimulation()
        {
            _stopTasks = false;

            foreach (Task task in _tasks)
            {
                task.Start();
            }
        }

        public override void ClearTable()
        {
            _stopTasks = true;

            foreach (Task task in _tasks)
            {
                if (task.IsCompleted)
                {
                    task.Dispose();
                }
            }
            _tasks.Clear();
            _balls.Clear();
        }

        public override List<List<int>> GetBallsPosition()
        {
            List<List<int>> positions = new List<List<int>>();
            foreach (Ball b in _balls)
            {
                List<int> BallPosition = new List<int>
                {
                    b._X,
                    b._Y
                };
                positions.Add(BallPosition);
            }
            return positions;
        }

        public override List<IBall> GetBalls()
        {
            return _balls;
        }
    }
}
