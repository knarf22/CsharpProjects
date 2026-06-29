using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.UI
{
    public class Menu
    {
        public void Display(bool isAdmin)
        {
            Console.WriteLine("Welcome to the C# Challenges!");
            Console.WriteLine("1. Show Balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Change Pin");
            Console.WriteLine("4. Transaction History");
            Console.WriteLine("5. Exit");
            if(isAdmin)
            {
                Console.WriteLine("6. Show ATM Balance");
                Console.WriteLine("7. Update Balance");
            }
        }

        public string GetChoice()
        {
            return Console.ReadLine();
        }
    }
}
