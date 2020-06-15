namespace SASDdb.entity.fwk
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SASDdbContext : DbContext
    {
        public SASDdbContext()
            : base("name=SASDdbContext")
        {
        }

        public virtual DbSet<article> article { get; set; }
        public virtual DbSet<articleLinks> articleLinks { get; set; }
        public virtual DbSet<articleRelation> articleRelation { get; set; }
        public virtual DbSet<domain> domain { get; set; }
        public virtual DbSet<domainCase> domainCase { get; set; }
        public virtual DbSet<entityClass> entityClass { get; set; }
        public virtual DbSet<entityClassVariable> entityClassVariable { get; set; }
        public virtual DbSet<fileRepository> fileRepository { get; set; }
        public virtual DbSet<interfaceParameter> interfaceParameter { get; set; }
        public virtual DbSet<interfaceProperty> interfaceProperty { get; set; }
        public virtual DbSet<networkServiceSource> networkServiceSource { get; set; }
        public virtual DbSet<project> project { get; set; }
        public virtual DbSet<projectMemberUser> projectMemberUser { get; set; }
        public virtual DbSet<projectVersion> projectVersion { get; set; }
        public virtual DbSet<servers> servers { get; set; }
        public virtual DbSet<stateMachine> stateMachine { get; set; }
        public virtual DbSet<stateMachineEvent2State> stateMachineEvent2State { get; set; }
        public virtual DbSet<stateMachineState> stateMachineState { get; set; }
        public virtual DbSet<systemEntity> systemEntity { get; set; }
        public virtual DbSet<systemInterface> systemInterface { get; set; }
        public virtual DbSet<systemLocation> systemLocation { get; set; }
        public virtual DbSet<systems> systems { get; set; }
        public virtual DbSet<systemTemplate> systemTemplate { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<article>()
                .Property(e => e.articleType)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.articleStatus)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.deleteBy)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.assignToUserId)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.authorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<articleLinks>()
                .Property(e => e.linkurl)
                .IsUnicode(false);

            modelBuilder.Entity<articleLinks>()
                .Property(e => e.linkDesc)
                .IsUnicode(false);

            modelBuilder.Entity<domain>()
                .Property(e => e.domainName)
                .IsUnicode(false);

            modelBuilder.Entity<domain>()
                .Property(e => e.baseType)
                .IsUnicode(false);

            modelBuilder.Entity<domainCase>()
                .Property(e => e.caseName)
                .IsUnicode(false);

            modelBuilder.Entity<entityClass>()
                .Property(e => e.className)
                .IsUnicode(false);

            modelBuilder.Entity<entityClass>()
                .Property(e => e._namespace)
                .IsUnicode(false);

            modelBuilder.Entity<entityClass>()
                .Property(e => e.moduleOrProject)
                .IsUnicode(false);

            modelBuilder.Entity<entityClassVariable>()
                .Property(e => e.variableName)
                .IsUnicode(false);

            modelBuilder.Entity<entityClassVariable>()
                .Property(e => e.variableType)
                .IsUnicode(false);

            modelBuilder.Entity<fileRepository>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<interfaceParameter>()
                .Property(e => e.parameterName)
                .IsUnicode(false);

            modelBuilder.Entity<interfaceParameter>()
                .Property(e => e.parameterType)
                .IsUnicode(false);

            modelBuilder.Entity<interfaceProperty>()
                .Property(e => e.propertyName)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.ownUserId)
                .IsUnicode(false);

            modelBuilder.Entity<projectMemberUser>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<servers>()
                .Property(e => e.hostnameOrIP)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachine>()
                .Property(e => e.stateMachineName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineEvent2State>()
                .Property(e => e.eventName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineEvent2State>()
                .Property(e => e.stateName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineEvent2State>()
                .Property(e => e.newStateName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineEvent2State>()
                .Property(e => e.actionName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineEvent2State>()
                .Property(e => e.endEventName)
                .IsUnicode(false);

            modelBuilder.Entity<stateMachineState>()
                .Property(e => e.stateName)
                .IsUnicode(false);

            modelBuilder.Entity<systemInterface>()
                .Property(e => e._namespace)
                .IsUnicode(false);

            modelBuilder.Entity<systemInterface>()
                .Property(e => e.moduleOrProject)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.userPassword)
                .IsUnicode(false);
        }
    }
}
