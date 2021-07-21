using System.Linq;
using Z_BulkyBook.DataAccess.Data;
using Z_BulkyBook.DataAccess.Repository.IRepository;
using Z_BulkyBook.Models;

namespace Z_BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {

        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var objFromDb = _db.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
                // ********
                //_db.SaveChanges();
                // ********
            }
            //throw new NotImplementedException();
        }
    }
}
