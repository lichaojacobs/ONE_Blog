using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ONE.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Common.验证类.V00002验证邮箱唯一性(ErrorMessage="邮箱已经被注册过")]
        public string 注册邮箱 { get; set; }
        [Required]
        public string 密码 { get; set; }

        [Required]
        [Compare("密码",ErrorMessage="俩次输入不一致")]
        public string 确认密码 { get; set; }

        

    }
}