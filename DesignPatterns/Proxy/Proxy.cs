using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Proxy
{
    public class Proxy: IYersterdayRate
    {
        public void GetRate()
        {
            YersterdayRate rate = new YersterdayRate();
            rate.GetRate();
        }

    }
}
