using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.PM
{
    public class userEditViewModel : ViewModelBase
    {
        public user editModel { get; set; }
        public string confirmPassword { get; set; }
        public userEditViewModel()
        {
            editModel = new user();
        }
    }
}
