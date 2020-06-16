using modelsfwk;

namespace planAndTest.Models.EPL
{
    public class EplAction1vm : IViewModelBase
    {
        public string txt1 { get; set; }
        public string cmdTxt { get; set; }
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
    }
}
