using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:quotaEngiInfo
	/// </summary>
	public partial class quotaEngiInfo
	{
		public quotaEngiInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string itermid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from quotaEngiInfo");
			strSql.Append(" where itermid=@itermid ");
			SqlParameter[] parameters = {
					new SqlParameter("@itermid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = itermid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.quotaEngiInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into quotaEngiInfo(");
			strSql.Append("itermid,itermname,itermlevel,standard,baseinfo)");
			strSql.Append(" values (");
			strSql.Append("@itermid,@itermname,@itermlevel,@standard,@baseinfo)");
			SqlParameter[] parameters = {
					new SqlParameter("@itermid", SqlDbType.NVarChar,15),
					new SqlParameter("@itermname", SqlDbType.NVarChar,100),
					new SqlParameter("@itermlevel", SqlDbType.NVarChar,2),
					new SqlParameter("@standard", SqlDbType.NVarChar,40),
					new SqlParameter("@baseinfo", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.itermid;
			parameters[1].Value = model.itermname;
			parameters[2].Value = model.itermlevel;
			parameters[3].Value = model.standard;
			parameters[4].Value = model.baseinfo;

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
		public bool Update(CEMM.Model.quotaEngiInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update quotaEngiInfo set ");
			strSql.Append("itermname=@itermname,");
			strSql.Append("itermlevel=@itermlevel,");
			strSql.Append("standard=@standard,");
			strSql.Append("baseinfo=@baseinfo");
			strSql.Append(" where itermid=@itermid ");
			SqlParameter[] parameters = {
					new SqlParameter("@itermname", SqlDbType.NVarChar,100),
					new SqlParameter("@itermlevel", SqlDbType.NVarChar,2),
					new SqlParameter("@standard", SqlDbType.NVarChar,40),
					new SqlParameter("@baseinfo", SqlDbType.NVarChar,100),
					new SqlParameter("@itermid", SqlDbType.NVarChar,15)};
			parameters[0].Value = model.itermname;
			parameters[1].Value = model.itermlevel;
			parameters[2].Value = model.standard;
			parameters[3].Value = model.baseinfo;
			parameters[4].Value = model.itermid;

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
		public bool Delete(string itermid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from quotaEngiInfo ");
			strSql.Append(" where itermid=@itermid ");
			SqlParameter[] parameters = {
					new SqlParameter("@itermid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = itermid;

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
		public bool DeleteList(string itermidlist )
		{
			if (string.IsNullOrWhiteSpace(itermidlist))
				return false;

			string[] ids = itermidlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (ids.Length == 0)
				return false;

			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from quotaEngiInfo where itermid in (");
			SqlParameter[] parameters = new SqlParameter[ids.Length];
			for (int i = 0; i < ids.Length; i++)
			{
				string pname = "@iid" + i;
				if (i > 0) strSql.Append(",");
				strSql.Append(pname);
				parameters[i] = new SqlParameter(pname, SqlDbType.NVarChar, 15);
				parameters[i].Value = ids[i].Trim();
			}
			strSql.Append(")");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CEMM.Model.quotaEngiInfo GetModel(string itermid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 itermid,itermname,itermlevel,standard,baseinfo from quotaEngiInfo ");
			strSql.Append(" where itermid=@itermid ");
			SqlParameter[] parameters = {
					new SqlParameter("@itermid", SqlDbType.NVarChar,15)			};
			parameters[0].Value = itermid;

			CEMM.Model.quotaEngiInfo model=new CEMM.Model.quotaEngiInfo();
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
		public CEMM.Model.quotaEngiInfo DataRowToModel(DataRow row)
		{
			CEMM.Model.quotaEngiInfo model=new CEMM.Model.quotaEngiInfo();
			if (row != null)
			{
				if(row["itermid"]!=null)
				{
					model.itermid=row["itermid"].ToString();
				}
				if(row["itermname"]!=null)
				{
					model.itermname=row["itermname"].ToString();
				}
				if(row["itermlevel"]!=null)
				{
					model.itermlevel=row["itermlevel"].ToString();
				}
				if(row["standard"]!=null)
				{
					model.standard=row["standard"].ToString();
				}
				if(row["baseinfo"]!=null)
				{
					model.baseinfo=row["baseinfo"].ToString();
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
			strSql.Append("select itermid,itermname,itermlevel,standard,baseinfo ");
			strSql.Append(" FROM quotaEngiInfo ");
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
			strSql.Append(" itermid,itermname,itermlevel,standard,baseinfo ");
			strSql.Append(" FROM quotaEngiInfo ");
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
			strSql.Append("select count(1) FROM quotaEngiInfo ");
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
				strSql.Append("order by T.itermid desc");
			}
			strSql.Append(")AS Row, T.*  from quotaEngiInfo T ");
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
			parameters[0].Value = "quotaEngiInfo";
			parameters[1].Value = "itermid";
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
        /// 用条件找相应数据行的基础工作量信息，只保留这一个属性
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListForBase(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select baseinfo");
            strSql.Append(" FROM quotaEngiInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 按名称模糊查询（参数化）
        /// </summary>
        public DataSet GetListByItermName(string itermname)
        {
            string sql = "select itermid,itermname,itermlevel,standard,baseinfo from quotaEngiInfo where itermname like @itermname and itermlevel=3";
            SqlParameter[] parameters = {
                new SqlParameter("@itermname", SqlDbType.NVarChar, 100) { Value = "%" + itermname + "%" }
            };
            return DbHelperSQL.Query(sql, parameters);
        }
		#endregion  ExtensionMethod
	}
}

