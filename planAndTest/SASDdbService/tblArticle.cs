using Microsoft.EntityFrameworkCore;
using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SASDdbService
{
    public class tblArticle : SASDdbBase
    {
        public tblArticle()
        {
        }
        public Article GetArticleById(string articleId)
        {
            Article ret=null ;
            var guid = Guid.Parse(articleId);
            var qry = //db.Article.FromSqlRaw($"select * from article where articleId={articleId}").FirstOrDefault();
                db.Article.Where(a => a.ArticleId == guid && a.DeleteTime==null).FirstOrDefault();
            if (qry != null)
                ret = qry;
            return ret;
        }
        public string isDirectoryId(string articleId, out bool isDir)
        {
            string ret = "";
            isDir = false;
            Article art = GetArticleById(articleId);
            if (art==null)
            {
                ret = "not found";
                return ret;
            }
            isDir = art.IsDir;
            return ret;
        }
        public string GetDirectoryIdByArticleId(string articleId, out string dirId)
        {
            string ret = "";
            dirId = "";
            Article art = GetArticleById(articleId);
            if (art==null)
            {
                ret = "not found";
                return ret;
            }
            if (art.IsDir)
                dirId = articleId;
            else
                dirId = art.BelongToArticleDirId.ToString();
            return ret;
        }
        public SortedList<string, string> directoriesByParentArticleId(string articleId)
        {
            SortedList<string, string> ret ;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where DeleteTime is null and isDir=1 and belongToArticleDirId is null";
            else
                where = $" where DeleteTime is null and isDir=1 and belongToArticleDirId = '{articleId}'";
            var qry = db.Article.FromSqlRaw($"select * from article {where}");
            ret = new SortedList<string, string>();
            if (qry.Any())
            {
                List<Article> articleDirs = qry.ToList();
                foreach (Article art in articleDirs)
                    ret.Add(art.ArticleId.ToString(), art.ArticleTitle);
            }
            return ret;
        }
        public SortedList<string, string> subjectsByParentArticleId(string articleId)
        {
            SortedList<string, string> ret = null;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where DeleteTime is null and isDir=0 and belongToArticleDirId is null";
            else
                where = $" where DeleteTime is null and isDir=0 and belongToArticleDirId = '{articleId}'";
            var qry = db.Article.FromSqlRaw($"select * from article {where}");
            if (!qry.Any())
            {
                ret = new SortedList<string, string>();
                return ret;
            }
            ret = new SortedList<string, string>();
            List<Article> articleDirs = qry.ToList();
            foreach (Article art in articleDirs)
                ret.Add(art.ArticleId.ToString(), art.ArticleTitle);
            return ret;
        }
        public List<Article> FulltextSearch(string keywords, int pagesize=0, int pageIndex=0)
        {
            List<Article> ret = null;
            //todo !!... fulltext search
            return ret;
        }
        public string Add(Article newArticle)
        {
            string ret = "";
            db.Article.Add(newArticle);
            return ret;
        }
        public string Update(Article updateArticle)
        {
            string ret = "";
            db.Article.Update(updateArticle);
            return ret;
        }
        /// <summary>
        /// delete actually update DeleteTime to now
        /// </summary>
        /// <param name="deleteArticle"></param>
        /// <returns></returns>
        public string Delete(Article deleteArticle)
        {
            string ret = "";
            //db.Article.Remove(deleteArticle);
            deleteArticle.DeleteTime = DateTime.Now;
            ret = Update(deleteArticle);
            return ret;
        }
        public string Delete(string articleId, string byUserId="")
        {
            string ret = "";
            Article deleteArticle = GetArticleById(articleId);
            deleteArticle.DeleteBy = byUserId;
            ret = Delete(deleteArticle);
            return ret;
        }
        /// <summary>
        /// physically remove files marked deleted 7 days (included) ago
        /// </summary>
        /// <returns></returns>
        public string PurgeDeleted()
        {
            string ret = "";
            var sql = @"
delete
from article
where DATEDIFF(day, deleteTime, getdate()) >= 7
";
            int rows = db.Database.ExecuteSqlCommand(sql);
            return ret;
        }
        /// <summary>
        /// restore article marked deleted within 7 days, also allowed to change to be under a new parent
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="newDirId"></param>
        /// <returns></returns>
        public string RestoreArticle(string articleId, string newDirId="")
        {
            string ret = "";
            //todo !!... get article
            //todo !!... set deletetime, deleteby to null, belong... to newdirid
            return ret;
        }
        public override string SaveChanges()
        {
            string ret;
            ret= base.SaveChanges();
            return ret;
        }
    }
}
