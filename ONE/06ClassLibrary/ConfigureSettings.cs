using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE._06ClassLibrary
{
    public class ConfigureSettings
    {
        //设置首页呈现博客的数目
        public int blogCount;
        //用户的别名
        public string userName;

        public int hotBlogNumber;

        private static  ConfigureSettings configure;

        /// <summary>
        /// 自行设置博客首页显示的博文数目
        /// </summary>
        private ConfigureSettings()
        {
            //userName = operateContext.BLLSession.IT001用户表BLL.NickNameByEmail();

            blogCount = 10;

            hotBlogNumber = 2;

        }

        //获取设置类的单例
        public static ConfigureSettings getConfigureSettings()
        {
            if (configure == null)
            {
                configure = new ConfigureSettings();
                return configure;
            }
            else
            {
                return configure;
            }

        
        }

        









    }
}