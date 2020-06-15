namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("networkServiceSource")]
    public partial class networkServiceSource
    {
        public Guid networkServiceSourceId { get; set; }

        public DateTime createtime { get; set; }

        public Guid? serverId { get; set; }

        [StringLength(999)]
        public string externalSourceDescription { get; set; }
    }
}
