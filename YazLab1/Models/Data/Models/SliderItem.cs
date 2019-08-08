using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazLab1.Models.Data.Models
{
    public class SliderItem
    {
        public int SliderItemId { get; set; }
        public string imageUrl { get; set; }
        public string baslik { get; set; }
        public string yazi { get; set; }
        public string url { get; set; }
        public string createDate { get; set; }
    }
}