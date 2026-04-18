using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Text;
using DataTable = System.Data.DataTable;
using System.Threading;
using System.Reflection;

namespace CEMM.Web.sgf
{
    public partial class machineCEFactor2cz : System.Web.UI.Page
    {
        CEMM.BLL.machineCEFactor2 factor2BLL = new CEMM.BLL.machineCEFactor2();
        static string fileName;//存储文件名
        static System.Data.DataTable dtResult;//存储结果数据

        protected void Page_Load(object sender, EventArgs e)// 页面加载事件
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)// 搜索按钮
        {
            // 获取查询关键字
            string keyword = txtSearch.Text.Trim();// 去除前后空格的搜索文本
            CurrentSearchKeyword = keyword; // 保存查询条件

            // 检查输入是否为空
            if (string.IsNullOrEmpty(keyword))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('查询内容为空，全部显示！');", true);
                BindGridView();
                return;
            }

            try
            {
                // 修改这里：使用包含energytype的查询方法
                DataSet ds = factor2BLL.GetListWithEnergyType("(name like '%" + keyword + "%' OR code like '%" + keyword + "%')");

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)// 如果有查询结果
                {
                    gvProjects1.DataSource = ds.Tables[0]; // 直接绑定DataTable
                    gvProjects1.DataBind();
                }
                else
                {
                    gvProjects1.DataSource = null; // 清空数据源
                    gvProjects1.DataBind();// 绑定空数据
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('未找到匹配记录！');", true);
                }
            }
            catch (Exception ex)// 捕获异常
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    string.Format("alert('查询出错：{0}');", ex.Message.Replace("'", "\\'")),
                    true);
            }
        }

        protected void gvProjects1_SelectedIndexChanged(object sender, EventArgs e)//行变化
        {
        }

        protected void gvProjects1_RowEditing(object sender, GridViewEditEventArgs e)//行编辑事件
        {
            gvProjects1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvProjects1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)//取消编辑事件
        {
            gvProjects1.EditIndex = -1;
            BindGridView();
        }

        protected void gvProjects1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvProjects1.Rows[e.RowIndex];
                int mfid = Convert.ToInt32(gvProjects1.DataKeys[e.RowIndex].Value);

                // 获取修改后的值 - 正确的语法
                string name = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtName")).Text;
                string code = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtCode")).Text;
                string specific = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtSpecific")).Text;
                string unit = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtUnit")).Text;

                // 处理可为空的decimal字段
                string energyfactorStr = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtEnergyFactor")).Text;
                decimal? energyfactor = string.IsNullOrEmpty(energyfactorStr) ? (decimal?)null : Convert.ToDecimal(energyfactorStr);

                string machinefactorStr = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtMachineFactor")).Text;
                decimal? machinefactor = string.IsNullOrEmpty(machinefactorStr) ? (decimal?)null : Convert.ToDecimal(machinefactorStr);

                string standardid = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtStandardId")).Text;

                // 处理能源类型字段（允许空值）
                string energyTypeStr = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtEnergyType")).Text;
                int? energyType = string.IsNullOrEmpty(energyTypeStr) ? (int?)null : Convert.ToInt32(energyTypeStr);

                // 创建模型对象并赋值
                CEMM.Model.machineCEFactor2 model = new CEMM.Model.machineCEFactor2();
                model.mfid = mfid;
                model.name = name;
                model.code = code;
                model.specific = specific;
                model.unit = unit;
                model.energyfactor = energyfactor;
                model.machinefactor = machinefactor;
                model.standardid = standardid;
                model.energytype = energyType; // 添加能源类型

                if (factor2BLL.Update(model))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('修改成功！');", true);
                    gvProjects1.EditIndex = -1;
                    BindGridView();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('修改失败！');", true);
                }
            }
            catch (FormatException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('数值格式不正确，请检查能源因子、机械因子和能源类型输入！');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    string.Format("alert('修改失败：{0}');", ex.Message.Replace("'", "\\'")), true);
            }
        }


        protected void gvProjects1_RowDeleting(object sender, GridViewDeleteEventArgs e)// 行删除事件
        {
            int mfid = Convert.ToInt32(gvProjects1.DataKeys[e.RowIndex].Value);// 获取要删除的主键ID

            if (factor2BLL.Delete(mfid))// 调用删除方法
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除成功！');", true);
                // 关键修改：保持搜索条件，只重新绑定数据
                BindGridView();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('删除失败！');", true);
            }
        }

        protected void gvProjects1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProjects1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void gvProjects1_PageIndexChanged(object sender, EventArgs e)
        {
            // 确保分页后数据正确显示
            BindGridView();
        }

        protected void gvProjects1_RowDataBound(object sender, GridViewRowEventArgs e)// 行数据绑定事件
        {
            if (e.Row.RowType == DataControlRowType.DataRow) // 如果是数据行
            {
                // 获取数据行
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // 检查机械名称是否为"重油"
                string machineName = rowView["name"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)// CSV导入按钮
        {
            if (!FileUpload1.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请选择CSV文件！');", true);
                return;
            }

            string fileName = FileUpload1.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (fileExt != ".csv")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('只支持CSV文件！');", true);
                return;
            }

            try
            {
                string savePath = Server.MapPath("~/UpFile/" + fileName);

                if (!Directory.Exists(Server.MapPath("~/UpFile")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/UpFile"));
                }

                FileUpload1.SaveAs(savePath);

                DataTable dtCsv = new DataTable();
                using (StreamReader sr = new StreamReader(savePath, System.Text.Encoding.Default))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dtCsv.Columns.Add(header.Trim('"'));
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] fields = ParseCsvLine(line);

                        if (fields.Length != headers.Length) continue;

                        DataRow dr = dtCsv.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = fields[i].Trim('"');
                        }
                        dtCsv.Rows.Add(dr);
                    }
                }

                // 创建失败记录表
                DataTable failedRecords = new DataTable();
                failedRecords.Columns.Add("name");
                failedRecords.Columns.Add("code");
                failedRecords.Columns.Add("specific");
                failedRecords.Columns.Add("unit");
                failedRecords.Columns.Add("energyfactor");
                failedRecords.Columns.Add("machinefactor");
                failedRecords.Columns.Add("standardid");
                failedRecords.Columns.Add("energytype"); // 添加能源类型列
                failedRecords.Columns.Add("Reason");

                int successCount = 0;
                int failCount = 0;

                // 遍历CSV数据行进行处理
                foreach (DataRow row in dtCsv.Rows)
                {
                    try
                    {
                        string name = row["name"].ToString();
                        string code = row["code"].ToString();

                        // 检查name和code是否为空（这两个字段是必填的）
                        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                        {
                            DataRow failedRow = failedRecords.NewRow();
                            failedRow["name"] = name;
                            failedRow["code"] = code;
                            failedRow["specific"] = row["specific"];
                            failedRow["unit"] = row["unit"];
                            failedRow["energyfactor"] = row["energyfactor"];
                            failedRow["machinefactor"] = row["machinefactor"];
                            failedRow["standardid"] = row["standardid"];
                            failedRow["energytype"] = row["energytype"]; // 添加能源类型
                            failedRow["Reason"] = "名称或代码不能为空";
                            failedRecords.Rows.Add(failedRow);
                            failCount++;
                            continue;
                        }

                        // 检查code是否已存在
                        if (factor2BLL.GetModelByCode(code) != null)
                        {
                            DataRow failedRow = failedRecords.NewRow();
                            failedRow["name"] = name;
                            failedRow["code"] = code;
                            failedRow["specific"] = row["specific"];
                            failedRow["unit"] = row["unit"];
                            failedRow["energyfactor"] = row["energyfactor"];
                            failedRow["machinefactor"] = row["machinefactor"];
                            failedRow["standardid"] = row["standardid"];
                            failedRow["energytype"] = row["energytype"]; // 添加能源类型
                            failedRow["Reason"] = "Code已存在";
                            failedRecords.Rows.Add(failedRow);
                            failCount++;
                            continue;
                        }

                        // 创建新记录并赋值
                        CEMM.Model.machineCEFactor2 model = new CEMM.Model.machineCEFactor2();
                        model.name = name;
                        model.code = code;

                        // specific - 可为空
                        model.specific = row["specific"].ToString();

                        // unit - 可为空
                        model.unit = row["unit"].ToString();

                        // energyfactor - 可为空，使用安全转换
                        string energyStr = row["energyfactor"].ToString();
                        if (string.IsNullOrEmpty(energyStr))
                        {
                            model.energyfactor = null; // 设置为null而不是0
                        }
                        else
                        {
                            decimal energyValue;
                            if (decimal.TryParse(energyStr, out energyValue))
                            {
                                model.energyfactor = energyValue;
                            }
                            else
                            {
                                model.energyfactor = null;
                            }
                        }

                        // machinefactor - 可为空，使用安全转换
                        string machineStr = row["machinefactor"].ToString();
                        if (string.IsNullOrEmpty(machineStr))
                        {
                            model.machinefactor = null; // 设置为null而不是0
                        }
                        else
                        {
                            decimal machineValue;
                            if (decimal.TryParse(machineStr, out machineValue))
                            {
                                model.machinefactor = machineValue;
                            }
                            else
                            {
                                model.machinefactor = null;
                            }
                        }

                        // standardid - 可为空
                        model.standardid = row["standardid"].ToString();

                        // energytype - 可为空，使用安全转换
                        string energyTypeStr = row["energytype"].ToString();
                        if (string.IsNullOrEmpty(energyTypeStr))
                        {
                            model.energytype = null; // 设置为null
                        }
                        else
                        {
                            int energyTypeValue;
                            if (int.TryParse(energyTypeStr, out energyTypeValue))
                            {
                                model.energytype = energyTypeValue;
                            }
                            else
                            {
                                model.energytype = null;
                            }
                        }

                        if (factor2BLL.AddWithoutId(model))// 添加记录
                        {
                            successCount++;
                        }
                        else
                        {
                            DataRow failedRow = failedRecords.NewRow();
                            failedRow["name"] = name;
                            failedRow["code"] = code;
                            failedRow["specific"] = row["specific"];
                            failedRow["unit"] = row["unit"];
                            failedRow["energyfactor"] = row["energyfactor"];
                            failedRow["machinefactor"] = row["machinefactor"];
                            failedRow["standardid"] = row["standardid"];
                            failedRow["energytype"] = row["energytype"]; // 添加能源类型
                            failedRow["Reason"] = "添加失败";
                            failedRecords.Rows.Add(failedRow);
                            failCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        DataRow failedRow = failedRecords.NewRow();
                        failedRow["name"] = row["name"];
                        failedRow["code"] = row["code"];
                        failedRow["specific"] = row["specific"];
                        failedRow["unit"] = row["unit"];
                        failedRow["energyfactor"] = row["energyfactor"];
                        failedRow["machinefactor"] = row["machinefactor"];
                        failedRow["standardid"] = row["standardid"];
                        failedRow["energytype"] = row["energytype"]; // 添加能源类型
                        failedRow["Reason"] = "数据格式错误: " + ex.Message;
                        failedRecords.Rows.Add(failedRow);
                        failCount++;
                    }
                }

                Session["FailedRecords"] = failedRecords;
                File.Delete(savePath);

                string message = string.Format("导入完成！成功: {0}条，失败: {1}条。", successCount, failCount);
                if (failCount > 0)
                {
                    message += " 您可以点击\"未导入成功记录下载\"按钮查看失败记录。";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('{0}');", message), true);

                BindGridView();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("alert('导入失败: {0}');", ex.Message.Replace("'", "\\'")), true);
            }
        }

        // 添加辅助方法
        private void AddFailedRecord(DataTable failedRecords, DataRow sourceRow, bool hasEnergyTypeColumn, string reason)
        {
            DataRow failedRow = failedRecords.NewRow();
            failedRow["name"] = SafeGetRowValue(sourceRow, "name");
            failedRow["code"] = SafeGetRowValue(sourceRow, "code");
            failedRow["specific"] = SafeGetRowValue(sourceRow, "specific");
            failedRow["unit"] = SafeGetRowValue(sourceRow, "unit");
            failedRow["energyfactor"] = SafeGetRowValue(sourceRow, "energyfactor");
            failedRow["machinefactor"] = SafeGetRowValue(sourceRow, "machinefactor");
            failedRow["standardid"] = SafeGetRowValue(sourceRow, "standardid");
            failedRow["energytype"] = hasEnergyTypeColumn ? SafeGetRowValue(sourceRow, "energytype") : "";
            failedRow["Reason"] = reason;
            failedRecords.Rows.Add(failedRow);
        }

        private decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            decimal result;
            return decimal.TryParse(value, out result) ? result : (decimal?)null;
        }

        private int? ParseInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            int result;
            return int.TryParse(value, out result) ? result : (int?)null;
        }

        // 添加辅助方法安全获取DataRow值
        private string SafeGetRowValue(DataRow row, string columnName)
        {
            try
            {
                if (row == null) return string.Empty;
                if (!row.Table.Columns.Contains(columnName)) return string.Empty;
                if (row[columnName] == null || row[columnName] == DBNull.Value) return string.Empty;

                return row[columnName].ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        // 解析CSV行（处理字段中包含逗号的情况）
        private string[] ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();// 字段列表
            bool inQuotes = false;// 是否在引号内
            StringBuilder currentField = new StringBuilder();// 当前字段构建器

            for (int i = 0; i < line.Length; i++)// 遍历每个字符
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField.ToString());
                    currentField.Clear();
                    continue;
                }

                currentField.Append(c);
            }

            fields.Add(currentField.ToString()); // 添加最后一个字段
            return fields.ToArray();
        }

        protected void Button4_Click(object sender, EventArgs e)// 导出CSV按钮事件
        {
            DataTable failedRecords = Session["FailedRecords"] as DataTable; // 从Session获取失败记录
            if (failedRecords == null || failedRecords.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('没有导入失败的记录需导出！');", true);
                return;
            }

            try
            {
                // 设置响应头
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=FailedRecords_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "text/csv";

                // 写入UTF-8 BOM头
                Response.BinaryWrite(Encoding.UTF8.GetPreamble());

                StringBuilder csvContent = new StringBuilder();

                // 添加标题行
                csvContent.AppendLine("机械名称,机械代码,规格型号,单位,能源因子,机械因子,标准,能源类型,失败原因");

                // 添加数据行（使用安全的方法访问列）
                foreach (DataRow row in failedRecords.Rows)
                {
                    csvContent.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",{4},{5},\"{6}\",{7},\"{8}\"",
                        SafeGetDataRowValue(row, "name"),
                        SafeGetDataRowValue(row, "code"),
                        SafeGetDataRowValue(row, "specific"),
                        SafeGetDataRowValue(row, "unit"),
                        SafeGetDataRowValue(row, "energyfactor"),
                        SafeGetDataRowValue(row, "machinefactor"),
                        SafeGetDataRowValue(row, "standardid"),
                        SafeGetDataRowValue(row, "energytype"),
                        SafeGetDataRowValue(row, "Reason")));
                }

                // 写入内容
                Response.Write(csvContent.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    string.Format("alert('导出失败: {0}');", ex.Message.Replace("'", "\\'")), true);
            }
        }

        // 添加辅助方法安全获取DataRow值
        private string SafeGetDataRowValue(DataRow row, string columnName)
        {
            try
            {
                if (row.Table.Columns.Contains(columnName) && row[columnName] != null)
                {
                    return row[columnName].ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置响应头
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=模板样例.csv");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.ContentType = "text/csv";

                // 写入UTF-8 BOM头（确保中文显示正确）
                Response.BinaryWrite(Encoding.UTF8.GetPreamble());

                // 生成CSV模板内容 - 根据你提供的示例格式
                StringBuilder csvContent = new StringBuilder();

                // 添加标题行（包含mfid字段）
                csvContent.AppendLine("mfid,name,code,specific,unit,energyfactor,machinefactor,standardid,energytype");

                // 添加示例数据行（按照你提供的格式）
                csvContent.AppendLine(",功率 60kW  以内履带式推土机,8001001,T80（可以为空值）,台班（可以为空值）,3.096（可以为空值）,126.5（可以为空值）,s2025002（可以为空值）,1（1:人工和机械工，2:重油、汽油、柴油，3:煤，4:使用燃料的机械设备及车辆，5:电，6:使用电力的机械设备及车辆，7:材料)");

                // 写入内容
                Response.Write(csvContent.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('下载失败: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        // 关键修改：简化BindGridView方法，移除ViewState缓存
        private void BindGridView()
        {
            DataSet ds;

            if (!string.IsNullOrEmpty(CurrentSearchKeyword))
            {
                // 使用包含energytype的查询方法
                ds = factor2BLL.GetListWithEnergyType("(name like '%" + CurrentSearchKeyword + "%' OR code like '%" + CurrentSearchKeyword + "%')");
            }
            else
            {
                // 获取所有数据
                ds = factor2BLL.GetListWithEnergyType("");
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvProjects1.DataSource = ds.Tables[0];
                gvProjects1.DataBind();
            }
            else
            {
                gvProjects1.DataSource = null;
                gvProjects1.DataBind();
            }
        }

        private string CurrentSearchKeyword // 当前搜索关键字属性
        {
            get { return ViewState["CurrentSearchKeyword"] as string; }// 获取
            set { ViewState["CurrentSearchKeyword"] = value; }// 设置
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // 使用Response.Redirect进行服务器端重定向
            Response.Redirect("~/sgf/sqlserver.aspx");
        }
    }
}