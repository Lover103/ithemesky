using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IThemeSky.UI.Models
{
    public class PostCommentModel
    {
        [Required]
        public int ThemeId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string UserMail { get; set; }

        public string Content { get; set; }
    }
}
