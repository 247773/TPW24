using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Model
{
    public abstract class IModelBall
    {
        public static IModelBall CreateModelBall(int x, int y, int r)
        {
            return new ModelBall(x, y, r);
        }

        public abstract int PositionX { get; set; }
        public abstract int PositionY { get; set; }
        public abstract int Radius { get; set; }

        public abstract void UpdateModelBall(Object s, PropertyChangedEventArgs e);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
