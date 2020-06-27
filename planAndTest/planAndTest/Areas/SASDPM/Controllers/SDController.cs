using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SDController : Controller
    {
        // todo !!... (1) later
        // (done)projectVersion
        // systems, systemTemplate, systemEntity, 
        // systemInterface, interfaceParameter, interfaceProperty
        // stateMachine, stateMachineState, stateMachineEvent2State
        // todo !!... (2) further later
        // entityClass, entityClassVariable
        //   entityBehavior (input/output)
        // domain, domainCase
        // systemLocation, servers

        // todo !!... (1) something missing
        // (A) module level, mainly shared library 
        // (B) database level, database, table, field
        // field property: pkOrder, type, nullable, length(string)
        public ActionResult Index()
        {
            return View();
        }
    }
}