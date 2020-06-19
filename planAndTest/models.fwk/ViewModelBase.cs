using System.Xml.Linq;

namespace modelsfwk
{
    public enum PAGE_STATUS
    {
        ADD=2,
        EDIT=1,
        SAVED=0,
        ADDSAVED = -1
    }
    public class ViewModelBase 
    {
        public string cmd { get; set; }
        private string _errorMsg="";
        public string errorMsg { 
            get { return _errorMsg; }
            set { 
                _errorMsg = value;
                _successMsg = "";
            } 
        }
        private string _successMsg = "";
        public string successMsg { 
            get { return _successMsg; }
            set
            {
                _successMsg = value;
                _errorMsg = "";
            }
        }
        public string singleSelect { get; set; }
        //public string multiSelect { get; set; } cannot use this column, must declared as hidden field and use request.form to receive
        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public PAGE_STATUS pageStatus { get; set; }
        public ViewModelBase()
        {
            pageStatus = PAGE_STATUS.SAVED;
        }
        public void clearMsg()
        {
            errorMsg = "";
            successMsg = "";
        }
    }
}
