using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z_BulkyBook.DataAccess.Data;
using Z_BulkyBook.DataAccess.Repository.IRepository;
using Z_BulkyBook.Models;

namespace Z_BulkyBook.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //

        //
    }
}
