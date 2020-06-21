using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.PM
{
    public class projectEditViewModel : ViewModelBase
    {
        public project editModel { get; set; }
        public projectEditViewModel()
        {
            editModel = new project();
        }
    }
}
