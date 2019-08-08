using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazLab1.Models.Data.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string DuyuruBasligi { get; set; }
        public string DuyuruMetni { get; set; }
        public string Olusturan { get; set; }
        public string CreateDate { get; set; }
        public string tip { get; set; }
    }
}