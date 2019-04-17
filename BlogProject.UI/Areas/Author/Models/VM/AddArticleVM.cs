using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Author.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.UI.Areas.Author.Models.VM
{
    public class AddArticleVM
    {
        public AddArticleVM()
        {
            Categories = new List<Category>();
            Article = new ArticleDTO();
            AppUser = new AppUserDTO();
        }
        public List<Category> Categories { get; set; }
        public ArticleDTO Article { get; set; }
        public AppUserDTO AppUser { get; set; }
    }
}