using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger = new InventoryLogger<InventoryItem>();
        private const string FilePath = "inventory.json";

        public void Run()
        {
            Console.WriteLine("Welcome to Erudite Inventory System!");

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SeedSampleData();
                        break;
                    case "2":
                        AddCustomItem();
                        break;
                    case "3":
                        SaveData();
                        break;
                    case "4":
                        LoadData();
                        break;
                    case "5":
                        PrintAllItems();
                        break;
                    case "6":
                        _logger.ClearLog();
                        Console.WriteLine("Memory cleared. Simulating a new session.");
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Thank you for using the system. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Seed sample data");
            Console.WriteLine("2. Add a new item");
            Console.WriteLine("3. Save data to file");
            Console.WriteLine("4. Load data from file");
            Console.WriteLine("5. Print all items in memory");
            Console.WriteLine("6. Clear memory (Simulate new session)");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        public void SeedSampleData()
        {
            Console.WriteLine("Seeding sample data...");
            _logger.Add(new InventoryItem(413, "Laptop", 10, DateTime.Now.AddDays(-30)));
            _logger.Add(new InventoryItem(414, "Mouse", 50, DateTime.Now.AddDays(-15)));
            _logger.Add(new InventoryItem(415, "Keyboard", 25, DateTime.Now.AddDays(-5)));
            Console.WriteLine("Sample data seeded successfully!");
        }

        private void AddCustomItem()
        {
            try
            {
                Console.WriteLine("\n--- Add a New Inventory Item ---");
                Console.Write("Enter Item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); return; }

                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity)) { Console.WriteLine("Invalid quantity."); return; }

                _logger.Add(new InventoryItem(id, name, quantity, DateTime.Now));
                Console.WriteLine("Item added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the item: {ex.Message}");
            }
        }

        public void SaveData()
        {
            Console.WriteLine("Saving data to file...");
            _logger.SaveToFile(FilePath);
            Console.WriteLine($"Data saved to '{FilePath}' successfully!");
        }

        public void LoadData()
        {
            Console.WriteLine("Loading data from file...");
            _logger.LoadFromFile(FilePath);
            Console.WriteLine("Data loaded successfully!");
        }

        public void PrintAllItems()
        {
            Console.WriteLine("\n--- Current Inventory ---");
            var items = _logger.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("No items in inventory. Please add or load some data.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Added: {item.DateAdded.ToShortDateString()}");
                }
            }
            Console.WriteLine("--------------------------");
        }
    }
}