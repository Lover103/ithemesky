using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// 主题Theme表完整映射实体
    /// </summary>
    [Serializable]
    public class Theme
    {
        /// <summary>
        /// 主题id
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 父类d
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 显示状态
        /// </summary>
        public DisplayStateOption DisplayState { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public CheckStateOption CheckState { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 审核人id
        /// </summary>
        public int CheckerId { get; set; }

        /// <summary>
        /// 推荐等级
        /// </summary>
        public int CommendIndex { get; set; }

        /// <summary>
        /// 缩略图名称
        /// </summary>
        public string ThumbnailName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 评价总分值
        /// </summary>
        public int RateScore { get; set; }

        /// <summary>
        /// 评价次数
        /// </summary>
        public int RateNumbers { get; set; }

        /// <summary>
        /// 评论总数
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// 下载数
        /// </summary>
        public int Downloads { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 上周下载数
        /// </summary>
        public int LastWeekDownloads { get; set; }

        /// <summary>
        /// 上月下载数
        /// </summary>
        public int LastMonthDownloads { get; set; }

        /// <summary>
        /// 来源网站
        /// </summary>
        public SourceOption Source { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
        
        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 作者联系邮件
        /// </summary>
        public string AuthorMail { get; set; }

        /// <summary>
        /// 是否是IPhone4主题
        /// </summary>
        public bool SupportIPhone4 { get; set; }
    }
}
