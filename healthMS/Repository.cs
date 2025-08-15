using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareSystem
{
    public class Repository<T> where T : class
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }

        public T? GetById(Func<T, bool> predicate)
        {
            return _items.FirstOrDefault(predicate);
        }

        public bool Remove(Func<T, bool> predicate)
        {
            T? itemToRemove = _items.FirstOrDefault(predicate);
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
                return true;
            }
            return false;
        }
    }
}