using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Util;

namespace IThemeSky.DataAccess
{
    /// <summary>
    /// 带缓存的主题视图数据操作接口
    /// </summary>
    public interface ICacheThemeViewRepository : IThemeViewRepository
    {
        /// <summary>
        /// 使下次调用该接口的任意方法时不从缓存读取
        /// </summary>
        void DisableCache();
        /// <summary>
        /// 设置下次调用该接口的任意方法时的缓存时间
        /// </summary>
        /// <param name="cacheTime">缓存时间</param>
        void SetCacheTime(CacheTimeOption cacheTime);
    }
}
