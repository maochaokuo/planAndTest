using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace models.SA
{
    public enum ARTICLE_CHANGE_MODE
    {
        CREATE, EDIT, REPLY_TO
    }
    public class articleEditViewModel : Article, IViewModelBase
    {
        public Article article { get; set; }
        public string parentDirTitle { get; set; }
        public ARTICLE_CHANGE_MODE changeMode { get; set; }
        public string cmd { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
        public int pagesize { get; set; }
        public int pageindex { get; set; }
    }
}
