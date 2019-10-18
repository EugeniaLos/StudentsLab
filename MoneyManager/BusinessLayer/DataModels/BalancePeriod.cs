using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DataModels
{
    public class BalancePeriod
    {
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
