namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uiForm")]
    public partial class uiForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uiFormId { get; set; }

        public DateTime createtime { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int systemEntityId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string formName { get; set; }

        [StringLength(999)]
        public string formDescription { get; set; }
    }
}
