using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE.Models
{
    public class BlogViewModel
    {

        public int blogId { get; set; }
        public string 标题 { get; set; }
        public string 正文 { get; set; }
        public string 图片链接 { get; set; }
        public string 音乐链接 { get; set; }
        public string 用户名 { get; set; }

        public string 博主邮箱 { get; set; }
        public string 标签 { get; set; }
        public int 点赞数 { get; set; }
        public bool 是否封锁 { get; set; }
        public bool 是否公开 { get; set; }

        public DateTime 发布时间 { get; set; }


        //blog集合
        public static  List<BlogViewModel> blogs;



    }
}