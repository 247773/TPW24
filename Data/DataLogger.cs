using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace Data
{
    internal class DataLogger
    {
        private ConcurrentQueue<LogBall> _ballsConcurrentQueue;
        private JArray _logArray;
        private string _pathToFile;
        private readonly object _writeLock = new object();
        private readonly object _queueLock = new object();
        private readonly int _queueSize = 100;
        private bool queueOverflow;

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
            _ballsConcurrentQueue = new ConcurrentQueue<LogBall>();
            queueOverflow = false;

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

            Task.Run(CollectData);
        }

        public void AddBall(LogBall logBall)
        {
            lock (_queueLock)
            {
                if (_ballsConcurrentQueue.Count < _queueSize)
                {
                    _ballsConcurrentQueue.Enqueue(logBall);
                }
                else
                {
                    queueOverflow = true;
                }
            }
        }

        private void CollectData()
        {
            while (true)
            {
                if (!_ballsConcurrentQueue.IsEmpty)
                {
                    while (_ballsConcurrentQueue.TryDequeue(out LogBall logBall))
                    {
                        JObject log = new JObject
                        {
                            ["Position"] = JObject.FromObject(logBall.Position),
                            ["Time"] = logBall.Time.ToString("o"),
                            ["Ball ID"] = logBall.ID
                        };

                        _logArray.Add(log);

                        if (queueOverflow)
                        {
                            JObject errorMessage = new JObject
                            {
                                ["Error"] = "Queue overflow - ball not added",
                                ["Time"] = DateTime.Now.ToString("o")
                            };
                            
                            _logArray.Add(errorMessage);
                            
                            lock (_queueLock)
                            {
                                queueOverflow = false;
                            }
                        }
                    }

                    if (_logArray.Count > _queueSize / 2)
                    {
                        SaveData();
                    }

                    // tu coś powinno być?

                }
            }
        }

        private void SaveData()
        {
            string diagnosticData = JsonConvert.SerializeObject(_logArray, Formatting.Indented);

            lock (_writeLock)
            {
                File.WriteAllText(_pathToFile, diagnosticData, Encoding.UTF8);
            }
        }
    }
}
