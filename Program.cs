using System;
using StackExchange.Redis;

namespace CuteRedisExample
{
    internal static class Program
    {
        private static string RedisConnection => "127.0.0.1:6379";
        public static void Main(string[] args)
        {
            // Store user to redis cache
            var userId = Guid.NewGuid().ToString();
            Set(userId, "Bob");
            Console.WriteLine("User saved to redis cache");
            
            // Get user from redis store
            var userName = Get(userId);
            Console.WriteLine($"User loaded from redis cache: {userName}");
        }

        private static void Set(string key, string value)
        {
            using IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(RedisConnection);
            var db = connectionMultiplexer.GetDatabase();
            db.StringSet(key, value);
        }
        
        private static string Get(string key)
        {
            using IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(RedisConnection);
            var db = connectionMultiplexer.GetDatabase();
            return db.StringGet(key);
        }
    }
}