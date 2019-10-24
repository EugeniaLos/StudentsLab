using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.DataAccessLayer.Entities
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(64)]
        public string Email { get; set; }

        public virtual List<Asset> Assets { get; set; }
    }
}
