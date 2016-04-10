using System;
using log4net;
using StackExchange.Redis;

namespace SmartAnalytics.Cache
{
    /// <summary>
    /// 连接池需要重新设计
    /// </summary>
    public class RedisContext : CacheContext
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (RedisContext));

        public static IDatabase RedisDatabase;

        public RedisContext()
        {
            CacheFormat = new JsonCacheFormat();
        }

        public override void Subscribe(string channel)
        {
            var sub = RedisDatabase.Multiplexer.GetSubscriber();

            //订阅消息源
            sub.Subscribe(channel, (c, m) => SendMessage(c, m));

        }

        public override void Init() { }

        public override T Get<T>(string key)
        {
            try
            {
                string value = RedisDatabase.StringGet(key);

                if (string.IsNullOrEmpty(value)) return null;

                return CacheFormat.Deserialize<T>(value);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("获取缓存失败{0},{1}", key, ex.Message));
                _logger.Error(ex);

                return null;
            }
        }

        public override bool Set<T>(string key, T t)
        {
            try
            {
                var value = CacheFormat.Serialize(t);

                return RedisDatabase.StringSet(key, value);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("设置缓存失败{0},{1}", key, ex.Message));
                _logger.Error(ex);

                return false;
            }
        }

        public override bool Remove(string key)
        {
            try
            {
                return RedisDatabase.KeyDelete(key);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("移除缓存失败{0},{1}", key, ex.Message));
                _logger.Error(ex);

                return false;
            }
        }

        public override void Dispose() { }
    }
}
