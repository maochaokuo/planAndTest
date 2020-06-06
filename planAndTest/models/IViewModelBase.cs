namespace models
{
    public interface IViewModelBase
    {
        string cmd { get; set; }
        string errorMsg { get; set; }
        string successMsg { get; set; }
    }
}