using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class systemEditViewModel : ViewModelBase
    {
        public systems editModel { get; set; }
        public systemEditViewModel()
        {
            editModel = new systems();
        }
    }
}
