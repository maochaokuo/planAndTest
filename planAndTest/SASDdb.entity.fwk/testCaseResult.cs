namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("testCaseResult")]
    public partial class testCaseResult
    {
        public Guid testCaseResultId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid testCaseId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid testResultId { get; set; }

        public DateTime createtime { get; set; }

        public short testResult { get; set; }
    }
}
