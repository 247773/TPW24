using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using Logic;

namespace Model
{
    internal class ModelBall : IModelBall, INotifyPropertyChanged
    {
        public override int PositionX { get => _PositionX; set { _PositionX = value; RaisePropertyChanged(); } }
        public override int PositionY { get => _PositionY; set { _PositionY = value; RaisePropertyChanged(); } }

        public override int Radius { get => _Radius; set { _Radius = value; RaisePropertyChanged(); } }

        private int _PositionX { get; set; }
        private int _PositionY { get; set; }
        private int _Radius { get; set; }

        public ModelBall(int x, int y, int r)
        {
            _PositionX = x;
            _PositionY = y;
            _Radius = r;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateModelBall(Object s, PropertyChangedEventArgs e)
        {
            IBall ball = (IBall)s;
            if (e.PropertyName == "X")
            {
                PositionX = ball.X;
            }
            else if (e.PropertyName == "Y")
            {
                PositionY = ball.Y;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}