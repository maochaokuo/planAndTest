using commonLib;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASDdbService.fwk
{
    public class tblProjectVersion : SASDdbBase
    {
        public tblProjectVersion() : base()
        {
        }
        public tblProjectVersion(SASDdbContext db) : base(db)
        {
        }
        public List<projectVersion> getByProjectId(string projectId)
        {
            List<projectVersion> ret;
            var qry = (from a in db.projectVersion
                       where a.projectId == new Guid(projectId)
                       select a).AsQueryable();
            if (qry.Any())
                ret = qry.ToList();
            else
                ret = null;
            return ret;
        }
        public projectVersion getProjectVersion(string 
            projectId, string version)
        {
            projectVersion ret;
            var qry = (from a in db.projectVersion
                       where a.projectId == new Guid(projectId)
                            && a.version == version
                       select a).FirstOrDefault();
            if (qry!=null)
                ret = qry;
            else
                ret = null;
            return ret;
        }
        public string Add(projectVersion newProjectVersion)
        {
            string ret = "";
            db.projectVersion.Add(newProjectVersion);
            return ret;
        }
        public string Update(projectVersion updateProjectVersion)
        {
            string ret = "";
            var aProjectVersion = db.projectVersion.SingleOrDefault(
                x => x.projectId == updateProjectVersion.projectId
                    && x.version == updateProjectVersion.version);
            if (aProjectVersion != null)
            {
                aProjectVersion = reflectionUtl.assign<projectVersion
                    , projectVersion>(aProjectVersion,
                    updateProjectVersion);
                db.Entry(aProjectVersion).State = EntityState.Modified;
            }
            else
                throw new Exception($"project version (" +
                    $"projectId:{updateProjectVersion.projectId}, " +
                    $"version:{updateProjectVersion.version}) " +
                    $"not found");
            return ret;
        }
        public string Delete(projectVersion deleteProjectVersion)
        {
            string ret = "";
            db.Entry(deleteProjectVersion).State = EntityState.Deleted;
            return ret;
        }
        public string Delete(string projectId, string version)
        {
            string ret ;
            projectVersion deleteProjectVersion =
                getProjectVersion(projectId, version);
            ret = Delete(deleteProjectVersion);
            return ret;
        }
    }
}
