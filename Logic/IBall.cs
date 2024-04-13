using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int x, int y, int r) => new Ball(x, y, r);

        public abstract int _x { get; set; }
        public abstract int _y { get; set; }
        public abstract int _r { get; set; }
        public abstract int _vx { get; set; }
        public abstract int _vy { get; set; }
        public abstract event PropertyChangedEventHandler? _propertyChanged;
    }
}
