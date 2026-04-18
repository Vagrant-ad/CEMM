using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:unitwork
	/// </summary>
	public partial class unitwork
	{
		public unitwork()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string workid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from unitwork");
			strSql.Append(" where workid=@workid ");
			SqlParameter[] parameters = {
					new SqlParameter("@workid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = workid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.unitwork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into unitwork(");
			strSql.Append("workid,workname,lotid,workstartdate,workenddate)");
			strSql.Append(" values (");
			strSql.Append("@workid,@workname,@lotid,@workstartdate,@workenddate)");
			SqlParameter[] parameters = {
					new SqlParameter("@workid", SqlDbType.NVarChar,50),
					new SqlParameter("@workname", SqlDbType.NVarChar,100),
					new SqlParameter("@lotid", SqlDbType.NVarChar,50),
					new SqlParameter("@workstartdate", SqlDbType.Date),
					new SqlParameter("@workenddate", SqlDbType.Date)};
			parameters[0].Value = model.workid;
			parameters[1].Value = model.workname;
			parameters[2].Value = model.lotid;
			parameters[3].Value = model.workstartdate;
			parameters[4].Value = model.workenddate;

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
		public bool Update(CEMM.Model.unitwork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update unitwork set ");
			strSql.Append("workname=@workname,");
			strSql.Append("lotid=@lotid,");
			strSql.Append("workstartdate=@workstartdate,");
			strSql.Append("workenddate=@workenddate");
			strSql.Append(" where workid=@workid ");
			SqlParameter[] parameters = {
					new SqlParameter("@workname", SqlDbType.NVarChar,100),
					new SqlParameter("@lotid", SqlDbType.NVarChar,50),
					new SqlParameter("@workstartdate", SqlDbType.Date),
					new SqlParameter("@workenddate", SqlDbType.Date),
					new SqlParameter("@workid", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.workname;
			parameters[1].Value = model.lotid;
			parameters[2].Value = model.workstartdate;
			parameters[3].Value = model.workenddate;
			parameters[4].Value = model.workid;

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
		public bool Delete(string workid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from unitwork ");
			strSql.Append(" where workid=@workid ");
			SqlParameter[] parameters = {
					new SqlParameter("@workid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = workid;

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
		public bool DeleteList(string workidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from unitwork ");
			strSql.Append(" where workid in ("+workidlist + ")  ");
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
		public CEMM.Model.unitwork GetModel(string workid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 workid,workname,lotid,workstartdate,workenddate from unitwork ");
			strSql.Append(" where workid=@workid ");
			SqlParameter[] parameters = {
					new SqlParameter("@workid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = workid;

			CEMM.Model.unitwork model=new CEMM.Model.unitwork();
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
		public CEMM.Model.unitwork DataRowToModel(DataRow row)
		{
			CEMM.Model.unitwork model=new CEMM.Model.unitwork();
			if (row != null)
			{
				if(row["workid"]!=null)
				{
					model.workid=row["workid"].ToString();
				}
				if(row["workname"]!=null)
				{
					model.workname=row["workname"].ToString();
				}
				if(row["lotid"]!=null)
				{
					model.lotid=row["lotid"].ToString();
				}
				if(row["workstartdate"]!=null && row["workstartdate"].ToString()!="")
				{
					model.workstartdate=DateTime.Parse(row["workstartdate"].ToString());
				}
				if(row["workenddate"]!=null && row["workenddate"].ToString()!="")
				{
					model.workenddate=DateTime.Parse(row["workenddate"].ToString());
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
			strSql.Append("select workid,workname,lotid,workstartdate,workenddate ");
			strSql.Append(" FROM unitwork ");
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
			strSql.Append(" workid,workname,lotid,workstartdate,workenddate ");
			strSql.Append(" FROM unitwork ");
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
			strSql.Append("select count(1) FROM unitwork ");
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
				strSql.Append("order by T.workid desc");
			}
			strSql.Append(")AS Row, T.*  from unitwork T ");
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
			parameters[0].Value = "unitwork";
			parameters[1].Value = "workid";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

