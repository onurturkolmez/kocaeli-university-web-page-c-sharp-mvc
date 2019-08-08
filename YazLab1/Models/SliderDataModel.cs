using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazLab1.Models
{
    public class SliderDataModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sliderda Gözükecek Bir Resim Ekleyin")]
        public HttpPostedFileBase image { get; set; }

        [Required(ErrorMessage = "Resmin Açıklaması İçin Başlık Girin")]
        public string baslik { get; set; }
        [Required(ErrorMessage = "Resmin Açıklaması İçin Yazı Girin")]
        public string yazi { get; set; }
        [Required(ErrorMessage = "Resmin Açılacağı Linki Girin")]
        public string url { get; set; }
        public string imageUrl { get; set; }
    }
}