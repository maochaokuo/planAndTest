namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("interfaceParameter")]
    public partial class interfaceParameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int interfaceParameterId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid systemInterfaceId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string parameterName { get; set; }

        [StringLength(33)]
        public string parameterType { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string parameterComments { get; set; }
    }
}
