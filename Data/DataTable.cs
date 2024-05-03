using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataTable : IDataTable
    {
        public override int Length { get; set; }
        public override int Width { get; set; }

        private List<IDataBall> _balls = new List<IDataBall>();

        internal DataTable(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public override List<IDataBall> GetBalls()
        {
            return _balls;
        }

        public override IDataBall CreateDataBall(int x, int y, int r, int m, int vX = 0, int vY = 0)
        {
            IDataBall ball = IDataBall.CreateDataBall(x, y, r, m, vX, vY);
            _balls.Add(ball);
            return ball;
        }

        public override void ClearTable()
        {
            _balls.Clear();
        }
    }
}
