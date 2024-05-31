namespace Data
{
    public abstract class IDataTable
    {
        public static IDataTable CreateAPI(int length = 580, int width = 420)
        {
            return new DataTable(length, width);
        }

        public abstract int Length { get; } // dluzsza krawedz stolu
        public abstract int Width { get; } // krotsza krawedz stolu

        public abstract List<IDataBall> GetBalls();
        public abstract IDataBall CreateDataBall(float x, float y, int r, int m, float vX , float vY, int id);
        public abstract void ClearTable();

    }
}
