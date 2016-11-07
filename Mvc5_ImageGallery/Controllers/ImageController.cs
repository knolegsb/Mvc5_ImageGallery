using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageGalleryContext dbcontext = new ImageGalleryContext();
        // GET: Image
        public ActionResult Index()
        {
            var imageModel = new Image();
            var imageFiles = Directory.GetFiles(Server.MapPath("~/Upload_Images"));
            foreach (var item in imageFiles)
            {
                imageModel.ImageList.Add(Path.GetFileName(item));
            }
            return View();
        }
    }
}