using models.fwk.SD;
using modelsfwk;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SystemEntityController : ControllerBase
    {
        public SystemEntityController() : base()
        {
        }
        // GET: SASDPM/SystemEntity
        public ActionResult Index(string parentEntityIdS)
        {
            systemEntityViewModel viewModel;
            var systemEntityModel = TempData["systemEntityModel"];
            if (systemEntityModel == null)
                viewModel = new systemEntityViewModel();
            else
                viewModel = (systemEntityViewModel)systemEntityModel;
            if (!string.IsNullOrWhiteSpace(parentEntityIdS))
            {
                int parentEntityId;
                if (int.TryParse(parentEntityIdS, out parentEntityId))
                    viewModel.editModel.parentEntityId = parentEntityId;
            }
            ViewBag.pageStatus = TempData["pageStatus"];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            TempData["systemEntityModel"] = systemEntityModel;
            TempData["pageStatus"] = ViewBag.pageStatus;
            return View();
        }
        protected string query(ref systemEntityViewModel viewModel)
        {
            string ret = "";
            systemEntity tmpModel = viewModel.editModel;
            tblSystem ts = new tblSystem(uow.GetDbContext());
            var qry = (from a in uow.systemEntityRepository.GetAll()
                       join b in uow.systemTemplateRepository.GetAll()
                            on a.systemTemplateId equals b.systemTemplateId
                            into c
                       from d in c.DefaultIfEmpty()
                       join e in uow.systemEntityRepository.GetAll()
                            on a.parentEntityId equals e.systemEntityId
                            into f
                        from g in f.DefaultIfEmpty()
                        join h in ts.getAll()
                            on a.systemId equals h.systemId into i
                        from j in i.DefaultIfEmpty()
                       select new systemEntityDisp
                       {
                           systemEntityId=a.systemEntityId,
                           createtime=a.createtime,
                           entityName=a.entityName,
                           entityDescription=a.entityDescription,
                           systemTemplateId=a.systemTemplateId,
                           templateName=d.templateName,
                           parentEntityId=a.parentEntityId,
                           parentEntityName=g.entityName,
                           systemId=a.systemId,
                           systemName=j.systemName
                       }).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.entityName))
                qry = qry.Where(x => x.entityName.Contains(tmpModel.entityName));
            if (!string.IsNullOrWhiteSpace(tmpModel.entityDescription))
                qry = qry.Where(x => x.entityDescription.Contains(
                    tmpModel.entityDescription));
            if (tmpModel.systemTemplateId > 0)
                qry = qry.Where(x => x.systemTemplateId == tmpModel.systemTemplateId);
            if (tmpModel.parentEntityId > 0)
                qry = qry.Where(x => x.parentEntityId == tmpModel.parentEntityId);
            if (tmpModel.systemId!=Guid.Empty &&
                    !string.IsNullOrWhiteSpace(tmpModel.systemId.ToString()))
                qry = qry.Where(x => x.systemId == tmpModel.systemId);
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(systemEntityViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.entityName))
                ret = "entity name cannot be empty";
            else if (viewModel.editModel.systemId != Guid.Empty ||
                    string.IsNullOrWhiteSpace(viewModel.editModel.ToString()))
                ret = "systemId cannot be invalid";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(systemEntityViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            systemEntityViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData["pageStatus"];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            switch (viewModel.cmd)
            {
                default:
                    viewModel.errorMsg = $"unhandled command {viewModel.cmd}";
                    ar = View(viewModel);
                    break;
            }
            TempData["systemGroupModel"] = viewModel;
            TempData["pageStatus"] = ViewBag.pageStatus;
            return ar;
        }
    }
}