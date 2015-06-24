 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ONE._07DataEntity;
using ONE._01IBLL;

namespace ONE._02BLL
{
	public partial class sysdiagramsBLL: BaseBLL<sysdiagrams>,IsysdiagramsBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IsysdiagramsDAL;
		}
    }
	public partial class T001用户表BLL: BaseBLL<T001用户表>,IT001用户表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT001用户表DAL;
		}
    }
	public partial class T002验证码表BLL: BaseBLL<T002验证码表>,IT002验证码表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT002验证码表DAL;
		}
    }
	public partial class T003关注表BLL: BaseBLL<T003关注表>,IT003关注表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT003关注表DAL;
		}
    }
	public partial class T010博文表BLL: BaseBLL<T010博文表>,IT010博文表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT010博文表DAL;
		}
    }
	public partial class T011点赞表BLL: BaseBLL<T011点赞表>,IT011点赞表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT011点赞表DAL;
		}
    }
	public partial class T012评论表BLL: BaseBLL<T012评论表>,IT012评论表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT012评论表DAL;
		}
    }
	public partial class T013评论回复表BLL: BaseBLL<T013评论回复表>,IT013评论回复表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT013评论回复表DAL;
		}
    }
}