using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazLab1.Models
{
    public class UserModel
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Girin")]
        public string username { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre Girin")]
        public string password { get; set; }
        [Required(ErrorMessage = "Lütfen Ünvan Bilgisi Girin")]
        public string title { get; set; }

        public string tip { get; set; }
    }
}