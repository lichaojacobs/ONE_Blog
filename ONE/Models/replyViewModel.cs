using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE.Models
{
    public class replyViewModel
    {

        public int T013_replyId { get; set; }
        public int 评论ID { get; set; }
        public string 回复内容 { get; set; }
        public string 评论人ID { get; set; }
        public string 回复至ID { get; set; }
        public string 回复时间 { get; set; }



    }
}