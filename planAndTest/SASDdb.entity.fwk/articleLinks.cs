namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class articleLinks
    {
        [Key]
        [Column(Order = 0)]
        public Guid articleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(800)]
        public string linkurl { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string linkDesc { get; set; }

        [StringLength(99)]
        public string linkType { get; set; }
    }
}
