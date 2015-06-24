using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ONE.Models;
using ONE._07DataEntity;
using ONE._06ClassLibrary;
using System.Web.Security;

namespace ONE.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Intro()
        {

            return View();

        }


      


        //用户登录
        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (operateContext.BLLSession.IT001用户表BLL.UserLogin(model.注册邮箱, Encryption.Encrype.GetMD5(model.密码)))
                {

                   //var roles= new ONE._06ClassLibrary.UserAuthtication().GetRoles(model.注册邮箱);
                    FormsAuthentication.SetAuthCookie(model.注册邮箱, true);
                    

                    
                    return RedirectToAction("Index","Blog");


                }
                else
                {

                    Response.Write("<script type='text/JavaScript'>alert('用户名或密码错误！')</script>");

                }





            }

            return View();



        }

        //用户注册
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Random rn=new Random();
                int uid=rn.Next(100000);

                T001用户表 user = new T001用户表();
                user.注册邮箱 = model.注册邮箱;
                user.密码 = Encryption.Encrype.GetMD5(model.密码);
                user.角色 = "匿名用户";
                user.是否封锁 = false;
                user.用户名 = "usr" + uid.ToString();
                //将数据插入数据库中
                var t001 = operateContext.BLLSession.IT001用户表BLL;
                t001.Add(user);
                //fasongyoujian
                t001.SendConfirmEmail(model.注册邮箱);


                return Content("<script type='text/JavaScript'>alert('注册成功！请前往邮箱激活');window.location='/Home/Login'</script>");


            }
            else
            {
                return View();
            }


        }


        [HttpGet]
        public ActionResult ConfirmUsers(string email, string code)
        {

            if (operateContext.BLLSession.IT001用户表BLL.ConfirmCode(email, code))
            {

                return View();

            }
            else
            {
                return View("ConfirmError");

            }


        }


        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();

            return View("Login");
        
        
        }


        /// <summary>
        /// 获取用户未关注的
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult GetUsers()
        {


           //获取用户未关注的列表
            var userList = FollowUsersMethodLibrary.getUsers_NotFollowed();

            return View(userList);
            
        
        }

        /// <summary>
        /// 获取用户已关注的
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsers_HasFollowed()
        { 
            
            //获取用户已关注的列表
            var userList = FollowUsersMethodLibrary.getUsers_HasFollowed();

            return View(userList);
        }
        /// <summary>
        /// 获取我的粉丝
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyFans()
        { 
        
            //获取我的粉丝列表

            var userList = FollowUsersMethodLibrary.getMyFans();

            return View(userList);

        
        }


    }
}