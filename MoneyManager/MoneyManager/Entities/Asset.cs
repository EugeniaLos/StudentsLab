using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.DataAccessLayer.Entities
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
