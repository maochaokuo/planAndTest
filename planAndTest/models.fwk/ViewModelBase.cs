namespace modelsfwk
{
    public class ViewModelBase 
    {
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public string singleSelect { get; set; }
        //public string multiSelect { get; set; } cannot use this column, must declared as hidden field and use request.form to receive
        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public ViewModelBase()
        {
        }
    }
}
