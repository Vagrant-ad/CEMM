using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using CEMM.BLL;

namespace CEMM.Web.sgf
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CEMM.BLL.quotaEngiInfo engiInfoBLL = new quotaEngiInfo();
        CEMM.BLL.quotaData quotaDataBLL = new quotaData();
        CEMM.BLL.machineCEFactor2 factor2BLL = new machineCEFactor2();
        CEMM.Model.quotaData quotaDataMdl = new Model.quotaData();

        /// <summary>
        /// 根据内容查找表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = engiInfoBLL.GetListByname(txtSearch.Text.Trim());         
            if (ds.Tables[0].Rows.Count != 0)
            {
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "itermname";
                DropDownList1.DataValueField = "itermid";
                DropDownList1.DataBind();
            }
            else
                MessageBox.Show(this, "不存在查找的信息！");
        }
        /// <summary>
        /// 根据表的编号，查找表的具体内容，并显示到gridview中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConform_Click(object sender, EventArgs e)
        {
            //GV中的内容
            string termid = DropDownList1.SelectedValue;
            DataSet ds = quotaDataBLL.GetList2("subitermid ='" + termid + "'");

            if (ds.Tables[0].Rows.Count != 0)
            {
                gvProjects1.DataSource = ds;
                gvProjects1.DataKeyNames = new string[] { "srid" };
                gvProjects1.DataBind();
            }
            else
                MessageBox.Show(this, "该表信息有误，查找失败！");

            //基础工作量信息内容
            DataSet ds1 = engiInfoBLL.GetListForBase("itermid='" + termid + "'");
            if (ds1.Tables[0].Rows.Count != 0)
            {
                //basemessage.Text = Convert.ToString(ds1.Tables);
                string str = ds1.Tables[0].Rows[0]["baseinfo"].ToString();
                if (str == "")
                {
                    lblBaseQuant.Text = "该表不存在基础工作量信息！";
                }
                else {
                lblBaseQuant.Text = ds1.Tables[0].Rows[0]["baseinfo"].ToString();
                lblBaseQuant.DataBind();
                //basemessage.Text = ds1.Tables[0].Rows[0].ToString();
                }

            }
            else
                MessageBox.Show(this, "基础工作量信息有误，请检查！");
            endquant.Text = "0.00";
            TextBox1.Text = "1";
        }

        protected void btnCompute_Click(object sender, EventArgs e)
        {
            //List<Dictionary<string, object>> selectedRowsData = new List<Dictionary<string, object>>();//测试用字典列表
            //List<string> selectedSubitermsrids = new List<string>();//调试的时候观察选中主键数目和主键内容用

            decimal? engResult = 0.0M;
            decimal? multi;
            string srid;
            if (TextBox1.Text != "")
                multi = Convert.ToDecimal(TextBox1.Text);
            else multi = 1;

            foreach (GridViewRow row in gvProjects1.Rows)
            {
                // 获取复选框控件 - 选择列是第一列（索引0，FindControl中已经从checkBox1更正为xzThis（aspx文件中的id））
                CheckBox chkSelect = row.Cells[0].FindControl("xzThis") as CheckBox;

                // 如果找不到，尝试其他可能的ID
                if (chkSelect == null)
                {
                    foreach (Control control in row.Cells[0].Controls)
                    {
                        if (control is CheckBox)
                        {
                            chkSelect = (CheckBox)control;
                            break;
                        }
                    }
                }

                if (chkSelect != null && chkSelect.Checked)
                {
                    // 获取数据键值subitermsrid--TEST 选中了表中哪些行，测试用
                    //string subitermsrid = gvProjects1.DataKeys[row.RowIndex].Value.ToString();
                    srid = gvProjects1.DataKeys[row.RowIndex].Value.ToString();
                    //selectedSubitermsrids.Add(srid);

                    // 获取行数据
                    Dictionary<string, object> rowData = new Dictionary<string, object>();

                    // 添加这一行的各列数据
                    for (int i = 1; i < row.Cells.Count; i++) // 从1开始跳过选择列,因为第一列只做选中判断
                    {
                        string columnName = gvProjects1.Columns[i].HeaderText;
                        rowData[columnName] = row.Cells[i].Text;
                    }
                }
                else continue;
                //获取参与计算的srid并且通过获取对应的toolid获取对应碳排放因子，因为计算需要srid和碳排放因子
                //这块正常应该获取MDL对象，通过属性获取值*********************************2025.6.28
                //下面的语句逻辑错误，待修改，2025.7.23
                //DataSet sridAndToolid = quotaDataBLL.GetList3(" subitermsrid = '" + gvProjects1.DataKeys[row.RowIndex].Value.ToString() + "' and toolquant = '" + row.Cells[5].Text+"'");
                //DataSet sridAndToolid = quotaDataBLL.GetList3(" srid = '" + srid + "'"); //2025.7.23修改
                //DataSet Factor = factor2BLL.GetMachineFactor(" toolid = '"+sridAndToolid.Tables[0].Rows[0]["toolid"].ToString()+"'");
                quotaDataMdl = quotaDataBLL.GetModel(Convert.ToInt32(srid)); //2025.7.23进一步修改
                DataSet Factor = factor2BLL.GetMachineFactor(" toolid = '" + quotaDataMdl.toolid + "'"); //2025.7.23进一步修改

                decimal? factor = Convert.ToDecimal(Factor.Tables[0].Rows[0]["machinefactor"]);
                
                //string test = row.Cells[6].Text;
                //int testsrid = Convert.ToInt32(sridAndToolid.Tables[0].Rows[0]["srid"].ToString().Trim());
                
                //距离对应控件获取值
                decimal? distance = 0.0M; int nullFlag = 0;
                TextBox txtConstructionQuant = row.FindControl("txtConstructionQuant1") as TextBox;
                if (txtConstructionQuant.Text.Trim() != "")
                    distance = Convert.ToDecimal(txtConstructionQuant.Text);
                else
                {
                    nullFlag = 1;
                    distance = 1;
                }
                
                //判断如果factor或者distance没有值，这行数据不参与计算，或者误选中但是赋值distance为0实际上也不参与计算,这会用flag判断是否是阶梯计算

                //bool flag = quotaDataBLL.HasDygx(Convert.ToInt32(sridAndToolid.Tables[0].Rows[0]["srid"].ToString().Trim()));
                bool flag = quotaDataBLL.HasDygx(Convert.ToInt32(srid)); //2025.7.23进一步修改
                if (!factor.HasValue || !distance.HasValue&&flag||distance==0.0M&&flag) continue;
                //engResult += quotaDataBLL.EngQuantCal(Convert.ToInt32(sridAndToolid.Tables[0].Rows[0]["srid"].ToString().Trim()),multi,distance,nullFlag)*factor;
                engResult += quotaDataBLL.EngQuantCal(Convert.ToInt32(srid), multi, distance, nullFlag) * factor; //2025.7.23进一步修改
            }
            engResult = Math.Round((decimal)engResult, 3);
            endquant.Text = Convert.ToString(engResult);
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {

        }

//        protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
//        {
//            
//        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void basemessage_TextChanged(object sender, EventArgs e)
        {

        }


        protected void basemessage_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void gvProjects1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        

        

        

         

        
        }
    }
