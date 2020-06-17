using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace planAndTest.Models.PM
{
    public class usersViewModel : ViewModelBase
    {
        public string userId { get; set; }
        public string userCommentsPublic { get; set; }
        public List<user> users {get ;set;}

        public usersViewModel()
        {
            users = new List<user>();
        }
    }
}