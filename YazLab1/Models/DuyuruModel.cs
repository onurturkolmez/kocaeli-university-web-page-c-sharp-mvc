using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace YazLab1.Models
{
    public partial class DuyuruModel
    {
        public int DuyuruId { get; set; }

        [Required(ErrorMessage="Lütfen Duyuru Başlığı Girin")]
        public string DuyuruBasligi { get; set; }

        [Required(ErrorMessage="Lütfen Duyuruyla İlgili Birşeyler Yazın")]
        public string DuyuruMetni { get; set; }
        public string Olusturan { get; set; }
        public string tip { get; set; }
    }
}