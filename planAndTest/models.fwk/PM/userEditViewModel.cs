using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.PM
{
    public class userEditViewModel : user, IViewModelBase
    {
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public userEditViewModel()
        {

        }
    }
}
