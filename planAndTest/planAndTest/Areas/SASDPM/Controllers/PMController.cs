using models.fwk.PM;
using modelsfwk;
using planAndTest.Models.PM;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class PMController : Controller
    {
        // GET: SASDPM/PM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            usersViewModel viewModel = new usersViewModel();
            //viewModel.errorMsg = loadUsers(ref viewModel);
            return View();
        }
        protected string loadUsers(ref usersViewModel viewModel)
        {
            string ret = "";
            tblUser tu = new tblUser();
            viewModel.users.Clear();
            List<user> users = tu.getAll();
            foreach (user rec in users)
                viewModel.users.Add(rec);
            return ret;
        }
        [HttpPost]
        public ActionResult Users(usersViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            // todo !!... (2) multi select not working
            tblUser tu = new tblUser();
            viewModel.clearMsg();
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = loadUsers(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    userEditViewModel tmpVMa = new userEditViewModel();
                    tmpVMa.pageStatus = PAGE_STATUS.ADD;
                    TempData["userEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateUser");
                    break; 
                case "update":
                    user u = tu.getById(viewModel.singleSelect);
                    if (u != null)
                    {
                        userEditViewModel tmpVM = new userEditViewModel();
                        tmpVM.editModel = u;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["userEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdateUser");
                        break;
                    }
                    else
                        viewModel.errorMsg = "error reading this user";
                    ar = View(viewModel);
                    break;
                case "delete":
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select user(s) to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach(string userId in selected.ToList())
                            viewModel.errorMsg += tu.Delete(userId);
                        tu.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            viewModel.successMsg = "successfully deleted";
                    }
                    loadUsers(ref viewModel);
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdateUser()
        {
            userEditViewModel viewModel;
            var tmpVM = TempData["userEditViewModel"] ;
            if (tmpVM == null)
                viewModel = new userEditViewModel();
            else
                viewModel =(userEditViewModel) tmpVM;
            return View(viewModel);
        }
        protected string checkForm(userEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.userId))
                ret = "userId cannot be empty";
            else if (viewModel.editModel.userPassword.Length<8)
                ret = "userPassword has to be at least 8 in length";
            else if (viewModel.editModel.userPassword.CompareTo(
                    viewModel.confirmPassword)!=0)
                ret = "userPassword is different from confirm password";
            else if (string.IsNullOrWhiteSpace(viewModel.editModel.hintQuestion))
                ret = "hintQuestion cannot be empty";
            else if (string.IsNullOrWhiteSpace(viewModel.editModel.hintAnswer))
                ret = "hintAnswer cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult AddUpdateUser(userEditViewModel viewModel)
        {
            ActionResult ar;
            string err = "";
            switch(viewModel.cmd)
            {
                case "save":
                    err = checkForm(viewModel);
                    if (err.Length>0)
                    {
                        viewModel.errorMsg = err;
                        ar = View(viewModel);
                        break;
                    }
                    tblUser tu = new tblUser();
                    if (viewModel.pageStatus == PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.createtime = DateTime.Now;
                        viewModel.editModel.modifytime = DateTime.Now;
                        err += tu.Add(viewModel.editModel);
                        err += tu.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "new user saved";
                            viewModel.pageStatus = PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == PAGE_STATUS.EDIT)
                    {
                        viewModel.editModel.modifytime = DateTime.Now;
                        err += tu.Update(viewModel.editModel);
                        err += tu.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "user updated";
                            viewModel.pageStatus = PAGE_STATUS.SAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else
                        viewModel.errorMsg = "wrong page status " + viewModel.pageStatus;
                    ar = View(viewModel);
                    break;
                case "addNew":
                    userEditViewModel tmpVMa = new userEditViewModel();
                    tmpVMa.pageStatus = PAGE_STATUS.ADD;
                    TempData["userEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateUser");
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}
