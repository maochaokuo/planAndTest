namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("testResult")]
    public partial class testResult
    {
        public Guid testResultId { get; set; }

        public DateTime? testStartTime { get; set; }

        public DateTime? testEndTime { get; set; }

        [StringLength(999)]
        public string testResultDescription { get; set; }

        public int passNum { get; set; }

        public int skipNum { get; set; }

        public int failNum { get; set; }

        public DateTime createtime { get; set; }
    }
}
