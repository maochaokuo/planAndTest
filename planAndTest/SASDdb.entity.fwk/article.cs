namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("article")]
    public partial class article
    {
        public Guid articleId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string articleTitle { get; set; }

        public string articleHtmlContent { get; set; }

        public string articleContent { get; set; }

        public bool isDir { get; set; }

        public Guid? belongToArticleDirId { get; set; }

        public Guid? projectId { get; set; }

        [Required]
        [StringLength(33)]
        public string articleType { get; set; }

        [Required]
        [StringLength(33)]
        public string articleStatus { get; set; }

        public short priority { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? deleteTime { get; set; }

        [StringLength(33)]
        public string deleteBy { get; set; }

        [StringLength(33)]
        public string assignToUserId { get; set; }
    }
}
