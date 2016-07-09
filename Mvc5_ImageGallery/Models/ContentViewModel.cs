using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Models
{
    public class ContentViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [AllowHtml]
        [Required]
        public string Contents { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}