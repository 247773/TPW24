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


        public Table(int length, int width)
        {
            this._length = length;
            this._width = width;
            this._balls = new List<Ball>();
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

        public override void CreateBalls(int numOfBalls, int radius)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                Random rand = new Random();
                int x = rand.Next(radius, _length - radius);
                int y = rand.Next(radius, _width - radius);
                _balls.Add(new Ball(x, y, radius));
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
    }
}

