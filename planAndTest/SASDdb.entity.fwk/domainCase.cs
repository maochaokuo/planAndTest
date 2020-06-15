namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("domainCase")]
    public partial class domainCase
    {
        public int domainCaseId { get; set; }

        public Guid domainId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string caseName { get; set; }

        [StringLength(999)]
        public string caseDescription { get; set; }
    }
}
