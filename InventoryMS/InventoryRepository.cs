using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseInventorySystem
{
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new Dictionary<int, T>();

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
            {
                throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
            }
            _items.Add(item.Id, item);
            Console.WriteLine($"Added item '{item.Name}' with ID {item.Id}.");
        }

        public T GetItemById(int id)
        {
            if (_items.TryGetValue(id, out T? item))
            {
                return item;
            }
            throw new ItemNotFoundException($"Item with ID {id} was not found.");
        }

        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Cannot remove item. Item with ID {id} was not found.");
            }
            _items.Remove(id);
            Console.WriteLine($"Removed item with ID {id}.");
        }

        public List<T> GetAllItems()
        {
            return new List<T>(_items.Values);
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Cannot update quantity. Item with ID {id} was not found.");
            }
            if (newQuantity < 0)
            {
                throw new InvalidQuantityException("New quantity cannot be negative.");
            }
            _items[id].Quantity = newQuantity;
            Console.WriteLine($"Updated quantity for item ID {id} to {newQuantity}.");
        }
    }
}