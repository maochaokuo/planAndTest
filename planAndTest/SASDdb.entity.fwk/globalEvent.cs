namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("globalEvent")]
    public partial class globalEvent
    {
        public Guid globalEventId { get; set; }

        [Required]
        [StringLength(33)]
        public string globalEventName { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string eventParameterJson { get; set; }

        [StringLength(33)]
        public string eventActionName { get; set; }

        [StringLength(33)]
        public string actionDoneEvent { get; set; }

        [StringLength(999)]
        public string actionDoneEventParaJson { get; set; }
    }
}
