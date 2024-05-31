namespace Data
{
    internal class DataTable : IDataTable
    {
        private DataLoggerAPI _logger = DataLoggerAPI.CreateAPI();
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
            IDataBall ballData = IDataBall.CreateDataBall(x, y, r, m, vX, vY, _logger, id);
            _balls.Add(ballData);
            return ballData;
        }
    }
}
