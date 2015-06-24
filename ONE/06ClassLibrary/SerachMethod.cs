using ONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE._06ClassLibrary
{
    public class SerachMethod
    {
        /// <summary>
        /// 返回要搜索的博客
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public static List<ONE.Models.BlogViewModel> getSearchBlogs(string keyWords)
        { 

            //获得博主邮箱
           string email= getEmailByUserName(keyWords);

           if (email != null)
           {
               var list_author = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == email);

               if (list_author != null)
               {
                   return convertToModels(list_author);
               }
               else
               {
                   return null;
               }

           }
           else//根据其他字段搜索
           {

               //搜索标题
               var list_ti = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.标题.Contains(keyWords) || m.标签.Contains(keyWords));

               if (list_ti != null)
               {
                   return convertToModels(list_ti);

               }
               else
               {
                   return null;

               }
           }


           //var list_qian = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.标签.Contains(keyWords));

           //if (list_ti != null && list_qian != null)
           //{

           //    list_ti.Concat(list_qian);

           //    return convertToModels(list_ti);

           //}
           //else
           //{
           //    if (list_ti != null)
           //    {
           //        return convertToModels(list_ti);

           //    }
           //    if (list_qian != null)
           //    {

           //        return convertToModels(list_qian);
               
           //    }
           //    return null;

           //}

        
        
        
        }

        public static List<ONE.Models.BlogViewModel> convertToModels(List<ONE._07DataEntity.T010博文表> entities)
        {
            //定义博客集合
            List<BlogViewModel> listBlogs = new List<BlogViewModel>();
            foreach (var model in entities)
            {
                BlogViewModel blog = new BlogViewModel();
                blog.blogId = model.T010_id;
                blog.标签 = model.标签;
                blog.标题 = model.标题;
                blog.用户名 = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail(model.博主邮箱);
                blog.图片链接 = model.图片链接;
                blog.音乐链接 = model.音乐链接;
                blog.正文 = model.正文 + "&lt;p&gt;......&lt;p&gt;";
                blog.点赞数 = model.点赞数;
                blog.是否封锁 = model.是否封锁;
                blog.是否公开 = model.是否公开;
                blog.发布时间 = (DateTime)model.发布时间;
                listBlogs.Add(blog);

            }

            return listBlogs;

        
        
        }
        /// <summary>
        /// 获得博主名字
        /// </summary>
        /// <returns></returns>
        public static string getEmailByUserName(string UserName)
        {

            var t001 = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.用户名==UserName).FirstOrDefault();

            //string emailList=new List<string>();
            //如果不为空则返回
            if (t001 != null)
            {



                return t001.注册邮箱;

            }
            else
            {

                return null;

            }

        
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string getUserNameByEmail(string email)
        {

            var t001 = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱==email).FirstOrDefault();

            //string emailList=new List<string>();
            //如果不为空则返回
            if (t001 != null)
            {



                return t001.用户名;

            }
            else
            {

                return null;

            }

        
        }

    }
}