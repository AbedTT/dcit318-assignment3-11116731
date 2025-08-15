using System;
using System.Collections.Generic;
using System.IO;

namespace GradingSystem
{
    class Program
    {
        private static readonly StudentResultProcessor Processor = new StudentResultProcessor();

        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            Console.WriteLine("Welcome to Erudite Student Grading System!");
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCustomDataFile();
                        break;
                    case "2":
                        RunGradingProcess();
                        break;
                    case "3":
                        ViewStudentReport();
                        break;
                    case "4":
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

        static void DisplayMenu()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Create a custom data file");
            Console.WriteLine("2. Run the grading process");
            Console.WriteLine("3. View student report");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
        }

        static void CreateCustomDataFile()
        {
            Console.Write("Enter the desired input file path (e.g., student_data.txt): ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    Console.WriteLine("Enter student data one by one. Enter 'done' to finish.");
                    string? input;
                    while (true)
                    {
                        Console.Write("Enter student data (ID,Full Name,Score): ");
                        input = Console.ReadLine();
                        if (input?.ToLower() == "done")
                        {
                            break;
                        }
                        if (!string.IsNullOrWhiteSpace(input))
                        {
                            writer.WriteLine(input);
                        }
                    }
                }
                Console.WriteLine($"Student data file '{filePath}' created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the file: {ex.Message}");
            }
        }

        static void RunGradingProcess()
        {
            Console.Write("Enter the path to the input file: ");
            string? inputFilePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputFilePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            Console.Write("Enter the path for the output report: ");
            string? outputFilePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(outputFilePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            try
            {
                Console.WriteLine("\nStarting student grading process...");
                List<Student> students = Processor.ReadStudentsFromFile(inputFilePath);

                Processor.WriteReportToFile(students, outputFilePath);

                Console.WriteLine("\nProcess completed successfully!");
                Console.WriteLine($"A summary report has been written to: {outputFilePath}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: The file '{ex.FileName}' was not found. Please check the path and try again.");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Data Error: {ex.Message}");
                Console.WriteLine("Processing stopped due to an invalid score format.");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"Data Error: {ex.Message}");
                Console.WriteLine("Processing stopped due to an incomplete student record.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void ViewStudentReport()
        {
            Console.Write("Enter the path to the student report file: ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            try
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"\n--- Student Report from '{Path.GetFileName(filePath)}' ---");
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"The file '{filePath}' does not exist. Please run the grading process first.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
}