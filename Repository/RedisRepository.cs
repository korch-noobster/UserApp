using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace UserApp.Repository
{
    public class RedisRepository
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisRepository(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["RedisServers"]))
            {
                throw new ArgumentException("The server list should not be null or empty.");
            }

            _connection = ConnectionMultiplexer.Connect(configuration["RedisServers"]);
        }

        public async Task Set(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("The key should not be null or empty.");
            }

            var db = _connection.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<string> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("The key should not be null or empty.");
            }

            var db = _connection.GetDatabase();

            return await db.StringGetAsync(key);
        }
    }
}
