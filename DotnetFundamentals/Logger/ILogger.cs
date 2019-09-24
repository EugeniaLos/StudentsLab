﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public interface ILogger
    {
        void Error(string message);
        void Error(Exception ex);
        void Warning(string message);
        void Info(string message);
    }
}
