using CSharpChallenges.Data;
using CSharpChallenges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Services
{
    public class BalanceService
    {
        private AppDbContext _context;

        public BalanceService(AppDbContext context)
        {
            _context = context;
        }

        public void GetAllBalance()
        {
               var balances = from balance in 
                        _context.TblBalance
                            select balance;
                balances.ToList();

                foreach (var balance in balances)
                {
                    Console.WriteLine($"Bill: {balance.Denomination}, Amount: {balance.Quantity}");
                }
        }
        
    }
}
