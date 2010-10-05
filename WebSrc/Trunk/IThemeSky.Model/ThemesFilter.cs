using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Library.Data;
using IThemeSky.Library.Extensions;

namespace IThemeSky.Model
{
    /// <summary>
    /// 主题列表过滤器
    /// </summary>
    public class ThemesFilter
    {
        /// <summary>
        /// 初始化默认主题列表过滤器
        /// </summary>
        public ThemesFilter()
        {
            InitProperties();
        }
        /// <summary>
        /// 初始化默认主题列表过滤器
        /// </summary>
        public ThemesFilter(DisplayStateOption displayState, CheckStateOption checkState)
        {
            DisplayState = displayState;
            CheckState = checkState;
            InitProperties();
        }
        /// <summary>
        /// 初始化属性默认值
        /// </summary>
        private void InitProperties()
        {
            DisplayState = DisplayStateOption.All;
            CheckState = CheckStateOption.All;
            IsCommend = BooleanFilterOption.NoFilter;
            CategoryIds = new List<int>();
            ParentCategoryIds = new List<int>();
            TagIds = new List<int>();
            TagNames = new List<string>();
        }
        /// <summary>
        /// 克隆过滤器
        /// </summary>
        /// <returns></returns>
        public ThemesFilter Clone()
        {
            return new ThemesFilter() 
            { 
                DisplayState = this.DisplayState,
                CheckState = this.CheckState,
                IsCommend = this.IsCommend,
                CategoryIds = new List<int>(this.CategoryIds),
                ParentCategoryIds = new List<int>(this.ParentCategoryIds),
                TagIds = new List<int>(this.TagIds),
                TagNames = new List<string>(this.TagNames),
                SearchKeyword = this.SearchKeyword,
            };
        }
        /// <summary>
        /// 获取或设置显示状态过滤器，如果为all，则不过滤
        /// </summary>
        public DisplayStateOption DisplayState { get; set; }
        /// <summary>
        /// 获取或设置审核状态过滤器，如果为all，则不过滤
        /// </summary>
        public CheckStateOption CheckState { get; set; }
        /// <summary>
        /// 获取或设置是否推荐主题，默认为不过滤
        /// </summary>
        public BooleanFilterOption IsCommend { get; set; }
        /// <summary>
        /// 获取或设置是否支持iphone4
        /// </summary>
        public bool SupportIPhone4 { get; set; }
        /// <summary>
        /// 获取或设置主题直属的分类id过滤器,如果集合为空，则不作分类过滤(该属性的集合已在内部初始化)
        /// </summary>
        public List<int> CategoryIds { get; private set; }
        /// <summary>
        /// 获取或设置主题所属的大类id过滤器,如果集合为空，则不作大类过滤(该属性的集合已在内部初始化)
        /// </summary>
        public List<int> ParentCategoryIds { get; private set; }
        /// <summary>
        /// 获取或设置主题所属的标签id过滤器,如果集合为空，则不作标签过滤(该属性的集合已在内部初始化)
        /// </summary>
        public List<int> TagIds { get; private set; }
        /// <summary>
        /// 获取或设置主题所属的标签名称过滤器,如果集合为空，则不作标签过滤(该属性的集合已在内部初始化)
        /// </summary>
        public List<string> TagNames { get; private set; }
        /// <summary>
        /// 获取或设置主题搜索关键字过滤器，如果为空(string.Empty)则不作搜索
        /// </summary>
        public string SearchKeyword { get; set; }

        /// <summary>
        /// 获取过滤器解析后的查询子句
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder conditions = new StringBuilder("1=1");
            if (this.SupportIPhone4)
            {
                conditions.Append(" AND SupportIPhone4 > 0");
            }
            if (this.ParentCategoryIds.Count > 0)
            {
                conditions.Append(SqlConditionBuilder.GetMultiQueryValues("ParentCategoryId", this.ParentCategoryIds));
            }
            if (this.CategoryIds.Count > 0)
            {
                conditions.Append(SqlConditionBuilder.GetMultiQueryValues("CategoryId", this.CategoryIds));
            }
            if (this.TagIds.Count > 0)
            {
                foreach (int tagId in this.TagIds)
                {
                    conditions.AppendFormat(" AND TagId={0}", tagId);
                }
            }
            if (this.TagNames.Count > 0)
            {
                conditions.Append(SqlConditionBuilder.GetMultiQueryValues("TagName", this.TagNames));
            }
            if (this.CheckState != CheckStateOption.All)
            {
                conditions.AppendFormat(" AND CheckState={0}", this.CheckState.ToInt32());
            }
            if (this.DisplayState != DisplayStateOption.All)
            {
                conditions.AppendFormat(" AND DisplayState={0}", this.DisplayState.ToInt32());
            }
            if (this.IsCommend != BooleanFilterOption.NoFilter)
            {
                if (this.IsCommend == BooleanFilterOption.True)
                {
                    conditions.Append(" AND CommendIndex > 0");
                }
                else
                {
                    conditions.Append(" AND CommendIndex <= 0");
                }
            }
            if (false == string.IsNullOrEmpty(this.SearchKeyword))
            {
                conditions.AppendFormat(" AND Title like '%{0}%'", this.SearchKeyword.Replace("'", "''"));
            }
            return conditions.ToString();
        }
    }
}
