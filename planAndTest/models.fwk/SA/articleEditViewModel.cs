//using SASDdb.entity.Models;
using commonLib;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Text;

namespace modelsfwk.SA
{
    public enum ARTICLE_CHANGE_MODE
    {
        CREATE=2, EDIT=1, REPLY_TO=3//, SAVED=0, ADDSAVED=-1
    }
    public class articleEditViewModel : ViewModelBase
    {
        public article editModel { get; set; }
        public string parentDirTitle { get; set; }
        public ARTICLE_CHANGE_MODE changeMode { get; set; }

        public articleEditViewModel()
        {
            editModel = new article();
            editModel.articleTitle = "";
            editModel.belongToArticleDirId = null;
            editModel.projectId = null;
            editModel.deleteTime = null;
        }
        //public article GetArticle()
        //{
        //    string json = jsonUtl.encodeJson(this);
        //    article ret = jsonUtl.decodeJson<article>(json);
        //    //article ret = new article();
        //    //ret.articleId = articleId;
        //    ////ret.createtime = createtime;
        //    //ret.articleTitle = articleTitle;
        //    //ret.articleHtmlContent = articleHtmlContent;
        //    //ret.articleContent = articleContent;
        //    //ret.isDir = isDir;
        //    //ret.belongToArticleDirId = belongToArticleDirId;
        //    //ret.projectId = projectId;
        //    //ret.articleTitle = articleTitle;
        //    //ret.articleStatus = articleStatus;
        //    //ret.priority = priority;
        //    ////ret.deleteTime = deleteTime;
        //    //ret.deleteBy = deleteBy;
        //    ////ret = (article) this.MemberwiseClone();// as article;
        //    return ret;
        //}
    }
}
