﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public static class RandomHelper
    {
        public static int NextInt32(this Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimal(this Random rng)
        {
            var scale = (byte) rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                rng.NextInt32(),
                rng.NextInt32(),
                sign,
                scale);
        }
    }
}
