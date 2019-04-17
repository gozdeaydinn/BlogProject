using BlogProject.DAL.ORM.Entity;
using BlogProject.DAL.ORM.Enum;
using BlogProject.UI.Areas.Author.Models.DTO;
using BlogProject.UI.Areas.Author.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogProject.UI.Areas.Author.Controllers
{
    public class ArticleController : BaseController
    {
        public ActionResult Add()
        {
            
            return View(service.CategoryService.GetActive());
        }
        [HttpPost]
        public ActionResult Add(Article data)
        {
            AppUser user = service.AppUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            data.AppUserID = user.ID;
            service.ArticleService.Add(data);
            return Redirect("/Author/Article/List");
        }
        public ActionResult List()
        {
            List<Article> model = service.ArticleService.GetActive();
            return View(model);
        }
    }
}