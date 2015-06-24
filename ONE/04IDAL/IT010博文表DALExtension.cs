using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;

namespace ONE._04IDAL
{
    public partial interface IT010博文表DAL : IBaseDAL<T010博文表>
    {

        //根据时间先后顺序获取前n的博文
         List<T010博文表> getIndexBlogs();

        //获取所有博客的数目
         int getBlogCount();

        //根据页数获得第几页的博客内容
         List<T010博文表> getBlogsByPageIndex(int pageIndex);

         /// <summary>
         /// 添加新的博客
         /// </summary>
         /// <param name="blog"></param>
         /// <returns></returns>
         bool addBlog(ONE.Models.BlogViewModel blog);


         /// <summary>
         ///   根据博客id返回博客的详情
         /// </summary>
         /// <param name="blogId"></param>
         /// <returns></returns>
         ONE.Models.BlogDetailModel getBlogDetail(int blogId);

         /// <summary>
         /// 通过博客id获取评论
         /// </summary>
         /// <returns></returns>
         ONE.Models.RemarkViewModel getCommentsByBlogId(int blogId);

         /// <summary>
         /// 添加评论
         /// </summary>
         /// <param name="remarkContent"></param>
         /// <param name="blogId"></param>
         /// <returns></returns>
         bool addRemark(string remarkContent, int blogId);

        /// <summary>
        /// 给博文点赞
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
         bool clickHit(int blogId);



    }
}