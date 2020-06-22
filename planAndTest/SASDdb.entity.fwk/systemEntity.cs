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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int systemEntityId { get; set; }

        public DateTime createtime { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(33)]
        public string entityName { get; set; }

        [StringLength(999)]
        public string entityDescription { get; set; }

        public int? systemTemplateId { get; set; }

        public int? parentEntityId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid systemId { get; set; }
    }
}
