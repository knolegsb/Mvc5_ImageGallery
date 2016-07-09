using Mvc5_ImageGallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Mvc5_ImageGallery.Repositories
{
    public class ContentRepository
    {
        private readonly ImageGalleryContext dbcontext = new ImageGalleryContext();

        public int UploadImageInDataBase(HttpPostedFileBase file, ContentViewModel contentViewModel)
        {
            contentViewModel.Image = ConvertToBytes(file);
            var content = new Content
            {
                Title = contentViewModel.Title,
                Description = contentViewModel.Description,
                Contents = contentViewModel.Contents,
                Image = contentViewModel.Image
            };
            dbcontext.Contents.Add(content);
            int i = dbcontext.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }
    }
}