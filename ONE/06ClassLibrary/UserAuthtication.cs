using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONE._06ClassLibrary
{
    /// <summary>
    /// 获取用户角色
    /// </summary>
   public class UserAuthtication
    {
       public  string[] GetRoles(string UserName )
       {
           string[] roles = new string[1];

           var tempRole1 = operateContext.BLLSession.IT001用户表BLL.GetListBy(m=>m.注册邮箱==UserName).FirstOrDefault().角色.ToString();
           roles[0] = tempRole1;
          
           return roles;
       }
    }
}
