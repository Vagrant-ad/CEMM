using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:impleStandard
	/// </summary>
	public partial class impleStandard
	{
		public impleStandard()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string standardid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from impleStandard");
			strSql.Append(" where standardid=@standardid ");
			SqlParameter[] parameters = {
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = standardid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.impleStandard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into impleStandard(");
			strSql.Append("standardid,standardcode,implementdate)");
			strSql.Append(" values (");
			strSql.Append("@standardid,@standardcode,@implementdate)");
			SqlParameter[] parameters = {
					new SqlParameter("@standardid", SqlDbType.NVarChar,15),
					new SqlParameter("@standardcode", SqlDbType.NVarChar,40),
					new SqlParameter("@implementdate", SqlDbType.Date)};
			parameters[0].Value = model.standardid;
			parameters[1].Value = model.standardcode;
			parameters[2].Value = model.implementdate;

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
		public bool Update(CEMM.Model.impleStandard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update impleStandard set ");
			strSql.Append("standardcode=@standardcode,");
			strSql.Append("implementdate=@implementdate");
			strSql.Append(" where standardid=@standardid ");
			SqlParameter[] parameters = {
					new SqlParameter("@standardcode", SqlDbType.NVarChar,40),
					new SqlParameter("@implementdate", SqlDbType.Date),
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)};
			parameters[0].Value = model.standardcode;
			parameters[1].Value = model.implementdate;
			parameters[2].Value = model.standardid;

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
		public bool Delete(string standardid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from impleStandard ");
			strSql.Append(" where standardid=@standardid ");
			SqlParameter[] parameters = {
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = standardid;

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
		public bool DeleteList(string standardidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from impleStandard ");
			strSql.Append(" where standardid in ("+standardidlist + ")  ");
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
		public CEMM.Model.impleStandard GetModel(string standardid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 standardid,standardcode,implementdate from impleStandard ");
			strSql.Append(" where standardid=@standardid ");
			SqlParameter[] parameters = {
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = standardid;

			CEMM.Model.impleStandard model=new CEMM.Model.impleStandard();
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
		public CEMM.Model.impleStandard DataRowToModel(DataRow row)
		{
			CEMM.Model.impleStandard model=new CEMM.Model.impleStandard();
			if (row != null)
			{
				if(row["standardid"]!=null)
				{
					model.standardid=row["standardid"].ToString();
				}
				if(row["standardcode"]!=null)
				{
					model.standardcode=row["standardcode"].ToString();
				}
				if(row["implementdate"]!=null && row["implementdate"].ToString()!="")
				{
					model.implementdate=DateTime.Parse(row["implementdate"].ToString());
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
			strSql.Append("select standardid,standardcode,implementdate ");
			strSql.Append(" FROM impleStandard ");
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
			strSql.Append(" standardid,standardcode,implementdate ");
			strSql.Append(" FROM impleStandard ");
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
			strSql.Append("select count(1) FROM impleStandard ");
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
				strSql.Append("order by T.standardid desc");
			}
			strSql.Append(")AS Row, T.*  from impleStandard T ");
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
			parameters[0].Value = "impleStandard";
			parameters[1].Value = "standardid";
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

