namespace SASDdb.entity.fwk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [StringLength(33)]
        public string userId { get; set; }

        [StringLength(33)]
        public string userPassword { get; set; }

        public DateTime createtime { get; set; }

        [StringLength(99)]
        public string userCommentsPublic { get; set; }

        [StringLength(99)]
        public string userCommentsPrivate { get; set; }

        public DateTime? lastLoginTime { get; set; }

        public DateTime modifytime { get; set; }

        [StringLength(99)]
        public string hintQuestion { get; set; }

        [StringLength(99)]
        public string hintAnswer { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? deleteTime { get; set; }

        [StringLength(33)]
        public string deleteBy { get; set; }
    }
}
