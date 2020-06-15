namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("entityClass")]
    public partial class entityClass
    {
        public int entityClassId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string className { get; set; }

        [StringLength(999)]
        public string classDescription { get; set; }

        [Column("namespace")]
        [StringLength(99)]
        public string _namespace { get; set; }

        [StringLength(99)]
        public string moduleOrProject { get; set; }

        public Guid? stateMachineId { get; set; }
    }
}
