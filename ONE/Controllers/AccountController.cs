using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ONE._06ClassLibrary;
using ONE._07DataEntity;
using ONE.Models;

namespace ONE.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult UserInfo()
        {
            return View();
        }


        /// <summary>
        /// 进入我的个人中心
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult GetMyZone()
        {


             @ViewBag.username = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(User.Identity.Name);
             var personalBlogs = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == User.Identity.Name);
             @ViewBag.fansnumber = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name).FirstOrDefault().粉丝数;
             @ViewBag.figureURL = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name).FirstOrDefault().头像;



             //定义博客集合
             List<BlogViewModel> listBlogs = new List<BlogViewModel>();


             foreach (var model in personalBlogs)
             {
                 BlogViewModel blog = new BlogViewModel();
                 blog.blogId = model.T010_id;
                 blog.标签 = model.标签;
                 blog.标题 = model.标题;
                 blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                 blog.博主邮箱 = model.博主邮箱;
                 blog.图片链接 = model.图片链接;
                 blog.音乐链接 = model.音乐链接;
                 //获取正文的前一百个字符
                 blog.正文 = model.正文 + "&lt;p&gt;......&lt;p&gt;";
                 blog.点赞数 = model.点赞数;
                 blog.是否封锁 = model.是否封锁;
                 blog.是否公开 = model.是否公开;
                 listBlogs.Add(blog);


             }

             return View(listBlogs);


        
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        /// <returns></returns>
       public ActionResult Settings()
        {

            @ViewBag.username = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(User.Identity.Name);

            return View();
        
        }

    }
}