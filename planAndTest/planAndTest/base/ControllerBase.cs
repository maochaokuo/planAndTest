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
		protected UnitOfWork uow = null;
		public ControllerBase()
        {
			uow = new UnitOfWork();
        }
	}
}