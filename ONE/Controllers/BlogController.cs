using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ONE._06ClassLibrary;
using ONE.Models;

namespace ONE.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            //定义博客集合
            List<BlogViewModel> listBlogs = new List<BlogViewModel>();
            // 获取前n条博客首页
            var blogs = operateContext.BLLSession.IT010博文表BLL.getIndexBlogs();
            //设置博客的当前页数为1
            int currentPageIndex=1;
            HttpCookie cookie = new HttpCookie("pageIndex",currentPageIndex.ToString());
            //cookie.Values.Set("page", currentPageIndex.ToString());
            //将cookie值添加回客户端
            Response.Cookies.Add(cookie);
            

            foreach(var model in blogs)
            {
                BlogViewModel blog=new BlogViewModel();
                blog.blogId = model.T010_id;
                blog.标签 = model.标签;
                blog.标题 = model.标题;
                blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                blog.博主邮箱 = model.博主邮箱;
                blog.图片链接 = model.图片链接;
                blog.音乐链接 = model.音乐链接;
                //获取正文的前一百个字符
                blog.正文 = model.正文+ "&lt;p&gt;......&lt;p&gt;";
                blog.点赞数 = model.点赞数;
                blog.是否封锁 = model.是否封锁;
                blog.是否公开 = model.是否公开;
                listBlogs.Add(blog);


            }

            return View(listBlogs);

        }
        /// <summary>
        /// 获取某个页面的博客
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult GetPageIndex(int page)
        { 
           
            //定义博客集合
            List<BlogViewModel> listBlogs = new List<BlogViewModel>();
            var blogs = operateContext.BLLSession.IT010博文表BLL.getBlogsByPageIndex(page);
            //HttpCookie cookie = new HttpCookie("pageindex",page.ToString());
           
            //获取请求
            HttpCookie pageIndexCookie = Request.Cookies["pageIndex"];
            //修改cookie的值
            pageIndexCookie.Value = page.ToString();
            //发送回客户端
            Response.SetCookie(pageIndexCookie);
           


            foreach (var model in blogs)
            {
                BlogViewModel blog = new BlogViewModel();
                blog.blogId = model.T010_id;
                blog.标签 = model.标签;
                blog.标题 = model.标题;
                blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                blog.博主邮箱 = model.博主邮箱;
                blog.图片链接 = model.图片链接;
                blog.音乐链接 = model.音乐链接;
                blog.正文 = model.正文+ "&lt;p&gt;......&lt;p&gt;";
                blog.点赞数 = model.点赞数;
                blog.是否封锁 = model.是否封锁;
                blog.是否公开 = model.是否公开;
                blog.发布时间 = (DateTime)model.发布时间;
                listBlogs.Add(blog);

            }

            return View("Index",listBlogs);


        
        }


        /// <summary>
        /// 搜索博文
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchResult()
        {

            string key=Request.Form["searchKeyWords"];

            @ViewBag.searchKeyWords = key;
            //获取查询关键字
            string keyWords = key;
            //返回结果
            var list = ONE._06ClassLibrary.SerachMethod.getSearchBlogs(keyWords);

            return View(list);




        
        }



        /// <summary>
        /// 写博客
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult WriteBlog() {

          

            return View();
        
        
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public String WriteBlog(BlogViewModel model)
        {
            

                try
                {

                    operateContext.BLLSession.IT010博文表BLL.addBlog(model);

                    return "true";



                }
                catch (Exception ex)
                {

                    return "false";


                }


                 


            }
                
           

        
     
        /// <summary>
        /// 个人博客的归档页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult BlogList()
        {

            var personalBlogs = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == User.Identity.Name).OrderByDescending(m=>m.发布时间);
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
        /// 获取博客的详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BlogDetail(int id,string userName) {

            //@ViewBag.username = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(User.Identity.Name);
            @ViewBag.username = userName;
            return View(operateContext.BLLSession.IT010博文表BLL.getBlogDetail(id));
        

        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ContentResult AddComments() { 
        
            //获取评论内容与对应的博客id
            int  blogId =int.Parse(Request.Form["blogDeail.blogId"]);
            string remarkContent=Request.Form["remarkcontent"];
            //调用添加评论的方法
            if (operateContext.BLLSession.IT010博文表BLL.addRemark(remarkContent, blogId))
            {


                return Content("<script type='text/javascript'>alert('评论添加成功！');window.top.location.href='/Blog/BlogDetail?id=" + blogId + "';</script>");



            }
            else
            {

                return Content("<script type='text/javascript'>alert('评论添加成功！');window.top.location.href='/Blog/BlogDetail?id=" + blogId + "';</script>");
                
            }



        
        
        }

        /// <summary>
        /// 获取自身或者他人的博客主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPersonalPage(string userEmail)
        {


            //判断是不是作者本人
            if (userEmail == User.Identity.Name)
            {

                return RedirectToAction("GetMyZone", "Account");


            }
            else
            {


                // @ViewBag.username = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(User.Identity.Name);
                // var personalBlogs = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == User.Identity.Name);

                @ViewBag.username = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(userEmail);

                @ViewBag.fansnumber = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == userEmail).FirstOrDefault().粉丝数;

                @ViewBag.figureURL = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == userEmail).FirstOrDefault().头像;


                //查看当前用户是否关注了目标用户
                var ifHasFollowed = operateContext.BLLSession.IT003关注表BLL.GetListBy(m => m.关注者邮箱 == User.Identity.Name && m.被关注者邮箱 == userEmail).FirstOrDefault();
                if (ifHasFollowed != null)
                {

                    @ViewBag.hasFollowed = true;

                }
                else
                {
                    @ViewBag.hasFollowed = false;

                }

                var personalBlogs = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == userEmail);
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

        
        }

        
        /// <summary>
        /// 获取我关注的人的博客
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult GetMyFavorite()
        {

            //定义博客集合
            List<BlogViewModel> listBlogs = new List<BlogViewModel>();

            var blogs = getMyFavoriteBlogs.getBlogs(User.Identity.Name);



            foreach (var model in blogs)
            {
                BlogViewModel blog = new BlogViewModel();
                blog.blogId = model.T010_id;
                blog.标签 = model.标签;
                blog.标题 = model.标题;
                blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                blog.博主邮箱 = model.博主邮箱;
                blog.图片链接 = model.图片链接;
                blog.音乐链接 = model.音乐链接;
                blog.正文 = model.正文 + "&lt;p&gt;......&lt;p&gt;";
                blog.点赞数 = model.点赞数;
                blog.是否封锁 = model.是否封锁;
                blog.是否公开 = model.是否公开;
                blog.发布时间 = (DateTime)model.发布时间;
                listBlogs.Add(blog);

            }

            return View(listBlogs);

            
        
        }



    }
}