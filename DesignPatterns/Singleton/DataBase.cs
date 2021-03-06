﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Singleton
{
    public class DataBase
    {
        private static DataBase _instance;
        private DataBase()
        {

        }

        public static DataBase GetInstance()
        {
            return _instance ?? (_instance = new DataBase());
        }

        public void Connect()
        {

        }

        public void ExecuteQuery()
        {

        }

        public void CloseConnection()
        {

        }
    }
}
