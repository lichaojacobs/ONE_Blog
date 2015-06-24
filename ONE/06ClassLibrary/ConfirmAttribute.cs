using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ONE._06ClassLibrary;

namespace Common

{
    public class 验证类
    {
        //验证码 
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
        public sealed class V00001验证码验证Attribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "验证码错误";

            public override bool IsValid(object value)
            {

                try
                {
                    string st = value.ToString();
                    if (st == HttpContext.Current.Session["code"].ToString())
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 验证邮箱的唯一性
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
        public sealed class V00002验证邮箱唯一性Attribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "该邮箱已经被注册过！";

            public override bool IsValid(object value)
            {
               
                    string str = value.ToString();

                    //调用接口层方法，返回相应的结果
                    return operateContext.BLLSession.IT001用户表BLL.checkEmail(str);
                    
            }

        
        }


    }
}