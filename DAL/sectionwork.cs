using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:sectionwork
	/// </summary>
	public partial class sectionwork
	{
		public sectionwork()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string sectionid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sectionwork");
			strSql.Append(" where sectionid=@sectionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@sectionid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = sectionid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.sectionwork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sectionwork(");
			strSql.Append("sectionid,itermid,subworkid)");
			strSql.Append(" values (");
			strSql.Append("@sectionid,@itermid,@subworkid)");
			SqlParameter[] parameters = {
					new SqlParameter("@sectionid", SqlDbType.NVarChar,50),
					new SqlParameter("@itermid", SqlDbType.NVarChar,15),
					new SqlParameter("@subworkid", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.sectionid;
			parameters[1].Value = model.itermid;
			parameters[2].Value = model.subworkid;

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
		public bool Update(CEMM.Model.sectionwork model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sectionwork set ");
			strSql.Append("itermid=@itermid,");
			strSql.Append("subworkid=@subworkid");
			strSql.Append(" where sectionid=@sectionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@itermid", SqlDbType.NVarChar,15),
					new SqlParameter("@subworkid", SqlDbType.NVarChar,50),
					new SqlParameter("@sectionid", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.itermid;
			parameters[1].Value = model.subworkid;
			parameters[2].Value = model.sectionid;

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
		public bool Delete(string sectionid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sectionwork ");
			strSql.Append(" where sectionid=@sectionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@sectionid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = sectionid;

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
		public bool DeleteList(string sectionidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sectionwork ");
			strSql.Append(" where sectionid in ("+sectionidlist + ")  ");
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
		public CEMM.Model.sectionwork GetModel(string sectionid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 sectionid,itermid,subworkid from sectionwork ");
			strSql.Append(" where sectionid=@sectionid ");
			SqlParameter[] parameters = {
					new SqlParameter("@sectionid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = sectionid;

			CEMM.Model.sectionwork model=new CEMM.Model.sectionwork();
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
		public CEMM.Model.sectionwork DataRowToModel(DataRow row)
		{
			CEMM.Model.sectionwork model=new CEMM.Model.sectionwork();
			if (row != null)
			{
				if(row["sectionid"]!=null)
				{
					model.sectionid=row["sectionid"].ToString();
				}
				if(row["itermid"]!=null)
				{
					model.itermid=row["itermid"].ToString();
				}
				if(row["subworkid"]!=null)
				{
					model.subworkid=row["subworkid"].ToString();
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
			strSql.Append("select sectionid,itermid,subworkid ");
			strSql.Append(" FROM sectionwork ");
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
			strSql.Append(" sectionid,itermid,subworkid ");
			strSql.Append(" FROM sectionwork ");
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
			strSql.Append("select count(1) FROM sectionwork ");
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
				strSql.Append("order by T.sectionid desc");
			}
			strSql.Append(")AS Row, T.*  from sectionwork T ");
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
			parameters[0].Value = "sectionwork";
			parameters[1].Value = "sectionid";
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

