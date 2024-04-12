using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Ball = Data.Ball;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        private int _length;
        private int _width;
        private List<Ball> _balls;
        private List<Task> _tasks;


        public Table(int length, int width)
        {
            this._length = length;
            this._width = width;
            this._balls = new List<Ball>();
            this._tasks = new List<Task>();
        }

        public int Length
        {
            get { return _length; }
        }

        public int Width
        {
            get { return _width; }
        }

        public List<Ball> Balls
        {
            get { return _balls; }
        }

        public List<Task> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public override void CreateBalls(int numOfBalls, int radius)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                _tasks.Add(Task.Run(() =>
                {
                    Random rand = new Random();
                    int x = rand.Next(radius, _length - radius);
                    int y = rand.Next(radius, _width - radius);
                    Ball newBall = new Ball(x, y, radius);
                    _balls.Add(newBall);

                    while (true)
                    {
                        newBall.RandomVelocity(-5, 5);
                        newBall.Move();
                        Thread.Sleep(10); // wstrzymanie wykonania biezacego watku na 10ms
                    }
                }));
            }
        }

        public override void UpdateBalls()
        {
            foreach (Ball ball in _balls)
            {
                ball.RandomVelocity(-5, 5);
                ball.Move();
            }
        }

        public override List<(int, int)> GetBallsPosition()
        {
            List<(int, int)> positions = new List<(int, int)>();
            foreach (Ball ball in _balls)
            {
                positions.Add((ball.X, ball.Y));
            }
            return positions;
        }

        public override void StartSimulation()
        {
            foreach (Task task in _tasks)
            {
                task.Start();
            }
        }
    }
}

