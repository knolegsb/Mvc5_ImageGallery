using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5_ImageGallery.Models
{
    public class ImageGalleryViewModel
    {
        public int ImageID { get; set; }
        public int ImageSize { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

    }
}