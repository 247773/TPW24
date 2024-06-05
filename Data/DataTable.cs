namespace Data
{
    internal class DataTable : IDataTable
    {
        public override int Length { get; }
        public override int Width { get; }

        private List<IDataBall> _balls = new List<IDataBall>();

        public DataTable(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public override List<IDataBall> GetBalls()
        {
            return _balls;
        }

        public override void ClearTable()
        {
            _balls.Clear();
        }

        public override IDataBall CreateDataBall(float x, float y, int r, int m, float vX, float vY, int id)
        {
            IDataBall ballData = new DataBall(x, y, r, m, vX, vY, id);
            _balls.Add(ballData);
            return ballData;
        }
    }
}
