
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONE._01IBLL;

namespace ONE._02BLL
{
	public partial class BLLSession:IBLLSession
    {
		#region 01 业务接口 IsysdiagramsBLL
		IsysdiagramsBLL isysdiagramsBLL;
		public IsysdiagramsBLL IsysdiagramsBLL
		{
			get
			{
				if(isysdiagramsBLL==null)
					isysdiagramsBLL= new sysdiagramsBLL();
				return isysdiagramsBLL;
			}
			set
			{
				isysdiagramsBLL= value;
			}
		}
		#endregion

		#region 02 业务接口 IT001用户表BLL
		IT001用户表BLL iT001用户表BLL;
		public IT001用户表BLL IT001用户表BLL
		{
			get
			{
				if(iT001用户表BLL==null)
					iT001用户表BLL= new T001用户表BLL();
				return iT001用户表BLL;
			}
			set
			{
				iT001用户表BLL= value;
			}
		}
		#endregion

		#region 03 业务接口 IT002验证码表BLL
		IT002验证码表BLL iT002验证码表BLL;
		public IT002验证码表BLL IT002验证码表BLL
		{
			get
			{
				if(iT002验证码表BLL==null)
					iT002验证码表BLL= new T002验证码表BLL();
				return iT002验证码表BLL;
			}
			set
			{
				iT002验证码表BLL= value;
			}
		}
		#endregion

		#region 04 业务接口 IT003关注表BLL
		IT003关注表BLL iT003关注表BLL;
		public IT003关注表BLL IT003关注表BLL
		{
			get
			{
				if(iT003关注表BLL==null)
					iT003关注表BLL= new T003关注表BLL();
				return iT003关注表BLL;
			}
			set
			{
				iT003关注表BLL= value;
			}
		}
		#endregion

		#region 05 业务接口 IT010博文表BLL
		IT010博文表BLL iT010博文表BLL;
		public IT010博文表BLL IT010博文表BLL
		{
			get
			{
				if(iT010博文表BLL==null)
					iT010博文表BLL= new T010博文表BLL();
				return iT010博文表BLL;
			}
			set
			{
				iT010博文表BLL= value;
			}
		}
		#endregion

		#region 06 业务接口 IT011点赞表BLL
		IT011点赞表BLL iT011点赞表BLL;
		public IT011点赞表BLL IT011点赞表BLL
		{
			get
			{
				if(iT011点赞表BLL==null)
					iT011点赞表BLL= new T011点赞表BLL();
				return iT011点赞表BLL;
			}
			set
			{
				iT011点赞表BLL= value;
			}
		}
		#endregion

		#region 07 业务接口 IT012评论表BLL
		IT012评论表BLL iT012评论表BLL;
		public IT012评论表BLL IT012评论表BLL
		{
			get
			{
				if(iT012评论表BLL==null)
					iT012评论表BLL= new T012评论表BLL();
				return iT012评论表BLL;
			}
			set
			{
				iT012评论表BLL= value;
			}
		}
		#endregion

		#region 08 业务接口 IT013评论回复表BLL
		IT013评论回复表BLL iT013评论回复表BLL;
		public IT013评论回复表BLL IT013评论回复表BLL
		{
			get
			{
				if(iT013评论回复表BLL==null)
					iT013评论回复表BLL= new T013评论回复表BLL();
				return iT013评论回复表BLL;
			}
			set
			{
				iT013评论回复表BLL= value;
			}
		}
		#endregion

    }

}