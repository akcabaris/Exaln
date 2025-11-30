using Exaln.Interfaces;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exaln.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        private IDatabase Db => _redis.GetDatabase();

        public async Task SetStringAsync(string key, string value, TimeSpan expiry)
        {
            await Db.StringSetAsync(key, value, expiry);
        }
        public async Task SetStringAsync(string key, string value)
        {
            await Db.StringSetAsync(key, value);
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var value = await Db.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task<bool> DeleteKeyAsync(string key)
        {
            return await Db.KeyDeleteAsync(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await Db.KeyExistsAsync(key);
        }

        public async Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return await Db.KeyExpireAsync(key, expiry);
        }

        public async Task AddToSetAsync(string key, string value)
        {
            await Db.SetAddAsync(key, value);
        }

        public async Task<string[]> GetSetMembersAsync(string key)
        {
            var members = await Db.SetMembersAsync(key);
            return members.Select(x => x.ToString()).ToArray();
        }

        public async Task<bool> RemoveFromSetAsync(string key, string value)
        {
            return await Db.SetRemoveAsync(key, value);
        }
    }
}
