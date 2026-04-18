using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
using System.Text; 
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:computeResultTabInfo
	/// </summary>
	public partial class computeResultTabInfo
	{
		public computeResultTabInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("tableID", "computeResultTabInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int tableID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from computeResultTabInfo");
			strSql.Append(" where tableID=@tableID");
			SqlParameter[] parameters = {
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
			parameters[0].Value = tableID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CEMM.Model.computeResultTabInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into computeResultTabInfo(");
			strSql.Append("tableName,inputTime)");
			strSql.Append(" values (");
			strSql.Append("@tableName,@inputTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.NVarChar,100),
					new SqlParameter("@inputTime", SqlDbType.DateTime)};
			parameters[0].Value = model.tableName;
			parameters[1].Value = model.inputTime;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(CEMM.Model.computeResultTabInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update computeResultTabInfo set ");
			strSql.Append("tableName=@tableName,");
			strSql.Append("inputTime=@inputTime");
			strSql.Append(" where tableID=@tableID");
			SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.NVarChar,100),
					new SqlParameter("@inputTime", SqlDbType.DateTime),
					new SqlParameter("@tableID", SqlDbType.Int,4)};
			parameters[0].Value = model.tableName;
			parameters[1].Value = model.inputTime;
			parameters[2].Value = model.tableID;

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
		public bool Delete(int tableID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from computeResultTabInfo ");
			strSql.Append(" where tableID=@tableID");
			SqlParameter[] parameters = {
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
			parameters[0].Value = tableID;

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
		public bool DeleteList(string tableIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from computeResultTabInfo ");
			strSql.Append(" where tableID in ("+tableIDlist + ")  ");
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
		public CEMM.Model.computeResultTabInfo GetModel(int tableID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 tableID,tableName,inputTime from computeResultTabInfo ");
			strSql.Append(" where tableID=@tableID");
			SqlParameter[] parameters = {
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
			parameters[0].Value = tableID;

			CEMM.Model.computeResultTabInfo model=new CEMM.Model.computeResultTabInfo();
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
		public CEMM.Model.computeResultTabInfo DataRowToModel(DataRow row)
		{
			CEMM.Model.computeResultTabInfo model=new CEMM.Model.computeResultTabInfo();
			if (row != null)
			{
				if(row["tableID"]!=null && row["tableID"].ToString()!="")
				{
					model.tableID=int.Parse(row["tableID"].ToString());
				}
				if(row["tableName"]!=null)
				{
					model.tableName=row["tableName"].ToString();
				}
				if(row["inputTime"]!=null && row["inputTime"].ToString()!="")
				{
					model.inputTime=DateTime.Parse(row["inputTime"].ToString());
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
			strSql.Append("select tableID,tableName,inputTime ");
			strSql.Append(" FROM computeResultTabInfo ");
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
			strSql.Append(" tableID,tableName,inputTime ");
			strSql.Append(" FROM computeResultTabInfo ");
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
			strSql.Append("select count(1) FROM computeResultTabInfo ");
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
				strSql.Append("order by T.tableID desc");
			}
			strSql.Append(")AS Row, T.*  from computeResultTabInfo T ");
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
			parameters[0].Value = "computeResultTabInfo";
			parameters[1].Value = "tableID";
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
        /// 得到一个对象实体
        /// </summary>
        public CEMM.Model.computeResultTabInfo GetModelByName(string tableName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 tableID,tableName,inputTime from computeResultTabInfo ");
            strSql.Append(" where tableName=@tableName");
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = tableName;

            CEMM.Model.computeResultTabInfo model = new CEMM.Model.computeResultTabInfo();
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
            strSql.Append(" tableID,tableName,inputTime ");
            strSql.Append(" FROM computeResultTabInfo ");
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

