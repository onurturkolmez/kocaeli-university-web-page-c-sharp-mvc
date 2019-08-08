using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YazLab1.Models;
using YazLab1.Models.Data.Context;
using YazLab1.Models.Data.Models;

namespace YazLab1.Controllers
{
    public class AdminController : Controller
    {
        private KouDatabaseContext context = null;

        public AdminController()
        {
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            context = new KouDatabaseContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Announcements()
        {
            string tip = Session["Type"].ToString();

            if (Session["LogedUserID"] != null)
            {
                List<Announcement> duyuruList;
                if (tip == "2")
                {
                    string olusturan = Session["Title"].ToString();
                    duyuruList = (from d in context.Duyuru where d.Olusturan == olusturan orderby d.CreateDate descending select d).ToList();
                }
                else
                {
                    duyuruList = (from d in context.Duyuru orderby d.CreateDate descending select d).ToList();
                }

                return View(duyuruList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddAnnouncement()
        {
            if (Session["LogedUserID"] != null)
            {
                DuyuruModel model = new DuyuruModel();
                model.Olusturan = Session["Title"].ToString();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddAnnouncement(DuyuruModel duyuru)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                string d = "";

                if (duyuru.tip == null)
                {
                    d = "1";
                }
                else
                {
                    d = duyuru.tip;
                }

                try
                {
                    Announcement an = new Announcement
                    {
                        DuyuruBasligi = duyuru.DuyuruBasligi,
                        DuyuruMetni = "<span>" + duyuru.DuyuruMetni + "</span>",
                        Olusturan = Session["Title"].ToString(),

                        CreateDate = DateTime.Now.ToString("dd.MM.yy HH:mm:ss"),
                        tip = d
                    };

                    context.Duyuru.Add(an);
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["add"] = "yes";
                    return RedirectToAction("Announcements", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }
            else
            {
                DuyuruModel model = new DuyuruModel();
                model.Olusturan = Session["Title"].ToString();
                return View("AddAnnouncement", model);
            }
        }

        public ActionResult EditAnnouncement(int id)
        {
            if (Session["LogedUserID"] != null)
            {
                var duyuru = context.Duyuru.Where(d => d.AnnouncementId == id).FirstOrDefault();
                DuyuruModel model = new DuyuruModel();
                model.DuyuruId = duyuru.AnnouncementId;
                model.DuyuruBasligi = duyuru.DuyuruBasligi;
                model.DuyuruMetni = duyuru.DuyuruMetni;
                model.tip = duyuru.tip;
                model.Olusturan = duyuru.Olusturan;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditAnnouncement(DuyuruModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;

                try
                {
                    var duyuru = context.Duyuru.Where(d => d.AnnouncementId == model.DuyuruId).FirstOrDefault();
                    duyuru.DuyuruBasligi = model.DuyuruBasligi;
                    duyuru.DuyuruMetni = "<span>" + model.DuyuruMetni + "</span>";
                    if (model.tip != null)
                    {
                        duyuru.tip = model.tip;
                    }
                    duyuru.CreateDate = DateTime.Now.ToString("dd.MM.yy hh:mm:ss");
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["edit"] = "yes";
                    return RedirectToAction("Announcements", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }
            else
            {
                DuyuruModel model2 = new DuyuruModel();
                model2.Olusturan = Session["Title"].ToString();
                return View("EditAnnouncement", model2);
            }
        }

        public ActionResult SliderItems()
        {
            if (Session["LogedUserID"] != null && Session["Type"].ToString() == "1")
            {
                List<SliderItem> sliderList = (from d in context.SliderItem orderby d.createDate descending select d).ToList();
                return View(sliderList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddSliderItem()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddSliderItem(SliderDataModel model)
        {
            if (ModelState.IsValid)
            {

                bool result = false;
                string fileName = "";

                try
                {
                    fileName = Guid.NewGuid().ToString("N") + ".jpg";
                    string path = Path.Combine(Server.MapPath("~/img/slider/"), fileName);
                    model.image.SaveAs(path);

                    SliderItem item = new SliderItem
                    {
                        imageUrl = "/img/slider/" + fileName,
                        baslik = model.baslik,
                        yazi = model.yazi,
                        url = model.url,
                        createDate = DateTime.Now.ToString("dd.MM.yy HH:mm:ss"),
                    };

                    context.SliderItem.Add(item);
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["add"] = "yes";
                    return RedirectToAction("SliderItems", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }
            else
            {
                return View("AddSliderItem");
            }
        }

        public ActionResult EditSliderItem(int id)
        {
            if (Session["LogedUserID"] != null)
            {
                var slider = context.SliderItem.Where(d => d.SliderItemId == id).FirstOrDefault();
                SliderDataModel model = new SliderDataModel();
                model.Id = slider.SliderItemId;
                model.baslik = slider.baslik;
                model.url = slider.url;
                model.yazi = slider.yazi;
                model.imageUrl = slider.imageUrl;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditSliderItem(SliderDataModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                string fileName = "";

                try
                {
                    var slider = context.SliderItem.Where(d => d.SliderItemId == model.Id).FirstOrDefault();

                    if (model.image != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Server.MapPath(slider.imageUrl))))
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath(slider.imageUrl)));
                        }
                    }

                    fileName = Guid.NewGuid().ToString("N") + ".jpg";
                    string path = Path.Combine(Server.MapPath("~/img/slider/"), fileName);
                    model.image.SaveAs(path);

                    slider.imageUrl = "/img/slider/" + fileName;
                    slider.baslik = model.baslik;
                    slider.yazi = model.yazi;
                    slider.url = model.url;
                    slider.createDate = DateTime.Now.ToString("dd.MM.yy hh:mm:ss");
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["edit"] = "yes";
                    return RedirectToAction("SliderItems", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }

            else
            {
                return View("EditSliderItem", model);
            }
        }

        public ActionResult Users()
        {
            if (Session["LogedUserID"] != null && Session["Type"].ToString() == "1")
            {
                List<User> kullaniciList = (from u in context.User orderby u.UserId descending select u).ToList();
                return View(kullaniciList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddUser()
        {
            if (Session["LogedUserID"] != null && Session["Type"].ToString() == "1")
            {
                UserModel model = new UserModel();
                model.tip = "1";
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;

                try
                {
                    User user = new User
                    {
                        tip = model.tip,
                        note = "",
                        title = model.title,
                        username = model.username,
                        password = model.password
                    };

                    context.User.Add(user);
                    context.SaveChanges();

                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["edit"] = "yes";
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }

            else
            {
                return View("AddUser", model);
            }
        }

        public ActionResult UserAccount(string userId)
        {
            int id = Convert.ToInt32(userId);

            User user = context.User.Where(u => u.UserId == id).FirstOrDefault();

            UserModel model = new UserModel();
            model.userId = user.UserId;
            model.title = user.title;
            model.username = user.username;
            model.password = user.password;

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UserAccount(UserModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;

                try
                {
                    var user = context.User.Where(d => d.UserId == model.userId).FirstOrDefault();

                    user.title = model.title;
                    user.username = model.username;
                    user.password = model.password;
                    context.SaveChanges();

                    Session["LogedUserID"] = null;
                    Session["Title"] = null;

                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

                if (result)
                {
                    TempData["edit"] = "yes";
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    return Content("Bir Hata Oluştu Lütfen Tekrar Deneyin!", "text/html");
                }
            }

            else
            {
                return View("UserAccount", model);
            }
        }

        public ActionResult Notes()
        {
            int id = Convert.ToInt32(Session["LogedUserId"].ToString());
            User user = context.User.Where(u => u.UserId == id).FirstOrDefault();
            return View(user);
        }

        [ValidateInput(false)]
        public string UserNoteSave(int Id, string Note)
        {
            try
            {
                User user = context.User.Where(a => a.UserId == Id).FirstOrDefault();
                user.note = Note;
                context.SaveChanges();
                return "basarili";
            }
            catch (Exception) { return "basarisiz"; }
        }

        public string DeleteSliderItem(int id)
        {
            var sliderItem = context.SliderItem.Where(t => t.SliderItemId == id).FirstOrDefault();

            try
            {
                if (System.IO.File.Exists(Path.Combine(Server.MapPath(sliderItem.imageUrl))))
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath(sliderItem.imageUrl)));
                }

                context.SliderItem.Remove(sliderItem);
                context.SaveChanges();
                return "yes";
            }
            catch (Exception) { return "no"; }
        }

        public string DeleteAnnouncement(int id)
        {
            var duyuru = context.Duyuru.Where(t => t.AnnouncementId == id).FirstOrDefault();

            try
            {
                context.Duyuru.Remove(duyuru);
                context.SaveChanges();
                return "yes";
            }
            catch (Exception) { return "no"; }
        }

        public string DeleteUser(int id)
        {
            var user = context.User.Where(t => t.UserId == id).FirstOrDefault();

            try
            {
                context.User.Remove(user);
                context.SaveChanges();
                return "yes";
            }
            catch (Exception) { return "no"; }
        }

        public string UserAccess(string username, string password)
        {
            var u = context.User.Where(a => a.username == username && a.password == password).FirstOrDefault();

            if (u != null)
            {
                Session["LogedUserID"] = u.UserId;
                Session["Title"] = u.title;
                Session["Type"] = u.tip;
                return "basarili";
            }
            else
            {
                Session["LogedUserID"] = null;
                Session["Title"] = null;
                Session["Type"] = null;
                return "basarisiz";
            }
        }

        public bool Logout()
        {
            bool result = false;
            try
            {
                Session["LogedUserID"] = null;
                Session["Title"] = null;
                Session["Type"] = null;
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
