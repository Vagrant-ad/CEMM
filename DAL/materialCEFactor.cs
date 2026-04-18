using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:materialCEFactor
	/// </summary>
	public partial class materialCEFactor
	{
		public materialCEFactor()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("mfid", "materialCEFactor"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int mfid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from materialCEFactor");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4)			};
			parameters[0].Value = mfid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(CEMM.Model.materialCEFactor model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into materialCEFactor(");
			strSql.Append("mfid,name,code,specific,unit,unitmass,emissfactor,standardid)");
			strSql.Append(" values (");
			strSql.Append("@mfid,@name,@code,@specific,@unit,@unitmass,@emissfactor,@standardid)");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.VarChar,15),
					new SqlParameter("@specific", SqlDbType.NVarChar,80),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@unitmass", SqlDbType.Decimal,9),
					new SqlParameter("@emissfactor", SqlDbType.Decimal,9),
					new SqlParameter("@standardid", SqlDbType.NVarChar,15)};
			parameters[0].Value = model.mfid;
			parameters[1].Value = model.name;
			parameters[2].Value = model.code;
			parameters[3].Value = model.specific;
			parameters[4].Value = model.unit;
			parameters[5].Value = model.unitmass;
			parameters[6].Value = model.emissfactor;
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
		public bool Update(CEMM.Model.materialCEFactor model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update materialCEFactor set ");
			strSql.Append("name=@name,");
			strSql.Append("code=@code,");
			strSql.Append("specific=@specific,");
			strSql.Append("unit=@unit,");
			strSql.Append("unitmass=@unitmass,");
			strSql.Append("emissfactor=@emissfactor,");
			strSql.Append("standardid=@standardid");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.VarChar,15),
					new SqlParameter("@specific", SqlDbType.NVarChar,80),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@unitmass", SqlDbType.Decimal,9),
					new SqlParameter("@emissfactor", SqlDbType.Decimal,9),
					new SqlParameter("@standardid", SqlDbType.NVarChar,15),
					new SqlParameter("@mfid", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.code;
			parameters[2].Value = model.specific;
			parameters[3].Value = model.unit;
			parameters[4].Value = model.unitmass;
			parameters[5].Value = model.emissfactor;
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
			strSql.Append("delete from materialCEFactor ");
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from materialCEFactor ");
			strSql.Append(" where mfid in ("+mfidlist + ")  ");
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
		public CEMM.Model.materialCEFactor GetModel(int mfid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 mfid,name,code,specific,unit,unitmass,emissfactor,standardid from materialCEFactor ");
			strSql.Append(" where mfid=@mfid ");
			SqlParameter[] parameters = {
					new SqlParameter("@mfid", SqlDbType.Int,4)			};
			parameters[0].Value = mfid;

			CEMM.Model.materialCEFactor model=new CEMM.Model.materialCEFactor();
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
		public CEMM.Model.materialCEFactor DataRowToModel(DataRow row)
		{
			CEMM.Model.materialCEFactor model=new CEMM.Model.materialCEFactor();
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
				if(row["unitmass"]!=null && row["unitmass"].ToString()!="")
				{
					model.unitmass=decimal.Parse(row["unitmass"].ToString());
				}
				if(row["emissfactor"]!=null && row["emissfactor"].ToString()!="")
				{
					model.emissfactor=decimal.Parse(row["emissfactor"].ToString());
				}
				if(row["standardid"]!=null)
				{
					model.standardid=row["standardid"].ToString();
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
			strSql.Append("select mfid,name,code,specific,unit,unitmass,emissfactor,standardid ");
			strSql.Append(" FROM materialCEFactor ");
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
			strSql.Append(" mfid,name,code,specific,unit,unitmass,emissfactor,standardid ");
			strSql.Append(" FROM materialCEFactor ");
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
			strSql.Append("select count(1) FROM materialCEFactor ");
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
			strSql.Append(")AS Row, T.*  from materialCEFactor T ");
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
			parameters[0].Value = "materialCEFactor";
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

		#endregion  ExtensionMethod
	}
}

