using System;

namespace SASDdb.entity.fwk
{
    public interface Iuser
    {
        DateTime createtime { get; set; }
        string hintAnswer { get; set; }
        string hintQuestion { get; set; }
        DateTime? lastLoginTime { get; set; }
        DateTime modifytime { get; set; }
        string userCommentsPrivate { get; set; }
        string userCommentsPublic { get; set; }
        string userId { get; set; }
        string userPassword { get; set; }
    }
}