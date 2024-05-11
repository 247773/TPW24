using Data;
using System.ComponentModel;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        public int Length { get; set; }
        public int Width { get; set; }
        
        private int _ballRadius { get; set; }
        
        public List<IBall> Balls { get; set; }
        public List<Task> Tasks { get; set; }

        private Object _locker = new Object();
        private Barrier _barrier1;
        private Barrier _barrier2;

        public IDataTable dataAPI;

        public Table(int length, int width)
        {
            this.Length = length;
            this.Width = width;
            Tasks = new List<Task>();
            Balls = new List<IBall>();
            dataAPI = IDataTable.CreateAPI(length, width);
        }

        public override List<IBall> GetBalls()
        {
            return Balls;
        }

        
        public override void CreateBalls(int n, int r)
        {
            _ballRadius = r;
            _barrier1 = new Barrier(n);
            _barrier2 = new Barrier(n);

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = random.Next(r, Length - r);
                int y = random.Next(r, Width - r);
                int m = random.Next(3, 3);
                int vX;
                do
                {
                    vX = random.Next(-3, 3);
                } while (vX == 0);
                int vY;
                do
                {
                    vY = random.Next(-3, 3);
                } while (vY == 0);
          
                IDataBall dataBall = dataAPI.CreateDataBall(x, y, _ballRadius, m, vX, vY);
                Ball ball = new Ball(dataBall.X, dataBall.Y, _ballRadius);

                dataBall.PropertyChanged += ball.UpdateBall;
                dataBall.PropertyChanged += CheckWallCollision;
                dataBall.PropertyChanged += CheckBallsCollision;
                Balls.Add(ball);
            }
        }

        private void CheckWallCollision(Object s, PropertyChangedEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            if (ball.X + ball.Vx + ball.R > dataAPI.Length || ball.X + ball.Vx - ball.R < 0)
            {
                ball.Vx = -ball.Vx;
            }
            if (ball.Y + ball.Vy + ball.R > dataAPI.Width || ball.Y + ball.Vy - ball.R < 0)
            {
                ball.Vy = -ball.Vy;
            }
        }

        private void CheckBallsCollision(Object s, PropertyChangedEventArgs e)
        {
            IDataBall me = (IDataBall)s;
            foreach (IDataBall ball in dataAPI.GetBalls())
            {
                if (ball != me)
                {
                    if (Math.Sqrt(Math.Pow(ball.X - me.X, 2) + Math.Pow(ball.Y - me.Y, 2)) <= me.R / 2 + ball.R / 2)
                    {
                        lock (me)
                        {
                            BallsCollision(me, ball);
                        }
                    }
                }
            }   
        }

        private void BallsCollision(IDataBall ball, IDataBall otherBall)
        {
            if (Math.Sqrt(Math.Pow(ball.X + ball.Vx - otherBall.X - otherBall.Vx, 2) + Math.Pow(ball.Y + ball.Vy - otherBall.Y - otherBall.Vy, 2)) <= otherBall.R/2 + ball.R/2)
            {
                double weight = 1d;

                double newXMovement = (2d * weight * ball.Vx) / (2d * weight);
                ball.Vx = (2d * weight * otherBall.Vx) / (2d * weight);
                otherBall.Vx = newXMovement;

                double newYMovement = (2 * weight * ball.Vy) / (2d * weight);
                ball.Vy = (2d * weight * otherBall.Vy) / (2d * weight);
                otherBall.Vy = newYMovement;
            }
        }

        public override void ClearTable()
        {
            bool _isEveryTaskCompleted = false;

            while (!_isEveryTaskCompleted)
            {
                _isEveryTaskCompleted = true;
                foreach (Task task in Tasks)
                {
                    if (!task.IsCompleted)
                    {
                        _isEveryTaskCompleted = false;
                        break;
                    }
                }
            }

            foreach (Task task in Tasks)
            {
                task.Dispose();
            }
            Balls.Clear();
            Tasks.Clear();
        }
    }
}
