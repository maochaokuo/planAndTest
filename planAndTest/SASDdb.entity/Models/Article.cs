using System;
using System.Collections.Generic;

namespace SASDdb.entity.Models
{
    public partial class Article
    {
        public Guid ArticleId { get; set; }
        public DateTime Createtime { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleHtmlContent { get; set; }
        public string ArticleContent { get; set; }
        public bool IsDir { get; set; }
        public Guid? BelongToArticleDirId { get; set; }
    }
}
