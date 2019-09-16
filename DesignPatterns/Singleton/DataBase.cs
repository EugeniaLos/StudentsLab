using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    class DataBase
    {
        private static DataBase instance;
        private DataBase()
        {

        }

        public static DataBase getInstance()
        {
            if(instance == null)
            {
                instance = new DataBase();
            }

            return instance;
        }

        public void connect()
        {

        }

        public void executeQuery()
        {

        }

        public void closeConnection()
        {

        }
    }
}
