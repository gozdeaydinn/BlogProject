using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.UI.Areas.Member.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Member/Home
        public ActionResult Index()
        {
            TempData["class"] = "custom-hide";

            if (!HttpContext.User.Identity.IsAuthenticated)//kullanıcı kimliği doğrulandığında doğrulanır ... kimliği doğrulanmış formlarda kimliği doğrulanmış kullanıcıyı tanımlamak için setauthcookie kullanırız ....user otantike ise modele yönlendir
            {
                return View();
            }

            AppUser appuser = new AppUser();
            appuser = service.AppUserService.FindByUserName(HttpContext.User.Identity.Name);
            if (appuser.Role == DAL.ORM.Enum.Role.Member)
            {
                TempData["class"] = "custom-show";
                return Redirect("/Member/Home/Index");
            }
            return View();
        }
    }
}