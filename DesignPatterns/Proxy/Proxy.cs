using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    class Proxy: IYersterdayRate
    {
        public void getRate()
        {
            YersterdayRate rate = new YersterdayRate();
            rate.getRate();
        }

    }
}
