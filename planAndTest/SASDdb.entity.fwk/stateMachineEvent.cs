namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stateMachineEvent")]
    public partial class stateMachineEvent
    {
        public Guid stateMachineEventId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid stateMachineId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string eventName { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(999)]
        public string eventParameterJson { get; set; }

        [StringLength(33)]
        public string eventActionName { get; set; }

        [StringLength(33)]
        public string actionDoneEvent { get; set; }

        [StringLength(999)]
        public string actionDoneEventParaJson { get; set; }

        public Guid? globalEventId { get; set; }

        [StringLength(99)]
        public string eventDescription { get; set; }
    }
}
