using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
namespace CEMM.BLL
{
	/// <summary>
	/// machineCEFactor
	/// </summary>
	public partial class machineCEFactor
	{
		private readonly CEMM.DAL.machineCEFactor dal=new CEMM.DAL.machineCEFactor();
		public machineCEFactor()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int mfid)
		{
			return dal.Exists(mfid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.machineCEFactor model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.machineCEFactor model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int mfid)
		{
			
			return dal.Delete(mfid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string mfidlist )
		{
			return dal.DeleteList(mfidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.machineCEFactor GetModel(int mfid)
		{
			
			return dal.GetModel(mfid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.machineCEFactor GetModelByCache(int mfid)
		{
			
			string CacheKey = "machineCEFactorModel-" + mfid;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(mfid);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CEMM.Model.machineCEFactor)objModel;
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
		public List<CEMM.Model.machineCEFactor> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.machineCEFactor> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.machineCEFactor> modelList = new List<CEMM.Model.machineCEFactor>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.machineCEFactor model;
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

