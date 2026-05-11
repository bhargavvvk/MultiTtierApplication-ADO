using System.Collections.Generic;
using SimpleNotificationSystem.Interfaces;

namespace SimpleNotificationSystem.DataAccessLayer
{
    public abstract class AbstractRepository<K,T>: IRepository<K,T> where K : notnull where T : class
    {
        protected Dictionary<K,T> _storage = new Dictionary<K,T>();
        public abstract T Create(T item);
        public T? Get(K key)
        {
            if (_storage.ContainsKey(key))
            {
                return _storage[key];
            }
            return null;
        }
        public List<T>? GetAll()
        {
            if(_storage.Count == 0)
            {
                return null;
            }
            return new List<T>(_storage.Values);
        }
        public T? Update(K key, T item)
        {
            if (_storage.ContainsKey(key))
            {
                _storage[key] = item;
                return item;
            }
            return null;
        }
        public T? Delete(K key)
        {
            if (!_storage.ContainsKey(key))
            {
                return null;
            }
            T item = _storage[key];
             _storage.Remove(key);
             return item;
        }
    }
}