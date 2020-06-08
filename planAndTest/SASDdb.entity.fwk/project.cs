namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("project")]
    public partial class project
    {
        public Guid projectId { get; set; }

        [StringLength(99)]
        public string projectName { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string ownUserId { get; set; }

        [StringLength(999)]
        public string projectDescription { get; set; }
    }
}
