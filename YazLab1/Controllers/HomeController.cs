using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YazLab1.Models.Data.Context;
using YazLab1.Models.Data.Models;

namespace YazLab1.Controllers
{
    public class HomeController : Controller
    {
        private KouDatabaseContext context = null;

        public HomeController()
        {
            context = new KouDatabaseContext();
        }

        public ActionResult Index()
        {
            List<Announcement> duyuruList = (from d in context.Duyuru orderby d.CreateDate descending select d).ToList();
            List<SliderItem> sliderList = (from s in context.SliderItem orderby s.createDate descending select s).ToList();
            List<User> userList = (from u in context.User orderby u.title ascending select u).ToList();
            Tuple<List<Announcement>, List<SliderItem>, List<User>> tuple = new Tuple<List<Announcement>, List<SliderItem>, List<User>>(duyuruList, sliderList, userList);
            return View(tuple);
        }

        public ActionResult DuyuruPopup(int id)
        {
            Announcement duyuru = context.Duyuru.Where(d => d.AnnouncementId == id).FirstOrDefault();
            return PartialView(duyuru);
        }

        [HttpPost]
        public ActionResult AramaSonucu(string k, string o, string s, string t)
        {
            List<Announcement> duyuru = context.Duyuru.Where(d => d.tip == t).ToList();

            if (!string.IsNullOrEmpty(k))
            {
                duyuru = duyuru.Where(a => a.DuyuruBasligi.Contains(k) || a.DuyuruMetni.Contains(k)).ToList();
            }

            if (o != "all")
            {
                duyuru = duyuru.Where(a => a.Olusturan == o).ToList();
            }

            if (s == "ye")
            {
                duyuru = duyuru.OrderByDescending(a => a.CreateDate).ToList();
            }

            return PartialView(duyuru);
        }

    }
}
