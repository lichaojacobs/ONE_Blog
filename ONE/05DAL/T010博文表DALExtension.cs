using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;
using ONE._06ClassLibrary;

namespace ONE._05DAL
{
    public partial class T010博文表DAL : BaseDAL<T010博文表>, ONE._04IDAL.IT010博文表DAL
    {

        //根据时间先后顺序获取前n的博文
        public List<T010博文表> getIndexBlogs()
        {

            List<T010博文表> blogList = new List<T010博文表>();

            try
            {
                //获取最新发布的n条博客
                int blogCount = ConfigureSettings.getConfigureSettings().blogCount;
                blogList = GetListBy(m => m.T010_id != null && m.是否公开 == true).OrderByDescending(m => m.发布时间).Take(blogCount).ToList();
                return blogList;

            }
            catch (Exception ex)
            {

                throw ex;
            }




        }

        //获取所有博客的数目
        public int getBlogCount()
        {

            try
            {
                return GetListBy(m => m.T010_id != null).Count();

            }
            catch (Exception ex)
            {

                throw ex;
            }




        }

        //根据页数获得第几页的博客内容
        public List<T010博文表> getBlogsByPageIndex(int pageIndex)
        {

            var collection = GetListBy(m => m.T010_id != null && m.是否公开 == true && m.是否封锁 == false);
            int blogCount = ConfigureSettings.getConfigureSettings().blogCount;
            return collection.OrderByDescending(m => m.发布时间).Skip((pageIndex - 1) * blogCount).Take(blogCount).ToList();


        }

        /// <summary>
        /// 添加新的博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool addBlog(ONE.Models.BlogViewModel blog) {

            try
            {



                T010博文表 blogInstance = new T010博文表();
                blogInstance.标签 = blog.标签;
                blogInstance.标题 = blog.标题;
                blogInstance.正文 = HttpUtility.HtmlEncode(blog.正文);
                blogInstance.图片链接 = HttpContext.Current.Session["photoUrl"].ToString() == null ? "/content/defaultfigure.jpg" : HttpContext.Current.Session["photoUrl"].ToString();
                blogInstance.是否公开 = blog.是否公开;
                blogInstance.博主邮箱 = HttpContext.Current.User.Identity.Name;
                blogInstance.发布时间 =(DateTime)System.DateTime.Now;
                

               return (base.Add(blogInstance))==-1?false:true;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        
        }

        /// <summary>
        ///   根据博客id返回博客的详情
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public ONE.Models.BlogDetailModel getBlogDetail(int blogId) {

            try
            {
                //实例化一个blogDetail类
                ONE.Models.BlogDetailModel blogDetail = new Models.BlogDetailModel();
                //获取博客的详细内容
                ONE.Models.BlogViewModel blog = new Models.BlogViewModel();
                //通过Id返回博客详细
                T010博文表 model = base.GetListBy(m => m.T010_id == blogId)[0];

                blog.blogId = model.T010_id;
                blog.标签 = model.标签;
                blog.标题 = model.标题;
                blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                blog.图片链接 = model.图片链接;
                blog.音乐链接 = model.音乐链接;
                blog.正文 = model.正文;
                blog.点赞数 = model.点赞数;
                blog.博主邮箱 = model.博主邮箱;
                blog.是否封锁 = model.是否封锁;
                blog.是否公开 = model.是否公开;
                blog.发布时间 = (DateTime)model.发布时间;

                //将blog引用复制给blogdetail
                blogDetail.blogDeail = blog;

                //根据id获取评论

                var remarklist = operateContext.BLLSession.IT012评论表BLL.GetListBy(m => m.博文id == blogId);
                if (remarklist != null)
                {
                    //声明一个评论集合类
                    List<ONE.Models.RemarkViewModel> remarkModelList = new List<ONE.Models.RemarkViewModel>();

                    foreach (var m in remarklist)
                    {

                     

                        ONE.Models.RemarkViewModel tempRemarkModel = new Models.RemarkViewModel();
                        
                        tempRemarkModel.remarkConetent = m.评论内容;
                        tempRemarkModel.remarkId = m.T012_id;
                        tempRemarkModel.remarkTime =(DateTime)m.评论时间;
                        tempRemarkModel.remarkUser = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(m.评论用户id);
                        tempRemarkModel.remarkUserLogo = operateContext.BLLSession.IT001用户表BLL.GetListBy(i => i.注册邮箱 == m.评论用户id).FirstOrDefault().头像;
                        remarkModelList.Add(tempRemarkModel);
                        

                    }

                    //将评论的引用复制给model,并且按照降序排列
                    blogDetail.remarks = remarkModelList.OrderByDescending(m=>m.remarkTime);

                }

                return blogDetail;

            }
            catch (Exception ex)
            {
                

                throw ex;


            }
           

        
        
        
        }

        /// <summary>
        /// 通过博客id获取评论
        /// </summary>
        /// <returns></returns>
        public ONE.Models.RemarkViewModel getCommentsByBlogId(int blogId) {


            ///待改进。。。
            return null;
        
        }


        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="remarkContent"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public bool addRemark(string remarkContent, int blogId)
        {
            try
            {
                T012评论表 remarkModel = new T012评论表();
                remarkModel.博文id = blogId;
                remarkModel.评论内容 = remarkContent;
                remarkModel.评论时间 = (DateTime)System.DateTime.Now;
                remarkModel.评论用户id = HttpContext.Current.User.Identity.Name;

                operateContext.BLLSession.IT012评论表BLL.Add(remarkModel);

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            





            



        }


        /// <summary>
        /// 给博文点赞
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public bool clickHit(int blogId)
        {

            return true;
            
        
        }



    }
}