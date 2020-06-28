namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uiControl")]
    public partial class uiControl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uiControlId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int uiFormId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string controlName { get; set; }

        [StringLength(999)]
        public string controlDescription { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string controlType { get; set; }
    }
}
