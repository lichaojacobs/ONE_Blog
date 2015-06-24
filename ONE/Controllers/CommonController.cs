using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ONE._06ClassLibrary;
using System.Web.Script.Serialization;
using ONE.Models;
using ONE._07DataEntity;


namespace ONE.Controllers
{
    public class CommonController : Controller
    {


        public JsonResult GetGoodsInfo()
        {
            var good = new
            {
                code = "1",
                Name = "电风扇",
                Price = "12"


            };
            return Json(good);
        }
        /// <summary>
        /// 验证图片
        /// </summary>
        /// <returns></returns>
        public FileContentResult GetImage()
        {
            MemoryStream My_Stream = Get_Images(12, 4, "#ffffff");
            return File(My_Stream.GetBuffer(), "image/Png");
        }

        private MemoryStream Get_Images(int Font_Size, int Char_Number, string BackgroundColor)
        {
            //把字符转换为图像，并且保存到内存流
            int image_w = Convert.ToInt32(Font_Size * 1.3) + Font_Size * Char_Number;
            //这个数字在调用页面需要,你要自己算出明确的数值 注意注意注意注意！！！！！
            int image_h = Convert.ToInt32(2.5 * Font_Size);
            //这个数字在调用页面需要,你要自己算出明确的数值    注意注意注意注意！！！！！

            Bitmap Temp_Bitmap = default(Bitmap);
            //封装GDI+位图 
            Graphics Temp_Graphics = default(Graphics);
            //封装GDI+绘图面
            Color Color_Back = ColorTranslator.FromHtml(BackgroundColor);
            //背景颜色 
            //Color_Back = Color.Transparent;

            Temp_Bitmap = new Bitmap(image_w, image_h, PixelFormat.Format32bppRgb);
            //注意注  确定背景大小

            Temp_Graphics = Graphics.FromImage(Temp_Bitmap);
            Temp_Graphics.FillRectangle(new SolidBrush(Color_Back), new Rectangle(0, 0, image_w, 5 * image_h));


            //注意注   绘制背景 

            //Dim Sesson_Company As String = "" '为了进行验证比较
            System.Random ran = new System.Random();
            int codeint = ran.Next(1000, 9999);
            string code = Convert.ToString(codeint);

            Session["code"] = code;   ///将结果放入session中
            Temp_Graphics.DrawString(code, new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);

            Temp_Bitmap.MakeTransparent(Color_Back);   //背景透明

            MemoryStream Temp_Stream = new MemoryStream();
            Temp_Bitmap.Save(Temp_Stream, ImageFormat.Png);
            Temp_Graphics.Dispose();
            //释放资源
            Temp_Bitmap.Dispose();
            //释放资源
            Temp_Stream.Close();
            //关闭打开的流文件
            return Temp_Stream;
            //返回流
        }


        /// <summary>
        /// 异步上传图片时候的处理函数
        /// </summary>
        /// <returns></returns>
        public ContentResult UpLoadImage()
        {

            //获得通过post方法得到的图片
            HttpPostedFileBase image = HttpContext.Request.Files["upload"];
            //获取图片名称
            string upLoadFileName = image.FileName;
            //图片扩展名
            string contentType = image.ContentType;

            // CKEditor提交的很重要的一个参数  
            //String callback = Request.Form["CKEditorFuncNum"];  
            string callback = HttpContext.Request.Params["CKEditorFuncNum"];

            if (image != null)
            {
                if (image.ContentLength <= 1048576)
                {
                    string upLoadPath = Server.MapPath("~/Content/assets/");

                    //Random rn = new Random();
                    //string unique = rn.Next(10000).ToString();
                    ///生成全局的唯一Id

                    // string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                    ///获取存储物理路径,并将图片存入指定物理路径
                    string fileAdress = upLoadPath + upLoadFileName;

                    image.SaveAs(fileAdress);


                    ///获取要存入数据库的图片地址，并将其作为img标签的src值
                    string photoAdress = "/content/assets/" + upLoadFileName;

                    //将图片地址存入cookies中(待改进)

                    //HttpCookie photoCookie = new HttpCookie("photoUrl");
                    //photoCookie.Value = photoAdress;
                    
                    Session["photoUrl"] = photoAdress;

                    //编辑返回内容
                    string returnContent = "<script type=\"text/javascript\">" + "window.parent.CKEDITOR.tools.callFunction(" + callback
                + ",'" + photoAdress + "','')" + "</script>";

                    ///将图片url返回
                    return Content(returnContent, "text/html", System.Text.Encoding.UTF8);

                }
                else
                {

                    //编辑返回内容
                    string returnContent = "<script type=\"text/javascript\">" + "window.parent.CKEDITOR.tools.callFunction(" + callback
                + ",''," + "'文件大小不超过1M');" + "</script>";

                    return Content(returnContent);
                }

            }
            else
            {
                return Content("传入错误！！");
            }


        }

        /// <summary>
        /// 上传个人头像
        /// </summary>
        /// <returns></returns>
        public ContentResult UpLoadPersonalFigure()
        {

            //获得通过post方法得到的图片
            HttpPostedFileBase image = HttpContext.Request.Files["uploadFile"];
            if (image != null)
            {

                string upLoadPath = Server.MapPath("~/Content/assets/");

                //Random rn = new Random();
                //string unique = rn.Next(10000).ToString();
                ///生成全局的唯一Id

                string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                ///获取存储物理路径,并将图片存入指定物理路径
                string fileAdress = upLoadPath + fileName;

                image.SaveAs(fileAdress);
                ///获取要存入数据库的图片地址，并将其作为img标签的src值
                string photoAdress = "/content/assets/" + fileName;


                //更改数据库中头像的字段
                ONE._07DataEntity.T001用户表 currentUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name).FirstOrDefault();

                currentUser.头像 = photoAdress;
                //保存修改
                operateContext.BLLSession.IT001用户表BLL.Modify(currentUser,"头像");
                
                

                ///将图片url返回
                return Content("<script type='text/javascript'>alert('上传头像成功！');window.location.href='/Account/GetMyZone';</script>");

            }
            else
            {
                return Content("<script type='text/javascript'>alert('上传失败');window.location.href='/Account/GetMyZone';</script>");
            }

        
        }



        /// <summary>
        /// 通过ajax获取blogs页面的总页数，然后在前台动态生成页码
        /// </summary>
        /// <returns></returns>
        public int GetBlogsPageCount()
        {

            int blogsNumber = ONE._06ClassLibrary.operateContext.BLLSession.IT010博文表BLL.getBlogCount();
            int blogsperPage = ONE._06ClassLibrary.ConfigureSettings.getConfigureSettings().blogCount;
            return (blogsNumber / blogsperPage + 1);

        }

        /// <summary>
        /// 获取热门文章的总页数
        /// </summary>
        /// <returns></returns>
        public int getHotBlogs()
        {

            int blogsNumber = ONE._06ClassLibrary.operateContext.BLLSession.IT010博文表BLL.getBlogCount();
            int blogsperPage = ONE._06ClassLibrary.ConfigureSettings.getConfigureSettings().hotBlogNumber;
            return (blogsNumber / blogsperPage + 1);
        
        }

        /// <summary>
        /// 给博文点赞
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>

        public String clickHit()
        {

            int blogId = int.Parse(Request.Form["blogId"]);

            int currentNumber = int.Parse(Request.Form["currentNumber"]);

            try
            {
                //获取当前用户
                string userName = User.Identity.Name;


                if (userName == "")
                {

                    return "-1";

                }
                else
                {

                    //先查询该用户是否已经点过赞
                    var ifExist = operateContext.BLLSession.IT011点赞表BLL.GetListBy(m => m.博文id == blogId && m.来自用户 == userName);



                    //判断用户是否点过赞,没点过赞添加，点过赞取消点赞
                    if (ifExist.Count == 0)
                    {
                        T011点赞表 clickHit = new T011点赞表();

                        clickHit.来自用户 = userName;
                        clickHit.博文id = blogId;
                        operateContext.BLLSession.IT011点赞表BLL.Add(clickHit);



                        //改变currentNumber的值
                        currentNumber++;
                        object c = new object();
                        //同步代码块
                        lock (c)
                        {
                            //修改用户表的点赞数目


                            T010博文表 blog = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.T010_id == blogId)[0];

                            blog.点赞数 = currentNumber;

                            operateContext.BLLSession.IT010博文表BLL.Modify(blog, "点赞数");

                            return currentNumber.ToString();

                        }


                    }
                    else
                    {

                        //获取点赞id
                        int hitId = ifExist[0].T011_id;
                        //取消点赞
                        operateContext.BLLSession.IT011点赞表BLL.DelBy(m => m.T011_id == hitId);

                        //改变currentNumber的值
                        currentNumber--;

                        //同步修改博文表中点赞数的值
                        object c = new object();

                        //同步代码块
                        lock (c)
                        {
                            //修改点赞表的点赞数目
                            T010博文表 blog = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.T010_id == blogId)[0];

                            blog.点赞数 = currentNumber;

                            operateContext.BLLSession.IT010博文表BLL.Modify(blog, "点赞数");

                            //首先detach一下
                            //operateContext.BLLSession.IT010博文表BLL.DetachModel(blogId);


                        }

                        return currentNumber.ToString();

                    }


                }


            }
            catch (Exception ex)
            {

                return "false";
            }







        }


        /// <summary>
        /// 设置博客的可见性
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public string makePrivate()
        {

            int blogId = int.Parse(Request.Form["blogId"]);
            string isPrivate = Request.Form["private"];

            //修改博客的可见性
            T010博文表 blog = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.T010_id == blogId)[0];

            if (isPrivate == "true")
            {

                blog.是否公开 = false;

                operateContext.BLLSession.IT010博文表BLL.Modify(blog, "是否公开");

                return "true";



            }
            else
            {
                blog.是否公开 = true;

                operateContext.BLLSession.IT010博文表BLL.Modify(blog, "是否公开");

                return "true";

            }







        }
        /// <summary>
        /// 评论回复
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public string makeReply()
        {
            try
            {

                int remarkId = int.Parse(Request.Form["remarkId"]);

                string remarkContent = Request.Form["remarkContent"];

                string remarkUserName = Request.Form["remarkUserName"];
                T013评论回复表 t013 = new T013评论回复表();
                t013.回复内容 = remarkContent;
                t013.评论ID = remarkId;
                t013.评论人ID = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(HttpContext.User.Identity.Name);
                t013.回复至ID = remarkUserName;
                t013.回复时间 = System.DateTime.Now;

                operateContext.BLLSession.IT013评论回复表BLL.Add(t013);

                //返回json格式数据
                JavaScriptSerializer js = new JavaScriptSerializer();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                js.Serialize(t013, sb);

                return sb.ToString();


            }
            catch (Exception ex)
            {

                return "false";
            }





        }

        /// <summary>
        /// 回复的回复
        /// </summary>
        /// <returns></returns>
        public ContentResult replyToReply()
        {

            try
            {

                int remarkId = int.Parse(Request.Form["remarkId"]);

                string remarkContent = Request.Form["remarkContent"];

                string remarkUserName = Request.Form["remarkUserName"];
                T013评论回复表 t013 = new T013评论回复表();
                t013.回复内容 = remarkContent;
                t013.评论ID = remarkId;
                t013.评论人ID = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(HttpContext.User.Identity.Name);
                t013.回复至ID = remarkUserName;
                t013.回复时间 = System.DateTime.Now;

                operateContext.BLLSession.IT013评论回复表BLL.Add(t013);


                return Content("<script type='text/javascript'>alert('回复成功');history.go(-1);</script>");


            }
            catch (Exception ex)
            {

                return Content("<script type='text/javascript'>alert('回复失败');history.go(-1)</script>");
            }
            

        
        }



        /// <summary>
        /// 获取评论回复
        /// </summary>
        /// <returns></returns>
       
        public string loadReply()
        {

            int remarkId = int.Parse(Request.Form["remarkId"]);

            List<T013评论回复表> replyList = operateContext.BLLSession.IT013评论回复表BLL.GetListBy(m => m.评论ID == remarkId).OrderByDescending(m=>m.回复时间).ToList();

            List<replyViewModel> replyViewModelList = new List<replyViewModel>();

            foreach (var model in replyList)
            {

                replyViewModel tempModel = new replyViewModel();
                tempModel.T013_replyId = model.T013_replyId;
                tempModel.回复内容 = model.回复内容;
                System.DateTime date = (DateTime)model.回复时间;
                tempModel.回复时间 = date.ToString();
                tempModel.回复至ID = model.回复至ID;
                tempModel.评论ID = model.评论ID;
                tempModel.评论人ID = model.评论人ID;

                replyViewModelList.Add(tempModel);
            
            }



            JavaScriptSerializer js = new JavaScriptSerializer();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            js.Serialize(replyViewModelList, sb);



            return sb.ToString();




        }

        /// <summary>
        /// 获取最热文章
        /// </summary>
        /// <returns></returns>
        public string loadHot()
        {

            try
            {
                //获取当前页码
                if (Request.Form["page"] != null && Request.Form["page"] != "")
                {

                    int page = int.Parse(Request.Form["page"]);

                    if (page > getHotBlogs())
                    {

                        page = 0;

                    }

                    var list = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.T010_id != 0).Skip((page) * ConfigureSettings.getConfigureSettings().hotBlogNumber).Take(ConfigureSettings.getConfigureSettings().hotBlogNumber).OrderByDescending(m => m.点赞数);

                   

                    //定义博客集合
                    List<BlogViewModel> listBlogs = new List<BlogViewModel>();

                    foreach (var model in list)
                    {
                        BlogViewModel blog = new BlogViewModel();
                        blog.blogId = model.T010_id;
                        blog.标签 = model.标签;
                        blog.标题 = model.标题;
                        blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                        blog.图片链接 = model.图片链接;
                        blog.音乐链接 = model.音乐链接;
                        blog.正文 = model.正文;
                        blog.点赞数 = model.点赞数;
                        blog.是否封锁 = model.是否封锁;
                        blog.是否公开 = model.是否公开;
                        blog.发布时间 = (DateTime)model.发布时间;
                        listBlogs.Add(blog);

                    }

                    //序列化
                    JavaScriptSerializer js = new JavaScriptSerializer();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    js.Serialize(listBlogs, sb);

                    //自加一
                    page = page + 1;
                    //更新cookie中的值
                    HttpCookie pageHotBlogs = Request.Cookies["hotPageIndex"];

                    pageHotBlogs.Value = page.ToString();
                   
                    //更新cookie中的值
                    Response.Cookies.Add(pageHotBlogs);

                    return sb.ToString();


                }
                else
                {
                    var list = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.T010_id != 0).Take(ConfigureSettings.getConfigureSettings().hotBlogNumber).OrderByDescending(m => m.点赞数);



                    //定义博客集合
                    List<BlogViewModel> listBlogs = new List<BlogViewModel>();

                    foreach (var model in list)
                    {
                        BlogViewModel blog = new BlogViewModel();
                        blog.blogId = model.T010_id;
                        blog.标签 = model.标签;
                        blog.标题 = model.标题;
                        blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                        blog.图片链接 = model.图片链接;
                        blog.音乐链接 = model.音乐链接;
                        blog.正文 = model.正文;
                        blog.点赞数 = model.点赞数;
                        blog.是否封锁 = model.是否封锁;
                        blog.是否公开 = model.是否公开;
                        blog.发布时间 = (DateTime)model.发布时间;
                        listBlogs.Add(blog);

                    }


                    JavaScriptSerializer js = new JavaScriptSerializer();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    js.Serialize(listBlogs, sb);

                    HttpCookie hotPageCookie = new HttpCookie("hotPageIndex","1");
                   
                    
                    //将cookie值添加回客户端
                    Response.Cookies.Add(hotPageCookie);

                    return sb.ToString();




                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public string follow()
        { 

           string beFollowed=Request.Form["beFollowed"];
           int isToFollow=int.Parse(Request.Form["isToFollow"]);
           int currentNumber = int.Parse(Request.Form["currentNumber"]);

            //根据被关注者的用户名获取其邮箱
            string beFollowedEmail = SerachMethod.getEmailByUserName(beFollowed);
            if (beFollowed != null)
            {
                if (isToFollow == 1)
                {
                    T003关注表 t003 = new T003关注表();
                    t003.被关注者邮箱 = beFollowedEmail;
                    t003.关注者邮箱 = User.Identity.Name;
                    //添加进关注表
                    operateContext.BLLSession.IT003关注表BLL.Add(t003);
                    //修改粉丝数据


                    //改变currentNumber的值
                    currentNumber++;
                    object c = new object();
                    //同步代码块
                    lock (c)
                    {
                        //修改用户表的点赞数目


                        T001用户表 beFollowedUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == beFollowedEmail)[0];

                        beFollowedUser.粉丝数 = currentNumber;

                        operateContext.BLLSession.IT001用户表BLL.Modify(beFollowedUser, "粉丝数");

                        return currentNumber.ToString();

                    }



                }
                else
                { 

                        //当前用户
                        string currentUser=User.Identity.Name;
                        //删除数据
                        operateContext.BLLSession.IT003关注表BLL.DelBy(m => m.关注者邮箱 == currentUser && m.被关注者邮箱== beFollowedEmail);

                        //改变currentNumber的值
                        currentNumber--;

                        //同步修改博文表中点赞数的值
                        object c = new object();

                        //同步代码块
                        lock (c)
                        {
                            //修改点赞表的点赞数目
                            T001用户表 beFollowedUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == beFollowedEmail)[0];

                            beFollowedUser.粉丝数 = currentNumber;

                            operateContext.BLLSession.IT001用户表BLL.Modify(beFollowedUser, "粉丝数");

                            //首先detach一下
                            //operateContext.BLLSession.IT010博文表BLL.DetachModel(blogId);


                        }

                        return currentNumber.ToString();

                
                
                
                }


                

            }
            else
            {


                return "false";
            
            }

        
        
        }

        /// <summary>
        /// 关注也面的一系列操作
        /// </summary>
        /// <returns></returns>
        public string followMethod()
        {

            string beFollowed = Request.Form["beFollowed"];
            int isToFollow = int.Parse(Request.Form["isToFollow"]);
           //int currentNumber = int.Parse(Request.Form["currentNumber"]);

            //根据被关注者的用户名获取其邮箱
            string beFollowedEmail = SerachMethod.getEmailByUserName(beFollowed);
            if (beFollowed != null)
            {
                if (isToFollow == 1)
                {
                    T003关注表 t003 = new T003关注表();
                    t003.被关注者邮箱 = beFollowedEmail;
                    t003.关注者邮箱 = User.Identity.Name;
                    //添加进关注表
                    operateContext.BLLSession.IT003关注表BLL.Add(t003);
                    //修改粉丝数据


                    //改变currentNumber的值
                   // currentNumber++;
                    object c = new object();
                    //同步代码块
                    lock (c)
                    {
                        //修改用户表的点赞数目


                        T001用户表 beFollowedUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == beFollowedEmail)[0];

                        beFollowedUser.粉丝数 = beFollowedUser.粉丝数+1;

                        operateContext.BLLSession.IT001用户表BLL.Modify(beFollowedUser, "粉丝数");

                        return "/Home/GetUsers";
                            

                    }



                }
                else
                    if (isToFollow == 0)
                    {

                        //当前用户
                        string currentUser = User.Identity.Name;
                        //删除数据
                        operateContext.BLLSession.IT003关注表BLL.DelBy(m => m.关注者邮箱 == currentUser && m.被关注者邮箱 == beFollowedEmail);

                        //改变currentNumber的值
                        //currentNumber--;

                        //同步修改博文表中点赞数的值
                        object c = new object();

                        //同步代码块
                        lock (c)
                        {
                            //修改点赞表的点赞数目
                            T001用户表 beFollowedUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == beFollowedEmail)[0];

                            beFollowedUser.粉丝数 = beFollowedUser.粉丝数 - 1;

                            operateContext.BLLSession.IT001用户表BLL.Modify(beFollowedUser, "粉丝数");

                            //首先detach一下
                            //operateContext.BLLSession.IT010博文表BLL.DetachModel(blogId);


                        }

                        return "/Home/GetUsers_HasFollowed";




                    }
                    else
                    {
                        //当前用户
                        string currentUser = User.Identity.Name;
                        //删除数据
                        operateContext.BLLSession.IT003关注表BLL.DelBy(m => m.关注者邮箱 == beFollowedEmail && m.被关注者邮箱 == currentUser);

                        //改变currentNumber的值
                        //currentNumber--;

                        //同步修改博文表中点赞数的值
                        object c = new object();

                        //同步代码块
                        lock (c)
                        {
                            //修改点赞表的点赞数目
                            T001用户表 beFollowedUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == currentUser)[0];

                            beFollowedUser.粉丝数 = beFollowedUser.粉丝数 - 1;

                            operateContext.BLLSession.IT001用户表BLL.Modify(beFollowedUser, "粉丝数");

                            //首先detach一下
                            //operateContext.BLLSession.IT010博文表BLL.DetachModel(blogId);


                        }

                        return "/Home/GetMyFans";


                        
                    
                    }



            }
            else
            {


                return "false";

            }

        
        
        
        }

        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <returns></returns>
        public string updateUserName()
        {

            try
            {
                string newUserName = Request.Form["newUserName"];

                var user = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name).FirstOrDefault();

                user.用户名 = newUserName;

                //保存修改

                operateContext.BLLSession.IT001用户表BLL.Modify(user, "用户名");

                return  newUserName;

            }
            catch (Exception ex)
            {

                return "false";
                   
                
            }
            

            



        
        }

        /// <summary>
        /// 检验旧密码输入是否正确
        /// </summary>
        /// <returns></returns>
        public string checkOldPassword()
        {

            try
            {

                var oldPassword = Request.Form["oldPassword"];

                string oldPasswordMD5 = Encryption.Encrype.GetMD5(oldPassword);

                var user = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name && m.密码==oldPasswordMD5).FirstOrDefault();


                if (user != null)
                {

                    return "success";
                }
                else
                {

                    return "false";
                    
                }


            }
            catch (Exception ex)
            {

                return "false";
                
            }


            
            
        


        
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string updatePassword()
        {

            try
            {
                string newPassword = Request.Form["newPassword"];

                string newPasswordMD5 = Encryption.Encrype.GetMD5(newPassword);

                var user = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == User.Identity.Name).FirstOrDefault();

                user.密码 = newPasswordMD5;

                operateContext.BLLSession.IT001用户表BLL.Modify(user, "密码");

                return "success";


            }
            catch (Exception ex)
            {

                return "false";
            }


        
        }

    }
}
