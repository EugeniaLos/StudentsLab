using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AssetId { get; set; }

        public string Comment { get; set; }
    }
}
