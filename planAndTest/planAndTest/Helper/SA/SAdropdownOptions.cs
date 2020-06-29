using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Helper.SA
{
    public class SAdropdownOptions
    {
        public static SelectList articleTypeOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            _itemType.Add(new SelectListItem() { Text = "General", Value = "General", Selected = true });
            _itemType.Add(new SelectListItem() { Text = "Requirement", Value = "Requirement", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Solution", Value = "Solution", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Issue", Value = "Issue", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Question", Value = "Question", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Answer", Value = "Answer", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Task", Value = "Task", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Bug", Value = "Bug", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Project", Value = "Project", Selected = false });
            return new SelectList(_itemType, "Value", "Text", null);
        }
        public static SelectList articleStatusOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            _itemType.Add(new SelectListItem() { Text = "New", Value = "New", Selected = true });
            _itemType.Add(new SelectListItem() { Text = "Open", Value = "Open", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Assigned", Value = "Assigned", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Resolved", Value = "Resolved", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Closed", Value = "Closed", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Removed", Value = "Removed", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Suspended", Value = "Suspended", Selected = false });
            return new SelectList(_itemType, "Value", "Text", null);
        }
        public static SelectList articlePriorityOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            for (int i = 1; i <= 9; i++)
            {
                if (i == 5)
                    _itemType.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                else
                    _itemType.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
            }
            return new SelectList(_itemType, "Value", "Text", null);
        }
    }
}