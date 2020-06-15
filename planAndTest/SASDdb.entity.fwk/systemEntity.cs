namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("systemEntity")]
    public partial class systemEntity
    {
        [Key]
        [Column("systemEntity")]
        public int systemEntity1 { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string entityName { get; set; }

        [StringLength(999)]
        public string entityDescription { get; set; }

        public int systemTemplateId { get; set; }
    }
}
