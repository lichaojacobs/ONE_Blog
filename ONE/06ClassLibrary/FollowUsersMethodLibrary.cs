using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONE._07DataEntity;

namespace ONE._06ClassLibrary
{
    /// <summary>
    /// 用户对于关注的一些操作方法
    /// </summary>
    public class FollowUsersMethodLibrary
    {
        /// <summary>
        /// 获取未关注的用户
        /// </summary>
        public static List<T001用户表> getUsers_NotFollowed()
        {
         
             ///获取全部用户
             List<T001用户表> users = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 != null);
            //获取用户关注的列表
            var followingList = operateContext.BLLSession.IT003关注表BLL.GetListBy(m => m.关注者邮箱 ==HttpContext.Current.User.Identity.Name).Select(m => m.被关注者邮箱).ToList();

            //从users列表中移除已关注的用户
            foreach(var i in followingList)
            {
            
                var temp=users.Where(m=>m.注册邮箱==i).FirstOrDefault();

                if(temp!=null)
                {
                
                    //移除
                    users.Remove(temp);
                
                }
            
            }

            //移除掉我自身
            var myself = users.Where(m => m.注册邮箱 == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            users.Remove(myself);

            return users;

            

            


 

        
        }

        /// <summary>
        /// 获取关注的用户
        /// </summary>
        /// <returns></returns>
        public static List<T001用户表> getUsers_HasFollowed()
        {

            //获取用户关注的列表
            var followingList = operateContext.BLLSession.IT003关注表BLL.GetListBy(m => m.关注者邮箱 == HttpContext.Current.User.Identity.Name).Select(m => m.被关注者邮箱).ToList();

            List<T001用户表> hasFollowedUsers = new List<T001用户表>();
            foreach (var email in followingList)
            {
                var tempUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == email).FirstOrDefault();

                hasFollowedUsers.Add(tempUser);

            
            }

            return hasFollowedUsers;

            
        
        }

        /// <summary>
        /// 获取我的粉丝
        /// </summary>
        /// <returns></returns>
        public static List<T001用户表> getMyFans()
        {

            //获取关注我的粉丝邮箱列表
            var followingList = operateContext.BLLSession.IT003关注表BLL.GetListBy(m => m.被关注者邮箱 == HttpContext.Current.User.Identity.Name).Select(m => m.关注者邮箱).ToList();

            List<T001用户表> FollowMeUsers = new List<T001用户表>();
            foreach (var email in followingList)
            {
                var tempUser = operateContext.BLLSession.IT001用户表BLL.GetListBy(m => m.注册邮箱 == email).FirstOrDefault();

                FollowMeUsers.Add(tempUser);


            }

            return FollowMeUsers;
        
        }


    }
}