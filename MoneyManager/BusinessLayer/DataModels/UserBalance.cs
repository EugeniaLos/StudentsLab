using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class UserBalance
    {
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
