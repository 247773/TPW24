using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private int _X;
        private int _Y;
        private int _R;
        private int _Vx;
        private int _Vy;
        public override event PropertyChangedEventHandler? PropertyChanged;
       
        public override int X
        {
            get => _X;
            set { _X = value; RaisePropertyChanged(); }
        }

        public override int Y
        {
            get => _Y;
            set { _Y = value; RaisePropertyChanged(); }
        }

        public override int R
        {
            get => _R;
            set { _R = value; RaisePropertyChanged(); }
        }

        public override int Vx
        {
            get => _Vx;
            set { _Vx = value; }
        }
        public override int Vy
        {
            get => _Vy;
            set { _Vy = value; }
        }
        public int _TempVx { get; set; }
        public int _TempVy { get; set; }
        public override bool BouncedBack { get; set; }

        internal Ball(int x, int y, int r)
        {
            this._X = x;
            this._Y = y;
            this.R = r;
            this.BouncedBack = false;
        }

        public override void MoveBall()
        {
            this._X += this.Vx;
            this._Y += this.Vy;
        }

        public override void CheckTableCollision(int length, int width)
        {
            if (this._X + this.Vx - this.R < 0 || this._X + this.Vx + this.R > length)
            {
                this.Vx = -this.Vx;
            }

            if (this._Y + this.Vy - this.R < 0 || this._Y + this.Vy + this.R > width)
            {
                this.Vy = -this.Vy;
            }
        }

        // chyba bedziemy pozniej przekazywac tu 2 kule i seterem ustawiac ich predkosci
        // sprawdz czy dobrze matme tu zrobilem
        public override void CheckBallCollision(IBall otherBall)
        {
            double myV = Math.Sqrt(this.Vx * this.Vx + this.Vy * this.Vy);
            double otherV = Math.Sqrt(otherBall.Vx * otherBall.Vx + otherBall.Vy * otherBall.Vy);

            double contactAngle = Math.Atan(Math.Abs(this._Y - otherBall.Y / this._X - otherBall.X));

            double myMovementAngle = Math.Atan(this.Vy / this.Vx);
            double otherMovementAngle = Math.Atan(otherBall.Vy / otherBall.Vx);

            double VxNumerator = (myV * Math.Cos(myMovementAngle - contactAngle) + 2 * otherV * Math.Cos(otherMovementAngle - contactAngle) * Math.Cos(contactAngle));
            double addToVx = myV * Math.Sin(myMovementAngle - contactAngle) * Math.Cos(contactAngle + Math.PI / 2f);

            double VyNumerator = (myV * Math.Cos(myMovementAngle - contactAngle) + 2 * otherV * Math.Cos(otherMovementAngle - contactAngle) * Math.Sin(contactAngle));
            double addToVy = myV * Math.Sin(myMovementAngle - contactAngle) * Math.Sin(contactAngle + Math.PI / 2f);

            this.Vx = (int)(VxNumerator + addToVx);
            this.Vy = (int)(VyNumerator + addToVy);
            if (this.Vy == 0)
            {
                this.Vy = 1;
            }
        }

        public override void UseTempSpeed()
        {
            this.Vx = this._TempVx;
            this.Vy = this._TempVy;
        }

        // nie wiem czy to tez powinno byc w API
        public void UpdateBall(Object s, PropertyChangedEventArgs e)
        {
            IDataBall ball = (IDataBall)s;
            this._X = ball.X;
            this._Y = ball.Y;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

