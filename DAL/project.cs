using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:project
	/// </summary>
	public partial class project
	{
		public project()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string projectid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from project");
			strSql.Append(" where projectid=@projectid ");
			SqlParameter[] parameters = {
					new SqlParameter("@projectid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = projectid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.project model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into project(");
			strSql.Append("projectid,projectname,projectsource,projectinfo,projstartdate,projenddate)");
			strSql.Append(" values (");
			strSql.Append("@projectid,@projectname,@projectsource,@projectinfo,@projstartdate,@projenddate)");
			SqlParameter[] parameters = {
					new SqlParameter("@projectid", SqlDbType.NVarChar,50),
					new SqlParameter("@projectname", SqlDbType.NVarChar,100),
					new SqlParameter("@projectsource", SqlDbType.NVarChar,100),
					new SqlParameter("@projectinfo", SqlDbType.NVarChar,200),
					new SqlParameter("@projstartdate", SqlDbType.Date),
					new SqlParameter("@projenddate", SqlDbType.Date)};
			parameters[0].Value = model.projectid;
			parameters[1].Value = model.projectname;
			parameters[2].Value = model.projectsource;
			parameters[3].Value = model.projectinfo;
			parameters[4].Value = model.projstartdate;
			parameters[5].Value = model.projenddate;

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
		public bool Update(CEMM.Model.project model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update project set ");
			strSql.Append("projectname=@projectname,");
			strSql.Append("projectsource=@projectsource,");
			strSql.Append("projectinfo=@projectinfo,");
			strSql.Append("projstartdate=@projstartdate,");
			strSql.Append("projenddate=@projenddate");
			strSql.Append(" where projectid=@projectid ");
			SqlParameter[] parameters = {
					new SqlParameter("@projectname", SqlDbType.NVarChar,100),
					new SqlParameter("@projectsource", SqlDbType.NVarChar,100),
					new SqlParameter("@projectinfo", SqlDbType.NVarChar,200),
					new SqlParameter("@projstartdate", SqlDbType.Date),
					new SqlParameter("@projenddate", SqlDbType.Date),
					new SqlParameter("@projectid", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.projectname;
			parameters[1].Value = model.projectsource;
			parameters[2].Value = model.projectinfo;
			parameters[3].Value = model.projstartdate;
			parameters[4].Value = model.projenddate;
			parameters[5].Value = model.projectid;

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
		public bool Delete(string projectid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from project ");
			strSql.Append(" where projectid=@projectid ");
			SqlParameter[] parameters = {
					new SqlParameter("@projectid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = projectid;

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
		public bool DeleteList(string projectidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from project ");
			strSql.Append(" where projectid in ("+projectidlist + ")  ");
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
		public CEMM.Model.project GetModel(string projectid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 projectid,projectname,projectsource,projectinfo,projstartdate,projenddate from project ");
			strSql.Append(" where projectid=@projectid ");
			SqlParameter[] parameters = {
					new SqlParameter("@projectid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = projectid;

			CEMM.Model.project model=new CEMM.Model.project();
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
		public CEMM.Model.project DataRowToModel(DataRow row)
		{
			CEMM.Model.project model=new CEMM.Model.project();
			if (row != null)
			{
				if(row["projectid"]!=null)
				{
					model.projectid=row["projectid"].ToString();
				}
				if(row["projectname"]!=null)
				{
					model.projectname=row["projectname"].ToString();
				}
				if(row["projectsource"]!=null)
				{
					model.projectsource=row["projectsource"].ToString();
				}
				if(row["projectinfo"]!=null)
				{
					model.projectinfo=row["projectinfo"].ToString();
				}
				if(row["projstartdate"]!=null && row["projstartdate"].ToString()!="")
				{
					model.projstartdate=DateTime.Parse(row["projstartdate"].ToString());
				}
				if(row["projenddate"]!=null && row["projenddate"].ToString()!="")
				{
					model.projenddate=DateTime.Parse(row["projenddate"].ToString());
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
			strSql.Append("select projectid,projectname,projectsource,projectinfo,projstartdate,projenddate ");
			strSql.Append(" FROM project ");
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
			strSql.Append(" projectid,projectname,projectsource,projectinfo,projstartdate,projenddate ");
			strSql.Append(" FROM project ");
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
			strSql.Append("select count(1) FROM project ");
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
				strSql.Append("order by T.projectid desc");
			}
			strSql.Append(")AS Row, T.*  from project T ");
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
			parameters[0].Value = "project";
			parameters[1].Value = "projectid";
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

