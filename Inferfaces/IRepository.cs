using System.Collections.Generic;
namespace SimpleNotificationSystem.Interfaces
{
    public interface IRepository<K, T>  where K : notnull where T : class
    {
        T Create(T item);

        T? Get(K key);

        List<T>? GetAll();

        T? Update(K key, T item);

        T? Delete(K key);
    }
}