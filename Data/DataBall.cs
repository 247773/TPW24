using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class DataBall : IDataBall
    {
        private int _X;
        private int _Y;

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

        public override int R { get; set; }
        public override int M { get; set; }
        public override int Vx { get; set; }
        public override int Vy { get; set; }

        internal DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            this._X = x;
            this._Y = y;
            this.R = r;
            this.M = m;
            this.Vx = vX;
            this.Vy = vY;
            Task task = Task.Run(StartSimulation);
        }

        public override void MoveBall()
        {
            X += Vx;
            Y += Vy;
        }

        public override void StartSimulation()
        {
            while (true)
            {
                MoveBall();
                Task.Delay(10).Wait();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
