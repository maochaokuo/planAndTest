namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("systemTemplate")]
    public partial class systemTemplate
    {
        public Guid systemTemplateId { get; set; }

        public DateTime createtime { get; set; }

        [Required]
        [StringLength(33)]
        public string templateName { get; set; }

        [StringLength(999)]
        public string templateDescription { get; set; }

        public Guid? baseSystemTemplateId { get; set; }
    }
}
