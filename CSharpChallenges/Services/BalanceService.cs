using CSharpChallenges.Data;
using CSharpChallenges.Models;
using CSharpChallenges.Models.DTOs;
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
                Console.WriteLine($"Balance: {b.Balance.ToString("N2")} ");
            }
        }


        public void WithdrawBalance(int userId)
        {

            Console.Write("Enter amount to withdraw: ");
            if(!int.TryParse(Console.ReadLine(), out int amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            var result = WithdrawalBalance(userId, amount);

            if(!result.Success)
            {
                Console.WriteLine(result.Message);
                return;
            }

            Console.WriteLine("Withdrawal successful!");
            Console.WriteLine("Dispensed:");

            foreach (var d in result.DispensedBills)
            {
                Console.WriteLine($"{d.Key} x {d.Value}");
            }

            Console.WriteLine($"Remaining Balance: {result.RemainingBalance}");
        }

        public WithdrawalResult WithdrawalBalance(int userId, int amount)
        {
            if(!DivisiblyBy100(amount))
            {
                return WithdrawalResult.CreateFailureResult("Amount must be divisible by 100");
            }

            var user = ValidateUser(userId);
            if (user == null)
            {
                return WithdrawalResult.CreateFailureResult("User not found.");
            }
            if (user.Balance < amount)
            {
                return WithdrawalResult.CreateFailureResult("Insufficient user balance.");
            }
            var atmBalance = GetATMAllBalance()
                .OrderByDescending(b => b.Denomination)
                .ToList();
            if(!TryComputeDispense(amount, atmBalance,out var deduction))
            {
                return WithdrawalResult.CreateFailureResult("ATM cannot dispense exact amount.");
            }

            //Apply deductions to ATM
            foreach(var bill in atmBalance)
            {
                if (deduction.ContainsKey(bill.Denomination))
                {
                    bill.Quantity -= deduction[bill.Denomination];
                }
            }

            //deduct to user's balance
            user.Balance -= amount;
            _context.SaveChanges();

            return WithdrawalResult.CreateSuccessResult(deduction, user.Balance);

        }

        private bool TryComputeDispense(int amount, IEnumerable<TblBalance> atmBalances, out Dictionary<int, int> deduction)
        {
            int remaining = amount;
            deduction = new Dictionary<int, int>();

            // 🔥 GREEDY ALGORITHM
            foreach (var bill in atmBalances.OrderByDescending(b => b.Denomination))
            {
                if (remaining <= 0) break;

                int needed = remaining / bill.Denomination;
                int toUse = Math.Min(needed, bill.Quantity);

                if (toUse > 0)
                {
                    deduction[bill.Denomination] = toUse;
                    remaining -= toUse * bill.Denomination;
                }
            }
            return remaining == 0;
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
                decimal totalBalance = balances.Sum(x => x.Quantity * x.Denomination);
                Console.WriteLine($"Total balance : {totalBalance.ToString("N2")} \n");
                
                foreach (var balance in balances)
                {
                    Console.WriteLine($"Bill: {balance.Denomination.ToString("N2")}, Amount: {balance.Quantity}");
                }
        }

        public void UpdateATMBalance()
        {


            var balance = GetATMAllBalance().OrderByDescending(b => b.Denomination).ToList();
             UpdateATMBalanceDisplay();

            Console.Write("Enter bill denomination to update: ");
            int denomination = int.Parse(Console.ReadLine());
            Console.Write("Enter new quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            var bill = _context.TblBalance
                .FirstOrDefault(b => b.BalanceId == denomination);
            if (bill == null)
            {
                Console.WriteLine("Bill denomination not found.");
                return;
            }
            bill.Quantity = quantity;
            _context.SaveChanges();
            Console.WriteLine("ATM balance updated successfully!");
        }

        private void UpdateATMBalanceDisplay()
        {
            var balance = GetATMAllBalance();

            Console.WriteLine("Current Balance: ");
            int index = 1;
            foreach (var b in balance)
            {
                Console.WriteLine($"{index}. Bill: {b.Denomination}, Quantity: {b.Quantity}");
                index++;    
            }
        }

        private bool DivisiblyBy100(int amount)
        {
            // ✅ Validate amount
           return amount % 100 == 0;
        }

        private TblUser ValidateUser(int userId)
        {
            var user = _context.TblUser
                .FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }
            return user;
        }

    }
}
