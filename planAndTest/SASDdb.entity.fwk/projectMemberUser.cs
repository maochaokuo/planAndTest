namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("projectMemberUser")]
    public partial class projectMemberUser
    {
        [Key]
        [Column(Order = 0)]
        public Guid projectId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string userId { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string roleDescription { get; set; }
    }
}
