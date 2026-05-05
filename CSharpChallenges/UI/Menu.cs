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
            Console.WriteLine("3. Update Profile");
            Console.WriteLine("4. Exit");
            if(isAdmin)
            {
                Console.WriteLine("5. Show ATM Balance");
                Console.WriteLine("6. Update Balance");
            }
        }

        public string GetChoice()
        {
            return Console.ReadLine();
        }
    }
}
