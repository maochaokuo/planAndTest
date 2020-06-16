using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASDdbService.fwk
{
    public class tblProject : SASDdbBase
    {
        public tblProject() : base()
        {
        }
        public tblProject(SASDdbContext db) : base(db)
        {
        }
        public List<project> getAll()
        {
            List<project> ret;
            var qry = (from a in db.project
                       select a).AsQueryable();
            if (qry.Any())
                ret = qry.ToList();
            else
                ret = null;
            return ret;
        }
    }
}
