using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
namespace CEMM.BLL
{
	/// <summary>
	/// quotaEngiInfo
	/// </summary>
	public partial class quotaEngiInfo
	{
		private readonly CEMM.DAL.quotaEngiInfo dal=new CEMM.DAL.quotaEngiInfo();
		public quotaEngiInfo()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string itermid)
		{
			return dal.Exists(itermid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.quotaEngiInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.quotaEngiInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string itermid)
		{
			
			return dal.Delete(itermid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string itermidlist )
		{
			return dal.DeleteList(itermidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.quotaEngiInfo GetModel(string itermid)
		{
			
			return dal.GetModel(itermid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.quotaEngiInfo GetModelByCache(string itermid)
		{
			
			string CacheKey = "quotaEngiInfoModel-" + itermid;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(itermid);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CEMM.Model.quotaEngiInfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.quotaEngiInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.quotaEngiInfo> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.quotaEngiInfo> modelList = new List<CEMM.Model.quotaEngiInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.quotaEngiInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据itermname,获得数据列表
        /// </summary>
        public DataSet GetListByname(string itermname)
        {
            return dal.GetListByItermName(itermname);
        }


        /// <summary>
        /// 根据查询条件查找对应定额信息行的基础工作量信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListForBase(string strWhere)
        {
            return dal.GetListForBase(strWhere);
        }
        

		#endregion  ExtensionMethod
	}
}

