
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONE._04IDAL
{
	public partial interface IDBSession
    {
		IsysdiagramsDAL IsysdiagramsDAL{get;set;}
		IT001用户表DAL IT001用户表DAL{get;set;}
		IT002验证码表DAL IT002验证码表DAL{get;set;}
		IT003关注表DAL IT003关注表DAL{get;set;}
		IT010博文表DAL IT010博文表DAL{get;set;}
		IT011点赞表DAL IT011点赞表DAL{get;set;}
		IT012评论表DAL IT012评论表DAL{get;set;}
		IT013评论回复表DAL IT013评论回复表DAL{get;set;}
    }

}