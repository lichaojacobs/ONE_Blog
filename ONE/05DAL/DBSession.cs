
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONE._04IDAL;

namespace ONE._05DAL
{
	public partial class DBSession:IDBSession
    {
		#region 01 数据接口 IsysdiagramsDAL
		IsysdiagramsDAL isysdiagramsDAL;
		public IsysdiagramsDAL IsysdiagramsDAL
		{
			get
			{
				if(isysdiagramsDAL==null)
					isysdiagramsDAL= new sysdiagramsDAL();
				return isysdiagramsDAL;
			}
			set
			{
				isysdiagramsDAL= value;
			}
		}
		#endregion

		#region 02 数据接口 IT001用户表DAL
		IT001用户表DAL iT001用户表DAL;
		public IT001用户表DAL IT001用户表DAL
		{
			get
			{
				if(iT001用户表DAL==null)
					iT001用户表DAL= new T001用户表DAL();
				return iT001用户表DAL;
			}
			set
			{
				iT001用户表DAL= value;
			}
		}
		#endregion

		#region 03 数据接口 IT002验证码表DAL
		IT002验证码表DAL iT002验证码表DAL;
		public IT002验证码表DAL IT002验证码表DAL
		{
			get
			{
				if(iT002验证码表DAL==null)
					iT002验证码表DAL= new T002验证码表DAL();
				return iT002验证码表DAL;
			}
			set
			{
				iT002验证码表DAL= value;
			}
		}
		#endregion

		#region 04 数据接口 IT003关注表DAL
		IT003关注表DAL iT003关注表DAL;
		public IT003关注表DAL IT003关注表DAL
		{
			get
			{
				if(iT003关注表DAL==null)
					iT003关注表DAL= new T003关注表DAL();
				return iT003关注表DAL;
			}
			set
			{
				iT003关注表DAL= value;
			}
		}
		#endregion

		#region 05 数据接口 IT010博文表DAL
		IT010博文表DAL iT010博文表DAL;
		public IT010博文表DAL IT010博文表DAL
		{
			get
			{
				if(iT010博文表DAL==null)
					iT010博文表DAL= new T010博文表DAL();
				return iT010博文表DAL;
			}
			set
			{
				iT010博文表DAL= value;
			}
		}
		#endregion

		#region 06 数据接口 IT011点赞表DAL
		IT011点赞表DAL iT011点赞表DAL;
		public IT011点赞表DAL IT011点赞表DAL
		{
			get
			{
				if(iT011点赞表DAL==null)
					iT011点赞表DAL= new T011点赞表DAL();
				return iT011点赞表DAL;
			}
			set
			{
				iT011点赞表DAL= value;
			}
		}
		#endregion

		#region 07 数据接口 IT012评论表DAL
		IT012评论表DAL iT012评论表DAL;
		public IT012评论表DAL IT012评论表DAL
		{
			get
			{
				if(iT012评论表DAL==null)
					iT012评论表DAL= new T012评论表DAL();
				return iT012评论表DAL;
			}
			set
			{
				iT012评论表DAL= value;
			}
		}
		#endregion

		#region 08 数据接口 IT013评论回复表DAL
		IT013评论回复表DAL iT013评论回复表DAL;
		public IT013评论回复表DAL IT013评论回复表DAL
		{
			get
			{
				if(iT013评论回复表DAL==null)
					iT013评论回复表DAL= new T013评论回复表DAL();
				return iT013评论回复表DAL;
			}
			set
			{
				iT013评论回复表DAL= value;
			}
		}
		#endregion

    }

}