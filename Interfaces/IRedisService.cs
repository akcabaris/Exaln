using Exaln.InternalModels;

namespace Exaln.Interfaces
{
    public interface IRedisService
    {
        Task SetStringAsync(string key, string value, TimeSpan expiry);
        Task SetStringAsync(string key, string value);
        Task<string?> GetStringAsync(string key);
        Task<bool> DeleteKeyAsync(string key);
        Task AddToSetAsync(string key, string value);
        Task<string[]> GetSetMembersAsync(string key);
        Task<bool> RemoveFromSetAsync(string key, string value);
        Task<bool> KeyExistsAsync(string key);
        Task<bool> ExpireAsync(string key, TimeSpan expiry);
    }
}
