using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Models
{
    public class TblBalance
    {
        public int BalanceId { get; set; }
        public int UserId { get; set; }
        public int Denomination { get; set; }
        public int Quantity { get; set; }
    }
}
