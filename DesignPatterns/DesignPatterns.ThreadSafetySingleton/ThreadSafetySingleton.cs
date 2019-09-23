using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.ThreadSafetySingleton
{
    public class ThreadSafetySingleton
    {
        private static ThreadSafetySingleton _instance;
        private static readonly object _locker = new object();

        private ThreadSafetySingleton()
        {

        }

        public static ThreadSafetySingleton GetInstance
        {
            get
            {
                lock (_locker)
                {
                    return _instance ?? (_instance = new ThreadSafetySingleton());
                }
            }
        }

    }
}
