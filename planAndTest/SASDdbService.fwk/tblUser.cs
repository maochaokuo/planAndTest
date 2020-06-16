using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASDdbService.fwk
{
    public class tblUser : SASDdbBase
    {
        public tblUser() : base()
        {
        }
        public tblUser(SASDdbContext db) : base(db)
        {
        }
        public List<user> getAll()
        {
            List<user> ret;
            var qry = (from a in db.user
                       select a).AsQueryable();
            if (qry.Any())
                ret = qry.ToList();
            else
                ret = null;
            return ret;
        }
    }
}
