//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc5_ImageGallery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Image
    {
        public Image()
        {
            ImageList = new List<string>();
        }

        public System.Guid ID { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public List<string> ImageList { get; set; }
    }
}