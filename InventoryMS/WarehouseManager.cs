using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseInventorySystem
{
    public class WareHouseManager
    {
        private readonly InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
        private readonly InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

        public void Run()
        {
            Console.WriteLine("Welcome to the Erudaite Warehouse Inventory Management System!");
            SeedData();

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintAllItems(_groceries);
                        PrintAllItems(_electronics);
                        break;
                    case "2":
                        AddItemToInventory();
                        break;
                    case "3":
                        UpdateItemQuantity();
                        break;
                    case "4":
                        RemoveItemFromInventory();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Congrats on experiencing the system. Goodbye!");
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
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. View all inventory");
            Console.WriteLine("2. Add a new item");
            Console.WriteLine("3. Update an item's quantity");
            Console.WriteLine("4. Remove an item");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        private void SeedData()
        {
            try
            {
                _groceries.AddItem(new GroceryItem(39, "Rice", 50, DateTime.Now.AddMonths(6)));
                _groceries.AddItem(new GroceryItem(40, "Sugar", 30, DateTime.Now.AddMonths(12)));

                _electronics.AddItem(new ElectronicItem(41, "Laptop", 15, "Dell", 24));
                _electronics.AddItem(new ElectronicItem(42, "Smartphone", 100, "Samsung", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }

        private void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            string itemType = typeof(T).Name.Replace("Item", "");
            Console.WriteLine($"\n--- All {itemType} Items in Inventory ---");
            List<T> items = repo.GetAllItems();
            if (items.Any())
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
                }
            }
            else
            {
                Console.WriteLine($"No {itemType} items found.");
            }
        }

        private void AddItemToInventory()
        {
            Console.WriteLine("\n--- Add a New Item ---");
            Console.Write("Is this a Grocery (G) or Electronic (E) item? ");
            string? itemTypeChoice = Console.ReadLine()?.ToUpper();

            if (itemTypeChoice == "G")
            {
                AddGroceryItem();
            }
            else if (itemTypeChoice == "E")
            {
                AddElectronicItem();
            }
            else
            {
                Console.WriteLine("Invalid item type. Please enter 'G' or 'E'.");
            }
        }

        private void AddGroceryItem()
        {
            try
            {
                Console.Write("Enter Item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); return; }

                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity)) { Console.WriteLine("Invalid quantity."); return; }

                Console.Write("Enter Expiry Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime expiryDate)) { Console.WriteLine("Invalid date format."); return; }

                _groceries.AddItem(new GroceryItem(id, name, quantity, expiryDate));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void AddElectronicItem()
        {
            try
            {
                Console.Write("Enter Item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); return; }

                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity)) { Console.WriteLine("Invalid quantity."); return; }

                Console.Write("Enter Brand: ");
                string brand = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Warranty in months: ");
                if (!int.TryParse(Console.ReadLine(), out int warranty)) { Console.WriteLine("Invalid warranty."); return; }

                _electronics.AddItem(new ElectronicItem(id, name, quantity, brand, warranty));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void UpdateItemQuantity()
        {
            Console.WriteLine("\n--- Update Item Quantity ---");
            Console.Write("Enter Item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Console.Write("Enter new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int newQuantity))
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            try
            {
                if (_groceries.GetAllItems().Any(item => item.Id == id))
                {
                    _groceries.UpdateQuantity(id, newQuantity);
                }
                else if (_electronics.GetAllItems().Any(item => item.Id == id))
                {
                    _electronics.UpdateQuantity(id, newQuantity);
                }
                else
                {
                    throw new ItemNotFoundException($"Item with ID {id} was not found in either inventory.");
                }
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void RemoveItemFromInventory()
        {
            Console.WriteLine("\n--- Remove an Item ---");
            Console.Write("Enter Item ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            try
            {
                if (_groceries.GetAllItems().Any(item => item.Id == id))
                {
                    _groceries.RemoveItem(id);
                }
                else if (_electronics.GetAllItems().Any(item => item.Id == id))
                {
                    _electronics.RemoveItem(id);
                }
                else
                {
                    throw new ItemNotFoundException($"Item with ID {id} was not found in either inventory.");
                }
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}