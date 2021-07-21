﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z_BulkyBook.DataAccess.Data;
using Z_BulkyBook.DataAccess.Repository.IRepository;

namespace Z_BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        //ctor
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);

            SP_Call = new SP_Call(_db);
            //
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }




        public ISP_Call SP_Call { get; private set; }



        //
        //
        public void Dispose()
        {
            _db.Dispose();
            //throw new NotImplementedException();
        }

        //this is link to Repository >>>
        public void Save()
        {
            _db.SaveChanges();
        }

        //
    }
}
