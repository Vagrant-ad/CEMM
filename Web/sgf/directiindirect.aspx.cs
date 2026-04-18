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

namespace CEMM.Web.sgf
{
    public partial class directiindirect : System.Web.UI.Page
    {
        CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
        CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();
        CEMM.BLL.machineCEFactor2 machineFactorBll = new BLL.machineCEFactor2();
        int width = 1200, height = 600;

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

        // 筛选按钮点击事件
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

        // 修改为直接和间接碳排放统计
        protected void btnPie_Click(object sender, EventArgs e)
        {
            if (ddlTable.SelectedValue == "0")
            {
                MessageBox.Show(this, "请选择数据表！");
                return;
            }

            int selectTableid = int.Parse(ddlTable.SelectedValue);

            // 获取选中数据表中的所有材料/机械数据
            DataSet resultData = resultInfoBll.GetList("tableID = " + selectTableid);

            if (resultData == null || resultData.Tables.Count == 0 || resultData.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "选中的数据表中没有数据！");
                return;
            }

            // 计算直接和间接碳排放
            double directEmission = 0;
            double indirectEmission = 0;
            string unmatchedCodes = "";
            int totalRecords = resultData.Tables[0].Rows.Count;
            int processedRecords = 0;
            int unmatchedCount = 0;

            foreach (DataRow row in resultData.Tables[0].Rows)
            {
                string materialCode = row["code"].ToString().Trim();

                // 检查排放量是否为空
                if (row["total_emission"] == null || row["total_emission"] == DBNull.Value)
                {
                    continue;
                }

                double emission = 0;
                try
                {
                    emission = Convert.ToDouble(row["total_emission"]);
                }
                catch
                {
                    continue;
                }

                // 跳过排放量为0的记录
                if (emission == 0)
                {
                    continue;
                }

                // 根据code查找碳排放因子（主要是为了获取能源类型）
                CEMM.Model.machineCEFactor2 factor = machineFactorBll.GetModelByCode(materialCode);

                if (factor != null && factor.energytype.HasValue)
                {
                    // 根据能源类型分类，直接使用数据库中的total_emission
                    switch (factor.energytype.Value)
                    {
                        case 1: // 人工
                        case 2: // 机械工
                        case 3: // 重油
                        case 4: // 汽油、柴油、煤等燃料
                            directEmission += emission;
                            break;
                        case 5: // 材料
                        case 6: // 电力设备
                        case 7: // 电
                            indirectEmission += emission;
                            break;
                        case 8: // 混合类型（既有直接又有间接）
                            if (factor.energyfactor.HasValue && factor.machinefactor.HasValue)
                            {
                                // 计算直接和间接的比例
                                double directRatio = (double)(factor.machinefactor.Value - factor.energyfactor.Value) / (double)factor.machinefactor.Value;
                                double indirectRatio = (double)factor.energyfactor.Value / (double)factor.machinefactor.Value;

                                directEmission += emission * directRatio;
                                indirectEmission += emission * indirectRatio;
                            }
                            else
                            {
                                // 如果没有energyfactor值，默认全部为直接排放
                                directEmission += emission;
                            }
                            break;
                        default:
                            directEmission += emission;
                            break;
                    }

                    processedRecords++;
                }
                else
                {
                    // 如果找不到对应的能源类型，默认作为直接排放
                    directEmission += emission;
                    unmatchedCodes += materialCode + ",";
                    unmatchedCount++;
                    processedRecords++;
                }
            }

            // 生成柱状图（显示直接和间接排放）
            Bitmap chartImage = DrawDirectIndirectBarChart(directEmission, indirectEmission);

            // 保存图像到内存流
            MemoryStream ms = new MemoryStream();
            chartImage.Save(ms, ImageFormat.Png);
            byte[] buffer = ms.ToArray();
            ms.Close();

            // 显示图像
            Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
            Image1.Visible = true;

            // 同时在页面上显示数值结果
            DisplayEmissionResults(directEmission, indirectEmission);
        }

        // 添加缺失的方法：绘制直接和间接碳排放柱状图
        // 绘制直接和间接碳排放柱状图（居中版本）
        // 绘制直接和间接碳排放柱状图（整体下移，图例在右上方）
        // 绘制直接和间接碳排放柱状图（整体右移版本）
        private Bitmap DrawDirectIndirectBarChart(double directEmission, double indirectEmission)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // 设置字体
                Font titleFont = new Font("Microsoft YaHei", 24, FontStyle.Bold);
                Font axisFont = new Font("Microsoft YaHei", 12);
                Font valueFont = new Font("Microsoft YaHei", 11);
                Font largeFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);

                // 整体下移的偏移量 - 减小下移量，让图表更高
                int verticalOffset = -80;

                // 增加右边距，让图表整体右移
                int rightShift = 80; // 新增：向右移动的像素量
                int margin = 160 + rightShift; // 修改：增加右边距

                // 绘制标题 - 居中（下移）
                string title = "直接与间接碳排放统计";
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.DarkBlue,
              new PointF(width / 2 - titleSize.Width / 2, 5 + 40));

                // 计算图表区域 - 增加图表高度
                int chartWidth = width - 2 * margin + rightShift; // 修改：调整图表宽度计算
                int chartHeight = height - margin - 100;

                // 计算总计
                double totalEmission = directEmission + indirectEmission;
                double directPercentage = totalEmission > 0 ? (directEmission / totalEmission) * 100 : 0;
                double indirectPercentage = totalEmission > 0 ? (indirectEmission / totalEmission) * 100 : 0;

                // 设置柱状图参数
                int barWidth = 100;
                int spacing = 80;

                // 计算起始X坐标以使柱状图居中
                int totalBarsWidth = (barWidth * 2) + spacing;
                int startX = margin + (chartWidth - totalBarsWidth) / 2;

                // 计算最大值用于缩放
                double maxEmission = Math.Max(directEmission, indirectEmission);
                if (maxEmission == 0) maxEmission = 1; // 避免除零错误

                // 绘制坐标轴（调整Y轴位置，让图表更高）
                Pen axisPen = new Pen(Color.Black, 2);
                int axisYStart = margin + verticalOffset; // 使用减小后的偏移量
                int axisYEnd = margin + chartHeight + verticalOffset; // 使用减小后的偏移量
                g.DrawLine(axisPen, margin, axisYStart, margin, axisYEnd);//x轴
                g.DrawLine(axisPen, margin, axisYEnd, margin + chartWidth, axisYEnd);//y轴

                // 绘制Y轴刻度和标签 - 右对齐
                int yTicks = 5;
                for (int i = 0; i <= yTicks; i++)
                {
                    double value = maxEmission * i / yTicks;
                    int y = axisYEnd - (int)(chartHeight * i / yTicks);

                    g.DrawLine(Pens.Black, margin - 5, y, margin, y);

                    string yLabel = value.ToString("N0") + " kg";
                    SizeF yLabelSize = g.MeasureString(yLabel, axisFont);
                    g.DrawString(yLabel, axisFont, Brushes.Black,
                        margin - 15 - yLabelSize.Width, y - yLabelSize.Height / 2);
                }

                // 绘制Y轴标题 - 右对齐
                string yAxisTitle = "碳排放量 (kg)";
                SizeF yAxisTitleSize = g.MeasureString(yAxisTitle, axisFont);
                g.DrawString(yAxisTitle, axisFont, Brushes.Black,
                    margin - 20 - yAxisTitleSize.Width, margin - 40 + verticalOffset);

                // 绘制直接排放柱状图
                int directBarHeight = (int)(chartHeight * (directEmission / maxEmission));
                int directX = startX;
                int directY = axisYEnd - directBarHeight;

                Color directColor = Color.FromArgb(79, 129, 189); // 蓝色
                using (Brush directBrush = new SolidBrush(directColor))
                {
                    g.FillRectangle(directBrush, directX, directY, barWidth, directBarHeight);
                    g.DrawRectangle(Pens.Black, directX, directY, barWidth, directBarHeight);
                }

                // 绘制直接排放数值
                string directValueText = directEmission.ToString("N0");
                SizeF directValueSize = g.MeasureString(directValueText, valueFont);
                g.DrawString(directValueText, valueFont, Brushes.Black,
                    directX + barWidth / 2 - directValueSize.Width / 2, directY - 25);

                // 绘制直接排放标签
                string directLabel = "直接碳排放";
                SizeF directLabelSize = g.MeasureString(directLabel, axisFont);
                g.DrawString(directLabel, axisFont, Brushes.Black,
                    directX + barWidth / 2 - directLabelSize.Width / 2, axisYEnd + 15);

                // 绘制间接排放柱状图
                int indirectBarHeight = (int)(chartHeight * (indirectEmission / maxEmission));
                int indirectX = startX + barWidth + spacing;
                int indirectY = axisYEnd - indirectBarHeight;

                Color indirectColor = Color.FromArgb(192, 80, 77); // 红色
                using (Brush indirectBrush = new SolidBrush(indirectColor))
                {
                    g.FillRectangle(indirectBrush, indirectX, indirectY, barWidth, indirectBarHeight);
                    g.DrawRectangle(Pens.Black, indirectX, indirectY, barWidth, indirectBarHeight);
                }

                // 绘制间接排放数值
                string indirectValueText = indirectEmission.ToString("N0");
                SizeF indirectValueSize = g.MeasureString(indirectValueText, valueFont);
                g.DrawString(indirectValueText, valueFont, Brushes.Black,
                    indirectX + barWidth / 2 - indirectValueSize.Width / 2, indirectY - 25);

                // 绘制间接排放标签
                string indirectLabel = "间接碳排放";
                SizeF indirectLabelSize = g.MeasureString(indirectLabel, axisFont);
                g.DrawString(indirectLabel, axisFont, Brushes.Black,
                    indirectX + barWidth / 2 - indirectLabelSize.Width / 2, axisYEnd + 15);

                // 绘制图例 - 调整位置（也向右移动）
                int legendX = margin + chartWidth - 250 + rightShift / 2; // 修改：图例也向右移动
                int legendY = margin + verticalOffset - 10;

                int legendSpace = 30;
                //显示表名
                g.DrawString("表名：" + ddlTable.SelectedItem.Text, valueFont, Brushes.DarkBlue, legendX, legendY - legendSpace * 2);

                // 图例标题
                g.DrawString("排放统计", axisFont, Brushes.Black, legendX, legendY - legendSpace);

                // 直接排放图例
                g.FillRectangle(new SolidBrush(directColor), legendX, legendY, 20, 15);
                g.DrawRectangle(Pens.Black, legendX, legendY, 20, 15);

                string directLegend = string.Format("直接: {0:N0} kg ({1:N1}%)", directEmission, directPercentage);
                g.DrawString(directLegend, valueFont, Brushes.Black, legendX + 25, legendY-5);

                // 间接排放图例
                g.FillRectangle(new SolidBrush(indirectColor), legendX, legendY + legendSpace, 20, 15);
                g.DrawRectangle(Pens.Black, legendX, legendY + legendSpace, 20, 15);

                string indirectLegend = string.Format("间接: {0:N0} kg ({1:N1}%)", indirectEmission, indirectPercentage);
                g.DrawString(indirectLegend, valueFont, Brushes.Black, legendX + 25, legendY + legendSpace);

                // 总排放图例
                g.DrawString(string.Format("总计: {0:N0} kg", totalEmission), valueFont, Brushes.DarkGreen, legendX, legendY + legendSpace*2);
                                
                // 添加说明文字
                string directDesc = "直接碳排放：人工+机械工+使用燃料的机械设备及车辆+重油、汽油、柴油、煤";
                string indirectDesc = "间接碳排放：材料+使用电力的机械设备及车辆+电";

                // 使用较小的字体
                Font descFont = new Font("Microsoft YaHei", 12);

                // 计算文字位置（对齐纵坐标开始的竖线）
                int descX = margin; // 使用与纵坐标相同的X坐标
                int descY = axisYEnd + 50; // 在原来总计文本的位置下方

                // 绘制直接排放说明
                g.DrawString(directDesc, descFont, Brushes.Black, descX, descY); // 使用黑色

                // 绘制间接排放说明
                g.DrawString(indirectDesc, descFont, Brushes.Black, descX, descY + 25); // 使用黑色
            }
            return bitmap;
        }

        // 在页面上显示排放结果
        private void DisplayEmissionResults(double directEmission, double indirectEmission)
        {
            double totalEmission = directEmission + indirectEmission;
            double directPercentage = totalEmission > 0 ? (directEmission / totalEmission) * 100 : 0;
            double indirectPercentage = totalEmission > 0 ? (indirectEmission / totalEmission) * 100 : 0;

            // 创建结果文本
            string resultText = string.Format(
                   "<div style='margin:20px; padding:15px; border:1px solid #ccc; background-color:#f9f9f9;'>\r\n" +
                   "    <h3>碳排放统计结果</h3>\r\n" +
                   "    <p><strong>直接碳排放:</strong> {0:N2} kg ({1:N1}%)</p>\r\n" +
                   "    <p><strong>间接碳排放:</strong> {2:N2} kg ({3:N1}%)</p>\r\n" +
                   "    <p><strong>总碳排放量:</strong> {4:N2} kg</p>\r\n" +
                   "    <p><strong>数据表:</strong> {5}</p>\r\n" +

                   "</div>",
                   directEmission, directPercentage, indirectEmission, indirectPercentage,
                   totalEmission, ddlTable.SelectedItem.Text, DateTime.Now
               );

            // 显示结果
            ClientScript.RegisterStartupScript(this.GetType(), "ShowResults",
    string.Format("document.getElementById('resultContainer').innerHTML = '{0}';", resultText.Replace("'", "\\'")), true);
        }

        protected void txtFilter_TextChanged(object sender, EventArgs e)
        {
            // 不需要实现
        }
    }
}