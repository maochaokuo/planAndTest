using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Helper.PM
{
    public class PMdropdownOption
    {
        public static SelectList userList()
        {
            List<SelectListItem> _userLst = new List<SelectListItem>();
            tblUser tu = new tblUser();
            List<user> users = tu.getAll();
            foreach (user u in users)
            {
                _userLst.Add(new SelectListItem() { Text = u.userCommentsPublic, Value = u.userId });
            }
            return new SelectList(_userLst, "Value", "Text", null);
        }
        public static SelectList projectList()
        {
            List<SelectListItem> _prjLst = new List<SelectListItem>();
            tblProject tp = new tblProject();
            List<project> prjs = tp.getAll();
            foreach (project p in prjs)
            {
                _prjLst.Add(new SelectListItem() { Text = p.projectName, Value = p.projectId.ToString() });
            }
            return new SelectList(_prjLst, "Value", "Text", null);
        }
    }
}