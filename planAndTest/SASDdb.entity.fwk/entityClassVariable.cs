namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("entityClassVariable")]
    public partial class entityClassVariable
    {
        public int entityClassVariableId { get; set; }

        public int? entityClassId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string variableName { get; set; }

        [Required]
        [StringLength(33)]
        public string variableType { get; set; }

        public int? typeClassId { get; set; }
    }
}
