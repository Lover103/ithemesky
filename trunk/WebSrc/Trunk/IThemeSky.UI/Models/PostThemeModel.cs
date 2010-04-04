using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IThemeSky.UI.Models
{
    public class PostThemeModel : ModelBase
    {
        [Required]
        public string Title { get; set; }

        public string Tags { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string AuthorMail { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }

        public string DownloadUrl { get; set; }

        public string ThumbnailName { get; set; }
    }
}
