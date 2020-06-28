namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class dbFields
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dbFieldId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(33)]
        public string databaseName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string tableName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(33)]
        public string fieldName { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string fieldDescription { get; set; }

        public short primaryKeyOrder { get; set; }

        [Required]
        [StringLength(33)]
        public string fieldType { get; set; }

        public bool nullable { get; set; }

        public short? maxlength { get; set; }
    }
}
