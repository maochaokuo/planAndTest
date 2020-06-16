using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace planAndTest.Models.PM
{
    public class usersViewModel :user, IViewModelBase
    {
        public string userId { get; set; }
        public List<user> users {get ;set;}
        public string cmd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string errorMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string successMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public usersViewModel()
        {
            users = new List<user>();
        }
    }
}