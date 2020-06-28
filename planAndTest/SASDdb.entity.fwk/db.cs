namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db")]
    public partial class db
    {
        public int dbId { get; set; }

        [Required]
        [StringLength(33)]
        public string databaseName { get; set; }

        [StringLength(99)]
        public string databaseLocation { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string databaseDescription { get; set; }
    }
}
