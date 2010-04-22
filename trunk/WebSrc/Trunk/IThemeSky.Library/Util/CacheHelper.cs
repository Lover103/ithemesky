using System;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace IThemeSky.Library.Util
{
	/// <summary>
	/// CacheHelper Web缓存帮助类 添加,移除,读取缓存
	/// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 通过缓存键获取相应的内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        /// <summary>
        /// 判断是否有此key的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="CacheTimeOption">缓存的时间长短</param>
        /// <param name="dependencies">缓存依赖项</param>
        /// <param name="cacheItemPriority">优先级</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, CacheDependency dependencies, CacheItemPriority cacheItemPriority)
        {
            DateTime absoluteExpiration = GetAbsoluteExpirationTime(CacheTimeOption, CacheExpirationOption);
            TimeSpan slidingExpiration = GetSlidingExpirationTime(CacheTimeOption, CacheExpirationOption);
            HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, cacheItemPriority, null);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="CacheTimeOption">缓存时间</param>
        /// <param name="CacheExpirationOption">缓存过期时间类别（绝对/弹性）</param>
        /// <param name="dependencies">缓存依赖项</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, CacheDependency dependencies)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption, dependencies, CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="CacheTimeOption">缓存时间</param>
        /// <param name="CacheExpirationOption">缓存过期时间类别（绝对/弹性）</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption, null, CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="CacheTimeOption">缓存时间</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption.SlidingExpiration);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        public static void Set<T>(string key, T value)
        {
            Set<T>(key, value, CacheTimeOption.Normal);
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void Clear()
        {
            foreach (DictionaryEntry elem in HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Remove(elem.Key.ToString());
            }
        }
        /// <summary>
        /// 移除相关关键字的缓存
        /// </summary>
        /// <param name="keyword">缓存关键字</param>
        public static void Remove(string keyword)
        {
            foreach (DictionaryEntry elem in HttpRuntime.Cache)
            {
                if (elem.Key.ToString().Contains(keyword))
                {
                    HttpRuntime.Cache.Remove(elem.Key.ToString());
                }
            }
        }

        /// <summary>
        /// 清除某项缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void RemoveAt(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 获取绝对日期时间
        /// </summary>
        /// <param name="CacheTimeOption">缓存的时间长短</param>
        /// <returns></returns>
        public static DateTime GetAbsoluteExpirationTime(CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            if (CacheExpirationOption == CacheExpirationOption.SlidingExpiration
                || CacheTimeOption == CacheTimeOption.NotRemovable)
            {
                return Cache.NoAbsoluteExpiration;
            }

            return DateTime.Now.AddMinutes((int)CacheTimeOption);
        }

        /// <summary>
        /// 获取弹性时间过期时间
        /// </summary>
        /// <param name="CacheTimeOption">缓存的时间长短</param>
        /// <returns></returns>
        private static TimeSpan GetSlidingExpirationTime(CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            if (CacheExpirationOption == CacheExpirationOption.AbsoluteExpiration
                || CacheTimeOption == CacheTimeOption.NotRemovable)
            {
                return Cache.NoSlidingExpiration;
            }

            return TimeSpan.FromMinutes((int)CacheTimeOption);
        }
    }


    /// <summary>
    /// 过期方式
    /// </summary>
    public enum CacheExpirationOption
    {
        /// <summary>
        /// 绝对过期
        /// </summary>
        AbsoluteExpiration = 0,
        /// <summary>
        /// 弹性过期

        /// </summary>
        SlidingExpiration = 1
    }

    /// <summary>
    /// 常用过期时间
    /// </summary>
    public enum CacheTimeOption
    {
        /// <summary>
        /// 永不过期
        /// </summary>
        NotRemovable = 0,
        /// <summary>
        /// 短时间 10分钟
        /// </summary>
        Short = 10,
        /// <summary>
        /// 一般正常过期时间30分钟
        /// </summary>
        Normal = 30,
        /// <summary>
        /// 长时间60分钟
        /// </summary>
        Long = 60,
    }

}
