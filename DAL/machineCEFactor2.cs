using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:machineCEFactor2
	/// </summary>
	public partial class machineCEFactor2
	{
		public machineCEFactor2()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("mfid", "machineCEFactor2"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int mfid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from machineCEFactor2");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4)			};
			parameters[0].Value = mfid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.machineCEFactor2 model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into machineCEFactor2(");
			strSql.Append("mfid,name,code,specific,unit,energyfactor,machinefactor,standardid)");
			strSql.Append(" values (");
			strSql.Append("@mfid,@name,@code,@specific,@unit,@energyfactor,@machinefactor,@standardid)");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.VarChar,15),
					new SqlParameter("@specific", SqlDbType.NVarChar,80),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@energyfactor", SqlDbType.Decimal,9),
					new SqlParameter("@machinefactor", SqlDbType.Decimal,9),
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)};
			parameters[0].Value = model.mfid;
			parameters[1].Value = model.name;
			parameters[2].Value = model.code;
			parameters[3].Value = model.specific;
			parameters[4].Value = model.unit;
			parameters[5].Value = model.energyfactor;
			parameters[6].Value = model.machinefactor;
			parameters[7].Value = model.standardid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.machineCEFactor2 model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update machineCEFactor2 set ");
			strSql.Append("name=@name,");
			strSql.Append("code=@code,");
			strSql.Append("specific=@specific,");
			strSql.Append("unit=@unit,");
			strSql.Append("energyfactor=@energyfactor,");
			strSql.Append("machinefactor=@machinefactor,");
			strSql.Append("standardid=@standardid");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.VarChar,15),
					new SqlParameter("@specific", SqlDbType.NVarChar,80),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@energyfactor", SqlDbType.Decimal,9),
					new SqlParameter("@machinefactor", SqlDbType.Decimal,9),
					new SqlParameter("@standardid", SqlDbType.NVarChar,15),
					new SqlParameter("@mfid", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.code;
			parameters[2].Value = model.specific;
			parameters[3].Value = model.unit;
			parameters[4].Value = model.energyfactor;
			parameters[5].Value = model.machinefactor;
			parameters[6].Value = model.standardid;
			parameters[7].Value = model.mfid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int mfid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from machineCEFactor2 ");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4)			};
			parameters[0].Value = mfid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string mfidlist )
		{
			if (string.IsNullOrWhiteSpace(mfidlist))
				return false;

			string[] ids = mfidlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (ids.Length == 0)
				return false;

			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from machineCEFactor2 where mfid in (");
			SqlParameter[] parameters = new SqlParameter[ids.Length];
			for (int i = 0; i < ids.Length; i++)
			{
				string pname = "@mid" + i;
				if (i > 0) strSql.Append(",");
				strSql.Append(pname);
				parameters[i] = new SqlParameter(pname, SqlDbType.Int, 4);
				parameters[i].Value = int.Parse(ids[i].Trim());
			}
			strSql.Append(")");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.machineCEFactor2 GetModel(int mfid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 mfid,name,code,specific,unit,energyfactor,machinefactor,standardid from machineCEFactor2 ");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4)			};
			parameters[0].Value = mfid;

			CEMM.Model.machineCEFactor2 model=new CEMM.Model.machineCEFactor2();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.machineCEFactor2 DataRowToModel(DataRow row)
		{
			CEMM.Model.machineCEFactor2 model=new CEMM.Model.machineCEFactor2();
			if (row != null)
			{
				if(row["mfid"]!=null && row["mfid"].ToString()!="")
				{
					model.mfid=int.Parse(row["mfid"].ToString());
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["code"]!=null)
				{
					model.code=row["code"].ToString();
				}
				if(row["specific"]!=null)
				{
					model.specific=row["specific"].ToString();
				}
				if(row["unit"]!=null)
				{
					model.unit=row["unit"].ToString();
				}
				if(row["energyfactor"]!=null && row["energyfactor"].ToString()!="")
				{
					model.energyfactor=decimal.Parse(row["energyfactor"].ToString());
				}
				if(row["machinefactor"]!=null && row["machinefactor"].ToString()!="")
				{
					model.machinefactor=decimal.Parse(row["machinefactor"].ToString());
				}
				if(row["standardid"]!=null)
				{
					model.standardid=row["standardid"].ToString();
				}
                if (row["energytype"] != null && row["energytype"].ToString() != "")
                {
                    model.energytype = int.Parse(row["energytype"].ToString());
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select mfid,name,code,specific,unit,energyfactor,machinefactor,standardid ");
			strSql.Append(" FROM machineCEFactor2 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" mfid,name,code,specific,unit,energyfactor,machinefactor,standardid ");
			strSql.Append(" FROM machineCEFactor2 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM machineCEFactor2 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.mfid desc");
			}
			strSql.Append(")AS Row, T.*  from machineCEFactor2 T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "machineCEFactor2";
			parameters[1].Value = "mfid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/
		#endregion  BasicMethod

		#region  ExtensionMethod
        /// <summary>
        /// 条件查询，通过获取的quotaData表的数据行中的toolid和machineCEFactor2中的code对应，获取工具或材料的碳排放因子
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetMachineFactor(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select machinefactor");
            strSql.Append(" from machineCEFactor2,quotaData");
            strSql.Append(" where quotaData.toolid = machineCEFactor2.code");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);

            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据code得到一个machineCEFactor2对象
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CEMM.Model.machineCEFactor2 GetModelByCode(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype from machineCEFactor2 ");
            strSql.Append(" where code=@code ");
            SqlParameter[] parameters = {
        new SqlParameter("@code", SqlDbType.VarChar,15)};
            parameters[0].Value = code;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModelWithEnergyType(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据name得到一个machineCEFactor2对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CEMM.Model.machineCEFactor2 GetModelByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype from machineCEFactor2 ");
            strSql.Append(" where name like @name + '%'");
            SqlParameter[] parameters = {
        new SqlParameter("@name", SqlDbType.NVarChar,50)};
            parameters[0].Value = name;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModelWithEnergyType(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回name,code,specific,machinefactor四列
        /// </summary>
        public DataSet GetList2()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select name,code,specific,machinefactor ");
            strSql.Append(" FROM machineCEFactor2 ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool AddWithoutId(CEMM.Model.machineCEFactor2 model)
        {
            // 获取当前最大ID并加1
            int nextId = GetMaxId() + 1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into machineCEFactor2(");
            strSql.Append("mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype)");
            strSql.Append(" values (");
            strSql.Append("@mfid,@name,@code,@specific,@unit,@energyfactor,@machinefactor,@standardid,@energytype)");

            SqlParameter[] parameters = {
        new SqlParameter("@mfid", SqlDbType.Int,4),
        new SqlParameter("@name", SqlDbType.NVarChar,50),
        new SqlParameter("@code", SqlDbType.VarChar,15),
        new SqlParameter("@specific", SqlDbType.NVarChar,80),
        new SqlParameter("@unit", SqlDbType.NVarChar,10),
        new SqlParameter("@energyfactor", SqlDbType.Decimal,9),
        new SqlParameter("@machinefactor", SqlDbType.Decimal,9),
        new SqlParameter("@standardid", SqlDbType.NVarChar,15),
        new SqlParameter("@energytype", SqlDbType.Int,4)};

            parameters[0].Value = nextId;
            parameters[1].Value = model.name;
            parameters[2].Value = model.code;
            parameters[3].Value = model.specific;
            parameters[4].Value = model.unit;
            parameters[5].Value = model.energyfactor;
            parameters[6].Value = model.machinefactor;
            parameters[7].Value = model.standardid;
            parameters[8].Value = model.energytype;

            try
            {
                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                return rows > 0;
            }
            catch (SqlException sqlEx)
            {
                // 如果是唯一约束违反，抛出特定异常
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // SQL Server 唯一约束错误代码
                {
                    throw new Exception("Code已存在", sqlEx);
                }
                throw;
            }
        }

        public bool AddWithEnergyType(CEMM.Model.machineCEFactor2 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into machineCEFactor2(");
            strSql.Append("mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype)");
            strSql.Append(" values (");
            strSql.Append("@mfid,@name,@code,@specific,@unit,@energyfactor,@machinefactor,@standardid,@energytype)");

            SqlParameter[] parameters = {
        new SqlParameter("@mfid", SqlDbType.Int,4),
        new SqlParameter("@name", SqlDbType.NVarChar,50),
        new SqlParameter("@code", SqlDbType.VarChar,15),
        new SqlParameter("@specific", SqlDbType.NVarChar,80),
        new SqlParameter("@unit", SqlDbType.NVarChar,10),
        new SqlParameter("@energyfactor", SqlDbType.Decimal,9),
        new SqlParameter("@machinefactor", SqlDbType.Decimal,9),
        new SqlParameter("@standardid", SqlDbType.NVarChar,15),
        new SqlParameter("@energytype", SqlDbType.Int,4)};

            parameters[0].Value = model.mfid;
            parameters[1].Value = model.name;
            parameters[2].Value = model.code;
            parameters[3].Value = model.specific;
            parameters[4].Value = model.unit;
            parameters[5].Value = model.energyfactor;
            parameters[6].Value = model.machinefactor;
            parameters[7].Value = model.standardid;
            parameters[8].Value = model.energytype;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }
        // 新增方法：包含energytype字段的Update方法
        public bool UpdateWithEnergyType(CEMM.Model.machineCEFactor2 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update machineCEFactor2 set ");
            strSql.Append("name=@name,");
            strSql.Append("code=@code,");
            strSql.Append("specific=@specific,");
            strSql.Append("unit=@unit,");
            strSql.Append("energyfactor=@energyfactor,");
            strSql.Append("machinefactor=@machinefactor,");
            strSql.Append("standardid=@standardid,");
            strSql.Append("energytype=@energytype");
            strSql.Append(" where mfid=@mfid ");

            SqlParameter[] parameters = {
        new SqlParameter("@name", SqlDbType.NVarChar,50),
        new SqlParameter("@code", SqlDbType.VarChar,15),
        new SqlParameter("@specific", SqlDbType.NVarChar,80),
        new SqlParameter("@unit", SqlDbType.NVarChar,10),
        new SqlParameter("@energyfactor", SqlDbType.Decimal,9),
        new SqlParameter("@machinefactor", SqlDbType.Decimal,9),
        new SqlParameter("@standardid", SqlDbType.NVarChar,15),
        new SqlParameter("@energytype", SqlDbType.Int,4),
        new SqlParameter("@mfid", SqlDbType.Int,4)};

            parameters[0].Value = model.name;
            parameters[1].Value = model.code;
            parameters[2].Value = model.specific;
            parameters[3].Value = model.unit;
            parameters[4].Value = model.energyfactor;
            parameters[5].Value = model.machinefactor;
            parameters[6].Value = model.standardid;
            parameters[7].Value = model.energytype;
            parameters[8].Value = model.mfid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }
        
        // 新增方法：包含energytype字段的查询
        public DataSet GetListWithEnergyType(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype ");
            strSql.Append(" FROM machineCEFactor2 ");
            if(strWhere.Trim()!="")
            {
                strSql.Append(" where "+strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        
        // 新增方法：按能源类型查询
        public DataSet GetListByEnergyType(string energyTypes, string additionalWhere = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype ");
            strSql.Append(" FROM machineCEFactor2 ");
            strSql.Append(" where energytype in (" + energyTypes + ")");
            
            if(!string.IsNullOrEmpty(additionalWhere))
            {
                strSql.Append(" and " + additionalWhere);
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 按名称关键字模糊查询（参数化）
        /// </summary>
        public DataSet GetListByKeyword(string keyword)
        {
            string sql = "select mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype from machineCEFactor2 where name like @keyword";
            SqlParameter[] parameters = {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50) { Value = "%" + keyword + "%" }
            };
            return DbHelperSQL.Query(sql, parameters);
        }

        /// <summary>
        /// 按名称模糊查询，返回前1条记录（参数化）
        /// </summary>
        public CEMM.Model.machineCEFactor2 GetFirstModelByKeyword(string keyword)
        {
            string sql = "select top 1 mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype from machineCEFactor2 where name like @keyword";
            SqlParameter[] parameters = {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50) { Value = "%" + keyword + "%" }
            };
            DataSet ds = DbHelperSQL.Query(sql, parameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return DataRowToModelWithEnergyType(ds.Tables[0].Rows[0]);
            return null;
        }

        // 新增方法：DataRow转换为包含energytype的Model
       /* public CEMM.Model.machineCEFactor2 DataRowToModelWithEnergyType(DataRow row)
        {
            CEMM.Model.machineCEFactor2 model = DataRowToModel(row); // 调用原有方法
            
            // 添加energytype字段处理
            if(row["energytype"]!=null && row["energytype"].ToString()!="")
            {
                model.energytype=int.Parse(row["energytype"].ToString());
            }
            
            return model;
        }*/

        // 新增方法：DataRow转换为包含energytype的Model
        public CEMM.Model.machineCEFactor2 DataRowToModelWithEnergyType(DataRow row)
        {
            CEMM.Model.machineCEFactor2 model = new CEMM.Model.machineCEFactor2();
            if (row != null)
            {
                // 原有字段处理
                if (row["mfid"] != null && row["mfid"].ToString() != "")
                {
                    model.mfid = int.Parse(row["mfid"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["code"] != null)
                {
                    model.code = row["code"].ToString();
                }
                if (row["specific"] != null)
                {
                    model.specific = row["specific"].ToString();
                }
                if (row["unit"] != null)
                {
                    model.unit = row["unit"].ToString();
                }
                if (row["energyfactor"] != null && row["energyfactor"].ToString() != "")
                {
                    model.energyfactor = decimal.Parse(row["energyfactor"].ToString());
                }
                if (row["machinefactor"] != null && row["machinefactor"].ToString() != "")
                {
                    model.machinefactor = decimal.Parse(row["machinefactor"].ToString());
                }
                if (row["standardid"] != null)
                {
                    model.standardid = row["standardid"].ToString();
                }

                // 添加energytype字段处理
                if (row["energytype"] != null && row["energytype"].ToString() != "")
                {
                    model.energytype = int.Parse(row["energytype"].ToString());
                }
                else
                {
                    model.energytype = null; // 设置为null而不是0
                }
            }
            return model;
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype ");
            strSql.Append(" FROM machineCEFactor2 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder + " desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
    
		#endregion  ExtensionMethod
	}
}

