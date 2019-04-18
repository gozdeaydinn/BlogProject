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
            data.PublishDate = DateTime.Now;
            service.ArticleService.Add(data);
            return Redirect("/Author/Article/List");
        }
        public ActionResult List()
        {
            Guid userid = service.AppUserService.FindByUserName(User.Identity.Name).ID;
            List<Article> model = service.ArticleService.GetDefault(x => x.AppUserID == userid && (x.Status == Status.Active || x.Status == Status.Modified));
            return View(model);
        }
        public ActionResult Update(Guid id)
        {
            Article article = service.ArticleService.GetById(id);
            UpdateArticleVM model = new UpdateArticleVM();
            model.Article.ID = article.ID;
            model.Article.Header = article.Header;
            model.Article.Content = article.Content;
            model.Article.PublishDate = DateTime.Now;
            List<Category> categorymodel = service.CategoryService.GetActive();
            model.Categories = categorymodel;
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(ArticleDTO data)
        {
            Article article = service.ArticleService.GetById(data.ID);
            article.Header = data.Header;
            article.Content = data.Content;
            article.PublishDate = data.PublishDate;
            article.UpdateDate = DateTime.Now;
            article.Status = Status.Modified;
            article.CategoryID = data.CategoryID;
            service.ArticleService.Update(article);
            return Redirect("/Author/Article/List");
        }
        public ActionResult Delete(Guid id)
        {
            service.ArticleService.Remove(id);
            return Redirect("/Author/Article/List");
        }
        public ActionResult Show(Article data)
        {
            AppUser user = service.AppUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            List<Article> model = service.ArticleService.GetDefault(x => x.AppUserID != user.ID);
            return View(model);
        }
    }
}