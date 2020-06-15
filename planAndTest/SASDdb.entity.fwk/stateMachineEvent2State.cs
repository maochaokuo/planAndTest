namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class stateMachineEvent2State
    {
        public int stateMachineEvent2StateId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid stateMachineId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string eventName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(33)]
        public string stateName { get; set; }

        [Required]
        [StringLength(33)]
        public string newStateName { get; set; }

        [StringLength(33)]
        public string actionName { get; set; }

        [StringLength(33)]
        public string endEventName { get; set; }

        public DateTime createtime { get; set; }
    }
}
