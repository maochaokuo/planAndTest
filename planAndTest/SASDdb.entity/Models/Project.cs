using System;
using System.Collections.Generic;

namespace SASDdb.entity.Models
{
    public partial class Project
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Createtime { get; set; }
        public string OwnUserId { get; set; }
        public string ProjectDescription { get; set; }
    }
}
