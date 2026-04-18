using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:quotaData
	/// </summary>
	public partial class quotaData
	{
		public quotaData()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("srid", "quotaData"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int srid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from quotaData");
			strSql.Append(" where srid=@srid ");
			SqlParameter[] parameters = {
					new SqlParameter("@srid", SqlDbType.Int,4)			};
			parameters[0].Value = srid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.quotaData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into quotaData(");
			strSql.Append("srid,subitermid,subitermsrid,subitermname,toolid,toolquant,jcjs,zljs,dygx,isuse)");
			strSql.Append(" values (");
			strSql.Append("@srid,@subitermid,@subitermsrid,@subitermname,@toolid,@toolquant,@jcjs,@zljs,@dygx,@isuse)");
			SqlParameter[] parameters = {
					new SqlParameter("@srid", SqlDbType.Int,4),
					new SqlParameter("@subitermid", SqlDbType.NVarChar,15),
					new SqlParameter("@subitermsrid", SqlDbType.NVarChar,20),
					new SqlParameter("@subitermname", SqlDbType.NVarChar,100),
					new SqlParameter("@toolid", SqlDbType.NVarChar,12),
					new SqlParameter("@toolquant", SqlDbType.Decimal,5),
					new SqlParameter("@jcjs", SqlDbType.Decimal,5),
					new SqlParameter("@zljs", SqlDbType.Decimal,5),
					new SqlParameter("@dygx", SqlDbType.NVarChar,15),
					new SqlParameter("@isuse", SqlDbType.NChar,1)};
			parameters[0].Value = model.srid;
			parameters[1].Value = model.subitermid;
			parameters[2].Value = model.subitermsrid;
			parameters[3].Value = model.subitermname;
			parameters[4].Value = model.toolid;
			parameters[5].Value = model.toolquant;
			parameters[6].Value = model.jcjs;
			parameters[7].Value = model.zljs;
			parameters[8].Value = model.dygx;
			parameters[9].Value = model.isuse;

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
		public bool Update(CEMM.Model.quotaData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update quotaData set ");
			strSql.Append("subitermid=@subitermid,");
			strSql.Append("subitermsrid=@subitermsrid,");
			strSql.Append("subitermname=@subitermname,");
			strSql.Append("toolid=@toolid,");
			strSql.Append("toolquant=@toolquant,");
			strSql.Append("jcjs=@jcjs,");
			strSql.Append("zljs=@zljs,");
			strSql.Append("dygx=@dygx,");
			strSql.Append("isuse=@isuse");
			strSql.Append(" where srid=@srid ");
			SqlParameter[] parameters = {
					new SqlParameter("@subitermid", SqlDbType.NVarChar,15),
					new SqlParameter("@subitermsrid", SqlDbType.NVarChar,20),
					new SqlParameter("@subitermname", SqlDbType.NVarChar,100),
					new SqlParameter("@toolid", SqlDbType.NVarChar,12),
					new SqlParameter("@toolquant", SqlDbType.Decimal,5),
					new SqlParameter("@jcjs", SqlDbType.Decimal,5),
					new SqlParameter("@zljs", SqlDbType.Decimal,5),
					new SqlParameter("@dygx", SqlDbType.NVarChar,15),
					new SqlParameter("@isuse", SqlDbType.NChar,1),
					new SqlParameter("@srid", SqlDbType.Int,4)};
			parameters[0].Value = model.subitermid;
			parameters[1].Value = model.subitermsrid;
			parameters[2].Value = model.subitermname;
			parameters[3].Value = model.toolid;
			parameters[4].Value = model.toolquant;
			parameters[5].Value = model.jcjs;
			parameters[6].Value = model.zljs;
			parameters[7].Value = model.dygx;
			parameters[8].Value = model.isuse;
			parameters[9].Value = model.srid;

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
		public bool Delete(int srid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from quotaData ");
			strSql.Append(" where srid=@srid ");
			SqlParameter[] parameters = {
					new SqlParameter("@srid", SqlDbType.Int,4)			};
			parameters[0].Value = srid;

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
		public bool DeleteList(string sridlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from quotaData ");
			strSql.Append(" where srid in ("+sridlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.quotaData GetModel(int srid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 srid,subitermid,subitermsrid,subitermname,toolid,toolquant,jcjs,zljs,dygx,isuse from quotaData ");
			strSql.Append(" where srid=@srid ");
			SqlParameter[] parameters = {
					new SqlParameter("@srid", SqlDbType.Int,4)			};
			parameters[0].Value = srid;

			CEMM.Model.quotaData model=new CEMM.Model.quotaData();
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
		public CEMM.Model.quotaData DataRowToModel(DataRow row)
		{
			CEMM.Model.quotaData model=new CEMM.Model.quotaData();
			if (row != null)
			{
				if(row["srid"]!=null && row["srid"].ToString()!="")
				{
					model.srid=int.Parse(row["srid"].ToString());
				}
				if(row["subitermid"]!=null)
				{
					model.subitermid=row["subitermid"].ToString();
				}
				if(row["subitermsrid"]!=null)
				{
					model.subitermsrid=row["subitermsrid"].ToString();
				}
				if(row["subitermname"]!=null)
				{
					model.subitermname=row["subitermname"].ToString();
				}
				if(row["toolid"]!=null)
				{
					model.toolid=row["toolid"].ToString();
				}
				if(row["toolquant"]!=null && row["toolquant"].ToString()!="")
				{
					model.toolquant=decimal.Parse(row["toolquant"].ToString());
				}
				if(row["jcjs"]!=null && row["jcjs"].ToString()!="")
				{
					model.jcjs=decimal.Parse(row["jcjs"].ToString());
				}
				if(row["zljs"]!=null && row["zljs"].ToString()!="")
				{
					model.zljs=decimal.Parse(row["zljs"].ToString());
				}
				if(row["dygx"]!=null)
				{
					model.dygx=row["dygx"].ToString();
				}
				if(row["isuse"]!=null)
				{
					model.isuse=row["isuse"].ToString();
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
			strSql.Append("select srid,subitermid,subitermsrid,subitermname,toolid,toolquant,jcjs,zljs,dygx,isuse ");
			strSql.Append(" FROM quotaData ");
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
			strSql.Append(" srid,subitermid,subitermsrid,subitermname,toolid,toolquant,jcjs,zljs,dygx,isuse ");
			strSql.Append(" FROM quotaData ");
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
			strSql.Append("select count(1) FROM quotaData ");
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
				strSql.Append("order by T.srid desc");
			}
			strSql.Append(")AS Row, T.*  from quotaData T ");
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
			parameters[0].Value = "quotaData";
			parameters[1].Value = "srid";
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
        ///  获取对应关系增量信息行
        ///  扩展：为了找对应关系里面最后一行，这行里面有增量和基础量
        ///  仿照GetModel方法写的，只不过传入参数和where变成了dygx，变成了逆序取第一个符合条件的
        /// </summary>

        public CEMM.Model.quotaData GetDataByDygx(string dygx)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 srid,subitermid,subitermsrid,subitermname,toolid,toolquant,jcjs,zljs,dygx,isuse from quotaData ");
            strSql.Append(" where dygx = @dygx ");
            strSql.Append(" order by srid desc");
            SqlParameter[] parameters = {
					new SqlParameter("@dygx", SqlDbType.NVarChar,15)			};
            parameters[0].Value = dygx;
            CEMM.Model.quotaData model = new CEMM.Model.quotaData();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 联合查询，同时获取材料/机械名称
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList2(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select srid,subitermid,subitermsrid,subitermname,name,toolquant");
            strSql.Append(" FROM quotaData,machineCEFactor2");
            strSql.Append(" where quotaData.toolid = machineCEFactor2.code");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere + " order by srid");

            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查询获取toolid
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList3(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select srid,toolid");
            strSql.Append(" FROM quotaData");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);

            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        


		#endregion  ExtensionMethod
	}
}

