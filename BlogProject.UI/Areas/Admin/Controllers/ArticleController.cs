using BlogProject.DAL.ORM.Context;
using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Admin.Models;
using BlogProject.UI.Areas.Admin.Models.DTO;
using BlogProject.UI.Areas.Admin.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        public ActionResult Add()
        {
            AddArticleVM model = new AddArticleVM()
            {
                Categories = service.CategoryService.GetActive(),
                AppUsers = service.AppUserService.GetActive(),
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Article data)
        {
            data.PublishDate = DateTime.Now;
            service.ArticleService.Add(data);
            return Redirect("/Admin/Article/List");
        }
        public ActionResult List()
        {
            List<Article> model = service.ArticleService.GetActive();
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
            List<AppUser> appusermodel = service.AppUserService.GetActive();
            model.AppUsers = appusermodel;
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
            article.CategoryID = data.CategoryID;
            article.AppUserID = data.AppUserID;
            article.Status = DAL.ORM.Enum.Status.Modified;
            service.ArticleService.Update(article);
            return Redirect("/Admin/Article/List");
        }
        public ActionResult Delete(Guid id)
        {
            service.ArticleService.Remove(id);
            TempData["Successful"] = "Transaction is successful.";
            return Redirect("/Admin/Article/List");
        }
    }
}