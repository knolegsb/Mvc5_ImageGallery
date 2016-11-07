using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Controllers
{
    public class ImageUploadController : Controller
    {
        private ImageGalleryContext db = new ImageGalleryContext();

        // GET: ImageUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc, HttpPostedFileBase file)
        {
            Image image = new Mvc5_ImageGallery.Image();
            var allowedExtensions = new[]
            {
                ".Jpg", ".png", ".jpg", ".jpeg"
            };

            image.ID = Guid.Parse(fc["Id"]);
            image.ImagePath = file.ToString();
            image.Name = fc["Name"].ToString();
            var fileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);

            if (allowedExtensions.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = name + "_" + image.ID + ext;
                var path = Path.Combine(Server.MapPath("~/Img"), myfile);
                image.ImagePath = path;
                db.Images.Add(image);
                db.SaveChanges();
                file.SaveAs(path);
            }else
            {
                ViewBag.message = "Please choose only Image file";
            }
            return View();
        }
    }
}