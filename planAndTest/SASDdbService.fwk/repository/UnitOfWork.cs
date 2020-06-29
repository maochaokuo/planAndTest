using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASDdbService.fwk.repository
{
    public class UnitOfWork : SASDdbBase
    {
        public Repository<articleLinks> articleLinksRepository;
        public Repository<articleRelation> articleRelationRepository;
        public Repository<db> dbRepository;
        public Repository<dbFields> dbFieldsRepository;
        public Repository<dbTable> dbTableRepository;
        public Repository<domain> domainRepository;
        public Repository<domainCase> domainCaseRepository;
        public Repository<entityClass> entityClassRepository;
        public Repository<entityClassVariable> entityClassVariableRepository;
        public Repository<fileRepository> fileRepositoryRepository;
        public Repository<interfaceParameter> interfaceParameterRepository;
        public Repository<interfaceProperty> interfacePropertyRepository;
        public Repository<networkServiceSource> networkServiceSourceRepository;
        public Repository<projectMemberUser> projectMemberUserRepository;
        public Repository<servers> serversRepository;
        public Repository<stateMachine> stateMachineRepository;
        public Repository<stateMachineEvent> stateMachineEventRepository;
        public Repository<stateMachineEvent2State> stateMachineEvent2StateRepository;
        public Repository<stateMachineState> stateMachineStateRepository;
        public Repository<systemEntity> systemEntityRepository;
        public Repository<systemGroup> systemGroupRepository;
        public Repository<systemInterface> systemInterfaceRepository;
        public Repository<systemLocation> systemLocationRepository;
        public Repository<systemTemplate> systemTemplateRepository;
        public Repository<templateEntity> templateEntityRepository;
        public Repository<uiControl> uiControlRepository;
        public Repository<uiForm> uiFormRepository;

        protected void init()
        {
            articleLinksRepository = new Repository<articleLinks>();
            articleRelationRepository=new Repository<articleRelation>();
            dbRepository=new Repository<db>();
            dbFieldsRepository=new Repository<dbFields>();
            dbTableRepository=new Repository<dbTable>();
            domainRepository=new Repository<domain>();
            domainCaseRepository=new Repository<domainCase>();
            entityClassRepository=new Repository<entityClass>();
            entityClassVariableRepository=new Repository<entityClassVariable>();
            fileRepositoryRepository=new Repository<fileRepository>();
            interfaceParameterRepository=new Repository<interfaceParameter>();
            interfacePropertyRepository=new Repository<interfaceProperty>();
            networkServiceSourceRepository=new Repository<networkServiceSource>();
            projectMemberUserRepository=new Repository<projectMemberUser>();
            serversRepository=new Repository<servers>();
            stateMachineRepository=new Repository<stateMachine>();
            stateMachineEventRepository=new Repository<stateMachineEvent>();
            stateMachineEvent2StateRepository=new Repository<stateMachineEvent2State>();
            stateMachineStateRepository=new Repository<stateMachineState>();
            systemEntityRepository=new Repository<systemEntity>();
            systemGroupRepository=new Repository<systemGroup>();
            systemInterfaceRepository=new Repository<systemInterface>();
            systemLocationRepository=new Repository<systemLocation>();
            systemTemplateRepository=new Repository<systemTemplate>();
            templateEntityRepository=new Repository<templateEntity>();
            uiControlRepository=new Repository<uiControl>();
            uiFormRepository=new Repository<uiForm>();
        }
        public UnitOfWork() : base()
        {
            init();
        }
        public UnitOfWork(SASDdbContext db) : base(db)
        {
            init();
        }
    }
}
