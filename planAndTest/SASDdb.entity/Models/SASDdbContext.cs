using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SASDdb.entity.Models
{
    public partial class SASDdbContext : DbContext
    {
        public SASDdbContext()
        {
        }

        public SASDdbContext(DbContextOptions<SASDdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//todo !!... db connection #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=SASDdb;User Id=sa;Password=sa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("articleId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArticleContent).HasColumnName("articleContent");

                entity.Property(e => e.ArticleTitle)
                    .HasColumnName("articleTitle")
                    .HasMaxLength(999);

                entity.Property(e => e.BelongToArticleDirId).HasColumnName("belongToArticleDirId");

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDir).HasColumnName("isDir");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
