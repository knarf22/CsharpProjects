using CSharpChallenges.Data;
using CSharpChallenges.Models;
using CSharpChallenges.Services;
using CSharpChallenges.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges
{
    public class App
    {
        private LoginService _loginService;
        private Menu _menu;
        private BalanceService _balanceService;
        public App(LoginService loginService, Menu menu, BalanceService balanceService)
        {
            _loginService = loginService;
            _menu = menu;
            _balanceService = balanceService;
        }

        private void Continue()
        {
            Console.WriteLine("Press something to continue...");
            Console.ReadLine();
        }
        public void Run()
        {
            Console.WriteLine("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter PIN: ");
            string inputPin = Console.ReadLine();
            var user = _loginService.LoginUser(firstName!, inputPin);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
                return;
            }

            Console.WriteLine("Login successful!");

            // 📋 MENU LOOP
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                _menu.Display(user.IsAdmin);
                string choice = _menu.GetChoice();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Show Balance logic here");
                        _balanceService.GetUserBalance(user.UserId);
                        Continue();
                        break;

                    case "2":
                        _balanceService.WithdrawBalance(user.UserId);
                        Continue();
                        break;

                    case "4":
                        Console.WriteLine("Goodbye!");
                        isRunning = false;
                        Continue();
                        break;

                    case "5":
                        if (user.IsAdmin)
                        {
                            _balanceService.GetATMBalance();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Continue();
                        break;
                    case "6":
                        if (user.IsAdmin)
                        {
                            Console.WriteLine("Update Balance logic here");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Continue();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Continue();
                        break;
                }
            }


        }
    }
}
