using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASDdbService.fwk
{
    public class Repository<T> : SASDdbBase, IRepository<T> where T : class
    {
        private DbSet<T> dbSet;

        private void init()
        {
            dbSet = db.Set<T>();
        }
        //public Repository() : base()
        //{
        //    init();
        //}
        public Repository(SASDdbContext db) : base(db)
        {
            init();
        }
        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public T GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(T getObjById)
        {
            //T getObjById = dbSet.Find(Id);
            dbSet.Remove(getObjById);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}
