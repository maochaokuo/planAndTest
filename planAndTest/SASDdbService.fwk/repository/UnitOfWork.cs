using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            articleLinksRepository = new Repository<articleLinks>(db);
            articleRelationRepository=new Repository<articleRelation>(db);
            dbRepository=new Repository<db>(db);
            dbFieldsRepository=new Repository<dbFields>(db);
            dbTableRepository=new Repository<dbTable>(db);
            domainRepository=new Repository<domain>(db);
            domainCaseRepository=new Repository<domainCase>(db);
            entityClassRepository=new Repository<entityClass>(db);
            entityClassVariableRepository=
                new Repository<entityClassVariable>(db);
            fileRepositoryRepository=new Repository<fileRepository>(db);
            interfaceParameterRepository=new Repository<interfaceParameter>(db);
            interfacePropertyRepository=new Repository<interfaceProperty>(db);
            networkServiceSourceRepository=
                new Repository<networkServiceSource>(db);
            projectMemberUserRepository=new Repository<projectMemberUser>(db);
            serversRepository=new Repository<servers>(db);
            stateMachineRepository=new Repository<stateMachine>(db);
            stateMachineEventRepository=new Repository<stateMachineEvent>(db);
            stateMachineEvent2StateRepository=
                new Repository<stateMachineEvent2State>(db);
            stateMachineStateRepository=new Repository<stateMachineState>(db);
            systemEntityRepository=new Repository<systemEntity>(db);
            systemGroupRepository=new Repository<systemGroup>(db);
            systemInterfaceRepository=new Repository<systemInterface>(db);
            systemLocationRepository=new Repository<systemLocation>(db);
            systemTemplateRepository=new Repository<systemTemplate>(db);
            templateEntityRepository=new Repository<templateEntity>(db);
            uiControlRepository=new Repository<uiControl>(db);
            uiFormRepository=new Repository<uiForm>(db);
        }
        public UnitOfWork() : base()
        {
            init();
        }
        public UnitOfWork(SASDdbContext db) : base(db)
        {
            init();
        }
        public override string SaveChanges()
        {
            string ret = base.SaveChanges();
            return ret;
        }
    }
}
