/*
using Data;
using Logic;

namespace LogicTest
{
    internal class FakeDataApi : DataAbstractAPI
    {

    }

    public class Tests
    {
        private DataAbstractAPI _data;
        private LogicAbstractAPI _table;

        public void Init()
        {
            _data = FakeDataApi.CreateAPI();
            _table = LogicAbstractAPI.CreateAPI(_data);
        }

        [Test]
        public void TestMethod1()
        {
            Init();
            _table.CreateBalls(5, 10);
            Assert.AreEqual(5, _table.GetBalls().Count);
        }

        [Test]
        public void TestMethod2()
        {
            Init();
            _table.CreateBalls(5, 10);
            _table.StartSimulation();
            Assert.AreEqual(5, _table.GetBalls().Count);
        }

        [Test]
        public void TestMethod3()
        {
            Init();
            _table.CreateBalls(5, 10);
            _table.StartSimulation();
            _table.ClearTable();
            Assert.AreEqual(0, _table.GetBalls().Count);
        }
    }
}
