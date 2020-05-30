using System;
using System.Collections.Generic;

namespace SASDdb.entity.Models
{
    public partial class User
    {
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public DateTime Createtime { get; set; }
        public string UserCommentsPublic { get; set; }
        public string UserCommentsPrivate { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime Modifytime { get; set; }
    }
}
