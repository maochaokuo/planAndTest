namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("systemGroup")]
    public partial class systemGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int systemGroupId { get; set; }

        public DateTime createtime { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(33)]
        public string systemGroupName { get; set; }

        [StringLength(999)]
        public string systemGroupDescription { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid projectId { get; set; }
    }
}
