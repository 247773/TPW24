namespace Logic
{
    internal class LogicAPI : LogicAbstractAPI
    {
        private Table _table;

        public override void CreateTable(int length, int width, int numOfBalls, int radius)
        {
            _table = new Table(length, width);
            _table.createBalls(numOfBalls, radius);
        }

        public override List<Ball> GetBalls()
        {
            return this._table.Balls;
        }
    }
}