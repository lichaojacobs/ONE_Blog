using ONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ONE._07DataEntity;
using System.Web;

namespace ONE._06ClassLibrary
{
    /// <summary>
    /// 获取我关注的人的博客
    /// </summary>
    public class getMyFavoriteBlogs
    {


        public static List<T010博文表> getBlogs(string email)
        { 
            
            //获取用户关注的列表
            var follwingList = operateContext.BLLSession.IT003关注表BLL.GetListBy(m => m.关注者邮箱 == email).Select(m => m.被关注者邮箱).ToList();

            List<T010博文表> blogList = new List<T010博文表>();

            foreach (var beFollowedUser in follwingList)
            {

                var blogs = operateContext.BLLSession.IT010博文表BLL.GetListBy(m => m.博主邮箱 == beFollowedUser);
                if (blogs != null)
                {
                    //concat是只要有一个为空就抛出空异常
                    foreach (var i in blogs)
                    {
                        blogList.Add(i);
                    
                    }
                }

            
            }

            //返回结果
            return blogList;

        

        
        }

       




    }
}