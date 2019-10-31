using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.BusinessLayer.DataModels
{
    public class AssetBalance
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public decimal Balance { get; set; }
    }
}
