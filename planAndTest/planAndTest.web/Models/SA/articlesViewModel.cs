using models;
using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.web.Models.SA
{
    [Serializable]
    public class articlesViewModel : ViewModelBase
    {
        public string articleId { get; set; }
        public string articleTitle { get; set; }
        public string parentDirId { get; set; } 
        public string parentDirTitle { get; set; }
        public SortedList<string, string> directories { get; set; }
        public SortedList<string, string> subjects { get; set; }
        public string articleHtmlContent { get; set; }
        public List<string> selectedArticleId { get; set; }
        public List<string> selectedDirId { get; set; }
        public articlesViewModel()
        {
            directories = null;
            subjects = null;
            selectedArticleId = new List<string>();
        }
    }
}
