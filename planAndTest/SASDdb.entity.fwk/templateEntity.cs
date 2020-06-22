namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("templateEntity")]
    public partial class templateEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int templateEntityId { get; set; }

        public DateTime createtime { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int systemTemplateId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string templateEntityName { get; set; }

        [StringLength(999)]
        public string templateEntityDescription { get; set; }

        public int? parentTemplateEntityId { get; set; }
    }
}
