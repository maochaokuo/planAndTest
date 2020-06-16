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
        public project getById(string projectId)
        {
            project ret;
            var qry = (from a in db.project
                       where a.projectId ==new Guid( projectId )
                       select a).FirstOrDefault();
            if (qry == null)
                ret = null;
            else
                ret = qry;
            return ret;
        }
        public string Add(project newProject)
        {
            string ret = "";
            db.project.Add(newProject);
            return ret;
        }
        public string Update(project updateProject)
        {
            string ret = "";
            var aProject = db.project.SingleOrDefault(x => x.projectId == updateProject.projectId);
            if (aProject != null)
            {
                aProject = reflectionUtl.assign<project, project>(aProject, updateProject);
                db.Entry(aProject).State = EntityState.Modified;
            }
            else
                throw new Exception($"user {updateProject.projectId} not found!");
            return ret;
        }
        public string Delete(project deleteProject)
        {
            string ret = "";
            db.Entry(deleteProject).State = EntityState.Deleted;
            // need save changes
            return ret;
        }
        public string Delete(string projectId)
        {
            string ret = "";
            project deleteProject = getById(projectId);
            ret = Delete(deleteProject);
            return ret;
        }
    }
}
