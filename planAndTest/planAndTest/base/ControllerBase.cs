using SASDdbService.fwk.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest
{
	public class ControllerBase : Controller
	{
		protected const string PageStatus = "pageStatus";
		protected const string MultiSelect = "multiSelect";
		protected UnitOfWork uow = null;
		protected readonly string modelName;
		protected readonly string modelMessage;
		public ControllerBase(string modelName, string modelMessage)
        {
			uow = new UnitOfWork();
			this.modelName = modelName;
			this.modelMessage = modelMessage;
        }
	}
}