using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BLL.Repository.Entity
{
    public class AppUserRepository:BaseRepository<AppUser>
    {
        public bool CheckCredentials(string userName,string password)//Kimlik bilgilerini kontrol et
        {

                return table.Any(x => x.UserName == userName && x.Password == password);
    
        }
        public AppUser FindByUserName(string userName)//UserName bulma
        {
            return table.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
