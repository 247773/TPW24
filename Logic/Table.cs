using Data;
using System.Numerics;
using static Data.IDataBall;

namespace Logic
{
    internal class Table : LogicAbstractAPI
    {
        private int _length;
        private int _width;

        private int _ballRadius { get; set; }
        public List<ILogicBall> Balls { get; set; }

        private Object _locker = new Object();

        public IDataTable dataAPI;

        public Table(IDataTable api)
        {
            _length = api.Length;
            _width = api.Width;
            Balls = new List<ILogicBall>();
            dataAPI = api;
        }

        public override void CreateBalls(int n, int r)
        {
            _ballRadius = r;
            Random random = new Random();
            for (int id = 0; id < n; id++)
            {
                int x = random.Next(r, _length - r);
                int y = random.Next(r, _width - r);
                int m = random.Next(3, 3);

                float vX;
                do
                {
                    vX = (float)random.NextDouble() / 3;
                } while (vX == 0);

                float vY;
                do
                {
                    vY = (float)random.NextDouble() / 3;
                } while (vY == 0);

                IDataBall dataBall = dataAPI.CreateDataBall(x, y, _ballRadius, m, vX, vY, id);
                LogicBall ball = new LogicBall(dataBall.Position.X, dataBall.Position.Y);

                dataBall.ChangedPosition += ball.UpdateBall;
                dataBall.ChangedPosition += CheckCollisionWithWall;
                dataBall.ChangedPosition += CheckBallsCollision;

                Balls.Add(ball);
            }
        }

        private void CheckCollisionWithWall(Object s, DataEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            if (ball.Position.X + ball.Velocity.X + _ballRadius > dataAPI.Length || ball.Position.X + ball.Velocity.X - _ballRadius < 0)
            {
                ball.Velocity = new BallVelocity(-ball.Velocity.X, ball.Velocity.Y);
            }
            if (ball.Position.Y + ball.Velocity.Y + _ballRadius > dataAPI.Width || ball.Position.Y + ball.Velocity.Y - _ballRadius < 0)
            {
                ball.Velocity = new BallVelocity(ball.Velocity.X, -ball.Velocity.Y);
            }
        }

        private void CheckBallsCollision(Object s, DataEventArgs e)
        {
            IDataBall me = (IDataBall)s;
            lock (_locker)
            {               
                foreach (IDataBall ball in dataAPI.GetBalls().ToArray())
                {
                    if (ball != me)
                    {
                        if (Math.Sqrt(Math.Pow(ball.Position.X - me.Position.X, 2) + Math.Pow(ball.Position.Y - me.Position.Y, 2)) <= 2 * _ballRadius / 2)
                        {
                            BallCollision(me, ball);
                        }
                    }
                }                
            }
        }

        private void BallCollision(IDataBall ball, IDataBall otherBall)
        {
            if (Math.Sqrt(Math.Pow(ball.Position.X + ball.Velocity.X - otherBall.Position.X - otherBall.Velocity.X, 2) + Math.Pow(ball.Position.Y + ball.Velocity.Y - otherBall.Position.Y - otherBall.Velocity.Y, 2)) <= _ballRadius / 2 + _ballRadius / 2)
            {
                float weight = 1f;

                float otherBallXMovement = (2f * weight * ball.Velocity.X) / (2f * weight);
                float ballXMovement = (2f * weight * otherBall.Velocity.X) / (2f * weight);

                float otherBallYMovement = (2f * weight * ball.Velocity.Y) / (2f * weight);
                float ballYMovement = (2f * weight * otherBall.Velocity.Y) / (2f * weight);


                ball.Velocity = new BallVelocity(ballXMovement, ballYMovement);
                otherBall.Velocity = new BallVelocity(otherBallXMovement, otherBallYMovement);
            }
        }

        public override void ClearTable()
        {
            foreach (IDataBall ball in dataAPI.GetBalls().ToArray())
            {
                ball.Dispose();
            }
            Balls.Clear();
            dataAPI.ClearTable();
        }

        public override List<ILogicBall> GetBalls()
        {
            return Balls;
        }
    }
}