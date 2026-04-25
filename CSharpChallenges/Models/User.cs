using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Models
{
    public class User
    {
        int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
