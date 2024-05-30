namespace Data
{
    public abstract class DataLoggerAPI
    {
        public abstract void AddBall(IDataBall ball);
        public abstract void AddTable(IDataTable table);
        public static DataLoggerAPI CreateAPI()
        {
            return new DataLogger();
        }
    }
}
