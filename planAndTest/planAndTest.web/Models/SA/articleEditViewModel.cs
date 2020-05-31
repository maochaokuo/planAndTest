using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.web.Models.SA
{
    [Serializable]
    public class articleEditViewModel : ViewModelBase
    {
        public string articleId { get; set; }
        public Article editingArticle { get; set; }
        public SortedList<string, string> directories { get; set; }
        public SortedList<string, string> subjects { get; set; }
        public articleEditViewModel()
        {
            editingArticle = new Article();
            directories = null;
            subjects = null;
        }
    }
}
