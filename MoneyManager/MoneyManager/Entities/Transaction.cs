using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.DataAccessLayer.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [DataType("decimal(16 ,3")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }

        public string Comment { get; set; }
    }
}
