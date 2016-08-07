using LennyBlog.Extensions;
using LennyBlog.Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LennyBlog.Models.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {

        public ArticleViewModel() { }

        public ArticleViewModel(Article model)
        {
            this.Id = model.Id;
            this.Title = model.Title;
            this.Description = model.Description;
            this.Browses = model.Browses;
            this.CategoryId = model.CategoryId;
            this.CategoryName = model.Category?.Name;
            this.CreatedDate = model.CreatedDate;
            this.IsDelete = model.IsDelete;
            this.TagArr = model.Tags.StringToStringArr(',');
            this.Tags = model.Tags;
            this.Summary = model.Description.SubString(200, "......");
            this.Top = model.Top;
            this.IsRecommend = model.IsRecommend;
            this.CreateBy = model.CreateBy;
            this.CreateUserName = model.User.UserName;
        }

        public Guid Id { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }

        public string Tags { set; get; }

        public string[] TagArr { set; get; }

        public Guid CategoryId { set; get; }

        public string CategoryName { set; get; }

        public DateTime CreatedDate { set; get; }

        public DateTime? ModifyDate { set; get; }

        public int Browses { get; set; }

        public bool IsDelete { set; get; }

        public string Summary { set; get; }

        /// <summary>
        /// 置顶
        /// </summary>
        public int Top { set; get; }

        /// <summary>
        /// 推荐
        /// </summary>
        public YesOrNo IsRecommend { get; set; }

        /// <summary>
        /// 下篇标题
        /// </summary>
        public string NextTitle { set; get; }

        /// <summary>
        /// 下篇Id
        /// </summary>
        public Guid NextId { set; get; }

        /// <summary>
        /// 上一篇
        /// </summary>
        public string LastTitle { set; get; }

        /// <summary>
        /// 上篇Id
        /// </summary>
        public Guid LastId { set; get; }


        public Guid CreateBy { set; get; }

        public string CreateUserName { set; get; }

        /// <summary>
        /// 评论数
        /// 通过远程调用api获取该数据
        /// </summary>
        public string Comments { set; get; }


        /// <summary>
        /// 通过远程调用api给评论数赋值
        /// </summary>
        public async Task SetComments()
        {
            try
            {
                string url = string.Format("http://api.duoshuo.com/threads/counts.json?short_name=lennyblog&threads={0}", this.Id);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(content);
                string comments = obj["response"][this.Id.ToString()]["comments"];
                this.Comments = comments;
            }
            catch (Exception ex)
            {
                this.Comments = "未知";
            }
        }

        /// <summary>
        /// 获取评论数
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetComments()
        {
            if (string.IsNullOrEmpty(this.Comments))
            {
                await SetComments();
            }
            return this.Comments;
        }

    }
}
