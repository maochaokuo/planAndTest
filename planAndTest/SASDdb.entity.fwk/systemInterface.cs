namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("systemInterface")]
    public partial class systemInterface
    {
        public Guid systemInterfaceId { get; set; }

        public int? systemTemplateId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string interfaceName { get; set; }

        [StringLength(999)]
        public string interfaceDescription { get; set; }

        [Column("namespace")]
        [StringLength(99)]
        public string _namespace { get; set; }

        [StringLength(99)]
        public string moduleOrProject { get; set; }
    }
}
