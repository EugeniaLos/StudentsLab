using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using MoneyManager;

namespace BusinessLayer
{
    public class OrderedTransactions
    {
        public string AssetName {get; set;}
        public  string CategoryName {get; set;}
        public decimal Amount {get; set;}
        public DateTime Date { get; set;}
        public string Comment { get; set; }
    }
}
