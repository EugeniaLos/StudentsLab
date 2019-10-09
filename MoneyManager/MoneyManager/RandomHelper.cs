using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public static class RandomHelper
    {
        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                               rng.NextInt32(),
                               rng.NextInt32(),
                               sign,
                               scale);
        }
    }
}
