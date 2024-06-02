using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data
{
    internal class DataLogger
    {
        private ConcurrentQueue<JObject> _ballsConcurrentQueue = new ConcurrentQueue<JObject>();
        private JArray _logArray;
        private string _pathToFile;
        private object _writeLock = new object();
        private object _queueLock = new object();
        private Task _logerTask;

        internal DataLogger()
        {
            _pathToFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, "Loggers", "logs.json");

            if (File.Exists(_pathToFile))
            {
                string input = File.ReadAllText(_pathToFile);
                _logArray = JArray.Parse(input);
            }
            else
            {
                _logArray = new JArray();
                File.Create(_pathToFile).Close();
            }
        }

        public void AddBall(IDataBall ball)
        {
            lock (_queueLock)
            {
                JObject log = JObject.FromObject(ball.Position);
                log["Time: "] = DateTime.Now.ToString("HH:mm:ss");
                log.Add("Ball ID", ball.ID);

                _ballsConcurrentQueue.Enqueue(log);
                if (_logerTask == null || _logerTask.IsCompleted)
                {
                    _logerTask = Task.Run(SaveDataToLog);
                }
            }
        }

        public void AddTable(IDataTable table)
        {
            ClearLogFile();
            JObject log = JObject.FromObject(table);
            _logArray.Add(log);
            SaveDataToLog();
        }

        private void SaveDataToLog()
        {
            while (_ballsConcurrentQueue.TryDequeue(out JObject ball))
            {
                _logArray.Add(ball);
            }
            lock (_writeLock)
            {
                File.WriteAllText(_pathToFile, JsonConvert.SerializeObject(_logArray, Formatting.Indented));
            }
        }

        private void ClearLogFile()
        {
            lock (_writeLock)
            {
                _logArray.Clear();
                File.WriteAllText(_pathToFile, string.Empty);
            }
        }
    }
}
