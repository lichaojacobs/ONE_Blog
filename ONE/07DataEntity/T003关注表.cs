//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ONE._07DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class T003关注表
    {
        public int T003_id { get; set; }
        public string 关注者邮箱 { get; set; }
        public string 被关注者邮箱 { get; set; }
    
        public virtual T001用户表 T001用户表 { get; set; }
        public virtual T001用户表 T001用户表1 { get; set; }
    }
}
