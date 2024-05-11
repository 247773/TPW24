using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _r;
        private int _vX;
        private int _vY;
        public override event PropertyChangedEventHandler? PropertyChanged;
       
        public override int X
        {
            get => _x;
            set { _x = value; RaisePropertyChanged(); }
        }

        public override int Y
        {
            get => _y;
            set { _y = value; RaisePropertyChanged(); }
        }

        public override int R
        {
            get => _r;
            set { _r = value; RaisePropertyChanged(); }
        }

        public override int Vx
        {
            get => _vX;
            set { _vX = value; }
        }
        public override int Vy
        {
            get => _vY;
            set { _vY = value; }
        }
        public int TempVx { get; set; }
        public int TempVy { get; set; }

        public override bool BouncedBack { get; set; }

        internal Ball(int x, int y, int r)
        {
            this._x = x;
            this._y = y;
            this.R = r;
            this.BouncedBack = false;
        }

        public override void MoveBall()
        {
            this._x += this.Vx;
            this._y += this.Vy;
        }

        public override void CheckTableCollision(int length, int width)
        {
            if (this.X + this.Vx - this.R < 0 || this.X + this.Vx + this.R > length)
            {
                this.Vx = -this.Vx;
            }

            if (this.Y + this.Vy - this.R < 0 || this.Y + this.Vy + this.R > width)
            {
                this.Vy = -this.Vy;
            }
        }

        // chyba bedziemy pozniej przekazywac tu 2 kule i seterem ustawiac ich predkosci
        // sprawdz czy dobrze matme tu zrobilem
        public override void CheckBallCollision(IBall otherBall)
        {
            double myMass = 1;
            double otherMass = 1;

            double myV = Math.Sqrt(this.Vx * this.Vx + this.Vy * this.Vy);
            double otherV = Math.Sqrt(otherBall.Vx * otherBall.Vx + otherBall.Vy * otherBall.Vy);

            double contactAngle = Math.Atan(Math.Abs(this.Y - otherBall.Y / this.X - otherBall.X));

            double myMovementAngle = Math.Atan(this.Vy / this.Vx);
            double otherMovementAngle = Math.Atan(otherBall.Vy / otherBall.Vx);

            double VxNumerator = (myV * Math.Cos(myMovementAngle - contactAngle) * (myMass - otherMass) + 2 * otherV * otherMass * Math.Cos(otherMovementAngle - contactAngle) * Math.Cos(contactAngle));
            double VxDenominator = myMass + otherMass;
            double addToVx = myV * Math.Sin(myMovementAngle - contactAngle) * Math.Cos(contactAngle + Math.PI / 2f);

            double VyNumerator = (myV * Math.Cos(myMovementAngle - contactAngle) * (myMass - otherMass) + 2 * otherV * otherMass * Math.Cos(otherMovementAngle - contactAngle) * Math.Sin(contactAngle));
            double addToVy = myV * Math.Sin(myMovementAngle - contactAngle) * Math.Sin(contactAngle + Math.PI / 2f);

            TempVx = (int)(VxNumerator / VxDenominator + addToVx);
            TempVy = (int)(VyNumerator / VxDenominator + addToVy);
            if (TempVy == 0)
            {
                TempVy = 1;
            }
        }

        public override void UseTempSpeed()
        {
            this.Vx = this.TempVx;
            this.Vy = this.TempVy;
        }

        public void UpdateBall(Object s, PropertyChangedEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            X = ball.X;
            Y = ball.Y;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

