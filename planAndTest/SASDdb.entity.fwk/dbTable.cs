namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbTable")]
    public partial class dbTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dbTableId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(33)]
        public string databaseName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string tableName { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string tableDescription { get; set; }
    }
}
