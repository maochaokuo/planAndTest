namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("systemLocation")]
    public partial class systemLocation
    {
        public int systemLocationId { get; set; }

        public Guid serverId { get; set; }

        public Guid systemId { get; set; }

        [StringLength(999)]
        public string systemCopyDescription { get; set; }

        public DateTime createtime { get; set; }
    }
}
