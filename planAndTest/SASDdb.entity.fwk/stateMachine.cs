namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stateMachine")]
    public partial class stateMachine
    {
        public Guid stateMachineId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string stateMachineName { get; set; }

        [StringLength(999)]
        public string stateMachineDescription { get; set; }

        [StringLength(999)]
        public string receiveSelfEvent { get; set; }

        [StringLength(999)]
        public string receiveParentEvent { get; set; }

        [StringLength(999)]
        public string receiveChildEvent { get; set; }

        [StringLength(33)]
        public string initialStateName { get; set; }
    }
}
