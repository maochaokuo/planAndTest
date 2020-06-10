namespace modelsfwk
{
    public class ViewModelBase : IViewModelBase
    {
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public ViewModelBase()
        {
        }
    }
}
