using Core.CrossCuttingConcerns.Logging.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.IO;

namespace Core.CrossCuttingConcerns.Logging.Concrete
{
    public class FileLogger : ILogger
    {
        public readonly string logFile = "log.txt";

        private void CreateLogFileIfNotExists()
        {
            if (!File.Exists(logFile))
            {
                File.Create(logFile);
            }
        }

        public IResult Log(string data)
        {
            CreateLogFileIfNotExists();
            File.AppendAllTextAsync(logFile, data);
            return new SuccessResult();
        }
    }
}
