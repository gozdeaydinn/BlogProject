using BlogProject.DAL.ORM.Entity;
using BlogProject.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    public class AppUserController : BaseController
    {
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AppUser data)
        {
            service.AppUserService.Add(data);
            return Redirect("/Admin/AppUser/List");
        }
        public ActionResult List()
        {
            List<AppUser> model=service.AppUserService.GetActive();
            return View(model);
        }
        public ActionResult Update(Guid id)
        {
            AppUser appuser = service.AppUserService.GetById(id);
            AppUserDTO model = new AppUserDTO();
            model.ID = appuser.ID;
            model.FirstName = appuser.FirstName;
            model.LastName = appuser.LastName;
            model.UserName = appuser.UserName;
            model.Email = appuser.Email;
            model.Password = appuser.Password;
            model.Role = appuser.Role;
            model.Gender = appuser.Gender;
            return View(model);
            
        }
        [HttpPost]
        public ActionResult Update(AppUserDTO data)
        {
            AppUser appuser = service.AppUserService.GetById(data.ID);
            appuser.FirstName = data.FirstName;
            appuser.LastName = data.LastName;
            appuser.UserName = data.UserName;
            appuser.Email = data.Email;
            appuser.Password = data.Password;
            appuser.Role = data.Role;
            appuser.Gender = data.Gender;
            appuser.UpdateDate = DateTime.Now;
            service.AppUserService.Update(appuser);
            return Redirect("/Admin/AppUser/List");
        }
        public ActionResult Delete(Guid id)
        {
            service.AppUserService.Remove(id);
            return Redirect("/Admin/AppUser/List");
        }
        

    }
}