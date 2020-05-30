using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SASDdbService
{
    public class SASDdbBase
    {
        protected SASDdbContext db;
        public SASDdbBase()
        {
            db = new SASDdbContext();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
