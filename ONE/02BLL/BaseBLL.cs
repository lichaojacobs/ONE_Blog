using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ONE._04IDAL;
using ONE._05DAL;
using ONE._03DI;



namespace ONE._02BLL
{
    public abstract class BaseBLL<T> : ONE._01IBLL.IBaseBLL<T> where T : class,new()
    {
        public BaseBLL()
        {
            SetDAL();
        }

   

        //1,定义数据操作层父接口类
        //protected DAL.BaseDAL<T> idal;//= new DAL.BaseDAL<T>();
        protected IBaseDAL<T> idal;
        
        /// <summary>
        /// 由子类实现，为 业务父类 里的 数据接口对象 设置 值！
        /// </summary>
        public abstract void SetDAL();


        private IDBSession iDbSession;

        #region 数据仓储 属性 + IDBSession DBSession
        /// <summary>
        /// 数据仓储 属性
        /// </summary>
        public IDBSession DBSession
        {
            get
            {
                if (iDbSession == null)
                {
                    
                    IDBSessionFactory sessionFactory = SpringHelper.GetObject<IDBSessionFactory>("DBSessionFactory");

                    //3.通过 工厂 创建 DBSession对象
                    iDbSession = sessionFactory.GetDBSession();
                }
                return iDbSession;
            }
        }
        #endregion

        
        //2，定义增删改查的方法
        //2.增删改查方法
        #region 1.0 新增 实体 +int Add(T model)
        /// <summary>
        /// 新增 实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T model)
        {
            return idal.Add(model);

        }
        #endregion

        #region 2。0 根据 用户 id 删除 +int Del(int uId)
        /// <summary>
        /// 根据 用户 id 删除
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public int Del(T model)
        {
            return idal.Del(model);
        }
        #endregion

        #region 3.0 根据条件删除 +int DelBy(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 3.0 根据条件删除
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public int DelBy(Expression<Func<T, bool>> delWhere)
        {
            return idal.DelBy(delWhere);
        }
        #endregion

        #region 4.0 修改 +int Modify(T model, params string[] proNames)
        /// <summary>
        /// 4.0 修改，如：
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public int Modify(T model,  params string[] proNames)
        {
            return idal.Modify(model, proNames);
        }
        #endregion

        public void DetachModel(int blogId)
        {

            idal.DetachModel(blogId);


        }

        #region  4.0 批量修改 +int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        /// <summary>
        ///  4.0 批量修改 +int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="whereLambda"></param>
        /// <param name="modifiedProNames"></param>
        /// <returns></returns>
        public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            return idal.ModifyBy(model, whereLambda, modifiedProNames);
        }
        #endregion

        #region 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// <summary>
        /// 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            return idal.GetListBy(whereLambda);
        }
        #endregion

        #region 5.1 根据条件 排序 和查询 + List<T> GetListBy<TKey>
        /// <summary>
        /// 5.1 根据条件 排序 和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            return idal.GetListBy(whereLambda, orderLambda);
        }
        #endregion

        #region 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// <summary>
        /// 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排序 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy)
        {
            return idal.GetPagedList(pageIndex, pageSize, whereLambda, orderBy);
        }
        #endregion

    }
}
