using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
namespace CEMM.BLL
{
	/// <summary>
	/// quotaData
	/// </summary>
	public partial class quotaData
	{
		private readonly CEMM.DAL.quotaData dal=new CEMM.DAL.quotaData();
		public quotaData()
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
		public bool Exists(int srid)
		{
			return dal.Exists(srid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.quotaData model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.quotaData model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int srid)
		{
			
			return dal.Delete(srid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string sridlist )
		{
			return dal.DeleteList(sridlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.quotaData GetModel(int srid)
		{
			
			return dal.GetModel(srid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.quotaData GetModelByCache(int srid)
		{
			
			string CacheKey = "quotaDataModel-" + srid;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(srid);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CEMM.Model.quotaData)objModel;
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
		public List<CEMM.Model.quotaData> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.quotaData> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.quotaData> modelList = new List<CEMM.Model.quotaData>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.quotaData model;
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
        /// 工程量计算调用
        /// </summary>
        public decimal? EngQuantCal(int srid, decimal? multi, decimal? distance, int nullFlag)
        {
            CEMM.Model.quotaData quotaDataMDL = new CEMM.Model.quotaData();
            quotaDataMDL = GetModel(srid);
            if (quotaDataMDL.isuse=="2") //2025.07.23，每增加10m之类这些行不参与运算
                return 0;
            decimal? basicValue = quotaDataMDL.jcjs;
            decimal? result = 0.0M;
            if (quotaDataMDL.dygx == "")
            {
                result = multi * quotaDataMDL.toolquant;
            }
            else
            {
                if (nullFlag == 1) distance = quotaDataMDL.jcjs;//没有增量，距离取基数值
                CEMM.Model.quotaData deltaModel = new Model.quotaData();
                deltaModel = dal.GetDataByDygx(quotaDataMDL.dygx);
                decimal? value = deltaModel.toolquant;
                decimal? delta = deltaModel.zljs;
                if (distance <= basicValue)
                {
                    result = (distance / basicValue) * quotaDataMDL.toolquant * multi;
                }
                else
                {
                    result = (quotaDataMDL.toolquant + ((distance - basicValue) / delta) * value) * multi;
                }
            }
            if (result == null)
                result = 0.0M;
            return result;
        }
        /// <summary>
        /// 联合查询，同时获取材料/机械名称
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList2(string strWhere)
        {
            return dal.GetList2(strWhere);
        }

        /// <summary>
        /// 获取toolid/code
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList3(string strWhere)
        {
            return dal.GetList3(strWhere);
        }
        /// <summary>
        /// 看这行数据是否有对应关系
        /// </summary>
        /// <param name="srid"></param>
        /// <param name="multi"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool HasDygx(int srid)
        {
            CEMM.Model.quotaData CalObj = new CEMM.Model.quotaData();
            CalObj = GetModel(srid);
            string dygx = CalObj.dygx;
            return dygx == "" ? false : true;
        }
		#endregion  ExtensionMethod
	}
}

