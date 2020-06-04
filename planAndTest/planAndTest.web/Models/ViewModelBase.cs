namespace planAndTest.web.Models
{
    public class ViewModelBase
    {
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public ViewModelBase()
        {
            pagesize = 0;// no paging, > 0 otherwise
            pageindex = 0;// no paping, >= 1 otherwise
        }
    }
}
