using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        // GET: ImageGallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            List<ImageGallery> all = new List<ImageGallery>();
            using (ImageGalleryContext context = new ImageGalleryContext())
            {
                all = context.ImageGalleries.ToList();
            }

            return View(all);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(ImageGallery image)
        {
            if (image.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
                return View(image);
            }
            if (!(image.File.ContentType == "image/jpeg" || image.File.ContentType == "image/png"))
            {
                ModelState.AddModelError("CustomError", "File type allowed : jpg and png");
                return View();
            }
            image.FileName = image.File.FileName;
            image.ImageSize = image.File.ContentLength;

            byte[] data = new byte[image.File.ContentLength];
            image.File.InputStream.Read(data, 0, image.File.ContentLength);

            image.ImageData = data;
            using (ImageGalleryContext context = new ImageGalleryContext())
            {
                context.ImageGalleries.Add(image);
                context.SaveChanges();
            }

            return RedirectToAction("Gallery");
        }
    }
}