using System;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace IThemeSky.Library.Util
{
	/// <summary>
	/// CacheHelper Web��������� ���,�Ƴ�,��ȡ����
	/// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// ͨ���������ȡ��Ӧ������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        /// <summary>
        /// �ж��Ƿ��д�key��ֵ
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
        /// ��ӻ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
        /// <param name="dependencies">����������</param>
        /// <param name="cacheItemPriority">���ȼ�</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, CacheDependency dependencies, CacheItemPriority cacheItemPriority)
        {
            DateTime absoluteExpiration = GetAbsoluteExpirationTime(CacheTimeOption, CacheExpirationOption);
            TimeSpan slidingExpiration = GetSlidingExpirationTime(CacheTimeOption, CacheExpirationOption);
            HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, cacheItemPriority, null);
        }

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        /// <param name="CacheExpirationOption">�������ʱ����𣨾���/���ԣ�</param>
        /// <param name="dependencies">����������</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption, CacheDependency dependencies)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption, dependencies, CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        /// <param name="CacheExpirationOption">�������ʱ����𣨾���/���ԣ�</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption, CacheExpirationOption CacheExpirationOption)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption, null, CacheItemPriority.NotRemovable);
        }

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        /// <param name="CacheTimeOption">����ʱ��</param>
        public static void Set<T>(string key, T value, CacheTimeOption CacheTimeOption)
        {
            Set<T>(key, value, CacheTimeOption, CacheExpirationOption.SlidingExpiration);
        }

        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="key">�����</param>
        /// <param name="value">ֵ</param>
        public static void Set<T>(string key, T value)
        {
            Set<T>(key, value, CacheTimeOption.Normal);
        }

        /// <summary>
        /// ������л���
        /// </summary>
        public static void Clear()
        {
            foreach (DictionaryEntry elem in HttpRuntime.Cache)
            {
                HttpRuntime.Cache.Remove(elem.Key.ToString());
            }
        }
        /// <summary>
        /// �Ƴ���عؼ��ֵĻ���
        /// </summary>
        /// <param name="keyword">����ؼ���</param>
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
        /// ���ĳ���
        /// </summary>
        /// <param name="key">�����</param>
        public static void RemoveAt(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// ��ȡ��������ʱ��
        /// </summary>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
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
        /// ��ȡ����ʱ�����ʱ��
        /// </summary>
        /// <param name="CacheTimeOption">�����ʱ�䳤��</param>
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
    /// ���ڷ�ʽ
    /// </summary>
    public enum CacheExpirationOption
    {
        /// <summary>
        /// ���Թ���
        /// </summary>
        AbsoluteExpiration = 0,
        /// <summary>
        /// ���Թ���

        /// </summary>
        SlidingExpiration = 1
    }

    /// <summary>
    /// ���ù���ʱ��
    /// </summary>
    public enum CacheTimeOption
    {
        /// <summary>
        /// ��������
        /// </summary>
        NotRemovable = 0,
        /// <summary>
        /// ��ʱ�� 10����
        /// </summary>
        Short = 10,
        /// <summary>
        /// һ����������ʱ��30����
        /// </summary>
        Normal = 30,
        /// <summary>
        /// ��ʱ��60����
        /// </summary>
        Long = 60,
    }

}
