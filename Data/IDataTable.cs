using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IDataTable
    {
        public static IDataTable CreateAPI(int length, int width)
        {
            return new DataTable(length, width);
        }

        public abstract int Length { get; set; }
        public abstract int Width { get; set; }

        public abstract List<IDataBall> GetBalls();
        public abstract IDataBall CreateDataBall(int x, int y, int r, int m, int vX = 0, int vY =0);
        public abstract void ClearTable();

    }
}
