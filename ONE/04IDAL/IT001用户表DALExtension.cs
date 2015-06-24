using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;

namespace ONE._04IDAL
{
    public partial interface IT001用户表DAL : IBaseDAL<T001用户表>
    {
        /// <summary>
        /// 检验邮箱是否被注册
        /// </summary>
        /// <param name="emailString"></param>
        /// <returns></returns>
        bool checkEmail(string emailString);


        /// <summary>
        /// 向注册邮箱发送验证码
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        bool SendConfirmEmail(string emailAdress);

        /// <summary>
        /// 激活操作
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="code"></param>
        /// <returns>激活成功返回true</returns>
        bool ConfirmCode(string emailAddress, string code);

        /// <summary>
        ///用户登录操作
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="passWord"></param>
        /// <returns>登录成功返回true</returns>
        bool UserLogin(string emailAddress, string passWord);


        /// <summary>
        /// 通过邮箱地址找到用户的昵称
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        string NickNameByEmail(string emailAddress);

    }
}