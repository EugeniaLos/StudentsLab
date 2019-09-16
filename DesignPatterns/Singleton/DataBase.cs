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

        public DataBase getInstance()
        {
            if(instance == null)
            {
                instance = new DataBase();
            }

            return instance;
        }
    }
}
