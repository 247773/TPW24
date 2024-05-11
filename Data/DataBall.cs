using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class DataBall : IDataBall
    {
        private double _x;
        private double _y;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override double X
        {
            get => _x;
            set { _x = value; RaisePropertyChanged(); }
        }

        public override double Y
        {
            get => _y;
            set { _y = value; RaisePropertyChanged(); }
        }

        public override int R { get; set; }
        public override int M { get; set; }
        public override double Vx { get; set; }
        public override double Vy { get; set; }
        public override double TempVx { get; set; }
        public override double TempVy { get; set; }
        public override bool IsMoved { get; set; }

        private Object _locker = new Object();

        public DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            _x = x;
            _y = y;
            R = r;
            M = m;
            Vx = vX;
            Vy = vY;
            Task.Run(StartSimulation);
            IsMoved = false;
        }

        public override void MoveBall()
        {
            X += Vx;
            Y += Vy;
        }

        public void StartSimulation()
        {
            while (true)
            {
                lock (this)
                {
                    if (!IsMoved)
                    {
                        MoveBall();
                    }
                }
                Task.Delay(10).Wait();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
