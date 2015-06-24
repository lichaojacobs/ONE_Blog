using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace ONE._05DAL
{
    public partial class T001用户表DAL : BaseDAL<T001用户表>, ONE._04IDAL.IT001用户表DAL
    {
        /// <summary>
        /// 检验邮箱是否被注册
        /// </summary>
        /// <param name="emailString"></param>
        /// <returns>返回值为true时可以注册，返回值为false邮箱已被注册过，或者出现错误</returns>
        public bool checkEmail(string emailString)
        {


            var t001 = db.Set<T001用户表>().Where(m => m.注册邮箱 == emailString).FirstOrDefault();

            if (t001==null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

         /// <summary>
        /// 向注册邮箱发送验证码
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool SendConfirmEmail(string emailAdress)
        {

            try
            {
                try
                {
                    Random rnd = new Random();
                    int activeCode = rnd.Next(1000, 9999);
                    //给验证码加密
                    string code = Encryption.Encrype.GetMD5(activeCode.ToString());

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("tju_team@163.com", "ONE_Blog");
                    mailMessage.To.Add(emailAdress);
                    mailMessage.Subject = "ONE_Blog激活验证";
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    
                    mailMessage.Body = "点击以下链接进行账号激活<br/>" + "<a href =' http://localhost:55540/Home/ConfirmUsers?email=" + emailAdress + "&code=" + code + "'>" + "http://ONE_Blog.com/Home/ConfirmUsers" + "</a>";
                    mailMessage.Priority = MailPriority.High;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.163.com", 25);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("tju_team@163.com", "tjuteam");
                    smtp.Timeout = 100000;

                    object userState = mailMessage;
                    smtp.Send(mailMessage);

                    //将加密字符写入验证表
                   // HttpContext.Current.Session.Add(emailAdress,code);
                    T002验证码表 code2 = new T002验证码表();
                    code2.生成日期 = System.DateTime.Now;
                    code2.验证码 = code;

                    db.Set<T002验证码表>().Add(code2);
                    db.SaveChanges();
                                                            
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }

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

            try
            {
                //string toConfirmCode = HttpContext.Current.Session[emailAddress].ToString();
                var t002=db.Set<T002验证码表>().Where(m=>m.验证码==code).FirstOrDefault();
                if (t002 != null)
                {

                    T001用户表 t001=new T001用户表(){
                        
                        注册邮箱=emailAddress,
                        角色="普通用户"
                    
                    
                    };

                    //var t001 = GetListBy(m => m.注册邮箱 == emailAddress).First();

                    Modify(t001, "角色");
                   

                    return true;


                }
                else
                {
                    return false;
                }               
              


            }
            catch (Exception ex)
            {

                return false;

            }
           


        
        }


        /// <summary>
        ///用户登录操作
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="passWord"></param>
        /// <returns>登录成功返回true</returns>
        public bool UserLogin(string emailAddress, string passWord) {

            var t001= db.Set<T001用户表>().Where(m => m.注册邮箱 == emailAddress && m.密码 == passWord).FirstOrDefault();

            if (t001 != null)
            {

                return true;

            }
            else
            {
                return false;
                
            }

        
        }


        /// <summary>
        /// 通过邮箱地址找到用户的昵称
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public string NickNameByEmail(string emailAddress) {

           
            //直接调用父类里面的方法
           return base.GetListBy(m => m.注册邮箱 == emailAddress)[0].用户名.ToString();
        
        
        }




    }
}