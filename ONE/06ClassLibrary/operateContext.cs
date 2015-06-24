using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONE._01IBLL;
using ONE._03DI;

namespace ONE._06ClassLibrary
{
  public  class operateContext
    {

      public static IBLLSession BLLSession = SpringHelper.GetObject<IBLLSession>("BLLSession");

    }
}
