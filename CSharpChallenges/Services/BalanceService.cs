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

        public void GetUserBalance(int userId)
        {
            var balances = (from balance in
                         _context.TblUser
                            where balance.UserId == userId
                            select balance).ToList();

            foreach (var b in balances)
            {
                Console.WriteLine($"Balance: {b.Balance} ");
            }
        }


        public void WithdrawBalance(int userId)
        {

            Console.Write("Enter amount to withdraw: ");
            int amount = int.Parse(Console.ReadLine());

            // ✅ Validate amount
            if (amount % 100 != 0)
            {
                Console.WriteLine("Amount must be divisible by 100.");
                return;
            }

            var user = _context.TblUser
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            if (user.Balance < amount)
            {
                Console.WriteLine("Insufficient user balance.");
                return;
            }

            // 🏧 Get ATM cash
            var atmBalance = _context.TblBalance
                .OrderByDescending(b => b.Denomination)
                .ToList();

            int remaining = amount;
            var deduction = new Dictionary<int, int>();

            // 🔥 GREEDY ALGORITHM
            foreach (var bill in atmBalance)
            {
                int needed = remaining / bill.Denomination;
                int toUse = Math.Min(needed, bill.Quantity);

                if (toUse > 0)
                {
                    deduction[bill.Denomination] = toUse;
                    remaining -= toUse * bill.Denomination;
                }
            }

            // ❌ ATM cannot fulfill request
            if (remaining > 0)
            {
                Console.WriteLine("ATM cannot dispense exact amount.");
                return;
            }

            // ✅ Deduct ATM bills
            foreach (var bill in atmBalance)
            {
                if (deduction.ContainsKey(bill.Denomination))
                {
                    bill.Quantity -= deduction[bill.Denomination];
                }
            }

            // ✅ Deduct user balance
            user.Balance -= amount;

            _context.SaveChanges();

            Console.WriteLine("Withdrawal successful!");
            Console.WriteLine("Dispensed:");

            foreach (var d in deduction)
            {
                Console.WriteLine($"{d.Key} x {d.Value}");
            }

            Console.WriteLine($"Remaining Balance: {user.Balance}");
        }

        private IList<TblBalance> GetATMAllBalance()
        {
            var balances = from balance in
                        _context.TblBalance
                           select balance;
            return balances.ToList();

        }
        public void GetATMBalance()
        {
                var balances = GetATMAllBalance();

                foreach (var balance in balances)
                {
                    Console.WriteLine($"Bill: {balance.Denomination}, Amount: {balance.Quantity}");
                }
        }
        
    }
}
