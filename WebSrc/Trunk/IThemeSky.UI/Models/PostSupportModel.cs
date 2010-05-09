using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IThemeSky.UI.Models
{
    public class PostSupportModel
    {
        public string Name { get; set; }

        [Required]
        public string Mail { get; set; }

        public int SupportType { get; set; }

        public int ThemeId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
