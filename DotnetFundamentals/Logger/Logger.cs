using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    class Logger: ILogger
    {
        private ILogger loger;

        public Logger(ILogger loger)
        {
            this.loger = loger;
        }

        public Logger()
        {
            this.loger = new ConsoleLogger();
        }

        public void Error(string message)
        {
            loger.Error(message);
        }
        public void Error(Exception ex)
        {
            loger.Error(ex);
        }
        public void Warning(string message)
        {
            loger.Warning(message);
        }
        public void Info(string message)
        {
            loger.Info(message);
        }
    }
}
