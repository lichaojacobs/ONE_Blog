
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONE._01IBLL
{
	public partial interface IBLLSession
    {
		IsysdiagramsBLL IsysdiagramsBLL{get;set;}
		IT001用户表BLL IT001用户表BLL{get;set;}
		IT002验证码表BLL IT002验证码表BLL{get;set;}
		IT003关注表BLL IT003关注表BLL{get;set;}
		IT010博文表BLL IT010博文表BLL{get;set;}
		IT011点赞表BLL IT011点赞表BLL{get;set;}
		IT012评论表BLL IT012评论表BLL{get;set;}
		IT013评论回复表BLL IT013评论回复表BLL{get;set;}
    }

}