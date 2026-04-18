using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
namespace CEMM.BLL
{
	/// <summary>
	/// lot
	/// </summary>
	public partial class lot
	{
		private readonly CEMM.DAL.lot dal=new CEMM.DAL.lot();
		public lot()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string lotid)
		{
			return dal.Exists(lotid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.lot model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.lot model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string lotid)
		{
			
			return dal.Delete(lotid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string lotidlist )
		{
			return dal.DeleteList(lotidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.lot GetModel(string lotid)
		{
			
			return dal.GetModel(lotid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.lot GetModelByCache(string lotid)
		{
			
			string CacheKey = "lotModel-" + lotid;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(lotid);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CEMM.Model.lot)objModel;
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
		public List<CEMM.Model.lot> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.lot> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.lot> modelList = new List<CEMM.Model.lot>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.lot model;
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

		#endregion  ExtensionMethod
	}
}

