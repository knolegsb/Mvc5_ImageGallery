using Mvc5_ImageGallery.Models;
using Mvc5_ImageGallery.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_ImageGallery.Controllers
{
    [RoutePrefix("Content")]
    [ValidateInput(false)]
    public class ContentController : Controller
    {
        private ImageGalleryContext dbcontext = new ImageGalleryContext();
        // GET: Content
        public ActionResult Index()
        {
            var content = dbcontext.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Contents,
                s.Description
            });

            List<ContentViewModel> contentModel = content.Select(c => new ContentViewModel()
            {
                ID = c.ID,
                Title = c.Title,
                Image = c.Image,
                Description = c.Description,
                Contents = c.Contents
            }).ToList();

            return View(contentModel);
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if(cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int id)
        {
            var q = from c in dbcontext.Contents where c.ID == id select c.Image;
            byte[] cover = q.First();
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(ContentViewModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDataBase(file, model);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}