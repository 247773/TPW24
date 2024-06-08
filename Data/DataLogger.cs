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
        private readonly object _writeLock = new object();
        private readonly object _queueLock = new object();
        private readonly int queueSize = 100;
        private Task _logerTask;

        private static DataLogger? _dataLogger = null;

        public static DataLogger getInstance()
        {
            _dataLogger ??= new DataLogger();
            return _dataLogger;
        }

        private DataLogger()
        {
            string tempPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName ?? string.Empty;
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
                Directory.CreateDirectory(loggersDir);
                File.Create(_pathToFile).Dispose();
            }
        }

        public void AddBall(LogBall ball)
        {
            lock (_queueLock)
            {
                if (_ballsConcurrentQueue.Count < queueSize)
                {
                    JObject log = new JObject
                    {
                        ["Position"] = JObject.FromObject(ball.Position),
                        ["Time"] = ball.Time.ToString("o"),
                        ["Ball ID"] = ball.ID
                    };

                    _ballsConcurrentQueue.Enqueue(log);
                    if (_logerTask == null || _logerTask.IsCompleted)
                    {
                        _logerTask = Task.Run(SaveDataToLog);
                    }
                }
                else
                {
                    JObject overflowLog = new JObject
                    {
                        ["Message"] = "Queue overflow - ball not added",
                        ["Time"] = DateTime.Now.ToString("o")
                    };
                    _logArray.Add(overflowLog);
                }
            }
        }

        public void AddTable(IDataTable table)
        {
            ClearLogFile();
            JObject log = JObject.FromObject(table);
            _logArray.Add(log);
            string diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);
            lock (_writeLock)
            {
                File.WriteAllText(_pathToFile, diagnosticData);
            }
        }

        private void SaveDataToLog()
        {
            while (_ballsConcurrentQueue.TryDequeue(out JObject ball))
            {
                _logArray.Add(ball);
            }
            string diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);

            lock (_writeLock)
            {
                File.WriteAllText(_pathToFile, diagnosticData);
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
