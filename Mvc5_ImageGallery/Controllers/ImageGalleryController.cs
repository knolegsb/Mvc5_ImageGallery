using Mvc5_ImageGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {
        private ImageGalleryContext dbcontext = new ImageGalleryContext();

        // GET: ImageGallery
        public ActionResult Index()
        {
            var image = dbcontext.ImageGalleries.Select(i => new
            {
                i.ImageID,
                i.ImageSize,
                i.FileName,
                i.ImageData
            });

            List<ImageGalleryViewModel> imageViewModel = image.Select(iv => new ImageGalleryViewModel()
            {
                ImageID = iv.ImageID,
                ImageSize = iv.ImageSize,
                FileName = iv.FileName,
                ImageData = iv.ImageData
            }).ToList();

            return View(imageViewModel);
        }

        public ActionResult RetrieveImage(int id)
        {
            //var q = from c in dbcontext.ImageGalleries where c.ImageID == id select c.ImageData;
            var q = dbcontext.ImageGalleries.Where(c => c.ImageID == id).Select(c => c.ImageData);
            byte[] cover = q.First();

            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
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