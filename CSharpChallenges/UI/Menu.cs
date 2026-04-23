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
            Console.WriteLine("Please select a challenge:");
            Console.WriteLine("1. Show Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Update Profile");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. Update Balance");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    // Call Challenge 1 method
                    break;
                case "2":
                    // Call Challenge 2 method
                    break;
                case "3":
                    // Call Challenge 3 method
                    break;
                case "4":
                    // Call Challenge 3 method
                    break;
                case "6":
                    // Call Challenge 3 method
                    break;
                case "5":
                    Console.WriteLine("Thank you for playing! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Display();
                    break;
            }
        }
    }
}
