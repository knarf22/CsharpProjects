using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Models.DTOs
{
    public sealed class WithdrawalResult
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public Dictionary<int, int> DispensedBills { get; init; }
        public decimal RemainingBalance { get; init; }

        private WithdrawalResult(bool success, string message, Dictionary<int, int> dispensedBills, decimal remainingBalance)
        {
            Success = success;
            Message = message;
            DispensedBills = dispensedBills;
            RemainingBalance = remainingBalance;
        }
        public static WithdrawalResult CreateSuccessResult(Dictionary<int, int> dispensedBills, decimal remainingBalance)
        {
            return new WithdrawalResult(true, "Withdrawal successful.", dispensedBills, remainingBalance);
        }
        public static WithdrawalResult CreateFailureResult(string message)
        {
            return new WithdrawalResult(false, message, new Dictionary<int, int>(), 0);
        }
    }
}
