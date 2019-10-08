using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string Hash { get; set; }

        //[Required]
        public string Salt { get; set; }

        public List<Asset> Assets { get; set; }
    }
}
