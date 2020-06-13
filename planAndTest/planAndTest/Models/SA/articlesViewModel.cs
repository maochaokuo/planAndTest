using modelsfwk;
using SASDdb.entity.fwk;
//using SASDdb.entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.Models.SA
{
    [Serializable]
    public class articlesViewModel : ViewModelBase
    {
        public string articleId { get; set; }
        public string articleTitle { get; set; }
        public string parentDirId { get; set; } 
        public string parentDirTitle { get; set; }
        public List< article> directories = null;
        public List< article> subjects = null;
        public string articleType { get; set; }
        public string articleHtmlContent { get; set; }
        public List<string> selectedArticleId { get; set; }
        public List<string> selectedDirId { get; set; }
        public articlesViewModel()
        {
            //directories = new SortedList<string, article>(new articleNameComparer());
            //subjects = new SortedList<string, article>(new articleNameComparer());
            selectedArticleId = new List<string>();
        }
        //create comparer
        public class articleNameComparer : IComparer<article>
        {
            public int Compare(article x, article y)
            {
                int result = x.articleTitle.CompareTo(y.articleTitle);
                return result;
            }
        }
    }
}
