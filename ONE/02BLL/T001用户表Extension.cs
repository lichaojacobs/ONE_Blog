using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;
using ONE._01IBLL;

namespace ONE._02BLL
{
    public partial class T001用户表BLL : BaseBLL<T001用户表>, IT001用户表BLL
    {

        protected _04IDAL.IT001用户表DAL idal2;


        /// <summary>
        /// 检验该注册邮箱是否唯一
        /// </summary>
        /// <param name="emailString"></param>
        /// <returns></returns>
        public bool checkEmail(string emailString)
        {

            idal2 = DBSession.IT001用户表DAL;

            return idal2.checkEmail(emailString);
            

        }


        /// <summary>
        /// 向注册邮箱发送验证码
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
       public bool SendConfirmEmail(string emailAdress) {

           try
           {
               idal2 = DBSession.IT001用户表DAL;

               return idal2.SendConfirmEmail(emailAdress);
           }
           catch (Exception)
           {

               return false;
           }
        
            
        
        }


       /// <summary>
       /// 激活操作
       /// </summary>
       /// <param name="emailAddress"></param>
       /// <param name="code"></param>
       /// <returns>激活成功返回true</returns>
       public bool ConfirmCode(string emailAddress, string code) {

           idal2 = DBSession.IT001用户表DAL;

           return idal2.ConfirmCode(emailAddress, code);
       
       
       }


       /// <summary>
       ///用户登录操作
       /// </summary>
       /// <param name="emailAddress"></param>
       /// <param name="passWord"></param>
       /// <returns>登录成功返回true</returns>
       public bool UserLogin(string emailAddress, string passWord) {


           idal2 = DBSession.IT001用户表DAL;
           return idal2.UserLogin(emailAddress, passWord);
       
       }

       /// <summary>
       /// 通过邮箱地址找到用户的昵称
       /// </summary>
       /// <param name="emailAddress"></param>
       /// <returns></returns>
       public string NickNameByEmail(string emailAddress) {

           idal2 = DBSession.IT001用户表DAL;

           return idal2.NickNameByEmail(emailAddress);
       
       }

      
        
    }

}