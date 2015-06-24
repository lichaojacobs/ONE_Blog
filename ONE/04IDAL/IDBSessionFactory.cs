using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONE._04IDAL
{
    public interface IDBSessionFactory
    {

        /// <summary>
        ///获取数据仓储工厂
        /// </summary>
       
         IDBSession GetDBSession();


    }
}