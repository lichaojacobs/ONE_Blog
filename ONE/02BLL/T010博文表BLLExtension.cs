using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;
using ONE._01IBLL;

namespace ONE._02BLL
{
    public partial class T010博文表BLL : BaseBLL<T010博文表>, IT010博文表BLL
    {

        protected _04IDAL.IT010博文表DAL idal2;


        //根据时间先后顺序获取前n的博文
        public List<T010博文表> getIndexBlogs()
        {

            try
            {

                idal2 = DBSession.IT010博文表DAL;
                return idal2.getIndexBlogs();

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
                idal2 = DBSession.IT010博文表DAL;
                return idal2.getBlogCount();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //根据页数获得第几页的博客内容
        public List<T010博文表> getBlogsByPageIndex(int pageIndex)
        {

            try
            {

                idal2 = DBSession.IT010博文表DAL;
                return idal2.getBlogsByPageIndex(pageIndex);


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        /// <summary>
        /// 添加新的博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool addBlog(ONE.Models.BlogViewModel blog) {

            idal2 = DBSession.IT010博文表DAL;
            return  idal2.addBlog(blog);
        
        
        }

        /// <summary>
        ///   根据博客id返回博客的详情
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public ONE.Models.BlogDetailModel getBlogDetail(int blogId) {

            idal2 = DBSession.IT010博文表DAL;

            return idal2.getBlogDetail(blogId);
        
        
        
        }


        /// <summary>
        /// 通过博客id获取评论
        /// </summary>
        /// <returns></returns>
        public ONE.Models.RemarkViewModel getCommentsByBlogId(int blogId) {

            idal2 = DBSession.IT010博文表DAL;
            return  idal2.getCommentsByBlogId(blogId);
        
        
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="remarkContent"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public bool addRemark(string remarkContent, int blogId) {

            idal2 = DBSession.IT010博文表DAL;
            return idal2.addRemark(remarkContent, blogId);
            
        
        }

        /// <summary>
        /// 给博文点赞
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public bool clickHit(int blogId)
        {

            idal2 = DBSession.IT010博文表DAL;

            return idal2.clickHit(blogId);

        
        }

    }
}