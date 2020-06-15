namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systems
    {
        [Key]
        public Guid systemId { get; set; }

        public int? projectVersionId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string systemName { get; set; }

        [StringLength(999)]
        public string systemDescription { get; set; }

        [StringLength(99)]
        public string systemType { get; set; }
    }
}
