using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IThemeSky.UI.Models
{
    public class PostSoftCommentModel
    {
        [Required]
        public string SoftIdentify { get; set; }
        [Required]
        public string SoftTitle { get; set; }
        [Required]
        public string SoftDescription { get; set; }
        [Required]
        public string UserName { get; set; }

        public string UserMail { get; set; }

        public string Content { get; set; }
    }
}
