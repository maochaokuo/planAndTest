namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class servers
    {
        [Key]
        public Guid serverId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string hostnameOrIP { get; set; }

        [StringLength(999)]
        public string serverDescription { get; set; }
    }
}
