using commonLib;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
                       where a.deleteTime == null
                       select a).AsQueryable();
            if (qry.Any())
                ret = qry.ToList();
            else
                ret = null;
            return ret;
        }
        public user getById(string userId)
        {
            user ret;
            var qry = (from a in db.user
                       where a.userId == userId && a.deleteTime == null
                       select a).FirstOrDefault();
            if (qry == null)
                ret = null;
            else
                ret = qry;
            return ret;
        }
        public string Add(user newUser)
        {
            string ret = "";
            db.user.Add(newUser);
            return ret;
        }
        public string Update(user updateUser)
        {
            string ret = "";
            var aUser = db.user.SingleOrDefault(x => x.userId == updateUser.userId);
            if (aUser != null)
            {
                aUser = reflectionUtl.assign<user, user>(aUser, updateUser);
                db.Entry(aUser).State = EntityState.Modified;
            }
            else
                throw new Exception($"user {updateUser.userId} not found!");
            return ret;
        }
        public string Delete(user deleteUser)
        {
            string ret = "";
            //db.article.Remove(deleteArticle);
            deleteUser.deleteTime = DateTime.Now;
            ret = Update(deleteUser);
            return ret;
        }
        public string Delete(string userId, string byUserId = "")
        {
            string ret = "";
            user deleteUser = getById(userId);
            deleteUser.deleteBy = byUserId;
            ret = Delete(deleteUser);
            return ret;
        }
        public string PurgeDeleted()
        {
            string ret = "";
            var qry = (from a in db.user
                       where SqlFunctions.DateDiff("day", 
                            a.deleteTime, DateTime.Now) >= 7
                       select a).AsQueryable();
            if (qry.Any())
            {
                List<user> users = qry.ToList();
                foreach(user rec in users)
                    db.Entry(rec).State = EntityState.Deleted;
                SaveChanges();
            }
            return ret;
        }
    }
}
