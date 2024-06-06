using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data
{
    internal class DataLogger
    {
        private ConcurrentQueue<JObject> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private Mutex _writeMutex = new Mutex();
        private Mutex _queueMutex = new Mutex();
        private readonly int queueSize = 10000;
        private Task _logerTask;

        private static DataLogger? _dataLogger = null;

        public static DataLogger getInstance()
        {
            _dataLogger ??= new DataLogger();
            return _dataLogger;
        }

        private DataLogger()
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string loggersDir = Path.Combine(tempPath, "Loggers");
            _pathToFile = Path.Combine(loggersDir, "logs.json");
            _ballsConcurrentQueue = new ConcurrentQueue<JObject>();

            if (File.Exists(_pathToFile))
            {
                try
                {
                    string input = File.ReadAllText(_pathToFile);
                    _logArray = JArray.Parse(input);
                }
                catch (JsonReaderException)
                {
                    _logArray = new JArray();
                }
            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_pathToFile);
                file.Close();
            }
        }

        public void AddBall(LogBall ball)
        {
            _queueMutex.WaitOne();
            try
            {

                if (_ballsConcurrentQueue.Count < queueSize)
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
                else
                {
                    JObject overflowLog = new JObject();
                    overflowLog["Message"] = "Queue overflow - ball not added";
                    overflowLog["Time"] = DateTime.Now.ToString("HH:mm:ss");
                    _logArray.Add(overflowLog);
                }
            }
            finally
            {
                _queueMutex.ReleaseMutex();
            }
        }

        public void AddTable(IDataTable table)
        {
            ClearLogFile();
            JObject log = JObject.FromObject(table);
            _logArray.Add(log);
            String diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
            _writeMutex.WaitOne();
            try
            {
                File.WriteAllText(_pathToFile, diagnosticData);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }

        private void SaveDataToLog()
        {
            while (_ballsConcurrentQueue.TryDequeue(out JObject ball))
            {
                _logArray.Add(ball);
            }
            String diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
            
            _writeMutex.WaitOne(); 
            try
            {
                File.WriteAllText(_pathToFile, diagnosticData, System.Text.Encoding.UTF8);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }

        private void ClearLogFile()
        {
            _writeMutex.WaitOne();
            try
            {
                _logArray.Clear();
                File.WriteAllText(_pathToFile, string.Empty, System.Text.Encoding.UTF8);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }
    }
}