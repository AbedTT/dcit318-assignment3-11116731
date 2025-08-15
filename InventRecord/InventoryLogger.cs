using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventorySystem
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new List<T>();

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_log); // Return a copy to prevent external modification
        }

        public void ClearLog()
        {
            _log.Clear();
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_log, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    var items = JsonSerializer.Deserialize<List<T>>(jsonString);
                    if (items != null)
                    {
                        _log.Clear();
                        _log.AddRange(items);
                    }
                }
                else
                {
                    Console.WriteLine($"File '{filePath}' not found. No data loaded.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
            }
        }
    }
}