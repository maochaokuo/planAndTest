namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("interfaceProperty")]
    public partial class interfaceProperty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int interfacePropertyId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid systemInterfaceId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string propertyName { get; set; }

        [StringLength(99)]
        public string propertyValue { get; set; }

        [StringLength(999)]
        public string propertyDescription { get; set; }
    }
}
