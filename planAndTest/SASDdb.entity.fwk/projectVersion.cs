namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("projectVersion")]
    public partial class projectVersion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int projectVersionId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid projectId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string version { get; set; }

        [StringLength(99)]
        public string versionDescription { get; set; }

        public DateTime createtime { get; set; }
    }
}
