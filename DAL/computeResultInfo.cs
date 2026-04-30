using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace CEMM.DAL
{
	/// <summary>
	/// 数据访问类:computeResultInfo
	/// </summary>
	public partial class computeResultInfo
	{
		public computeResultInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("resultid", "computeResultInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int resultid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from computeResultInfo");
			strSql.Append(" where resultid=@resultid");
			SqlParameter[] parameters = {
					new SqlParameter("@resultid", SqlDbType.Int,4)
			};
			parameters[0].Value = resultid;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CEMM.Model.computeResultInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into computeResultInfo(");
			strSql.Append("code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID)");
			strSql.Append(" values (");
			strSql.Append("@code,@formName,@unit,@price,@emissionfactor,@total_quantity,@total_emission,@temp_project,@temp_emission,@subgrade_project,@subgrade_emission,@pavement_project,@pavement_emission,@bridge_project,@bridge_emission,@tunnel_project,@tunnel_emission,@crossing_project,@crossing_emission,@traffic_project,@traffic_emission,@greening_project,@greening_emission,@other_project,@other_emission,@assistant_project,@assistant_emission,@tableID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.NVarChar,15),
					new SqlParameter("@formName", SqlDbType.NVarChar,50),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@emissionfactor", SqlDbType.Decimal,9),
					new SqlParameter("@total_quantity", SqlDbType.Decimal,13),
					new SqlParameter("@total_emission", SqlDbType.Decimal,13),
					new SqlParameter("@temp_project", SqlDbType.Decimal,13),
					new SqlParameter("@temp_emission", SqlDbType.Decimal,13),
					new SqlParameter("@subgrade_project", SqlDbType.Decimal,13),
					new SqlParameter("@subgrade_emission", SqlDbType.Decimal,13),
					new SqlParameter("@pavement_project", SqlDbType.Decimal,13),
					new SqlParameter("@pavement_emission", SqlDbType.Decimal,13),
					new SqlParameter("@bridge_project", SqlDbType.Decimal,13),
					new SqlParameter("@bridge_emission", SqlDbType.Decimal,13),
					new SqlParameter("@tunnel_project", SqlDbType.Decimal,13),
					new SqlParameter("@tunnel_emission", SqlDbType.Decimal,13),
					new SqlParameter("@crossing_project", SqlDbType.Decimal,13),
					new SqlParameter("@crossing_emission", SqlDbType.Decimal,13),
					new SqlParameter("@traffic_project", SqlDbType.Decimal,13),
					new SqlParameter("@traffic_emission", SqlDbType.Decimal,13),
					new SqlParameter("@greening_project", SqlDbType.Decimal,13),
					new SqlParameter("@greening_emission", SqlDbType.Decimal,13),
					new SqlParameter("@other_project", SqlDbType.Decimal,13),
					new SqlParameter("@other_emission", SqlDbType.Decimal,13),
					new SqlParameter("@assistant_project", SqlDbType.Decimal,13),
					new SqlParameter("@assistant_emission", SqlDbType.Decimal,13),
					new SqlParameter("@tableID", SqlDbType.Int,4)};
			parameters[0].Value = model.code;
			parameters[1].Value = model.formName;
			parameters[2].Value = model.unit;
			parameters[3].Value = model.price;
			parameters[4].Value = model.emissionfactor;
			parameters[5].Value = model.total_quantity;
			parameters[6].Value = model.total_emission;
			parameters[7].Value = model.temp_project;
			parameters[8].Value = model.temp_emission;
			parameters[9].Value = model.subgrade_project;
			parameters[10].Value = model.subgrade_emission;
			parameters[11].Value = model.pavement_project;
			parameters[12].Value = model.pavement_emission;
			parameters[13].Value = model.bridge_project;
			parameters[14].Value = model.bridge_emission;
			parameters[15].Value = model.tunnel_project;
			parameters[16].Value = model.tunnel_emission;
			parameters[17].Value = model.crossing_project;
			parameters[18].Value = model.crossing_emission;
			parameters[19].Value = model.traffic_project;
			parameters[20].Value = model.traffic_emission;
			parameters[21].Value = model.greening_project;
			parameters[22].Value = model.greening_emission;
			parameters[23].Value = model.other_project;
			parameters[24].Value = model.other_emission;
			parameters[25].Value = model.assistant_project;
			parameters[26].Value = model.assistant_emission;
			parameters[27].Value = model.tableID;

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
		public bool Update(CEMM.Model.computeResultInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update computeResultInfo set ");
			strSql.Append("code=@code,");
			strSql.Append("formName=@formName,");
			strSql.Append("unit=@unit,");
			strSql.Append("price=@price,");
			strSql.Append("emissionfactor=@emissionfactor,");
			strSql.Append("total_quantity=@total_quantity,");
			strSql.Append("total_emission=@total_emission,");
			strSql.Append("temp_project=@temp_project,");
			strSql.Append("temp_emission=@temp_emission,");
			strSql.Append("subgrade_project=@subgrade_project,");
			strSql.Append("subgrade_emission=@subgrade_emission,");
			strSql.Append("pavement_project=@pavement_project,");
			strSql.Append("pavement_emission=@pavement_emission,");
			strSql.Append("bridge_project=@bridge_project,");
			strSql.Append("bridge_emission=@bridge_emission,");
			strSql.Append("tunnel_project=@tunnel_project,");
			strSql.Append("tunnel_emission=@tunnel_emission,");
			strSql.Append("crossing_project=@crossing_project,");
			strSql.Append("crossing_emission=@crossing_emission,");
			strSql.Append("traffic_project=@traffic_project,");
			strSql.Append("traffic_emission=@traffic_emission,");
			strSql.Append("greening_project=@greening_project,");
			strSql.Append("greening_emission=@greening_emission,");
			strSql.Append("other_project=@other_project,");
			strSql.Append("other_emission=@other_emission,");
			strSql.Append("assistant_project=@assistant_project,");
			strSql.Append("assistant_emission=@assistant_emission,");
			strSql.Append("tableID=@tableID");
			strSql.Append(" where resultid=@resultid");
			SqlParameter[] parameters = {
					new SqlParameter("@code", SqlDbType.NVarChar,15),
					new SqlParameter("@formName", SqlDbType.NVarChar,50),
					new SqlParameter("@unit", SqlDbType.NVarChar,10),
					new SqlParameter("@price", SqlDbType.Decimal,9),
					new SqlParameter("@emissionfactor", SqlDbType.Decimal,9),
					new SqlParameter("@total_quantity", SqlDbType.Decimal,13),
					new SqlParameter("@total_emission", SqlDbType.Decimal,13),
					new SqlParameter("@temp_project", SqlDbType.Decimal,13),
					new SqlParameter("@temp_emission", SqlDbType.Decimal,13),
					new SqlParameter("@subgrade_project", SqlDbType.Decimal,13),
					new SqlParameter("@subgrade_emission", SqlDbType.Decimal,13),
					new SqlParameter("@pavement_project", SqlDbType.Decimal,13),
					new SqlParameter("@pavement_emission", SqlDbType.Decimal,13),
					new SqlParameter("@bridge_project", SqlDbType.Decimal,13),
					new SqlParameter("@bridge_emission", SqlDbType.Decimal,13),
					new SqlParameter("@tunnel_project", SqlDbType.Decimal,13),
					new SqlParameter("@tunnel_emission", SqlDbType.Decimal,13),
					new SqlParameter("@crossing_project", SqlDbType.Decimal,13),
					new SqlParameter("@crossing_emission", SqlDbType.Decimal,13),
					new SqlParameter("@traffic_project", SqlDbType.Decimal,13),
					new SqlParameter("@traffic_emission", SqlDbType.Decimal,13),
					new SqlParameter("@greening_project", SqlDbType.Decimal,13),
					new SqlParameter("@greening_emission", SqlDbType.Decimal,13),
					new SqlParameter("@other_project", SqlDbType.Decimal,13),
					new SqlParameter("@other_emission", SqlDbType.Decimal,13),
					new SqlParameter("@assistant_project", SqlDbType.Decimal,13),
					new SqlParameter("@assistant_emission", SqlDbType.Decimal,13),
					new SqlParameter("@tableID", SqlDbType.Int,4),
					new SqlParameter("@resultid", SqlDbType.Int,4)};
			parameters[0].Value = model.code;
			parameters[1].Value = model.formName;
			parameters[2].Value = model.unit;
			parameters[3].Value = model.price;
			parameters[4].Value = model.emissionfactor;
			parameters[5].Value = model.total_quantity;
			parameters[6].Value = model.total_emission;
			parameters[7].Value = model.temp_project;
			parameters[8].Value = model.temp_emission;
			parameters[9].Value = model.subgrade_project;
			parameters[10].Value = model.subgrade_emission;
			parameters[11].Value = model.pavement_project;
			parameters[12].Value = model.pavement_emission;
			parameters[13].Value = model.bridge_project;
			parameters[14].Value = model.bridge_emission;
			parameters[15].Value = model.tunnel_project;
			parameters[16].Value = model.tunnel_emission;
			parameters[17].Value = model.crossing_project;
			parameters[18].Value = model.crossing_emission;
			parameters[19].Value = model.traffic_project;
			parameters[20].Value = model.traffic_emission;
			parameters[21].Value = model.greening_project;
			parameters[22].Value = model.greening_emission;
			parameters[23].Value = model.other_project;
			parameters[24].Value = model.other_emission;
			parameters[25].Value = model.assistant_project;
			parameters[26].Value = model.assistant_emission;
			parameters[27].Value = model.tableID;
			parameters[28].Value = model.resultid;

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
		public bool Delete(int resultid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from computeResultInfo ");
			strSql.Append(" where resultid=@resultid");
			SqlParameter[] parameters = {
					new SqlParameter("@resultid", SqlDbType.Int,4)
			};
			parameters[0].Value = resultid;

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
		public bool DeleteList(string resultidlist )
		{
			if (string.IsNullOrWhiteSpace(resultidlist))
				return false;

			string[] ids = resultidlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (ids.Length == 0)
				return false;

			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from computeResultInfo where resultid in (");
			SqlParameter[] parameters = new SqlParameter[ids.Length];
			for (int i = 0; i < ids.Length; i++)
			{
				string pname = "@rid" + i;
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
		public CEMM.Model.computeResultInfo GetModel(int resultid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 resultid,code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID from computeResultInfo ");
			strSql.Append(" where resultid=@resultid");
			SqlParameter[] parameters = {
					new SqlParameter("@resultid", SqlDbType.Int,4)
			};
			parameters[0].Value = resultid;

			CEMM.Model.computeResultInfo model=new CEMM.Model.computeResultInfo();
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
		public CEMM.Model.computeResultInfo DataRowToModel(DataRow row)
		{
			CEMM.Model.computeResultInfo model=new CEMM.Model.computeResultInfo();
			if (row != null)
			{
				if(row["resultid"]!=null && row["resultid"].ToString()!="")
				{
					model.resultid=int.Parse(row["resultid"].ToString());
				}
				if(row["code"]!=null)
				{
					model.code=row["code"].ToString();
				}
				if(row["formName"]!=null)
				{
					model.formName=row["formName"].ToString();
				}
				if(row["unit"]!=null)
				{
					model.unit=row["unit"].ToString();
				}
				if(row["price"]!=null && row["price"].ToString()!="")
				{
					model.price=decimal.Parse(row["price"].ToString());
				}
				if(row["emissionfactor"]!=null && row["emissionfactor"].ToString()!="")
				{
					model.emissionfactor=decimal.Parse(row["emissionfactor"].ToString());
				}
				if(row["total_quantity"]!=null && row["total_quantity"].ToString()!="")
				{
					model.total_quantity=decimal.Parse(row["total_quantity"].ToString());
				}
				if(row["total_emission"]!=null && row["total_emission"].ToString()!="")
				{
					model.total_emission=decimal.Parse(row["total_emission"].ToString());
				}
				if(row["temp_project"]!=null && row["temp_project"].ToString()!="")
				{
					model.temp_project=decimal.Parse(row["temp_project"].ToString());
				}
				if(row["temp_emission"]!=null && row["temp_emission"].ToString()!="")
				{
					model.temp_emission=decimal.Parse(row["temp_emission"].ToString());
				}
				if(row["subgrade_project"]!=null && row["subgrade_project"].ToString()!="")
				{
					model.subgrade_project=decimal.Parse(row["subgrade_project"].ToString());
				}
				if(row["subgrade_emission"]!=null && row["subgrade_emission"].ToString()!="")
				{
					model.subgrade_emission=decimal.Parse(row["subgrade_emission"].ToString());
				}
				if(row["pavement_project"]!=null && row["pavement_project"].ToString()!="")
				{
					model.pavement_project=decimal.Parse(row["pavement_project"].ToString());
				}
				if(row["pavement_emission"]!=null && row["pavement_emission"].ToString()!="")
				{
					model.pavement_emission=decimal.Parse(row["pavement_emission"].ToString());
				}
				if(row["bridge_project"]!=null && row["bridge_project"].ToString()!="")
				{
					model.bridge_project=decimal.Parse(row["bridge_project"].ToString());
				}
				if(row["bridge_emission"]!=null && row["bridge_emission"].ToString()!="")
				{
					model.bridge_emission=decimal.Parse(row["bridge_emission"].ToString());
				}
				if(row["tunnel_project"]!=null && row["tunnel_project"].ToString()!="")
				{
					model.tunnel_project=decimal.Parse(row["tunnel_project"].ToString());
				}
				if(row["tunnel_emission"]!=null && row["tunnel_emission"].ToString()!="")
				{
					model.tunnel_emission=decimal.Parse(row["tunnel_emission"].ToString());
				}
				if(row["crossing_project"]!=null && row["crossing_project"].ToString()!="")
				{
					model.crossing_project=decimal.Parse(row["crossing_project"].ToString());
				}
				if(row["crossing_emission"]!=null && row["crossing_emission"].ToString()!="")
				{
					model.crossing_emission=decimal.Parse(row["crossing_emission"].ToString());
				}
				if(row["traffic_project"]!=null && row["traffic_project"].ToString()!="")
				{
					model.traffic_project=decimal.Parse(row["traffic_project"].ToString());
				}
				if(row["traffic_emission"]!=null && row["traffic_emission"].ToString()!="")
				{
					model.traffic_emission=decimal.Parse(row["traffic_emission"].ToString());
				}
				if(row["greening_project"]!=null && row["greening_project"].ToString()!="")
				{
					model.greening_project=decimal.Parse(row["greening_project"].ToString());
				}
				if(row["greening_emission"]!=null && row["greening_emission"].ToString()!="")
				{
					model.greening_emission=decimal.Parse(row["greening_emission"].ToString());
				}
				if(row["other_project"]!=null && row["other_project"].ToString()!="")
				{
					model.other_project=decimal.Parse(row["other_project"].ToString());
				}
				if(row["other_emission"]!=null && row["other_emission"].ToString()!="")
				{
					model.other_emission=decimal.Parse(row["other_emission"].ToString());
				}
				if(row["assistant_project"]!=null && row["assistant_project"].ToString()!="")
				{
					model.assistant_project=decimal.Parse(row["assistant_project"].ToString());
				}
				if(row["assistant_emission"]!=null && row["assistant_emission"].ToString()!="")
				{
					model.assistant_emission=decimal.Parse(row["assistant_emission"].ToString());
				}
				if(row["tableID"]!=null && row["tableID"].ToString()!="")
				{
					model.tableID=int.Parse(row["tableID"].ToString());
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
			strSql.Append("select resultid,code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID ");
			strSql.Append(" FROM computeResultInfo ");
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
			strSql.Append(" resultid,code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID ");
			strSql.Append(" FROM computeResultInfo ");
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
			strSql.Append("select count(1) FROM computeResultInfo ");
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
				strSql.Append("order by T.resultid desc");
			}
			strSql.Append(")AS Row, T.*  from computeResultInfo T ");
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
			parameters[0].Value = "computeResultInfo";
			parameters[1].Value = "resultid";
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
        /// 得到一个对象实体，根据code、tableID
        /// </summary>
        public CEMM.Model.computeResultInfo GetModel2(string code, int tableID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 resultid,code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID from computeResultInfo ");
            strSql.Append(" where code=@code and tableID=@tableID");
            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,15),
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
            parameters[0].Value = code;
            parameters[1].Value = tableID;

            CEMM.Model.computeResultInfo model = new CEMM.Model.computeResultInfo();
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
        /// 根据tableId，得到材料生产的碳排放量
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public double GetMateCEmission(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(total_emission) FROM computeResultInfo ");
            strSql.Append(" where tableID=@tableID ");
            strSql.Append(" and code in (select code from machineCEFactor2 where mfid>=5000 and mfid<999998)");
            SqlParameter[] parameters = {
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters); //加parameters
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// 根据tableId，得到材料运输的碳排放量
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public double GetTransCEmission(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(total_emission) FROM computeResultInfo ");
            strSql.Append(" where tableID=@tableID ");
            strSql.Append(" and ((code between '8003031' and '8003034') or (code between '8005022' and '8005038') or ");
            strSql.Append(" (code between '8007001' and '8007022') or (code between '8007034' and '8007049') or (code in ('8007054','8007127')))");
            //strSql.Append(" (code between '8007055' and '8007059') or (code between '8007063' and '8007067') "); //2025.7.30去掉了
            SqlParameter[] parameters = {
                    new SqlParameter("@tableID", SqlDbType.Int,4),
			};
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters); //加parameters
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// 根据tableId，得到人工&机械工的碳排放量
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public double GetLaborCEmission(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(total_emission) FROM computeResultInfo ");
            strSql.Append(" where tableID=@tableID ");
            strSql.Append(" and code in ('1001001','1051001')");
            SqlParameter[] parameters = {
					new SqlParameter("@tableID", SqlDbType.Int,4)
			};
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters); //加parameters
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
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
            strSql.Append(" resultid,code,formName,unit,price,emissionfactor,total_quantity,total_emission,temp_project,temp_emission,subgrade_project,subgrade_emission,pavement_project,pavement_emission,bridge_project,bridge_emission,tunnel_project,tunnel_emission,crossing_project,crossing_emission,traffic_project,traffic_emission,greening_project,greening_emission,other_project,other_emission,assistant_project,assistant_emission,tableID ");
            strSql.Append(" FROM computeResultInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder + " desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetTopNWithFields(int Top, string strWhere, string orderField, string[] fields)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }

            strSql.Append(" " + string.Join(", ", fields));
            strSql.Append(" FROM computeResultInfo ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by " + orderField + " desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取指定表的所有材料使用量总和
        /// </summary>
        public double GetTotalMaterialQuantity(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(total_quantity) from computeResultInfo ");
            strSql.Append("WHERE tableID = @tableID AND (code  between '1509001' and '7701030') ");
            //strSql.Append("(SELECT code FROM machineCEFactor2 WHERE mfid >= 5000 AND mfid < 999998)");

            SqlParameter[] parameters = {
        new SqlParameter("@tableID", SqlDbType.Int, 4)
    };
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// 获取指定表的所有机械使用量总和
        /// </summary>
        public double GetTotalMachineQuantity(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(total_quantity) from computeResultInfo ");
            strSql.Append("WHERE tableID = @tableID AND code LIKE '8%'");

            SqlParameter[] parameters = {
        new SqlParameter("@tableID", SqlDbType.Int, 4)
    };
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// 执行标量查询
        /// </summary>
        public object ExecuteScalar(string sql, SqlParameter[] parameters)
        {
            return DbHelperSQL.GetSingle(sql, parameters);
        }

        /// <summary>
        /// 获取指定表的所有记录的总使用量总和
        /// </summary>
        public double GetTotalUsageQuantity(int tableID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(total_quantity) from computeResultInfo ");
            strSql.Append("WHERE tableID = @tableID");

            SqlParameter[] parameters = {
        new SqlParameter("@tableID", SqlDbType.Int, 4)
    };
            parameters[0].Value = tableID;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null || obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
		#endregion  ExtensionMethod
	}
}

