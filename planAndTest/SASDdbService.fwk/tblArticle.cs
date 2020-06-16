//using Microsoft.EntityFrameworkCore;
using commonLib;
using SASDdb.entity.fwk;
//using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SASDdbService
{
    public class tblArticle : SASDdbBase
    {
        public tblArticle() : base()
        {
        }
        public tblArticle(SASDdbContext db) : base(db)
        {
        }
        public article GetArticleById(string articleId)
        {
            article ret=null ;
            var guid = Guid.Parse(articleId);
            var qry = //db.article.FromSqlRaw($"select * from article where articleId={articleId}").FirstOrDefault();
                db.article.Where(a => a.articleId == guid && a.deleteTime==null).FirstOrDefault();
            if (qry != null)
                ret = qry;
            return ret;
        }
        public string isDirectoryId(string articleId, out bool isDir)
        {
            string ret = "";
            isDir = false;
            article art = GetArticleById(articleId);
            if (art==null)
            {
                ret = "not found";
                return ret;
            }
            isDir = art.isDir;
            return ret;
        }
        //public string GetDirectoryIdByArticleId(string articleId, out string dirId)
        //{
        //    string ret = "";
        //    dirId = "";
        //    article art = GetArticleById(articleId);
        //    if (art==null)
        //    {
        //        ret = "not found";
        //        return ret;
        //    }
        //    if (art.isDir)
        //        dirId = articleId;
        //    else
        //        dirId = art.belongToArticleDirId.ToString();
        //    return ret;
        //}
        public List<article> directoriesByParentArticleId(string articleId)
        {
            List< article> ret ;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where DeleteTime is null and isDir=1 and belongToArticleDirId is null";
            else
                where = $" where DeleteTime is null and isDir=1 and belongToArticleDirId = '{articleId}'";
            var qry = db.Database.SqlQuery<article>($"select * from article {where}");
            ret = new List< article>();
            if (qry.Any())
            {
                List<article> articleDirs = qry.ToList();
                foreach (article art in articleDirs)
                    ret.Add( art);
                ret.Sort(delegate (article x, article y)
                {
                    return x.articleTitle.CompareTo(y.articleTitle);
                });
            }
            return ret;
        }
        public List<article> subjectsByParentArticleId(string articleId)
        {
            List<article> ret = null;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where DeleteTime is null and isDir=0 and belongToArticleDirId is null";
            else
                where = $" where DeleteTime is null and isDir=0 and belongToArticleDirId = '{articleId}'";
            var qry = db.Database.SqlQuery<article>($"select * from article {where}");
            if (!qry.Any())
            {
                ret = new List<article>();
                return ret;
            }
            ret = new List<article>();
            List<article> articleDirs = qry.ToList();
            foreach (article art in articleDirs)
                ret.Add( art);
            ret.Sort(delegate(article x, article y)
            {
                return -x.createtime.CompareTo(y.createtime);
            });
            return ret;
        }
        public List<article> FulltextSearch(string keywords, int pagesize=0, int pageIndex=0)
        {
            List<article> ret = null;
            //todo !!...(5) fulltext search
            return ret;
        }
        public string Add(article newArticle)
        {
            string ret = "";
            db.article.Add(newArticle);
            return ret;
        }
        public string Update(article updateArticle)
        {
            string ret = "";
            var anArticle = db.article.SingleOrDefault(x => x.articleId == updateArticle.articleId);
            if (anArticle != null)
            {
                anArticle = reflectionUtl.assign<article, article>(anArticle, updateArticle);
                db.Entry(anArticle).State = EntityState.Modified;// .article.Update(updateArticle);
            }
            else
                throw new Exception($"article {updateArticle.articleId} not found!");
            return ret;
        }
        /// <summary>
        /// delete actually update DeleteTime to now
        /// </summary>
        /// <param name="deleteArticle"></param>
        /// <returns></returns>
        public string Delete(article deleteArticle)
        {
            string ret = "";
            //db.article.Remove(deleteArticle);
            deleteArticle.deleteTime = DateTime.Now;
            ret = Update(deleteArticle);
            return ret;
        }

        public string Delete(string articleId, string byUserId="")
        {
            string ret = "";
            article deleteArticle = GetArticleById(articleId);
            deleteArticle.deleteBy = byUserId;
            ret = Delete(deleteArticle);
            return ret;
        }
        public string DeleteArticleOrDir(string articleId, string byUserId = "")
        {
            string ret = "";
            article articleOrDir = GetArticleById(articleId);
            if (articleOrDir.isDir)
            {
                List<article> dirs = directoriesByParentArticleId(articleId);
                if (dirs.Count > 0)
                    ret = $"directory {articleId} '{articleOrDir.articleTitle}' is not empty";
                else
                {
                    List<article> subj = subjectsByParentArticleId(articleId);
                    if (subj.Count>0)
                        ret = $"directory {articleId} '{articleOrDir.articleTitle}' is not empty";
                }
            }
            if (ret.Length==0)
            {
                articleOrDir.deleteBy = byUserId;
                ret = Delete(articleOrDir);
            }
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
            //todo !!...(7) get article, set deletetime, deleteby to null, belong... to newdirid
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
