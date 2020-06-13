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
        public virtual DbSet<project> project { get; set; }
        public virtual DbSet<projectMemberUser> projectMemberUser { get; set; }
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

            modelBuilder.Entity<project>()
                .Property(e => e.ownUserId)
                .IsUnicode(false);

            modelBuilder.Entity<projectMemberUser>()
                .Property(e => e.userId)
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
