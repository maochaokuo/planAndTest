using SASDdb.entity.fwk;
using SASDdbService.fwk;
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
        public static SelectList systemList()
        {
            List<SelectListItem> _system = new List<SelectListItem>();
            tblSystem ts = new tblSystem();
            List<systems> systemList = ts.getAll().ToList();
            foreach (systems rec in systemList)
                _system.Add(new SelectListItem()
                {
                    Text = rec.systemName,
                    Value = rec.systemId.ToString()
                });
            return new SelectList(_system, "Value", "Text", null);
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
        public static SelectList systemEntityList()
        {
            List<SelectListItem> _systemEntity = new List<SelectListItem>();
            UnitOfWork uow = new UnitOfWork();
            List<systemEntity> systemEntities = uow.systemEntityRepository
                .GetAll().ToList();
            foreach (systemEntity se in systemEntities)
                _systemEntity.Add(new SelectListItem()
                {
                    Text = se.entityName,
                    Value = se.systemEntityId
                        .ToString()
                });
            return new SelectList(_systemEntity, "Value", "Text", null);
        }
        public static SelectList systemTemplateList()
        {
            List<SelectListItem> _systemTemplate = new List<SelectListItem>();
            UnitOfWork uow = new UnitOfWork();
            List<systemTemplate> systemTemplates = uow.systemTemplateRepository
                .GetAll().ToList();
            foreach (systemTemplate st in systemTemplates)
                _systemTemplate.Add(new SelectListItem()
                {
                    Text = st.templateName,
                    Value = st.systemTemplateId.ToString()
                });
            return new SelectList(_systemTemplate, "Value", "Text", null);
        }
        public static SelectList stateMachineList()
        {
            List<SelectListItem> _stateMachine = new List<SelectListItem>();
            UnitOfWork uow = new UnitOfWork();
            List<stateMachine> stateMachines = uow.stateMachineRepository
                .GetAll().ToList();
            foreach (stateMachine sm in stateMachines)
                _stateMachine.Add(new SelectListItem()
                {
                    Text = sm.stateMachineName,
                    Value = sm.stateMachineId.ToString()
                });
            return new SelectList(_stateMachine, "Value", "Text", null);
        }
        public static SelectList globalEventList()
        {
            List<SelectListItem> _globalEvent = new List<SelectListItem>();
            UnitOfWork uow = new UnitOfWork();
            List<globalEvent> globalEvents = uow.globalEventRepository
                .GetAll().ToList();
            foreach (globalEvent ge in globalEvents)
                _globalEvent.Add(new SelectListItem()
                {
                    Text = ge.globalEventName,
                    Value = ge.globalEventId.ToString()
                });
            return new SelectList(_globalEvent, "Value", "Text", null);
        }
    }
}