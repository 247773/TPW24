using Data;
using System.ComponentModel;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Radius { get; set; }
        public List<IBall> Balls { get; set; }
        public List<Task> Tasks { get; set; }

        private bool stopTasks;
        internal object locker = new object();

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
            Radius = r;
            for (int i = 0; i < n; i++)
            {
                Random random = new Random();
                int x = random.Next(r, Length - r);
                int y = random.Next(r, Width - r);
                int m = 5;
                int vX = random.Next(-5, 5);
                int vY = random.Next(-5, 5);
                IDataBall dataBall = dataAPI.CreateDataBall(x, y, Radius, m, vX, vY);
                Ball ball = new Ball(dataBall.X, dataBall.Y, Radius);
                dataBall.PropertyChanged += ball.UpdateBall;
                dataBall.PropertyChanged += CheckWallCollision;
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

        //private void CheckBallCollision(IBall me)
        //{
        //    foreach (IBall ball in Balls)
        //    {
        //        if (!ball.Equals(me))
        //        {
        //            // TODO zmienic na odleglosc euklidesowa, poki co jest kwadratowa
        //            if (Math.Abs(ball.X - me.X) < me.R + ball.R && Math.Abs(ball.Y - me.Y) < me.R + ball.R)
        //            {
        //                Monitor.Enter(ball);
        //                Monitor.Enter(me);
        //                try
        //                {
        //                    ball.CheckBallCollision(me);
        //                    me.CheckBallCollision(ball);
        //                    ball.UseTempSpeed();
        //                    me.UseTempSpeed();
        //                    ball.MoveBall();
        //                    me.MoveBall();
        //                }
        //                finally { Monitor.Exit(ball); Monitor.Exit(me); }
        //            }
        //            return;
        //        }
        //    }
        //}

        public override void ClearTable()
        {
            stopTasks = true;
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
