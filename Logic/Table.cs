using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        private readonly DataAbstractAPI _dataLayer;
        public int _length { get; set; }
        public int _width { get; set; }
        public List<IBall> _balls { get; set; }
        public List<Task> _tasks { get; set; }

        private bool _stopTasks;

        public Table(int length, int width)
        {
            this._length = length;
            this._width = width;
            _tasks = new List<Task>();
            _balls = new List<IBall>();
            _dataLayer = DataAbstractAPI.CreateDataAPI();
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

        public override async void ClearTable()
        {
            _stopTasks = true;

            await Task.WhenAll(_tasks);

            foreach (Task task in _tasks)
            {
                task.Dispose();
            }
            _balls.Clear();
            _tasks.Clear();
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
