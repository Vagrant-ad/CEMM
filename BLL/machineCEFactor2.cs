using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using CEMM.Model;
namespace CEMM.BLL
{
	/// <summary>
	/// machineCEFactor2
	/// </summary>
	public partial class machineCEFactor2
	{
		private readonly CEMM.DAL.machineCEFactor2 dal=new CEMM.DAL.machineCEFactor2();
		public machineCEFactor2()
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
		public bool Add(CEMM.Model.machineCEFactor2 model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.machineCEFactor2 model)
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
		public CEMM.Model.machineCEFactor2 GetModel(int mfid)
		{
			
			return dal.GetModel(mfid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CEMM.Model.machineCEFactor2 GetModelByCache(int mfid)
		{
			
			string CacheKey = "machineCEFactor2Model-" + mfid;
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
			return (CEMM.Model.machineCEFactor2)objModel;
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
		public List<CEMM.Model.machineCEFactor2> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CEMM.Model.machineCEFactor2> DataTableToList(DataTable dt)
		{
			List<CEMM.Model.machineCEFactor2> modelList = new List<CEMM.Model.machineCEFactor2>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CEMM.Model.machineCEFactor2 model;
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
        /// 获取碳排放因子
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetMachineFactor(string strWhere)
        {
            return dal.GetMachineFactor(strWhere);
        }

        /// <summary>
        /// 根据code得到一个machineCEFactor2对象
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CEMM.Model.machineCEFactor2 GetModelByCode(string code)
        {
            return dal.GetModelByCode(code);
        }

        /// <summary>
        /// 根据name得到一个machineCEFactor2对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CEMM.Model.machineCEFactor2 GetModelByName(string name)
        {
            return dal.GetModelByName(name);
        }

        /// <summary>
        /// 返回name,code,specific,machinefactor四列
        /// </summary>
        public DataSet GetList2()
        {
            return dal.GetList2();
        }

		#endregion  ExtensionMethod
        /*2025.8.1添加*/
        public DataSet GetListByNameOrCode(string keyword)
        {
            // 构建查询条件
            string strWhere = "";

            if (!string.IsNullOrEmpty(keyword))
            {
                // 按名称(name)模糊查询
                strWhere = "(name like '%" + keyword + "%' )";
            }

            // 调用DAL层的GetList方法
            return dal.GetList(strWhere);
        }

        #region 新增的Add方法

        /// <summary>
        /// 增加一条数据（不包含mfid字段，用于自增主键）
        /// </summary>
        // 在BLL层的 AddWithoutId 方法中
        public bool AddWithoutId(CEMM.Model.machineCEFactor2 model)
        {
            try
            {
                return dal.AddWithoutId(model);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("已存在") || (ex.InnerException != null && ex.InnerException.Message.Contains("唯一")))
                {
                    throw new Exception("Code已存在", ex);
                }
                throw;
            }
        }

        #endregion

         // 新增方法：添加包含energytype的记录2025.9.5
        public bool AddWithEnergyType(CEMM.Model.machineCEFactor2 model)
        {
            return dal.AddWithEnergyType(model);
        }

        
        // 新增方法：更新包含energytype的记录2025.9.5
        public bool UpdateWithEnergyType(CEMM.Model.machineCEFactor2 model)
        {
            return dal.UpdateWithEnergyType(model);
        }
        
        // 新增方法：获取包含energytype的数据列表2025.9.5
        public DataSet GetListWithEnergyType(string strWhere)
        {
            return dal.GetListWithEnergyType(strWhere);
        }
        
        // 新增方法：按能源类型获取数据2025.9.5
        public DataSet GetListByEnergyType(string energyTypes, string additionalWhere = "")
        {
            return dal.GetListByEnergyType(energyTypes, additionalWhere);
        }
        
        // 新增方法：计算碳排放（核心业务逻辑）2025.9.5
        public Dictionary<string, decimal> CalculateCarbonEmissions(string strWhere = "")
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            decimal directEmission = 0;
            decimal indirectEmission = 0;
            
            // 使用新的包含energytype的查询方法
            DataSet ds = string.IsNullOrEmpty(strWhere) ? 
                dal.GetListWithEnergyType("") : 
                dal.GetListWithEnergyType(strWhere);
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // 使用新的转换方法
                    var model = dal.DataRowToModelWithEnergyType(row);
                    
                    if (model.energytype.HasValue && model.energyfactor.HasValue && model.machinefactor.HasValue)
                    {
                        // 计算碳排放量（根据实际业务公式调整）
                        decimal emission = model.energyfactor.Value * model.machinefactor.Value;
                        
                        if (model.energytype.Value >= 1 && model.energytype.Value <= 4)
                        {
                            directEmission += emission;
                        }
                        else if (model.energytype.Value >= 5 && model.energytype.Value <= 7)
                        {
                            indirectEmission += emission;
                        }
                    }
                }
            }
            
            result.Add("DirectEmission", directEmission);
            result.Add("IndirectEmission", indirectEmission);
            result.Add("TotalEmission", directEmission + indirectEmission);
            
            return result;
        }
        
        // 新增方法：按能源类型详细统计2025.9.5
        public Dictionary<int, decimal> GetEmissionsByEnergyType(string strWhere = "")
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            
            DataSet ds = string.IsNullOrEmpty(strWhere) ? 
                dal.GetListWithEnergyType("") : 
                dal.GetListWithEnergyType(strWhere);
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = dal.DataRowToModelWithEnergyType(row);
                    
                    if (model.energytype.HasValue && model.energyfactor.HasValue && model.machinefactor.HasValue)
                    {
                        decimal emission = model.energyfactor.Value * model.machinefactor.Value;
                        int energyType = model.energytype.Value;
                        
                        if (result.ContainsKey(energyType))
                        {
                            result[energyType] += emission;
                        }
                        else
                        {
                            result.Add(energyType, emission);
                        }
                    }
                }
            }
            
            return result;
        }
        public CEMM.Model.machineCEFactor2 DataRowToModelWithEnergyType(DataRow row)
        {
            // 明确调用DAL层的方法
            return this.dal.DataRowToModelWithEnergyType(row);
        }
        /// <summary>
        /// 获取按指定字段降序排序前N的记录
        /// </summary>
        /// <param name="Top">数量</param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <returns></returns>
        public DataSet GetTopN(int Top, string strWhere, string filedOrder)
        {
            return dal.GetTopN(Top, strWhere, filedOrder);
        }
        // 在 machineCEFactor2 BLL 类中添加这个方法2025.9.5
        public CEMM.Model.machineCEFactor2 GetListByNameOrCode2(string name)
        {
            // 先尝试按名称查找
            string strWhere = "name LIKE '%" + name.Replace("'", "''") + "%'";
            DataSet ds = dal.GetListWithEnergyType(strWhere);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return dal.DataRowToModelWithEnergyType(ds.Tables[0].Rows[0]);
            }

            // 如果按名称没找到，尝试按代码查找（如果需要）
            // 这里可以根据您的具体需求调整查找逻辑

            return null;
        }
        // 在BLL层检查这个方法
       
	}
}

