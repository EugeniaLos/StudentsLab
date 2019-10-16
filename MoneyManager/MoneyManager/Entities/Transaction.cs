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

        public Category Category { get; set; }

        [Required]
        [DataType("decimal(16 ,3")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AssetId { get; set; }

        public Asset Asset { get; set; }

        public string Comment { get; set; }
    }
}
