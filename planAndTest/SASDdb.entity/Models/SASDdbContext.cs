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
        public virtual DbSet<ArticleRelation> ArticleRelation { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=SASDdb;User Id=sa;Password=sa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.HasIndex(e => e.BelongToArticleDirId)
                    .HasName("IX_article");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("IX_article_1");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("articleId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArticleContent).HasColumnName("articleContent");

                entity.Property(e => e.ArticleHtmlContent).HasColumnName("articleHtmlContent");

                entity.Property(e => e.ArticleTitle)
                    .HasColumnName("articleTitle")
                    .HasMaxLength(999);

                entity.Property(e => e.BelongToArticleDirId).HasColumnName("belongToArticleDirId");

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDir).HasColumnName("isDir");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");
            });

            modelBuilder.Entity<ArticleRelation>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.RelatedArticleId });

                entity.ToTable("articleRelation");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("IX_articleRelation");

                entity.HasIndex(e => e.RelatedArticleId)
                    .HasName("IX_articleRelation_1");

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.RelatedArticleId).HasColumnName("relatedArticleId");

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RelationToOriginalArticle)
                    .HasColumnName("relationToOriginalArticle")
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.HasIndex(e => e.OwnUserId)
                    .HasName("IX_project");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("projectId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OwnUserId)
                    .IsRequired()
                    .HasColumnName("ownUserId")
                    .HasMaxLength(33)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectDescription)
                    .HasColumnName("projectDescription")
                    .HasMaxLength(999);

                entity.Property(e => e.ProjectName)
                    .HasColumnName("projectName")
                    .HasMaxLength(99);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasMaxLength(33)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("lastLoginTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Modifytime)
                    .HasColumnName("modifytime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserCommentsPrivate)
                    .HasColumnName("userCommentsPrivate")
                    .HasMaxLength(99);

                entity.Property(e => e.UserCommentsPublic)
                    .HasColumnName("userCommentsPublic")
                    .HasMaxLength(99);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("userPassword")
                    .HasMaxLength(33)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
