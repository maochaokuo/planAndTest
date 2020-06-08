namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("articleRelation")]
    public partial class articleRelation
    {
        [Key]
        [Column(Order = 0)]
        public Guid articleId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid relatedArticleId { get; set; }

        [StringLength(99)]
        public string relationToOriginalArticle { get; set; }

        public DateTime createtime { get; set; }
    }
}
