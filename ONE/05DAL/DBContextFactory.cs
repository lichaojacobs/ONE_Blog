using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
namespace ONE._05DAL
{
    public class DBContextFactory
    {

        /// <summary>
        /// 创建上下文对象，使用单例模式
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDBContext()
        {
            //从当前线程中 获取 EF上下文对象
            DbContext dbContext = CallContext.GetData(typeof(DBContextFactory).Name) as DbContext;

            if (dbContext == null)
            {
                dbContext = new ONE._07DataEntity.BlogEntities();

                CallContext.SetData(typeof(DBContextFactory).Name, dbContext);
                return dbContext;


            }
            else
                return dbContext;


        
        }


    }
}