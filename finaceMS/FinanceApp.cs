
using finaceMS;
using System;
using System.Collections.Generic;

namespace finaceMS
{
    public class FinanceApp
    {
        private readonly List<User> _users = new List<User>();
        private Account? _activeAccount; 
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            Console.WriteLine("Welcome to Erudite Finance Solutions System!");

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Approaches for the system usage:");
            Console.WriteLine("1. Use the pre-configured Savings Account");
            Console.WriteLine("2. Create a new Savings Account");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                //  Instantiate a SavingsAccount with an account number and initial balance (e.g., 1000)
                _activeAccount = new SavingsAccount("9876543210", 1000m);
                Console.WriteLine("Using the pre-configured Savings Account. Initial balance: 1000.00");

                // Simulate initial transactions 
                Console.WriteLine("\n--- Simulating Initial Transactions ---");
                SimulateInitialTransactions();
                Console.WriteLine("--- Initial Transaction Simulation Complete ---\n");
            }
            else if (choice == "2")
            {
                CreateAccount();
            }
            else
            {
                Console.WriteLine("Invalid choice. The application will now exit.");
                _activeAccount = null;
                return;
            }

            if (_activeAccount != null)
            {
                MainMenu();
            }
        }

        private void SimulateInitialTransactions()
        {
            // Create three Transaction records with sample values
            var transaction1 = new Transaction(_transactions.Count + 1, DateTime.Now.AddDays(-3), 150m, "Groceries");
            var transaction2 = new Transaction(_transactions.Count + 1, DateTime.Now.AddDays(-2), 50m, "Utilities");
            var transaction3 = new Transaction(_transactions.Count + 1, DateTime.Now.AddDays(-1), 200m, "Entertainment");

            // Use the following processors to process each transaction:
            ITransactionProcessor mobileMoneyProcessor = new MobileMoneyProcessor();
            ITransactionProcessor bankTransferProcessor = new BankTransferProcessor();
            ITransactionProcessor cryptoWalletProcessor = new CryptoWalletProcessor();

            mobileMoneyProcessor.Process(transaction1);
            bankTransferProcessor.Process(transaction2);
            cryptoWalletProcessor.Process(transaction3);
            Console.WriteLine("--------------------------------------------------");

            // Apply each transaction to the SavingsAccount using ApplyTransaction
            _activeAccount!.ApplyTransaction(transaction1);
            _activeAccount!.ApplyTransaction(transaction2);
            _activeAccount!.ApplyTransaction(transaction3);
            Console.WriteLine("--------------------------------------------------");

            _transactions.Add(transaction1);
            _transactions.Add(transaction2);
            _transactions.Add(transaction3);

            Console.WriteLine("Initial transactions have been recorded.");
        }

        private void CreateAccount()
        {
            Console.Write("Enter your fullname: ");
            string fullname = Console.ReadLine() ?? string.Empty; 
            Console.Write("Enter address: ");
            string address = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter phone number: ");
            int phonenumber = int.TryParse(Console.ReadLine(), out var num) ? num : 0;
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter initial balance: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance) && initialBalance >= 0)
            {
                var user = new User(_users.Count + 1, fullname, address, phonenumber, password);
                _users.Add(user);

                // Generate random 10-digit account number.
                Random random = new Random();
                string accountNumber = random.Next(1000000000, 1999999999).ToString();

                _activeAccount = new SavingsAccount(accountNumber, initialBalance);
                Console.WriteLine("Account created successfully!");
                Console.WriteLine($"Your account number is: GHS {_activeAccount.AccountNumber:N2}");
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
                string? choice = Console.ReadLine();
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
                        HandleMobileMoneyTransaction();
                        break;
                    case "5":
                        HandleBankTransferTransaction();
                        break;
                    case "6":
                        HandleCryptoTransaction();
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Thank you for using the system. Until your next transaction. Goodbye!");
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
            Console.WriteLine("Select a transaction option:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Mobile Money Transaction");
            Console.WriteLine("5. Bank Transfer Transaction");
            Console.WriteLine("6. Crypto Transaction");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        private void HandleDeposit()
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                string category = "Deposit"; 
                var transaction = new Transaction(_transactions.Count + 1, DateTime.Now, amount, category);
                var depositProcessor = new DepositProcessor();
                depositProcessor.Process(transaction);
                _activeAccount!.ApplyDeposit(transaction);
                _transactions.Add(transaction);
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
                string category = Console.ReadLine() ?? string.Empty;
                var transaction = new Transaction(_transactions.Count + 1, DateTime.Now, amount, category);
                var withdrawalProcessor = new MobileMoneyProcessor();
                withdrawalProcessor.Process(transaction);
                _activeAccount!.ApplyTransaction(transaction);
                _transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private void ViewBalance()
        {
            if (_activeAccount != null)
            {
                Console.WriteLine($"Current balance for account {_activeAccount.AccountNumber}: GHS {_activeAccount.Balance:N2}");
            }
        }

        private void HandleMobileMoneyTransaction()
        {
            Console.Write("Enter amount for Mobile Money transaction: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.Write("Enter category for Mobile Money transaction: ");
                string category = Console.ReadLine() ?? string.Empty;
                var transaction = new Transaction(_transactions.Count + 1, DateTime.Now, amount, category);
                var processor = new MobileMoneyProcessor();
                processor.Process(transaction);
                _activeAccount!.ApplyTransaction(transaction);
                _transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private void HandleBankTransferTransaction()
        {
            Console.Write("Enter amount for Bank Transfer transaction: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.Write("Enter category for Bank Transfer transaction: ");
                string category = Console.ReadLine() ?? string.Empty; 
                var transaction = new Transaction(_transactions.Count + 1, DateTime.Now, amount, category);
                var processor = new BankTransferProcessor();
                processor.Process(transaction);
                _activeAccount!.ApplyTransaction(transaction);
                _transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        private void HandleCryptoTransaction()
        {
            Console.Write("Enter amount for Crypto transaction: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                Console.Write("Enter category for Crypto transaction: ");
                string category = Console.ReadLine() ?? string.Empty; 
                var transaction = new Transaction(_transactions.Count + 1, DateTime.Now, amount, category);
                var processor = new CryptoWalletProcessor();
                processor.Process(transaction);
                _activeAccount!.ApplyTransaction(transaction); 
                _transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }
    }
}