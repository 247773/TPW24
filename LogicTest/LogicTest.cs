using Data;
using Logic;
using NUnit.Framework;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace LogicTest
{
    public class TableTests
    {

        LogicAbstractAPI board = LogicAbstractAPI.CreateAPI(new FakeDataAPI(500, 500));
        internal class FakeDataBall : IDataBall
        {
            private Vector2 _position;
            public override Vector2 Position { get => _position; }

            public override Vector2 Velocity { get; set; }
            public override bool HasCollided { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override bool ContinueMoving { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override event EventHandler<DataEventArgs> ChangedPosition;

            public override void MoveBall()
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


        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(board);
        }

        [Test]
        public void CreateBallsTest()
        {
            board.CreateBalls(3, 5);
            Assert.AreEqual(board.GetBalls().Count, 3);
        }
        [Test]
        public void ClearTableTest()
        {
            board.CreateBalls(3, 5);
            Assert.AreEqual(board.GetBalls().Count, 3);

            board.ClearTable();
            Assert.AreEqual(board.GetBalls().Count, 0);
        }
        [Test]
        public void ClearingEmptyBoardTest()
        {
            board.ClearTable();
            Assert.AreEqual(board.GetBalls().Count, 0);
        }
    }
}