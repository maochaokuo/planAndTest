using System.Xml.Linq;

namespace modelsfwk
{
    public enum PAGE_STATUS
    {
        ADD=2,
        EDIT=1,
        SAVED=0,
        ADDSAVED=-1
    }
    public class ViewModelBase 
    {
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public string singleSelect { get; set; }
        //public string multiSelect { get; set; } cannot use this column, must declared as hidden field and use request.form to receive
        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public PAGE_STATUS pageStatus { get; set; }
        public ViewModelBase()
        {
            pageStatus = PAGE_STATUS.SAVED;
        }
    }
}
