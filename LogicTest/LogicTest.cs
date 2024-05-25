using Data;
using Logic;
using System.Numerics;

namespace LogicTest
{
    internal class FakeDataBall : IDataBall
    {
        private Vector2 _position;
        public override Vector2 Position { get => _position; }

        public override Vector2 Velocity { get; set; }

        public override event EventHandler<DataEventArgs> ChangedPosition;

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal class FakeDataAPI : IDataTable
    {
        public FakeDataAPI(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public override int Length { get; }

        public override int Width { get; }

        public override IDataBall CreateDataBall(int x, int y, int r, int m, int vX, int vY)
        {
            return new FakeDataBall();
        }

        public override List<IDataBall> GetBalls()
        {
            return new List<IDataBall>();
        }

        public override void ClearTable()
        {
            return;
        }
    }

    public class Tests
    {
        private IDataTable _data;
        private LogicAbstractAPI _table;

        [SetUp]
        public void Init()
        {
            _data = new FakeDataAPI(500, 500);
            _table = LogicAbstractAPI.CreateAPI(_data);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_table);
        }

        [Test]
        public void CreateBallsTest()
        {
            _table.CreateBalls(3, 5);
            Assert.AreEqual(_table.GetBalls().Count, 3);
        }

        [Test]
        public void ClearTableTest()
        {
            _table.CreateBalls(3, 5);
            Assert.AreEqual(_table.GetBalls().Count, 3);

            _table.ClearTable();
            Assert.AreEqual(_table.GetBalls().Count, 0);
        }

        [Test]
        public void ClearingEmptyTableTest()
        {
            _table.ClearTable();
            Assert.AreEqual(_table.GetBalls().Count, 0);
        }
    } 
}