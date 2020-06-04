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
                db.Article.Where(a => a.ArticleId == guid).FirstOrDefault();
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
        public SortedList<string, string> directoriesByArticleId(string articleId)
        {
            SortedList<string, string> ret = null;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where isDir=1 and belongToArticleDirId is null";
            else
                where = $" where isDir=1 and belongToArticleDirId = \"{articleId}\"";
            var qry = db.Article.FromSqlRaw($"select * from article {where}");
            if (!qry.Any())
                return ret;
            ret = new SortedList<string, string>();
            List<Article> articleDirs = qry.ToList();
            foreach (Article art in articleDirs)
                ret.Add(art.ArticleId.ToString(), art.ArticleTitle);
            return ret;
        }
        public SortedList<string, string> subjectsByArticleId(string articleId)
        {
            SortedList<string, string> ret = null;
            //var guid = Guid.Parse(articleId);
            string where;
            if (string.IsNullOrWhiteSpace(articleId))
                where = " where isDir=0 and belongToArticleDirId is null";
            else
                where = $" where isDir=0 and belongToArticleDirId = \"{articleId}\"";
            var qry = db.Article.FromSqlRaw($"select * from article {where}");
            if (!qry.Any())
                return ret;
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
        public string Delete(Article deleteArticle)
        {
            string ret = "";
            db.Article.Remove(deleteArticle);
            return ret;
        }
        public string Delete(string articleId)
        {
            string ret = "";
            Article deleteArticle = GetArticleById(articleId);
            ret = Delete(deleteArticle);
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
