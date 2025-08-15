using finaceMS;
using healthMS;
using InventoryMS;
using InventRecord;
using ResultsMS;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Unified Management System!");

        bool exit = false;
        while (!exit)
        {
            DisplayMainMenu();
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Run the Finance Management System
                    var financeApp = new FinanceApp();
                    financeApp.Run();
                    break;
                case "2":
                    // Run the Healthcare System
                    var healthApp = new HealthSystemApp();
                    healthApp.Run();
                    break;
                case "3":
                    // Run the Warehouse Inventory System
                    var inventoryApp = new WareHouseManager();
                    inventoryApp.Run();
                    break;
                case "4":
                    // Run the Grading System
                    var resultsApp = new GradingSystem.Program();
                    resultsApp.Run(); // Assuming a Run method in the grading system's Program.cs
                    break;
                case "5":
                    // Run the Record-based Inventory System
                    var recordApp = new InventorySystem.Program();
                    recordApp.Run(); // Assuming a Run method in the record-based system
                    break;
                case "6":
                    exit = true;
                    Console.WriteLine("Exiting the unified system. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Select a system to manage:");
        Console.WriteLine("1. Finance Management System");
        Console.WriteLine("2. Healthcare System");
        Console.WriteLine("3. Warehouse Inventory System");
        Console.WriteLine("4. Student Grading System");
        Console.WriteLine("5. Inventory Record System");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }
}