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


            var user = AuthenticateLoop();
            if (user == null)
            {
                Console.WriteLine("Exiting application...");
                return;
            }

            Console.WriteLine("Login successful!");
            MainMenuLoop(user);
        }

        private TblUser? AuthenticateLoop()
        {
            while (true)
            {
                var (firstName, pin) = PromptCredentials();
                var user = _loginService.LoginUser(firstName, pin);
                if (user != null)
                {
                    return user;
                }
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine("Would you like to try again? (y/n)");
                var retry = ((Console.ReadLine() ?? string.Empty).Trim());
                if (!retry.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                Console.Clear();
            }

        }
        private (string FirstName, string Pin) PromptCredentials()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine() ?? string.Empty;

            return (firstName.Trim(), pin.Trim());
        }

        private void MainMenuLoop(TblUser user)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                _menu.Display(user.IsAdmin);
                string choice = _menu.GetChoice();
                switch (choice)
                {
                    case "1":
                        _balanceService.GetUserBalance(user.UserId);
                        Continue();
                        break;
                    case "2":
                        _balanceService.WithdrawBalance(user.UserId);
                        Continue();
                        break;
                    case "3":
                        _loginService.ChangePin(user.UserId);
                        Continue();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                    case "5":
                        if (user.IsAdmin)
                        {
                            // Show ATM Balance
                            Console.WriteLine("ATM Balance: $100,000");
                            Continue();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            Continue();
                        }
                        break;
                    case "6":
                        if (user.IsAdmin)
                        {
                            // Update Balance
                            Console.WriteLine("Balance updated successfully!");
                            Continue();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            Continue();
                        }
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
