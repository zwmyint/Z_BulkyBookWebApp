using System;
using System.Linq;
using Z_BulkyBook.DataAccess.Data;
using Z_BulkyBook.DataAccess.Repository.IRepository;
using Z_BulkyBook.Models;

namespace Z_BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                // ********
                _db.SaveChanges();
                // ********
            }
            //throw new NotImplementedException();
        }



        //
    }
}
