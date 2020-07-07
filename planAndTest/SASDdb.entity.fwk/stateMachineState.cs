namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stateMachineState")]
    public partial class stateMachineState
    {
        public Guid stateMachineStateId { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid stateMachineId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(33)]
        public string stateName { get; set; }

        [StringLength(999)]
        public string stateDescription { get; set; }

        public DateTime createtime { get; set; }
    }
}
