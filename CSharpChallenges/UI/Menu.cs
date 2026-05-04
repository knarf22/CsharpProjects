using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.UI
{
    public class Menu
    {
        public void Display()
        {
            Console.WriteLine("Welcome to the C# Challenges!");
            Console.WriteLine("1. Show Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Update Profile");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. Update Balance");
        }

        public string GetChoice()
        {
            return Console.ReadLine();
        }
    }
}
