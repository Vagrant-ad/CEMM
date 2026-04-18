using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
using System.Text;
using System.Data.SqlClient;

namespace CEMM.BLL
{
	/// <summary>
	/// computeResultInfo
	/// </summary>
	public partial class computeResultInfo
	{
		private readonly CEMM.DAL.computeResultInfo dal=new CEMM.DAL.computeResultInfo();
		public computeResultInfo()
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
		public bool Exists(int resultid)
		{
			return dal.Exists(resultid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CEMM.Model.computeResultInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.computeResultInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int resultid)
		{
			
			return dal.Delete(resultid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string resultidlist )
		{
			return dal.DeleteList(resultidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.computeResultInfo GetModel(int resultid)
		{
			
			return dal.GetModel(resultid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.computeResultInfo GetModelByCache(int resultid)
		{
			
			string CacheKey = "computeResultInfoModel-" + resultid;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(resultid);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (CEMM.Model.computeResultInfo)objModel;
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
		public List<CEMM.Model.computeResultInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.computeResultInfo> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.computeResultInfo> modelList = new List<CEMM.Model.computeResultInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.computeResultInfo model;
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
        /// 得到一个对象实体，根据code、tableID
        /// </summary>
        public CEMM.Model.computeResultInfo GetModel2(string code, int tableID)
        {
            return dal.GetModel2(code, tableID);
        }

        /// <summary>
        /// 根据tableId，得到材料生产的碳排放量
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public double GetMateCEmission(int tableID)
        {
            return dal.GetMateCEmission(tableID);
        }

        /// <summary>
        /// 根据tableId，得到材料运输的碳排放量
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public double GetTransCEmission(int tableID)
        {
            return dal.GetTransCEmission(tableID);
        }

        /// <summary>
        /// 根据tableId，得到人工&机械工的碳排放量
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public double GetLaborCEmission(int tableID)
        {
            return dal.GetLaborCEmission(tableID);
        }

        /// <summary>
        /// 获取指定字段降序排序前N的材料或机械
        /// </summary>
        /// <param name="Top">数量</param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public DataSet GetTopN(int Top, string strWhere, string filedOrder)
        {
            return dal.GetTopN(Top, strWhere, filedOrder);
        }

        public DataSet GetTopMaterialsByEmission(int tableID, int topN)
        {
            string strWhere = string.Format(
            "tableID = {0} AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)",
            tableID
            );
            return dal.GetTopN(topN, strWhere, "total_emission");
        }

        public DataSet GetTopMachinesByEmission(int tableID, int topN)
        {
            string strWhere = string.Format(
             "tableID = {0} AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)",
             tableID
            );
            return dal.GetTopN(topN, strWhere, "total_emission");
        }

        public DataSet GetTopMaterialsByUnit(int tableID, string unitProject, int topN)
        {
            string emissionField = GetEmissionFieldByUnit(unitProject);
            string strWhere = string.Format(
                "tableID = {0} AND {1} > 0 AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)",
                tableID,
                emissionField
            );
            return dal.GetTopN(topN, strWhere, emissionField);
        }

        public DataSet GetTopMachinesByUnit(int tableID, string unitProject, int topN)
        {
            string emissionField = GetEmissionFieldByUnit(unitProject);
            string strWhere = string.Format(
                "tableID = {0} AND {1} > 0 AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)",
                tableID,
                emissionField
            );
            return dal.GetTopN(topN, strWhere, emissionField);
        }

        private string GetEmissionFieldByUnit(string unitProject)
        {
            switch (unitProject)
            {
                case "临时工程": return "temp_emission";
                case "路基工程": return "subgrade_emission";
                case "路面工程": return "pavement_emission";
                case "桥涵工程": return "bridge_emission";
                case "隧道工程": return "tunnel_emission";
                case "交叉工程": return "crossing_emission";
                case "交通工程": return "traffic_emission";
                case "绿化环保工程": return "greening_emission";
                case "其他工程": return "other_emission";
                default: return "total_emission";
            }
        }

        /// <summary>
        /// 根据单位工程名称，获取材料碳排放前topN，2025.09.20
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="unitProject"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public DataSet GetTopMaterialsEmissionByUnit(int tableID, string unitProject, int topN)
        {
            string emissionField = GetEmissionFieldByUnit(unitProject);
            string quantityField = GetQuantityFieldByUnit(unitProject);
    
            string strWhere = string.Format(
                "tableID = {0} AND {1} > 0 AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)",
                tableID, 
                emissionField
            );
    
            return dal.GetTopNWithFields(topN, strWhere, emissionField,
                new string[] { "formName","unit", emissionField, quantityField });
        }
        /// <summary>
        /// 根据单位工程名称，获取机械碳排放前topN，2025.09.20
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="unitProject"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public DataSet GetTopMachinesEmissionByUnit(int tableID, string unitProject, int topN)
        {
            string emissionField = GetEmissionFieldByUnit(unitProject);
            string quantityField = GetQuantityFieldByUnit(unitProject);

            // 使用简单的代码前缀查询，就像 WebForm5 中那样
            string strWhere = string.Format(
                "tableID = {0} AND {1} > 0 AND code LIKE '8%'",
                tableID,
                emissionField
            );

            return dal.GetTopNWithFields(topN, strWhere, emissionField,
                new string[] { "formName", "unit", emissionField, quantityField });
        }
        /*public DataSet GetTopMachinesByUnitWithQuantity(int tableID, string unitProject, int topN)
        {
            string emissionField = GetEmissionFieldByUnit(unitProject);
            string quantityField = GetQuantityFieldByUnit(unitProject);

            string strWhere = string.Format(
                "tableID = {0} AND {1} > 0 AND code IN (SELECT code FROM machineCEFactor2 WHERE mfid < 5000 AND mfid >= 999998)",
                tableID,
                emissionField
            );
    
            return dal.GetTopNWithFields(topN, strWhere, emissionField, 
                new string[] { "formName", emissionField, quantityField });
        }*/

        private string GetQuantityFieldByUnit(string unitProject)
        {
            switch (unitProject)
            {
                case "临时工程": return "temp_project";
                case "路基工程": return "subgrade_project";
                case "路面工程": return "pavement_project";
                case "桥涵工程": return "bridge_project";
                case "隧道工程": return "tunnel_project";
                case "交叉工程": return "crossing_project";
                case "交通工程": return "traffic_project";
                case "绿化环保工程": return "greening_project";
                case "其他工程": return "other_project";
                default: return "total_quantity";
            }
        }

        /// <summary>
        /// 获取指定表的所有材料使用量总和
        /// </summary>
        public double GetTotalMaterialQuantity(int tableID)
        {
            return dal.GetTotalMaterialQuantity(tableID);
        }

        /// <summary>
        /// 获取指定表的所有机械使用量总和
        /// </summary>
        public double GetTotalMachineQuantity(int tableID)
        {
            return dal.GetTotalMachineQuantity(tableID);
        }
        /// <summary>
        /// 获取指定表的所有记录的总使用量总和
        /// </summary>
        public double GetTotalUsageQuantity(int tableID)
        {
            return dal.GetTotalUsageQuantity(tableID);
        }

        /// <summary>
        /// 获取指定表和单位工程的材料总使用量,测试用的
        /// </summary>
        public double GetTotalMaterialQuantityByUnit(int tableID, string unitProject)
        {
            string quantityField = GetQuantityFieldByUnit(unitProject);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(");
            strSql.Append("CASE ");
            strSql.Append("WHEN ISNUMERIC(REPLACE(" + quantityField + ", ',', '.')) = 1 ");
            strSql.Append("THEN CAST(REPLACE(" + quantityField + ", ',', '.') AS FLOAT) ");
            strSql.Append("ELSE 0 ");
            strSql.Append("END) ");
            strSql.Append("FROM computeResultInfo ");
            strSql.Append("WHERE tableID = @tableID AND code IN ");
            strSql.Append("(SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)");

            SqlParameter[] parameters = {
        new SqlParameter("@tableID", SqlDbType.Int, 4)
    };
            parameters[0].Value = tableID;

            object obj = dal.ExecuteScalar(strSql.ToString(), parameters);
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// 获取指定表和单位工程的机械总使用量,测试用的
        /// </summary>
        public double GetTotalMachineQuantityByUnit(int tableID, string unitProject)
        {
            string quantityField = GetQuantityFieldByUnit(unitProject);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(");
            strSql.Append("CASE ");
            strSql.Append("WHEN ISNUMERIC(REPLACE(" + quantityField + ", ',', '.')) = 1 ");
            strSql.Append("THEN CAST(REPLACE(" + quantityField + ", ',', '.') AS FLOAT) ");
            strSql.Append("ELSE 0 ");
            strSql.Append("END) ");
            strSql.Append("FROM computeResultInfo ");
            strSql.Append("WHERE tableID = @tableID AND code LIKE '8%'");

            SqlParameter[] parameters = {
        new SqlParameter("@tableID", SqlDbType.Int, 4)
    };
            parameters[0].Value = tableID;

            object obj = dal.ExecuteScalar(strSql.ToString(), parameters);
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

		#endregion  ExtensionMethod
	}
}

