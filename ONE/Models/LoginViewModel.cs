using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ONE._06ClassLibrary;

namespace ONE.Models
{
    public class LoginViewModel
    {
        [RegularExpression("^([a-z0-9A-Z]+[-|\\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\\.)+[a-zA-Z]{2,}$",ErrorMessage="邮箱格式不正确")]

        public string 注册邮箱 { get; set; }


        public string 密码 { get; set; }

        [Common.验证类.V00001验证码验证]
        public string 验证码 { get; set; }
    }
}