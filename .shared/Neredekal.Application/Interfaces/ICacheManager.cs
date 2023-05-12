namespace Neredekal.Application.Interfaces
{
    public interface ICacheManager
    {
        public T Get<T>(string key);
        public T Set<T>(string key, T value, int expiration = 60);
        public void Remove(string key);
    }
}