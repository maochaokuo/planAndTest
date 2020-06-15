namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fileRepository")]
    public partial class fileRepository
    {
        public Guid fileRepositoryId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(99)]
        public string url { get; set; }

        public Guid? serverId { get; set; }

        [StringLength(999)]
        public string externalSourceDescription { get; set; }
    }
}
