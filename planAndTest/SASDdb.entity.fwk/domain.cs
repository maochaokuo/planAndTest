namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("domain")]
    public partial class domain
    {
        public Guid domainId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string domainName { get; set; }

        [StringLength(999)]
        public string domainDescription { get; set; }

        [Required]
        [StringLength(33)]
        public string baseType { get; set; }
    }
}
