using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
