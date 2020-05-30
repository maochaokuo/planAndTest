using System;
using System.Collections.Generic;

namespace SASDdb.entity.Models
{
    public partial class ArticleRelation
    {
        public Guid ArticleId { get; set; }
        public Guid RelatedArticleId { get; set; }
        public string RelationToOriginalArticle { get; set; }
        public DateTime Createtime { get; set; }
    }
}
