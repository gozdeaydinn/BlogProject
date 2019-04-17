using BlogProject.DAL.ORM.Context;
using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BLL.Repository.Entity
{
    public class BaseRepository<T>where T:BaseEntity
    {
        private ProjectContext db;

        protected DbSet<T> table;//DAL daki tablolara erişilsin diye protected tanımladık

        public BaseRepository()
        {
            db = new ProjectContext();//veri tabanı işlemi
            table = db.Set<T>();//T:içine gelecek tip tablolar
        }
        public List<T> GetAll()
        {
          return  table.ToList();//tabloyu liste çektik
        }
        public void Add(T item)//Satatusu aktif olarak tabloya item ekledik
        {
            item.Status = BlogProject.DAL.ORM.Enum.Status.Active;
            table.Add(item);
            Save();
        }
        public void Add(List<T> items)//AddRange:Bir başka listeyi tek seferde eklemeyi sağlar-ICollection metoddur
        {
            table.AddRange(items);//Birden fazla item'ı Liste halinde tabloya aktardık.
            Save();
        }
        public bool Any(Expression<Func<T, bool>> exp) => table.Any(exp);// tabloda verilen şartlara uygun kayıt olup olmadığını kontrol etmektektedir.
        //Func<T>:bir veya daha fazla parametre alan bir delege türü-Bu methodlar parametre olarak lambda ifadesi alırlar ve bu sayede dinamik filtreleme sağlanmış oluyor.LINQ desteği sunabilmek içinde expression'ları kullanıyoruz-verilen ifadeyi verilere erişmek için gereken dile çevirir. Örneğin, LING to SQL'de SQL veya bir REST API'si için belirli bir HTTP isteği.-Bu filtrede yer alan ifadelere dayanarak bir LINQ ifadesi oluşturur.
        public List<T> GetActive()
        {
            return table.Where(x => x.Status == BlogProject.DAL.ORM.Enum.Status.Active || x.Status== BlogProject.DAL.ORM.Enum.Status.Modified).ToList();//Status'u aktif olanları listeledi
        }
        public T GetByDefault(Expression<Func<T,bool>> exp)
        {
            return table.Where(exp).FirstOrDefault();//Veritabanında ilgili şarta göre eşleşen kayıt olup olmadığını kontrol eder-tek bir kayıt için
        }
        public List<T> GetDefault(Expression<Func<T,bool>> exp)
        {
            return table.Where(exp).ToList();//Veritabanında ilgili şarta göre eşleşen kayıtla ilgili herşeyi listeler
        }
        public T GetById(Guid id)
        {
            return table.Find(id);//ID den item'ı yakaladık**çok kullanılacak 
            //return table.FirstOrDefault(x => x.ID == id);
        }
        public void Remove(Guid id)//Tek bir tablo elemanı silme
        {
            T item = GetById(id);//Id den yakalatıp item'a attı
            item.Status = DAL.ORM.Enum.Status.Deleted;//id den yakalanan item'ın statusunu deleted yaptı
            item.DeleteDate = DateTime.Now;
            Update(item);//durumu güncelledi
        }
        public void RemoveAll(Expression<Func<T,bool>>exp)//Tabloya ait tüm kayıtları silme
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = DAL.ORM.Enum.Status.Deleted;
                item.DeleteDate = DateTime.Now;
                Update(item);
            }
        }

        public void Update(T item)
        {
            T updated = GetById(item.ID);//Güncellenecek tablo elemanını id den yakaladık
            DbEntityEntry entry = db.Entry(updated);////DBEntityEntry, verilen varlığın özelliğinin varlık durumuna, şu anki ve orijinal değerlerine erişmesini sağlar. 
            entry.CurrentValues.SetValues(item);//SetValues ​​işlevi bir varlığı alır ve özelliklerini bir parametre olarak geçirilen varlığın değerlerine günceller
            //Yani;   var updatedEntity = currentEntity.CurrentValues.SetValues(newValues);
            Save();
        }
        public int Save()
        {
            return db.SaveChanges();//Değişiklikleri veritabanına kaydet
        }
    }
}
