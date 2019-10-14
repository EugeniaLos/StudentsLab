using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        public List<Transaction> transactions { get; set; }

        public int ParentId { get; set; }
    }
}
