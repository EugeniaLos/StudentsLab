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

        public static ThreadSafetySingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            return new ThreadSafetySingleton();
                        } 
                    }
                }
                return _instance;
            }
        }

    }
}
