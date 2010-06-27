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

        #region 2010-04-26�������¼����淽��
        /// <summary>
        /// ��������Դ����ί��
        /// </summary>
        /// <returns></returns>
        public delegate object RefreshCacheDataHandler();
        /// <summary>
        /// ��ref int�Ļ�������Դ����ί�У�һ�����ڷ�ҳ����
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public delegate object RefreshCacheDataWithRefParamHandler(ref int recordCount);
        /// <summary>
        /// ��out int�Ļ�������Դ����ί�У�һ�����ڷ�ҳ����
        /// </summary>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public delegate object RefreshCacheDataWithOutParamHandler(out int recordCount);
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, CacheTimeOption cacheTime, RefreshCacheDataHandler callback) where T : class
        {
            if (Contains(key))
            {
                return Get<T>(key);
            }
            T content = callback() as T;
            Set<T>(key, content, cacheTime);
            return content;
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(ref)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, string recordCountKey, ref int recordCount, CacheTimeOption cacheTime, RefreshCacheDataWithRefParamHandler callback) where T : class
        {
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            T content = callback(ref recordCount) as T;
            Set<T>(key, content, cacheTime);
            Set<int>(recordCountKey, recordCount, cacheTime);
            return content;
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(out)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T Get<T>(string key, string recordCountKey, out int recordCount, CacheTimeOption cacheTime, RefreshCacheDataWithOutParamHandler callback) where T : class
        {
            recordCount = 0;
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            T content = callback(out recordCount) as T;
            Set<T>(key, content, cacheTime);
            Set<int>(recordCountKey, recordCount, cacheTime);
            return content;
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, CacheTimeOption cacheTime, RefreshCacheDataHandler callback) where T : class
        {
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback() as T;
                    Set<T>(key, content, cacheTime);
                    return content;
                }
            }
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(ref)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, string recordCountKey, ref int recordCount, CacheTimeOption cacheTime, RefreshCacheDataWithRefParamHandler callback) where T : class
        {
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(recordCountKey))
                {
                    recordCount = Get<int>(recordCountKey);
                }
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback(ref recordCount) as T;
                    Set<T>(key, content, cacheTime);
                    Set<int>(recordCountKey, recordCount, cacheTime);
                    return content;
                }
            }
        }
        /// <summary>
        /// ��ȡָ��key�������ݣ�����û��治���ڣ����Զ���������Դί�����ɻ���(�̰߳�ȫ���������һ�����ܿ���)
        /// </summary>
        /// <typeparam name="T">�������ݵ�����</typeparam>
        /// <param name="key">����key</param>
        /// <param name="recordCountKey">��¼�����Ļ���key</param>
        /// <param name="recordCount">��¼����(out)</param>
        /// <param name="cacheTime">����ʱ��</param>
        /// <param name="callback">����Դί��</param>
        /// <returns></returns>
        public static T GetWithLock<T>(string key, string recordCountKey, out int recordCount, CacheTimeOption cacheTime, RefreshCacheDataWithOutParamHandler callback) where T : class
        {
            recordCount = 0;
            if (Contains(recordCountKey))
            {
                recordCount = Get<int>(recordCountKey);
            }
            if (Contains(key))
            {
                return Get<T>(key);
            }
            lock (string.Intern(key))
            {
                if (Contains(recordCountKey))
                {
                    recordCount = Get<int>(recordCountKey);
                }
                if (Contains(key))
                {
                    return Get<T>(key);
                }
                else
                {
                    T content = callback(out recordCount) as T;
                    Set<T>(key, content, cacheTime);
                    Set<int>(recordCountKey, recordCount, cacheTime);
                    return content;
                }
            }
        }
        #endregion 
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
