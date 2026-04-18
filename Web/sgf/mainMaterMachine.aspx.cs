using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CEMM.Web.sgf
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
        CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();

        int width = 1400, height = 1000;//绘图的区域大小

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTableDropdown(""); // 初始加载所有数据
            }
        }

        // 绑定数据表下拉框
        private void BindTableDropdown(string filterText)
        {
            DataSet ds;
            if (string.IsNullOrEmpty(filterText))
            {
                ds = resultTabInfoBll.GetTopN(60, "", "tableID");
            }
            else
            {
                string safeFilterText = filterText.Replace("'", "''");
                string whereClause = "tableName LIKE '%" + safeFilterText + "%'";
                ds = resultTabInfoBll.GetList(whereClause);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('未找到包含\"" + filterText + "\"的数据表！');", true);
                }
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
            {
                ddlTable.DataSource = ds;
                ddlTable.DataTextField = "tableName";
                ddlTable.DataValueField = "tableID";
                ddlTable.DataBind();
            }
            else
            {
                ddlTable.Items.Clear();
                ddlTable.Items.Add(new ListItem("未找到匹配的数据表", "0"));
            }
            ddlTable.Items.Insert(0, new ListItem("请选择数据表", "0"));
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filterText = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(filterText))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请输入筛选关键词！');", true);
                return;
            }

            string safeFilterText = filterText.Replace("'", "''");
            string whereClause = "tableName LIKE '%" + safeFilterText + "%'";
            DataSet ds = resultTabInfoBll.GetList(whereClause);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('未找到包含\"" + filterText + "\"的数据表！');", true);
                ddlTable.Items.Clear();
                ddlTable.Items.Add(new ListItem("未找到匹配的数据表", "0"));
            }
            else
            {
                ddlTable.DataSource = ds;
                ddlTable.DataTextField = "tableName";
                ddlTable.DataValueField = "tableID";
                ddlTable.DataBind();
            }

            ddlTable.Items.Insert(0, new ListItem("请选择数据表", "0"));
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindTableDropdown("");
            MessageBox.Show(this, "筛选条件已清空！");
        }

        protected void btnPie_Click(object sender, EventArgs e)
        {
            CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
            int selectTableid;
            DataTable materialTable = new DataTable();
            DataTable machineTable = new DataTable();
            int r;
            double totalEmission = 0;
            //double totalUsage = 0; // 修改：使用总使用量
            double materialTotalUse = 0; //材料总使用量，2025.09.20
            double machineTotalUse = 0; //机械总使用量，2025.09.20

            if (int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
            }
            else
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }

            // 获取正确的总使用量（所有记录的总和）
            //totalUsage = resultInfoBll.GetTotalUsageQuantity(selectTableid);
            materialTotalUse = resultInfoBll.GetTotalMaterialQuantity(selectTableid); //2025.09.20
            machineTotalUse = resultInfoBll.GetTotalMachineQuantity(selectTableid); //2025.09.20
            //MessageBox.Show(this,totalUsage.ToString() + ", " + materialTotalUse.ToString() + ", " + machineTotalUse.ToString()); //test use

            // 定义数据
            string[] materialNames = { "材料1", "材料2", "材料3", "材料4", "材料5", "材料6", "材料7", "材料8", "材料9", "材料10" };
            double[] materialEmissions = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] materialQuantities = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] materialUnit = { "kg", "kg", "kg", "kg", "kg", "kg", "kg", "kg", "kg", "kg" };

            string[] machineNames = { "机械1", "机械2", "机械3", "机械4", "机械5", "机械6", "机械7", "机械8", "机械9", "机械10" };
            double[] machineEmissions = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] machineQuantities = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] machineUnit = { "台班", "台班", "台班", "台班", "台班", "台班", "台班", "台班", "台班", "台班" };

            int namelen = 0;
            try
            {
                totalEmission = Convert.ToDouble(resultInfoMdl.total_emission);

                // 获取材料数据
                materialTable = resultInfoBll.GetTopN(10, "tableID=" + selectTableid.ToString() + " and (code between '1509001' and '7701030')", "total_emission").Tables[0];

                // 获取机械数据
                machineTable = resultInfoBll.GetTopN(10, "tableID=" + selectTableid.ToString() + " and code like '8%'", "total_emission").Tables[0];

                // 处理材料数据
                for (r = 0; r < materialTable.Rows.Count; r++)
                {
                    //if (materialTable.Rows[r][2].ToString().Length > 22)
                    //    materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, 11) + "\n" + materialTable.Rows[r][2].ToString().Substring(11, 11);
                    //else if (materialTable.Rows[r][2].ToString().Length > 18)
                    //    materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, 9) + "\n" + materialTable.Rows[r][2].ToString().Substring(9, 9);
                    //else if (materialTable.Rows[r][2].ToString().Length > 14)
                    //    materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, 7) + "\n" + materialTable.Rows[r][2].ToString().Substring(7, 7);
                    //else
                    //    materialNames[r] = materialTable.Rows[r][2].ToString();

                    namelen = materialTable.Rows[r][2].ToString().Length; //2025.09.20修改
                    if (namelen > 22)
                        materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, 11) + "\n" + materialTable.Rows[r][2].ToString().Substring(11, 11);
                    else if (namelen >= 12)
                    {
                        if (namelen % 2 == 0)
                            materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, namelen / 2) + "\n" + materialTable.Rows[r][2].ToString().Substring(namelen / 2, namelen / 2);
                        else
                            materialNames[r] = materialTable.Rows[r][2].ToString().Substring(0, namelen / 2) + "\n" + materialTable.Rows[r][2].ToString().Substring(namelen / 2, namelen / 2 + 1);
                    }
                    else
                        materialNames[r] = materialTable.Rows[r][2].ToString();


                    materialEmissions[r] = Convert.ToDouble(materialTable.Rows[r][7].ToString());
                    materialQuantities[r] = Convert.ToDouble(materialTable.Rows[r][6].ToString());
                    materialUnit[r] = materialTable.Rows[r][3].ToString();
                }

                // 处理机械数据
                for (r = 0; r < machineTable.Rows.Count; r++)
                {
                    //if (machineTable.Rows[r][2].ToString().Length > 22)
                    //    machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, 11) + "\n" + machineTable.Rows[r][2].ToString().Substring(11, 11);
                    //else if (machineTable.Rows[r][2].ToString().Length > 18)
                    //    machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, 9) + "\n" + machineTable.Rows[r][2].ToString().Substring(9, 9);
                    //else if (machineTable.Rows[r][2].ToString().Length > 14)
                    //    machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, 7) + "\n" + machineTable.Rows[r][2].ToString().Substring(7, 7);
                    //else
                    //    machineNames[r] = machineTable.Rows[r][2].ToString();

                    namelen = machineTable.Rows[r][2].ToString().Length; //2025.09.20修改
                    if (namelen > 22)
                        machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, 11) + "\n" + machineTable.Rows[r][2].ToString().Substring(11, 11);
                    else if (namelen >= 12)
                    {
                        if (namelen % 2 == 0)
                            machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, namelen / 2) + "\n" + machineTable.Rows[r][2].ToString().Substring(namelen / 2, namelen / 2);
                        else
                            machineNames[r] = machineTable.Rows[r][2].ToString().Substring(0, namelen / 2) + "\n" + machineTable.Rows[r][2].ToString().Substring(namelen / 2, namelen / 2 + 1);
                    }
                    else
                        machineNames[r] = machineTable.Rows[r][2].ToString();

                    machineEmissions[r] = Convert.ToDouble(machineTable.Rows[r][7].ToString());
                    machineQuantities[r] = Convert.ToDouble(machineTable.Rows[r][6].ToString());
                    machineUnit[r] = machineTable.Rows[r][3].ToString();
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show(this, "数据类型不兼容，请检查!");
            }
            catch (FormatException)
            {
                MessageBox.Show(this, "数据格式不正确，请检查!");
            }
            catch (OverflowException)
            {
                MessageBox.Show(this, "数值超出了目标类型的范围，请检查!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            // 生成图表图像 - 传入正确的总使用量
            Bitmap chartImage = DrawBarCharts(materialNames, materialEmissions, materialQuantities,
                                machineNames, machineEmissions, machineQuantities,
                                totalEmission, materialTotalUse, machineTotalUse, materialUnit, machineUnit);

            // 保存图像到内存流
            MemoryStream ms = new MemoryStream();
            chartImage.Save(ms, ImageFormat.Png);
            byte[] buffer = ms.ToArray();
            ms.Close();

            // 显示图像
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
        }

        private Bitmap DrawBarCharts(string[] materialNames, double[] materialEmissions, double[] materialQuantities,
   string[] machineNames, double[] machineEmissions, double[] machineQuantities,
   double totalEmission, double materialTotalUse, double machineTotalUse, string[] materialUnit, string[] machineUnit)
        {


            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // 设置字体
                Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);
                Font labelFont = new Font("Microsoft YaHei", 10);
                Font bottomlblFont = new Font("Microsoft YaHei", 10);
                Font smallBottomFont = new Font("Microsoft YaHei", 10);

                Color[] colors = {
                   Color.FromArgb(5, 150, 0),     // 绿色
                   Color.FromArgb(0, 140, 255),     // 蓝色
                   Color.FromArgb(235, 47, 142),    // 粉色
                   Color.FromArgb(85, 65, 72),     // 紫褐色
                   Color.FromArgb(189, 165, 173),     // 浅灰
                   Color.FromArgb(164, 122, 0),     //aa，浅卡奇色
                   Color.FromArgb(0, 156, 149),     //bb，蓝绿浅
                   Color.FromArgb(108, 73, 0),     //aa，深卡奇色
                   Color.FromArgb(0, 102, 198),     //bb，蓝绿深
                   Color.FromArgb(113, 0, 46),     //酱紫色
                   Color.FromArgb(175, 227, 255),  //cc,淡蓝色
                   Color.FromArgb(115, 172, 255),  //cc,中蓝色
                
                   Color.FromArgb(255, 255, 0),    // 黄色
                   Color.FromArgb(128, 128, 0),    // 墨绿和黑色之间
                   Color.FromArgb(255, 128, 64),     // 黄和粉之间
                   Color.FromArgb(0, 64, 0),     // 墨绿色
                   Color.FromArgb(128, 128, 128),     // 灰色
                   Color.FromArgb(255, 128, 192),     // 淡粉色
                   Color.FromArgb(255, 128, 0),    // 桔色
                   Color.FromArgb(128, 0, 128),     // 紫色
                };

                int maxHeightSpace = 60;
                int leftReserveSpace = 10; //原来是20，2025.09.20
                int rightReserveSpace = 200;
                int bottomLabelSpace = 25;

                // ========== 绘制材料碳排放柱状图 ==========
                g.DrawString("材料碳排放量前10", titleFont, Brushes.DarkBlue, new PointF(20, 20));

                Rectangle materialArea = new Rectangle(leftReserveSpace, 50, width - rightReserveSpace, height / 2 - 120);

                int barWidth = (materialArea.Width - 100) / materialNames.Length - 20;
                int maxMaterialHeight = materialArea.Height - maxHeightSpace;

                // 计算碳排放的最大高度用于缩放
                double maxMaterialEmission = 0;
                foreach (double q in materialEmissions) if (q > maxMaterialEmission) maxMaterialEmission = q;

                // 计算使用量的最大高度用于缩放（使用总使用量作为基准）
                double maxMaterialQuantity = 0;
                foreach (double q in materialQuantities) if (q > maxMaterialQuantity) maxMaterialQuantity = q;

                // 绘制柱形和标签
                for (int i = 0; i < materialNames.Length; i++)
                {
                    // 计算碳排放柱子高度
                    float emissionHeight = (float)(materialEmissions[i] / maxMaterialEmission * maxMaterialHeight);

                    // 计算使用量柱子高度
                    //float quantityHeight = (float)(materialQuantities[i] / totalUsage * maxMaterialHeight); //2025.09.20之前的
                    float quantityHeight = (float)(materialQuantities[i] / maxMaterialQuantity * maxMaterialHeight); //2025.09.20

                    // 确保小值可见，设置最小高度
                    float minVisibleHeight = 6f; //2025.9.20，原来是8
                    if (quantityHeight < minVisibleHeight && materialQuantities[i] > 0)
                    {
                        quantityHeight = minVisibleHeight;
                    }
                    if (quantityHeight > maxMaterialHeight)
                        quantityHeight = maxMaterialHeight;

                    int x = materialArea.Left + 20 + i * (barWidth + 20);
                    int emissionY = materialArea.Top + maxMaterialHeight - (int)emissionHeight + 20;
                    int quantityY = materialArea.Top + maxMaterialHeight - (int)quantityHeight + 20;

                    // 绘制碳排放量柱形（左侧，实心）
                    g.FillRectangle(new SolidBrush(colors[i % colors.Length]), x, emissionY, barWidth / 2, emissionHeight);
                    g.DrawRectangle(Pens.Black, x, emissionY, barWidth / 2, emissionHeight);

                    // 绘制使用量柱形（右侧，带边框和背景色）
                    Color currentColor = colors[i % colors.Length];
                    Color backgroundColor = Color.FromArgb(100, currentColor);
                    g.FillRectangle(new SolidBrush(backgroundColor), x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //背景

                    HatchBrush hatchBrush = new HatchBrush(HatchStyle.LargeGrid, currentColor, Color.Transparent);
                    g.FillRectangle(hatchBrush, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //网格线
                    g.DrawRectangle(Pens.Black, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //边框线

                    // 在碳排放柱子顶部绘制百分比标签（占总碳排放的百分比）
                    float emissionPercentage = (float)(materialEmissions[i] / totalEmission);
                    string percentText = String.Format("{0:P2}", emissionPercentage);
                    SizeF percentSize = g.MeasureString(percentText, labelFont);
                    g.DrawString(percentText, labelFont, Brushes.Black, x + barWidth / 4 - percentSize.Width / 2, emissionY - 20);

                    // 在使用量柱子顶部显示占总使用量的百分比（使用总使用量）
                    float quantityPercentage = (float)(materialQuantities[i] / materialTotalUse);
                    string quantityPercentText = String.Format("{0:P2}", quantityPercentage);
                    SizeF quantityPercentSize = g.MeasureString(quantityPercentText, labelFont);
                    g.DrawString(quantityPercentText, labelFont, Brushes.Black, x + barWidth * 3 / 4 - quantityPercentSize.Width / 2 + 5, quantityY - 20);

                    // 在柱子底部显示标签（旋转-45°）
                    string labelText = materialNames[i];
                    Font labelFontToUse = labelText.Length > 15 ? smallBottomFont : bottomlblFont;
                    SizeF labelSize = g.MeasureString(labelText, labelFontToUse);

                    // 计算旋转中心点
                    float centerX = x + barWidth / 2;
                    float centerY = materialArea.Top + maxMaterialHeight + 45 + bottomLabelSpace;

                    // 保存当前图形状态
                    GraphicsState state = g.Save();

                    // 设置旋转变换
                    g.TranslateTransform(centerX, centerY);
                    g.RotateTransform(-45);
                    g.TranslateTransform(-centerX, -centerY);

                    // 绘制旋转后的文本
                    g.DrawString(labelText, labelFontToUse, Brushes.Black, centerX - labelSize.Width / 2 - 10, centerY - labelSize.Height / 2); //2025.09.20，X轴的位置多了-10

                    // 恢复图形状态
                    g.Restore(state);
                }

                // ========== 绘制机械碳排放柱状图 ==========
                g.DrawString("机械碳排放量前10", titleFont, Brushes.DarkBlue, new PointF(20, height / 2));

                Rectangle machineArea = new Rectangle(leftReserveSpace, height / 2 + 30, width - rightReserveSpace, height / 2 - 120);
                int maxMachineHeight = machineArea.Height - maxHeightSpace;

                // 计算碳排放的最大高度用于缩放
                double maxMachineEmission = 0;
                foreach (double q in machineEmissions) if (q > maxMachineEmission) maxMachineEmission = q;

                // 计算使用量的最大高度用于缩放（使用总使用量作为基准）
                double maxMachineQuantity = 0;
                foreach (double q in machineQuantities) if (q > maxMachineQuantity) maxMachineQuantity = q;

                for (int i = 0; i < machineNames.Length; i++)
                {
                    // 计算碳排放柱子高度
                    float emissionHeight = (float)(machineEmissions[i] / maxMachineEmission * maxMachineHeight);

                    // 计算使用量柱子高度
                    //float quantityHeight = (float)(machineQuantities[i] / totalUsage * maxMachineHeight); //2025.09.20之前的
                    float quantityHeight = (float)(machineQuantities[i] / maxMachineQuantity * maxMachineHeight); //2025.09.20

                    // 确保小值可见，设置最小高度
                    float minVisibleHeight = 6f;  //2025.9.20，原来是8
                    if (quantityHeight < minVisibleHeight && machineQuantities[i] > 0)
                    {
                        quantityHeight = minVisibleHeight;
                    }
                    if (quantityHeight > maxMachineHeight)
                        quantityHeight = maxMachineHeight;

                    int x = machineArea.Left + 20 + i * (barWidth + 20);
                    int emissionY = machineArea.Top + maxMachineHeight - (int)emissionHeight + 20;
                    int quantityY = machineArea.Top + maxMachineHeight - (int)quantityHeight + 20;

                    // 绘制碳排放量柱形和边框
                    g.FillRectangle(new SolidBrush(colors[i % colors.Length]), x, emissionY, barWidth / 2, emissionHeight);
                    g.DrawRectangle(Pens.Black, x, emissionY, barWidth / 2, emissionHeight);

                    // 绘制使用量柱形和边框
                    Color currentColor = colors[i % colors.Length];
                    Color backgroundColor = Color.FromArgb(100, currentColor);
                    g.FillRectangle(new SolidBrush(backgroundColor), x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //背景

                    HatchBrush hatchBrush = new HatchBrush(HatchStyle.LargeGrid, currentColor, Color.Transparent);
                    g.FillRectangle(hatchBrush, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //网格线
                    g.DrawRectangle(Pens.Black, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //边框线

                    // 碳排放百分比
                    float emissionPercentage = (float)(machineEmissions[i] / totalEmission);
                    string percentText = String.Format("{0:P2}", emissionPercentage);
                    SizeF percentSize = g.MeasureString(percentText, labelFont);
                    g.DrawString(percentText, labelFont, Brushes.Black, x + barWidth / 4 - percentSize.Width / 2, emissionY - 20);

                    // 使用量百分比（使用总使用量）
                    float quantityPercentage = (float)(machineQuantities[i] / machineTotalUse);
                    string quantityPercentText = String.Format("{0:P2}", quantityPercentage);
                    SizeF quantityPercentSize = g.MeasureString(quantityPercentText, labelFont);
                    g.DrawString(quantityPercentText, labelFont, Brushes.Black, x + barWidth * 3 / 4 - quantityPercentSize.Width / 2 + 5, quantityY - 20);

                    // 在柱子底部显示标签（旋转-45°）
                    string labelText = machineNames[i];
                    Font labelFontToUse = labelText.Length > 15 ? smallBottomFont : bottomlblFont;
                    SizeF labelSize = g.MeasureString(labelText, labelFontToUse);

                    // 计算旋转中心点
                    float centerX = x + barWidth / 2;
                    float centerY = machineArea.Top + maxMachineHeight + 60 + bottomLabelSpace;

                    // 保存当前图形状态
                    GraphicsState state = g.Save();

                    // 设置旋转变换
                    g.TranslateTransform(centerX, centerY);
                    g.RotateTransform(-45);
                    g.TranslateTransform(-centerX, -centerY);

                    // 绘制旋转后的文本
                    g.DrawString(labelText, labelFontToUse, Brushes.Black, centerX - labelSize.Width / 2 - 10, centerY - labelSize.Height / 2); //2025.09.20，X轴的位置多了-10

                    // 恢复图形状态
                    g.Restore(state);
                }

                // ========== 绘制图例 ==========
                int legendStartX = width - (rightReserveSpace + 40); ////图例的左起始位置，原来是加20，又向左移动了20，2025.09.20
                int legendY = 55;
                int legendYspace = 35;
                Font titleFont2 = new Font("Microsoft YaHei", 12, FontStyle.Bold);

                // 材料图例
                g.DrawString("材料碳排放量前10", titleFont2, Brushes.DarkBlue, legendStartX, legendY - legendYspace);
                for (int i = 0; i < materialNames.Length; i++)
                {
                    Color currentColor = colors[i % colors.Length];
                    g.FillRectangle(new SolidBrush(currentColor), legendStartX, legendY + i * legendYspace, 20, 20);
                    g.DrawRectangle(Pens.Black, legendStartX, legendY + i * legendYspace, 20, 20);

                    Color backgroundColor = Color.FromArgb(100, currentColor);
                    g.FillRectangle(new SolidBrush(backgroundColor), legendStartX + 25, legendY + i * legendYspace, 20, 20);
                    HatchBrush legendHatchBrush = new HatchBrush(HatchStyle.LargeGrid, currentColor, Color.Transparent);
                    g.FillRectangle(legendHatchBrush, legendStartX + 25, legendY + i * legendYspace, 20, 20);
                    g.DrawRectangle(Pens.Black, legendStartX + 25, legendY + i * legendYspace, 20, 20);

                    g.DrawString(String.Format("碳排放: {0:N2} t", materialEmissions[i] / 1000), labelFont, Brushes.Black, legendStartX + 50, legendY + i * legendYspace-5); //2025.09.20，原来单位是kg,没有/1000
                    g.DrawString(String.Format("使用量: {0:N2}{1}", materialQuantities[i], materialUnit[i]), labelFont, Brushes.Black, legendStartX + 50, legendY + i * legendYspace + 10);
                }

                // 机械图例
                g.DrawString("机械碳排放量前10", titleFont2, Brushes.DarkBlue, legendStartX, legendY - legendYspace + height / 2);
                for (int i = 0; i < machineNames.Length; i++)
                {
                    Color currentColor = colors[i % colors.Length];
                    g.FillRectangle(new SolidBrush(currentColor), legendStartX, legendY + height / 2 + i * legendYspace, 20, 20);
                    g.DrawRectangle(Pens.Black, legendStartX, legendY + height / 2 + i * legendYspace, 20, 20);

                    Color backgroundColor = Color.FromArgb(100, currentColor);
                    g.FillRectangle(new SolidBrush(backgroundColor), legendStartX + 25, legendY + height / 2 + i * legendYspace, 20, 20);
                    HatchBrush legendHatchBrush = new HatchBrush(HatchStyle.LargeGrid, currentColor, Color.Transparent);
                    g.FillRectangle(legendHatchBrush, legendStartX + 25, legendY + height / 2 + i * legendYspace, 20, 20);
                    g.DrawRectangle(Pens.Black, legendStartX + 25, legendY + height / 2 + i * legendYspace, 20, 20);

                    g.DrawString(String.Format("碳排放: {0:N2} t", machineEmissions[i] / 1000), labelFont, Brushes.Black, legendStartX + 50, legendY + height / 2 + i * legendYspace-5); //2025.09.20，原来单位是kg,没有/1000
                    g.DrawString(String.Format("使用量: {0:N2}{1}", machineQuantities[i], machineUnit[i]), labelFont, Brushes.Black, legendStartX + 50, legendY + height / 2 + i * legendYspace + 10);
                }
            }
            return bitmap;
        }

        protected void txtFilter_TextChanged(object sender, EventArgs e)
        {
        }
    }
}