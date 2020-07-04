using SASDdb.entity.fwk;
using SASDdbService.fwk.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Helper.SD
{
    public class SDdropdownOptions
    {
        public static SelectList systemTypeList()
        {
            SelectList ret;
            List<SelectListItem> _item = new List<SelectListItem>();
            _item.Add(new SelectListItem() { Text = "Web", Value = "Web", Selected = true });
            _item.Add(new SelectListItem() { Text = "Console", Value = "Console", Selected = true });
            _item.Add(new SelectListItem() { Text = "Windows", Value = "Windows", Selected = true });
            _item.Add(new SelectListItem() { Text = "WindowService", Value = "WindowService", Selected = true });
            _item.Add(new SelectListItem() { Text = "Library", Value = "Library", Selected = true });
            ret =new SelectList(_item, "Value", "Text", null);
            return ret;
        }
        public static SelectList systemGroupList()
        {
            List<SelectListItem> _systemGroup = new List<SelectListItem>();
            UnitOfWork uow = new UnitOfWork();
            List<systemGroup> systemGroups = uow.systemGroupRepository
                .GetAll().ToList();
            foreach (systemGroup sg in systemGroups)
                _systemGroup.Add(new SelectListItem() { 
                    Text = sg.systemGroupName, Value = sg.systemGroupName
                        .ToString() });
            return new SelectList(_systemGroup, "Value", "Text", null);
        }
    }

}