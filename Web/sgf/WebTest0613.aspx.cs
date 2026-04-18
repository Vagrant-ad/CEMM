using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Text;
using System.Threading;
using OfficeOpenXml; // EPPlus库的主命名空间

namespace CEMM.Web.sgf
{
    public partial class WebTest0613 : System.Web.UI.Page
    {
        static System.Data.DataTable dtResult;
        static string fileName;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导入计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            // 获取输入的千米数2025.9.2修改
            double kmValue;
            if (!double.TryParse(txtKm.Text.Trim(), out kmValue) || kmValue <= 0.001)
            {
                MessageBox.Show(this, "请输入有效的施工千米数（大于0）");
                lblExportProgress.Text = "暂无计算/导出";
                return;
            }

            //string filepath = "D:\\data\\20250612表格zjN.xlsx";
            fileName = FileUpload1.PostedFile.FileName;
            HttpPostedFile mypost = Request.Files[0];
            string savepath = Server.MapPath(@"./UpFile/" + System.IO.Path.GetFileName(mypost.FileName));
            if (fileName == "")
            {
                MessageBox.Show(this, "请选择excel文件");
                lblExportProgress.Text = "暂无计算/导出";
            }
            else
            {
                //取得文件扩展名 
                string strExt = Path.GetExtension(fileName);
                if (strExt == ".xlsx" || strExt == ".xls")
                {
                    mypost.SaveAs(savepath);
                    string strConn = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + savepath + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'";
                    OleDbConnection Oleconn = new OleDbConnection(strConn);
                    string strExcel = "";
                    OleDbDataAdapter excelCommand = null;
                    DataSet excel_ds = new DataSet();
                    strExcel = "select * from [Sheet1$]";
                    try
                    {
                        Oleconn.Open();
                        excelCommand = new OleDbDataAdapter(strExcel, Oleconn);
                        excelCommand.Fill(excel_ds, "[Sheet1$]");//得到dataset
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show(this, "访问xls文件发生错误!");
                    }

                    finally
                    {
                        Oleconn.Close();
                        Oleconn.Dispose();
                        File.Delete(savepath);//删除文件
                    }
                    //if (excel_ds == null || excel_ds.Tables.Count == 0)
                    //   {
                    //       MessageBox.Show(this, "未读取到任何数据表");
                    //       return;
                    //   }

                    //   DataTable dtSheet = excel_ds.Tables[0];

                    //   if (dtSheet.Rows.Count == 0)
                    //   {
                    //       MessageBox.Show(this, "数据表为空");
                    //       return;
                    //   }


                    //MessageBox.Show(this, excel_ds.Tables[0].Rows.Count.ToString());

                    int colN = excel_ds.Tables[0].Columns.Count;
                    int colN2 = (colN - 4) * 2 + 4 + 1;//多一列排放因子

                    double[] totalEmissions = new double[colN2]; // 索引E-N
                    double[] rowEmissions = new double[colN2];
                    // 创建临时DataTable存储结果（添加碳排放列）
                    //DataTable dtResult = excel_ds.Tables[0].Copy();

                    //string[] columnNames = {
                    //    "总数量碳排放", "临时工程碳排放", "路基工程碳排放", "路面工程碳排放",
                    //    "桥梁涵洞工程碳排放", "隧道工程碳排放", "交叉工程碳排放", 
                    //    "交通工程及沿线设施碳排放", "绿化及环境保护工程碳排放", "其他工程碳排放"
                    //};

                    //foreach (var colName in columnNames)
                    //{
                    //    dtResult.Columns.Add(colName, typeof(double));
                    //}
                    // 创建BLL实例
                    CEMM.BLL.machineCEFactor2 machFactor2Bll = new CEMM.BLL.machineCEFactor2();
                    dtResult = new System.Data.DataTable();
                    DataRow newRow;

                    StringBuilder error = new StringBuilder("以下代码的碳排放因子不存在：");
                    string code;
                    double emissionFactor = 0;
                    double emission;
                    int i;
                    CEMM.Model.machineCEFactor2 machFact2Mdl = new Model.machineCEFactor2();

                    //项表中增加列，前4列
                    for (i = 0; i <= 3; i++)
                    {
                        dtResult.Columns.Add(excel_ds.Tables[0].Columns[i].ToString(), typeof(string));
                    }
                    dtResult.Columns.Add("排放因子", typeof(double));
                    //后面的列
                    for (i = 4; i < colN; i++)
                    {
                        dtResult.Columns.Add(excel_ds.Tables[0].Columns[i].ToString(), typeof(double));
                        dtResult.Columns.Add(excel_ds.Tables[0].Columns[i].ToString() + "碳排放", typeof(double));
                    }

                    double excelQty = 0;
                    int colIndex;
                    //int n;

                    foreach (DataRow dr in excel_ds.Tables[0].Rows)
                    {
                        // 跳过空行或表头行
                        if (dr[0] == DBNull.Value || string.IsNullOrEmpty(dr[0].ToString()))
                            continue;

                        code = dr[0].ToString().Trim(); // 获取代号


                        machFact2Mdl = machFactor2Bll.GetModelByCode(code);
                        if (machFact2Mdl != null && machFact2Mdl.machinefactor != null)
                        {
                            emissionFactor = (double)machFact2Mdl.machinefactor;
                        }
                        else
                        {
                            error.Append(dr[0].ToString() + "-" + dr[1].ToString() + "；");//dr[0]是代码，dr[1]是名称
                            continue;
                        }

                        //n = 0;
                        newRow = dtResult.NewRow();
                        for (i = 0; i < 4; i++)
                        {
                            //newRow[i] = "";
                            newRow[i] = dr[i].ToString();
                        }
                        newRow[i] = emissionFactor;
                        for (i = 5; i < colN2; i++) //加了一列排放因子，所以从i = 5开始
                        {
                            rowEmissions[i] = 0;
                        }
                        //  计算总碳排放
                        for (i = 4; i < colN; i++)
                        {
                            emission = 0;
                            excelQty = 0;
                            colIndex = i;
                            if (dr[colIndex] != DBNull.Value && !string.IsNullOrWhiteSpace(dr[colIndex].ToString()))
                            {
                                // 特殊处理带等号的公式单元格（如SUM公式）
                                string excelColvalue = dr[colIndex].ToString();
                                if (excelColvalue.StartsWith("="))
                                {

                                    excelQty = 0;
                                }
                                else if (double.TryParse(excelColvalue, out excelQty))
                                {
                                    emission = emissionFactor * excelQty;
                                    //n++;
                                    rowEmissions[i * 2 - 3 + 1] = emission; //加了一列排放因子，所以+1
                                    totalEmissions[i * 2 - 3 + 1] += emission; // 累计该列碳排放总量
                                }
                            }
                            newRow[i * 2 - 4 + 1] = excelQty; //记录数量，加了一列排放因子，所以+1
                        }
                        for (i = 6; i < colN2; i += 2) //加一列排放因子之前是i=5
                        {
                            newRow[i] = Math.Round(rowEmissions[i], 3);
                        }
                        dtResult.Rows.Add(newRow);

                    } //foreach end

                    //  创建新行,保存最后的碳排放结果
                    newRow = dtResult.NewRow();
                    newRow[1] = "碳排放总计";
                    newRow[2] = "kg";
                    //先清零
                    for (i = 6; i < colN2; i += 2) //没加一列排放因子之前是i=5
                    {
                        newRow[i] = (double)0;
                    }
                    for (i = 6; i < colN2; i += 2) //没加一列排放因子之前是i=5
                    {
                        newRow[i] = Math.Round(totalEmissions[i], 3);
                    }
                    dtResult.Rows.Add(newRow);

                    //2025.9.19
                    // 添加新行，显示每km二氧化碳排放量
                    newRow = dtResult.NewRow();
                    newRow[1] = "每公里万吨CO2排放量";
                    newRow[2] = "万吨CO2/km";
                    for (i = 6; i < colN2; i += 2) //只处理碳排放列
                    {
                        if (i < dtResult.Columns.Count && totalEmissions[i] != 0)
                        {
                            // 计算每km排放量：总碳排放量 / km数 / 10000000，万吨
                            double emissionPerKm = totalEmissions[i] / kmValue / 10000000;
                            newRow[i] = Math.Round(emissionPerKm, 6); // 保留6位小数
                        }
                        else
                        {
                            newRow[i] = 0;
                        }
                    }
                    dtResult.Rows.Add(newRow);


                    //写数据库
                    CEMM.Model.computeResultTabInfo resultTabInfoMdl = new Model.computeResultTabInfo();
                    CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
                    CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
                    CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();

                    if (resultTabInfoBll.GetModelByName(fileName) == null)  //不存在写入,已存在不写入
                    {
                        //表的信息加入
                        resultTabInfoMdl.tableName = fileName;
                        resultTabInfoMdl.inputTime = Convert.ToDateTime(DateTime.Now);
                        resultTabInfoBll.Add(resultTabInfoMdl);

                        //添加表的内容
                        int rows = dtResult.Rows.Count;
                        //int cols = 27;//dtResult.Columns.Count;最后两列不要了
                        int tableId;
                        resultTabInfoMdl = resultTabInfoBll.GetModelByName(fileName);
                        tableId = resultTabInfoMdl.tableID;

                        try
                        {
                            //相对于V1版，多增加"万吨CO2/km"，最后两行是统计结果，最后一行"万吨CO2/km"不写入数据库
                            for (i = 0; i <= rows - 2; i++)
                            {
                                if (i < rows - 2)
                                {
                                    resultInfoMdl.code = dtResult.Rows[i][0].ToString();
                                    resultInfoMdl.formName = dtResult.Rows[i][1].ToString();
                                    resultInfoMdl.unit = dtResult.Rows[i][2].ToString();
                                    resultInfoMdl.price = Convert.ToDecimal(dtResult.Rows[i][3]);
                                    resultInfoMdl.emissionfactor = Convert.ToDecimal(dtResult.Rows[i][4]);

                                    resultInfoMdl.total_quantity = Convert.ToDecimal(dtResult.Rows[i][5]);
                                    resultInfoMdl.total_emission = Convert.ToDecimal(dtResult.Rows[i][6]);
                                    resultInfoMdl.temp_project = Convert.ToDecimal(dtResult.Rows[i][7]);
                                    resultInfoMdl.temp_emission = Convert.ToDecimal(dtResult.Rows[i][8]);
                                    resultInfoMdl.subgrade_project = Convert.ToDecimal(dtResult.Rows[i][9]);
                                    resultInfoMdl.subgrade_emission = Convert.ToDecimal(dtResult.Rows[i][10]);
                                    resultInfoMdl.pavement_project = Convert.ToDecimal(dtResult.Rows[i][11]);
                                    resultInfoMdl.pavement_emission = Convert.ToDecimal(dtResult.Rows[i][12]);

                                    resultInfoMdl.bridge_project = Convert.ToDecimal(dtResult.Rows[i][13]);
                                    resultInfoMdl.bridge_emission = Convert.ToDecimal(dtResult.Rows[i][14]);
                                    resultInfoMdl.tunnel_project = Convert.ToDecimal(dtResult.Rows[i][15]);
                                    resultInfoMdl.tunnel_emission = Convert.ToDecimal(dtResult.Rows[i][16]);
                                    resultInfoMdl.crossing_project = Convert.ToDecimal(dtResult.Rows[i][17]);
                                    resultInfoMdl.crossing_emission = Convert.ToDecimal(dtResult.Rows[i][18]);
                                    resultInfoMdl.traffic_project = Convert.ToDecimal(dtResult.Rows[i][19]);
                                    resultInfoMdl.traffic_emission = Convert.ToDecimal(dtResult.Rows[i][20]);

                                    resultInfoMdl.greening_project = Convert.ToDecimal(dtResult.Rows[i][21]);
                                    resultInfoMdl.greening_emission = Convert.ToDecimal(dtResult.Rows[i][22]);
                                    resultInfoMdl.other_project = Convert.ToDecimal(dtResult.Rows[i][23]);
                                    resultInfoMdl.other_emission = Convert.ToDecimal(dtResult.Rows[i][24]);
                                    resultInfoMdl.assistant_project = Convert.ToDecimal(dtResult.Rows[i][25]);
                                    resultInfoMdl.assistant_emission = Convert.ToDecimal(dtResult.Rows[i][26]);
                                    resultInfoMdl.tableID = tableId;
                                }
                                else
                                {
                                    resultInfoMdl.code = "tpfzj";
                                    resultInfoMdl.formName = dtResult.Rows[i][1].ToString();
                                    resultInfoMdl.unit = "";
                                    resultInfoMdl.price = 0;
                                    resultInfoMdl.emissionfactor = 0;

                                    resultInfoMdl.total_quantity = 0;
                                    resultInfoMdl.total_emission = Convert.ToDecimal(dtResult.Rows[i][6]);
                                    resultInfoMdl.temp_project = 0;
                                    resultInfoMdl.temp_emission = Convert.ToDecimal(dtResult.Rows[i][8]);
                                    resultInfoMdl.subgrade_project = 0;
                                    resultInfoMdl.subgrade_emission = Convert.ToDecimal(dtResult.Rows[i][10]);
                                    resultInfoMdl.pavement_project = 0;
                                    resultInfoMdl.pavement_emission = Convert.ToDecimal(dtResult.Rows[i][12]);

                                    resultInfoMdl.bridge_project = 0;
                                    resultInfoMdl.bridge_emission = Convert.ToDecimal(dtResult.Rows[i][14]);
                                    resultInfoMdl.tunnel_project = 0;
                                    resultInfoMdl.tunnel_emission = Convert.ToDecimal(dtResult.Rows[i][16]);
                                    resultInfoMdl.crossing_project = 0;
                                    resultInfoMdl.crossing_emission = Convert.ToDecimal(dtResult.Rows[i][18]);
                                    resultInfoMdl.traffic_project = 0;
                                    resultInfoMdl.traffic_emission = Convert.ToDecimal(dtResult.Rows[i][20]);

                                    resultInfoMdl.greening_project = 0;
                                    resultInfoMdl.greening_emission = Convert.ToDecimal(dtResult.Rows[i][22]);
                                    resultInfoMdl.other_project = 0;
                                    resultInfoMdl.other_emission = Convert.ToDecimal(dtResult.Rows[i][24]);
                                    resultInfoMdl.assistant_project = 0;
                                    resultInfoMdl.assistant_emission = Convert.ToDecimal(dtResult.Rows[i][26]);
                                    resultInfoMdl.tableID = tableId;
                                }

                                resultInfoBll.Add(resultInfoMdl);
                            }
                        }
                        catch (InvalidCastException)
                        {
                            MessageBox.Show(this, "数据类型不兼容，请检查，代号不能为空!");
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show(this, "数据格式不正确，请检查，代号不能为空!");
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show(this, "数值超出了目标类型的范围，请检查!");
                        }
                        catch
                        {
                            MessageBox.Show(this, "数据存在错误，请检查，代号不能为空!");
                        }

                    }

                    // 绑定到GridView
                    GridView1.DataSource = dtResult;
                    GridView1.DataBind();
                    Label1.Text = error.ToString();
                    //MessageBox.Show(this, error.ToString()"); 
                    lblExportProgress.Text = fileName + "计算完成";
                }
                else
                {
                    MessageBox.Show(this, "选择的文件不是excel文件，无法导入!");
                    lblExportProgress.Text = "暂无计算/导出";
                }
            }
        }
        /// <summary>
        /// 导出计算结果，存储格式为Excel格式并提供下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (fileName == null || fileName == "" || dtResult == null || dtResult.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('暂无可导出的计算结果！');", true);
                return;
            }
            string str1 = Path.GetFileNameWithoutExtension(fileName);
            string str2 = ".xlsx";
            lblExportProgress.Text = "开始导出，请等待";
            try
            {
                // 创建Excel包
                using (var excelPackage = new ExcelPackage())
                {
                    string tempFileName = str1 + "-计算结果" + str2;
                    // 添加工作表
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("计算结果");

                    // 加载数据
                    worksheet.Cells["A1"].LoadFromDataTable(dtResult, true);

                    // 自动调整列宽
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // 设置文件名
                    string downloadFileName = tempFileName;  //$"{Path.GetFileNameWithoutExtension(fileName)}-计算结果.xlsx";

                    // 准备响应
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(downloadFileName, Encoding.UTF8) + "\"");

                    // 写入响应流
                    Response.BinaryWrite(excelPackage.GetAsByteArray());
                    Response.Flush();
                }
            }
            catch (ThreadAbortException)
            {
                // 忽略响应终止异常
            }
            catch (Exception ex)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "error", $"alert('导出失败: {HttpUtility.JavaScriptStringEncode(ex.Message)}');", true);
                MessageBox.Show(this, "导出失败:" + ex.Message);
            }
            finally
            {
                lblExportProgress.Text = fileName + "导出完成！";
                // 确保结束响应
                try
                {
                    Response.End();
                }
                catch (ThreadAbortException) { }
            }
            //以下是原来的
            /*if (fileName == null || fileName == "" || dtResult == null || dtResult.Rows.Count == 0)
                MessageBox.Show(this, "暂无可导出的计算结果！");
            else
            {
                string str1 = Path.GetFileNameWithoutExtension(fileName); 
                string str2 = Path.GetExtension(fileName);
                //string outFile = "C:\\" + str1 + "-计算结果" + str2;
                string outFile = Server.MapPath(@"./UpFile/" + str1 + "-计算结果" + str2);  //2025.07.26后修改的
                if (File.Exists(outFile))
                    MessageBox.Show(this, "该文件已存在！");
                else
                {
                    lblExportProgress.Text = "开始导出，请等待";
                    WriteResultExcel(outFile);
                    lblExportProgress.Text = fileName + "导出完成！";
                    MessageBox.Show(this, "Excel文件导出完成！");
                }
                Response.Redirect(outFile);  //2025.07.26后修改的，不对
                File.Delete(outFile);  //2025.07.26后修改的，不对
            }*/
        }
        /// <summary>
        /// 导出计算结果，存储格式为CSV格式并提供下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (fileName == null || fileName == "" || dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show(this, "暂无可导出的计算结果！");
                return;
            }
            string str1 = Path.GetFileNameWithoutExtension(fileName);
            string str2 = ".csv";
            string tempFilePath = null;
            lblExportProgress.Text = "开始导出，请等待";
            try
            {
                // 生成临时文件名（使用GUID避免冲突）
                //string tempFileName = Guid.NewGuid().ToString() + ".csv";
                string tempFileName = str1 + "-计算结果" + str2;
                tempFilePath = Server.MapPath(Path.Combine("./UpFile/", tempFileName));

                // 确保目录存在
                //Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));

                // 写入CSV文件
                WriteResultCSV(tempFilePath);

                // 设置响应头触发下载
                string downloadFileName = tempFileName; //$"{Path.GetFileNameWithoutExtension(fileName)}-计算结果.csv";
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(downloadFileName, Encoding.UTF8) + "\"");
                Response.TransmitFile(tempFilePath);
                Response.Flush();
            }
            catch (ThreadAbortException)
            {
                // 忽略响应终止异常（正常下载流程）
            }
            catch (Exception ex)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "error", $"alert('导出失败: {ex.Message.Replace("'", "\\'")}');", true);
                MessageBox.Show(this, "CSV导出失败:"+ex.Message);
            }
            finally
            {
                lblExportProgress.Text = fileName + "导出完成！";
                // 确保删除临时文件
                if (tempFilePath != null && File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch
                    {
                        // 记录日志即可
                    }
                }

                // 结束响应防止继续渲染页面
                try
                {
                    Response.End();
                }
                catch (ThreadAbortException) { }
            }

            //以下是原来的
            //string outFile = "C:\\" + str1 + "-计算结果" + str2;
            //if (File.Exists(outFile))
            //    MessageBox.Show(this, "该文件已存在！");
            //else
            //{
            //    lblExportProgress.Text = "开始导出，请等待";
            //    WriteResultCSV(outFile);
            //    lblExportProgress.Text = fileName + "导出完成！";
            //    MessageBox.Show(this, "CSV文件导出完成！");

            //}
        }
        /// <summary>
        /// 计算结果写入Excel表
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteResultExcel(string fileName)
        {
            int rows = dtResult.Rows.Count;
            int cols = dtResult.Columns.Count;
            int i, j;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Application.Workbooks.Add(true);
            try
            {

                //添加列
                for (j = 1; j <= cols; j++)
                {
                    excel.Cells[1, j] = dtResult.Columns[j - 1].ToString();
                }
                //添加行
                for (i = 2; i <= rows + 1; i++)
                    for (j = 1; j <= cols; j++)
                    {
                        excel.Cells[i, j] = dtResult.Rows[i - 2][j - 1].ToString();
                    }


                //设置禁止弹出保存和覆盖的询问提示框   
                excel.DisplayAlerts = false;
                excel.AlertBeforeOverwriting = false;
            }
            catch
            {
                MessageBox.Show(this, "存储路径错误或当前系统配置不支持该功能！");
            }
            finally
            {
                wb.Close(true, fileName, null);
                excel.Quit();
                excel = null;
                GC.Collect();//垃圾回收
            }
            return;
        }

        /// <summary>
        /// 计算结果写入csv表
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteResultCSV(string fileName)
        {
            int rows = dtResult.Rows.Count;
            int cols = dtResult.Columns.Count;
            int i, j;
            //文件流写入
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true, Encoding.GetEncoding("UTF-8"));//第二个参数为true，表示以追加的方式打开文件并写入数据
            //StreamWriter sw = File.AppendText("fileName");//或者使用File.AppendText方法：
            try
            {
                string dcStr = "";
                //添加列
                for (j = 0; j < cols; j++)
                {
                    dcStr += dtResult.Columns[j].ToString() + ",";
                }
                //将表头写入文件第一行
                sw.WriteLine(dcStr);
                dcStr = "";

                string drStr = "";
                //将每行数据写入文件
                for (i = 0; i < rows; i++)
                {
                    for (j = 0; j < cols; j++)
                    {
                        if (dtResult.Rows[i][j].ToString().Contains(","))
                            drStr += dtResult.Rows[i][j].ToString().Replace(',', '，') + ","; //英文逗号替换为中文逗号
                        else
                            drStr += dtResult.Rows[i][j].ToString() + ",";
                    }
                    sw.WriteLine(drStr);
                    drStr = "";
                }
                drStr = "";
            }
            catch
            {
                MessageBox.Show(this, "存储路径错误或当前系统配置不支持该功能！");
            }
            finally
            {
                //输出缓冲区内的数据
                sw.Flush();
                //释放流
                sw.Close();
                GC.Collect();//垃圾回收
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
        

    }
}