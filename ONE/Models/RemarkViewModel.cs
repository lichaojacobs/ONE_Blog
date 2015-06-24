using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE.Models
{
    public class RemarkViewModel
    {
        public int remarkId { get; set; }

        public string remarkUser { get; set; }

        public string remarkUserLogo { get; set; }

        public int blogId { get; set; }

        public string remarkConetent { get; set; }

        public int parentId { get; set; }

        public DateTime remarkTime { get; set; }


    }
}