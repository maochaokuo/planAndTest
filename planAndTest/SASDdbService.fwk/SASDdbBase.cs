//using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Serialization;
using SASDdb.entity.fwk;
//using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;

namespace SASDdbService
{
    public class SASDdbBase
    {
        protected SASDdbContext db;
        protected DbContextTransaction trans;
        public SASDdbBase()
        {
            db = new SASDdbContext();
            trans = null;
        }
        public SASDdbBase(SASDdbContext db)
        {
            this.db = db;
        }
        public SASDdbContext GetDbContext()
        {
            return db;
        }
        public DbContextTransaction BeginTransaction()
        {
            DbContextTransaction ret = 
                db.Database.BeginTransaction();
            return ret;
        }
        public string Commit()
        {
            string ret = "";
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
            }
            else
                ret = "no transaction to commit";
            return ret;
        }
        public string Rollback()
        {
            string ret = "";
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
            }
            else
                ret = "no transaction to commit";
            return ret;
        }
        public virtual string SaveChanges()
        {
            string ret = "";
            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException ex0)
            {
                foreach(var eve in ex0.EntityValidationErrors)
                {
                    ret += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ret += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                ret = inner.Message;
            }
            return ret;
        }
    }
}
