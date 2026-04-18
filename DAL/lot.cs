using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:lot
	/// </summary>
	public partial class lot
	{
		public lot()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string lotid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from lot");
			strSql.Append(" where lotid=@lotid ");
			SqlParameter[] parameters = {
					new SqlParameter("@lotid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = lotid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.lot model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into lot(");
			strSql.Append("lotid,lotname,lotstartpos,lotendpos,projectid,Construparty,lotstartdate,lotenddate)");
			strSql.Append(" values (");
			strSql.Append("@lotid,@lotname,@lotstartpos,@lotendpos,@projectid,@Construparty,@lotstartdate,@lotenddate)");
			SqlParameter[] parameters = {
					new SqlParameter("@lotid", SqlDbType.NVarChar,50),
					new SqlParameter("@lotname", SqlDbType.NVarChar,100),
					new SqlParameter("@lotstartpos", SqlDbType.NVarChar,100),
					new SqlParameter("@lotendpos", SqlDbType.NVarChar,100),
					new SqlParameter("@projectid", SqlDbType.NVarChar,50),
					new SqlParameter("@Construparty", SqlDbType.NVarChar,100),
					new SqlParameter("@lotstartdate", SqlDbType.Date),
					new SqlParameter("@lotenddate", SqlDbType.Date)};
			parameters[0].Value = model.lotid;
			parameters[1].Value = model.lotname;
			parameters[2].Value = model.lotstartpos;
			parameters[3].Value = model.lotendpos;
			parameters[4].Value = model.projectid;
			parameters[5].Value = model.Construparty;
			parameters[6].Value = model.lotstartdate;
			parameters[7].Value = model.lotenddate;

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
		public bool Update(CEMM.Model.lot model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update lot set ");
			strSql.Append("lotname=@lotname,");
			strSql.Append("lotstartpos=@lotstartpos,");
			strSql.Append("lotendpos=@lotendpos,");
			strSql.Append("projectid=@projectid,");
			strSql.Append("Construparty=@Construparty,");
			strSql.Append("lotstartdate=@lotstartdate,");
			strSql.Append("lotenddate=@lotenddate");
			strSql.Append(" where lotid=@lotid ");
			SqlParameter[] parameters = {
					new SqlParameter("@lotname", SqlDbType.NVarChar,100),
					new SqlParameter("@lotstartpos", SqlDbType.NVarChar,100),
					new SqlParameter("@lotendpos", SqlDbType.NVarChar,100),
					new SqlParameter("@projectid", SqlDbType.NVarChar,50),
					new SqlParameter("@Construparty", SqlDbType.NVarChar,100),
					new SqlParameter("@lotstartdate", SqlDbType.Date),
					new SqlParameter("@lotenddate", SqlDbType.Date),
					new SqlParameter("@lotid", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.lotname;
			parameters[1].Value = model.lotstartpos;
			parameters[2].Value = model.lotendpos;
			parameters[3].Value = model.projectid;
			parameters[4].Value = model.Construparty;
			parameters[5].Value = model.lotstartdate;
			parameters[6].Value = model.lotenddate;
			parameters[7].Value = model.lotid;

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
		public bool Delete(string lotid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from lot ");
			strSql.Append(" where lotid=@lotid ");
			SqlParameter[] parameters = {
					new SqlParameter("@lotid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = lotid;

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
		public bool DeleteList(string lotidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from lot ");
			strSql.Append(" where lotid in ("+lotidlist + ")  ");
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
		public CEMM.Model.lot GetModel(string lotid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 lotid,lotname,lotstartpos,lotendpos,projectid,Construparty,lotstartdate,lotenddate from lot ");
			strSql.Append(" where lotid=@lotid ");
			SqlParameter[] parameters = {
					new SqlParameter("@lotid", SqlDbType.NVarChar,50)			};
			parameters[0].Value = lotid;

			CEMM.Model.lot model=new CEMM.Model.lot();
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
		public CEMM.Model.lot DataRowToModel(DataRow row)
		{
			CEMM.Model.lot model=new CEMM.Model.lot();
			if (row != null)
			{
				if(row["lotid"]!=null)
				{
					model.lotid=row["lotid"].ToString();
				}
				if(row["lotname"]!=null)
				{
					model.lotname=row["lotname"].ToString();
				}
				if(row["lotstartpos"]!=null)
				{
					model.lotstartpos=row["lotstartpos"].ToString();
				}
				if(row["lotendpos"]!=null)
				{
					model.lotendpos=row["lotendpos"].ToString();
				}
				if(row["projectid"]!=null)
				{
					model.projectid=row["projectid"].ToString();
				}
				if(row["Construparty"]!=null)
				{
					model.Construparty=row["Construparty"].ToString();
				}
				if(row["lotstartdate"]!=null && row["lotstartdate"].ToString()!="")
				{
					model.lotstartdate=DateTime.Parse(row["lotstartdate"].ToString());
				}
				if(row["lotenddate"]!=null && row["lotenddate"].ToString()!="")
				{
					model.lotenddate=DateTime.Parse(row["lotenddate"].ToString());
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
			strSql.Append("select lotid,lotname,lotstartpos,lotendpos,projectid,Construparty,lotstartdate,lotenddate ");
			strSql.Append(" FROM lot ");
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
			strSql.Append(" lotid,lotname,lotstartpos,lotendpos,projectid,Construparty,lotstartdate,lotenddate ");
			strSql.Append(" FROM lot ");
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
			strSql.Append("select count(1) FROM lot ");
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
				strSql.Append("order by T.lotid desc");
			}
			strSql.Append(")AS Row, T.*  from lot T ");
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
			parameters[0].Value = "lot";
			parameters[1].Value = "lotid";
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

