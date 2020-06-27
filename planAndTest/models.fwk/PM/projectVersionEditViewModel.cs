using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.PM
{
    public class projectVersionEditViewModel : ViewModelBase
    {
        public projectVersion editModel { get; set; }
        public projectVersionEditViewModel()
        {
            editModel = new projectVersion();
        }
    }
}
