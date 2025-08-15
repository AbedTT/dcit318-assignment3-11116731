
using System;
using System.Collections.Generic;


    public class FinanceApp
    {
        private readonly List<User> _users = new List<User>();
        private Account _activeAccount;

        public void Run()
        {
            Console.WriteLine("Welcome to Erudite Finance Solutions System!");
            Console.WriteLine("Please create an account to get started.");
            CreateAccount();

            if (_activeAccount != null)
            {
                MainMenu();
            }
        }

        private void CreateAccount()
        {
            Console.Write("Enter a username: ");
            string username = Console.ReadLine();
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();
            Console.Write("Enter initial balance: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance) && initialBalance >= 0)
            {
                var user = new User(_users.Count + 1, username, password);
                _users.Add(user);

                _activeAccount = new SavingsAccount(Guid.NewGuid().ToString(), initialBalance);
                Console.WriteLine("Account created successfully!");
                Console.WriteLine($"Your account number is: {_activeAccount.AccountNumber}");
            }
            else
            {
                Console.WriteLine("Invalid initial balance. Account creation failed.");
                _activeAccount = null;
            }
        }

        private void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        HandleDeposit();
                        break;
                    case "2":
                        HandleWithdrawal();
                        break;
                    case "3":
                        ViewBalance();
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Thank you for using the system. Until the next transaction.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a number from the menu.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
        }

        private void HandleDeposit()
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                var transaction = new Transaction(0, DateTime.Now, amount, "Deposit");
                var depositProcessor = new DepositProcessor();
                depositProcessor.Process(transaction);
                _activeAccount.ApplyDeposit(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private void HandleWithdrawal()
        {
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.Write("Enter category (e.g., Groceries): ");
                string category = Console.ReadLine();
                var transaction = new Transaction(0, DateTime.Now, amount, category);
                var withdrawalProcessor = new MobileMoneyProcessor();
                withdrawalProcessor.Process(transaction);
                _activeAccount.ApplyTransaction(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private void ViewBalance()
        {
            Console.WriteLine($"Current balance for account {_activeAccount.AccountNumber}: {_activeAccount.Balance:C}");
        }
    }