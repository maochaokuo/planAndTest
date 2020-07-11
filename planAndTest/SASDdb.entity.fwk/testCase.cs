namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("testCase")]
    public partial class testCase
    {
        public Guid testCaseId { get; set; }

        public Guid? projectId { get; set; }

        [StringLength(33)]
        public string version { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string testCaseDescription { get; set; }

        [StringLength(999)]
        public string extraPassCondition { get; set; }

        [StringLength(999)]
        public string extraSkipCondition { get; set; }

        [StringLength(999)]
        public string extraFailCondition { get; set; }

        public Guid? parentTestCaseId { get; set; }
    }
}
