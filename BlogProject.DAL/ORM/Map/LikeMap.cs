using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.ORM.Map
{
    public class LikeMap:BaseMap<Like>
    {
        public LikeMap()
        {
            ToTable("dbo.Likes");

            HasKey(c => new { c.AppUserID, c.ArticleID });
        }
    }
}
