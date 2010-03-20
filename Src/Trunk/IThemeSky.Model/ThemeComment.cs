using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IThemeSky.Model
{
    /// <summary>
    /// ThemeComment
    /// </summary>
    public class ThemeComment
    {
        /// <summary>
        /// CommentId
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// ThemeId
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// RateType
        /// </summary>
        public int RateType { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// UserMail
        /// </summary>
        public string UserMail { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// UserIp
        /// </summary>
        public string UserIp { get; set; }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// DiggNumber
        /// </summary>
        public int DiggNumber { get; set; }

        /// <summary>
        /// BuryNumber
        /// </summary>
        public int BuryNumber { get; set; }
    }
}
