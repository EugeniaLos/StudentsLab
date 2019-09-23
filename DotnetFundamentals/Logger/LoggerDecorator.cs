using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    class LoggerDecorator: ILogger
    {
        protected ILogger Wrapee;

        public LoggerDecorator(ILogger source)
        {
            Wrapee = source;
        }

        public void Error(string message)
        {
            Wrapee.Error(message);
        }

        public void Error(Exception ex)
        {
            Wrapee.Error(ex);
        }

        public void Warning(string message)
        {
            Wrapee.Warning(message);
        }

        public void Info(string message)
        {
            Wrapee.Info(message);
        }
    }
}
