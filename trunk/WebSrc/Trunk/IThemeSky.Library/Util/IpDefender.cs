using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IThemeSky.Library.Util
{
    /// <summary>
    /// IP防护
    /// </summary>
    public static class IpDefender
    {
        private const string IPDEFENDER_CACHE_KEY = "IpDefender";
        /// <summary>
        /// 判断是否机器访问
        /// </summary>
        /// <returns></returns>
        public static bool IsRobot()
        {
            Dictionary<string, int> dicIP = GetDefenderDictionary();
            if (dicIP != null)
            {
                string userIp = HttpContext.Current.Request.UserHostAddress;
                string userAgent = string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent) ? "" : HttpContext.Current.Request.UserAgent.ToLower();
                if (userIp.Equals("66.249.68.230"))
                {
                    return false;
                }
                if (userAgent.Contains("googlebot") || userAgent.Contains("baidubot") || userAgent.Contains("sosobot") || userAgent.Contains("sogou") || userAgent.Contains("msnbot") || userAgent.Contains("slurp"))
                {
                    return false;
                }
                if (dicIP.ContainsKey(userIp))
                {
                    if (dicIP[userIp] > 10)    //10分钟内访问超过10次，且没有一次访问到防火墙的js
                    {
                        return true;
                    }
                    else
                    {
                        dicIP[userIp]++;
                    }
                }
                else
                {
                    lock (dicIP)
                    {
                        if (!dicIP.ContainsKey(userIp))
                        {
                            dicIP.Add(userIp, 1);
                        }
                        else
                        {
                            dicIP[userIp]++;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 移除指定ip
        /// </summary>
        /// <returns></returns>
        public static bool RemoveIp()
        {
            string userIp = HttpContext.Current.Request.UserHostAddress;
            Dictionary<string, int> dicIP = GetDefenderDictionary();
            lock (dicIP)
            {
                return dicIP.Remove(userIp);
            }
        }

        public static Dictionary<string, int> GetDefenderDictionary()
        {
            Dictionary<string, int> dicIP;
            if (CacheHelper.Contains(IPDEFENDER_CACHE_KEY))
            {
                dicIP = CacheHelper.Get<Dictionary<string, int>>(IPDEFENDER_CACHE_KEY);
            }
            else
            {
                lock (typeof(IpDefender))
                {
                    if (!CacheHelper.Contains(IPDEFENDER_CACHE_KEY))
                    {
                        dicIP = new Dictionary<string, int>();
                        CacheHelper.Set<Dictionary<string, int>>(IPDEFENDER_CACHE_KEY, dicIP, CacheTimeOption.Short);
                    }
                    else
                    {
                        dicIP = CacheHelper.Get<Dictionary<string, int>>(IPDEFENDER_CACHE_KEY);
                    }
                }
            }
            return dicIP;
        }
    }

}
