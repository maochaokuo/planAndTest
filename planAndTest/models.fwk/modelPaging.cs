using System;
using System.Collections.Generic;
using System.Text;

namespace modelsfwk
{
    public class modelPaging : ImodelPaging
    {
        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public modelPaging()
        {
            pagesize = 0;// no paging, > 0 otherwise
            pageindex = 0;// no paping, >= 1 otherwise
        }
    }
}
