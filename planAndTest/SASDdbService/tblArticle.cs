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
