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
using System.Text;
using System.Data.SqlClient;

namespace CEMM.Web.sgf
{
    public partial class unitMaterMachine : System.Web.UI.Page
    {
        CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
        CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();

        int width = 1500, height = 800;//绘图的区域大小，后面不用了，下面用了adjustedWidth=1860，adjustedHeight

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = resultTabInfoBll.GetTopN(60, "", "tableID");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlTable.DataSource = ds;
                    ddlTable.DataTextField = "tableName";
                    ddlTable.DataValueField = "tableID";
                    ddlTable.DataBind();
                }
                ddlTable.Items.Insert(0, "请选择数据表");
            }

        }

        /// <summary>
        /// 绘制饼状图，（未使用，隐藏）
        /// </summary>
        protected void btnPie_Click(object sender, EventArgs e)
        {
            CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
            int selectTableid;
            string targetFile, suffix = "pie01UnitWork.png";
            if (int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                string folderPath = Server.MapPath(".\\UpFile\\");
                string fileName = selectTableid + suffix;
                string filePath = Path.Combine(folderPath, fileName);
                if ((File.Exists(filePath)))
                {
                    targetFile = ".\\UpFile\\" + selectTableid + suffix;
                    Image1.ImageUrl = string.Format("{0}?t={1}", targetFile, DateTime.Now.Ticks);
                    return;
                }

                resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
            }
            else
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }

            Bitmap bm = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bm);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double[] quantities = new double[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            try
            {
                quantities[0] = Convert.ToDouble(resultInfoMdl.temp_emission);
                quantities[1] = Convert.ToDouble(resultInfoMdl.subgrade_emission);
                quantities[2] = Convert.ToDouble(resultInfoMdl.pavement_emission);
                quantities[3] = Convert.ToDouble(resultInfoMdl.bridge_emission);
                quantities[4] = Convert.ToDouble(resultInfoMdl.tunnel_emission);
                quantities[5] = Convert.ToDouble(resultInfoMdl.crossing_emission);
                quantities[6] = Convert.ToDouble(resultInfoMdl.traffic_emission);
                quantities[7] = Convert.ToDouble(resultInfoMdl.greening_emission);
                quantities[8] = Convert.ToDouble(resultInfoMdl.other_emission);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            string[] labels = {
                "临时工程", "路基工程", "路面工程", "桥涵工程", 
                "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
            };

            Rectangle pieRect = new Rectangle(160, 60, 560, 560);
            try
            {
                DrawPieChart(graphics, quantities, labels, pieRect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            targetFile = ".\\UpFile\\" + selectTableid + suffix;
            bm.Save(Server.MapPath(targetFile), System.Drawing.Imaging.ImageFormat.Png);
            Image1.ImageUrl = string.Format("{0}?t={1}", targetFile, DateTime.Now.Ticks);
            bm.Dispose();
            graphics.Dispose();
        }

        private void DrawPieChart(Graphics graphics, double[] values, string[] labels, Rectangle area)
        {
            if (values == null || labels == null || values.Length != labels.Length)
                throw new ArgumentException("DrawPieChart参数数据无效");

            double total = 0;
            double[] percents = new double[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (double value in values) total += value;
            if (total == 0) return;

            Color[] colors = {
                Color.FromArgb(255, 128, 128),
                Color.FromArgb(255, 255, 0),
                Color.FromArgb(255, 128, 64),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(0, 128, 192),
                Color.FromArgb(255, 0, 255),
                Color.FromArgb(128, 64, 64),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(128, 0, 128)
            };

            using (Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Bold))
            using (Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold))
            {
                float startAngle = 0;
                Brush textBrush = Brushes.White;

                for (int i = 0; i < values.Length; i++)
                {
                    percents[i] = values[i] / total;
                    float sweepAngle = (float)(360 * percents[i]);
                    using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                    {
                        graphics.FillPie(brush, area, startAngle - 90, sweepAngle);
                    }
                    graphics.DrawPie(Pens.Black, area, startAngle - 90, sweepAngle);
                    AddPercentageLabel(graphics, percents[i], total, startAngle, sweepAngle, area, percentFont, textBrush);
                    startAngle += sweepAngle;
                }
                DrawLegend(graphics, labels, colors, percents, total, new Point(area.Right + 40, area.Top + 20));
                string titleStr = "单位工程碳排放统计";
                SizeF titleSize = graphics.MeasureString(titleStr, titleFont);
                graphics.DrawString(titleStr, titleFont, Brushes.DarkBlue, new PointF(area.Left + (area.Width - titleSize.Width) / 2, 10));
            }
        }

        private void AddPercentageLabel(Graphics graphics, double percent, double total,
                                       float startAngle, float sweepAngle, Rectangle area, Font percentFont, Brush textBrush)
        {
            try
            {
                string percentText = String.Format("{0:P2}", percent);
                float centerX = area.X + area.Width / 2;
                float centerY = area.Y + area.Height / 2;
                float radius = Math.Min(area.Width, area.Height) / 3;
                float midAngle = startAngle + sweepAngle / 2;
                double radian = (midAngle - 90) * (Math.PI / 180);
                radian += 2 * Math.PI;
                radian %= 2 * Math.PI;
                float labelX = centerX + (float)(radius * Math.Cos(radian));
                float labelY = centerY + (float)(radius * Math.Sin(radian));
                SizeF textSize = graphics.MeasureString(percentText, percentFont);
                labelX -= textSize.Width / 2;
                labelY -= textSize.Height / 2;
                if (sweepAngle > 10)
                {
                    RectangleF backgroundRect = new RectangleF(labelX - 2, labelY - 1, textSize.Width + 4, textSize.Height + 2);
                    using (Brush bgBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                    {
                        graphics.FillEllipse(bgBrush, backgroundRect);
                    }
                    graphics.DrawString(percentText, percentFont, textBrush, labelX, labelY);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AddPercentageLabel：" + ex.ToString());
            }
        }

        private void DrawLegend(Graphics graphics, string[] labels, Color[] colors,
                               double[] percents, double total, Point startPoint)
        {
            try
            {
                int boxSize = 20;
                int spacing = 30;

                using (Font labelFont = new Font("Microsoft YaHei", 10))
                using (Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Italic))
                {
                    Font titleFont = new Font("Microsoft YaHei", 12, FontStyle.Bold);
                    Brush textBrush = Brushes.Black;
                    for (int i = 0; i < labels.Length; i++)
                    {
                        double percentage = percents[i];
                        string percentText = String.Format("{0:P2}", percentage);
                        Rectangle colorRect = new Rectangle(startPoint.X, startPoint.Y + i * spacing, boxSize, boxSize);
                        using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                        {
                            graphics.FillRectangle(brush, colorRect);
                        }
                        graphics.DrawRectangle(Pens.Black, colorRect);
                        PointF textPoint = new PointF(startPoint.X + boxSize + 10, startPoint.Y + i * spacing);
                        graphics.DrawString(labels[i], labelFont, textBrush, textPoint);
                        PointF percentPoint = new PointF(startPoint.X + boxSize + 150, startPoint.Y + i * spacing);
                        if (percentage < 0.028)
                            graphics.DrawString(percentText, percentFont, Brushes.Red, percentPoint);
                        else
                            graphics.DrawString(percentText, percentFont, Brushes.Gray, percentPoint);
                    }
                    graphics.DrawString("工程类型", titleFont, Brushes.DarkBlue, startPoint.X, startPoint.Y - 30);
                    graphics.DrawString("占比", titleFont, Brushes.DarkBlue, startPoint.X + 150 + 20, startPoint.Y - 30);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("DrawLegend：" + ex.ToString());
            }
        }


        /// <summary>
        /// 绘制柱状图
        /// </summary>
        protected void btnBar_Click(object sender, EventArgs e)
        {
            int selectTableid;
            if (!int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }

            try
            {
                CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
                // 获取所有单位工程的数据
                Dictionary<string, DataSet[]> unitData = new Dictionary<string, DataSet[]>();

                resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
                double totalEmission = Convert.ToDouble(resultInfoMdl.total_emission);

                string[] unitProjects = {
                    "临时工程", "路基工程", "路面工程", "桥涵工程", 
                    "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
                };

                foreach (string unit in unitProjects)
                {
                    DataSet materialData = resultInfoBll.GetTopMaterialsEmissionByUnit(selectTableid, unit, 10);
                    DataSet machineData = resultInfoBll.GetTopMachinesEmissionByUnit(selectTableid, unit, 10);
                    unitData.Add(unit, new DataSet[] { materialData, machineData });
                }

                // 获取正确的总使用量（所有记录的总和）
                //double totalUsage = resultInfoBll.GetTotalUsageQuantity(selectTableid);
                double materialTotalUse = resultInfoBll.GetTotalMaterialQuantity(selectTableid); //2025.09.20
                double machineTotalUse = resultInfoBll.GetTotalMachineQuantity(selectTableid); //2025.09.20

                byte[] chartBytes = DrawUnitBarChartsByEmission(unitData, selectTableid, totalEmission, materialTotalUse, machineTotalUse); //2025.09.20
                
                // 将图像显示在Image控件中
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(chartBytes);

                //string tempFileName = Guid.NewGuid().ToString() + ".png";
                //string tempFilePath = Server.MapPath("~/TempImages/" + tempFileName);
                //Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));
                //File.WriteAllBytes(tempFilePath, chartBytes);
                //Image1.ImageUrl = "~/TempImages/" + tempFileName + "?t=" + DateTime.Now.Ticks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "生成图表时出错: " + ex.Message);
            }
        }

        private byte[] DrawUnitBarChartsByEmission(Dictionary<string, DataSet[]> unitData, int selectTableid, double totalEmission,double materialTotalUse, double machineTotalUse)
        {
            int unitCount = unitData.Count;
            int unitChartHeight = 340;//单位工程图表的高度
            int unitSpaceVertical = 150;//图与图之间的垂直间隔
            int headerHeight = 60;//也可以让图表整体往下移，各单位工程材料与机械碳排放TOP10统计与下方的图表区域空开间隔，还需调整g.DrawString("各单位工程材料与机械碳排放TOP10统计", titleFont, Brushes.DarkBlue,


            int adjustedHeight = headerHeight + unitCount * (unitChartHeight + unitSpaceVertical);
            int adjustedWidth = 1860;//画布的宽度，数字越大图越大，需要重新调整各个参数

            using (Bitmap bitmap = new Bitmap(adjustedWidth, adjustedHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);
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

                    int chartWidth = 670;//控制每个柱状图区域的宽度，2025.09.20
                    int unitChartHeight2 = 330;//柱子绘制高度,配合 int unitChartHeight = 340，unitChartHeight可以更大一些，2025.09.20
                    int legendWidth = 200;//右侧图例宽度，计算机械区域的位置时用到，原来350，2025.09.20

                    int currentY = 70;//整体图表都上下移动

                    g.DrawString("各单位工程材料与机械碳排放TOP10统计", titleFont, Brushes.DarkBlue, new PointF(10, 10));//标题位置第一个10左右

                    string[] unitNames = {
                "临时工程", "路基工程", "路面工程", "桥涵工程", 
                "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
            };

                    foreach (string unit in unitNames)
                    {
                        if (unitData.ContainsKey(unit))
                        {
                            DataSet materialData = unitData[unit][0];
                            DataSet machineData = unitData[unit][1];

                            Font unitNameFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);//单位工程名称的字体“路基工程”等

                            // 绘制左侧材料区域的标题“路基工程”等 15左右位置
                            g.DrawString(unit, unitNameFont, Brushes.DarkBlue, new PointF(15, currentY - 10));
                            //材料图表区域
                            Rectangle materialArea = new Rectangle(0, currentY + 45, chartWidth, unitChartHeight2);//材料左侧从0开始，，图表往下移动currentY + 45
                            //材料图例区域，大小在DrawCompactLegend中修改
                            Rectangle materialLegendArea = new Rectangle(
                                materialArea.Right + 30,//材料图例位置（向右移动）
                                materialArea.Top - 35,
                                legendWidth,
                                unitChartHeight2 + 50
                            );

                            // 计算机械图表的水平起始位置，左侧材料，右侧机械
                            int machineStartX = materialArea.Right + legendWidth + 50; // 机械图表往右移动的位置50，原来是45，2025.09.20
                            //机械图表区域
                            Rectangle machineArea = new Rectangle(
                                machineStartX,  
                                currentY + 45,//图表往下移动currentY + 45
                                chartWidth,
                                unitChartHeight2
                            );

                            // 机械图例区域
                            Rectangle machineLegendArea = new Rectangle(
                                machineArea.Right + 30,  // 图例可以独立移动
                                machineArea.Top - 35,
                                legendWidth,
                                unitChartHeight2 + 50
                            );

                            // 在机械区域上方也绘制相同的单位工程标题“路基工程”machineStartX+10左右的位置
                            g.DrawString(unit, unitNameFont, Brushes.DarkBlue, new PointF(machineStartX + 10, currentY - 10));

                            if (materialData != null && materialData.Tables.Count > 0 && materialData.Tables[0].Rows.Count > 0)
                            {
                                //DrawSingleUnitChart(g, materialData, "材料TOP10（条形标签数值为百分比）", materialArea, true, colors, totalEmission, materialTotalUse);
                                DrawSingleUnitChart(g, materialData, "材料TOP10", materialArea, true, colors, totalEmission, materialTotalUse);
                                DrawCompactLegend(g, materialData, materialLegendArea, colors, true);
                            }
                            else
                            {
                                DrawNoDataMessage(g, "暂无材料数据", materialArea);
                            }

                            if (machineData != null && machineData.Tables.Count > 0 && machineData.Tables[0].Rows.Count > 0)
                            {
                                //DrawSingleUnitChart(g, machineData, "机械TOP10（条形标签数值为百分比）", machineArea, false, colors, totalEmission, machineTotalUse);
                                DrawSingleUnitChart(g, machineData, "机械TOP10", machineArea, false, colors, totalEmission, machineTotalUse);
                                DrawCompactLegend(g, machineData, machineLegendArea, colors, false);
                            }
                            else
                            {
                                DrawNoDataMessage(g, "暂无机械数据", machineArea);
                            }

                            currentY += unitChartHeight2 + unitSpaceVertical;//控制图与图之间的行间距， 配合 int unitSpaceVertical = 150;//图与图之间的间隔
                        }
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        //绘制每个小图
        private void DrawSingleUnitChart(Graphics g, DataSet data, string title, Rectangle area, bool isMaterial, Color[] colors, double totalEmission, double totalUsage)//柱子的画法
        {
            //System.Diagnostics.Debug.WriteLine("DrawSingleUnitChart - totalUsage: " + totalUsage);

            if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
            {
                DrawNoDataMessage(g, "暂无数据", area);
                return;
            }

            List<string> labels = new List<string>();
            List<double> emissions = new List<double>();
            List<double> quantities = new List<double>();
            
            string emissionField = GetEmissionFieldFromDataSet(data);
            string quantityField = GetQuantityFieldFromDataSet(data);

            double unitTotalEmission = 0;

            foreach (DataRow row in data.Tables[0].Rows)
            {
                labels.Add(row["formName"].ToString());
                double emission = Convert.ToDouble(row[emissionField]);
                double quantity = Convert.ToDouble(row[quantityField]);

                unitTotalEmission += emission;

                emissions.Add(emission);
                quantities.Add(quantity);
                
            }

            int barWidth = Math.Max(10, area.Width / Math.Max(labels.Count, 1));//如果柱子宽度<10，则取10
            int maxChartHeight = area.Height - 40;
            //比例计算
            double maxEmission = emissions.Count > 0 ? emissions.Max() : 1;
            double maxQuantity = quantities.Count > 0 ? quantities.Max() : 1;

            double emissionScale = maxChartHeight / maxEmission;//修改比例调整柱子高度
            double quantityScale = maxChartHeight / maxQuantity;//修改比例调整柱子高度

            //Font labelFont = new Font("Microsoft YaHei", 8);
            Font percentFont = new Font("Microsoft YaHei", 8);//, FontStyle.Bold
            Font bottomlblFont = new Font("Microsoft YaHei", 10);//x轴标签字体大小 
            //Font smallBottomFont = new Font("Microsoft YaHei", 10);//x轴标签字体大小 

            for (int i = 0; i < labels.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Item " + i + ": quantity=" + quantities[i] + ", totalUsage=" + totalUsage + ", percentage=" + (quantities[i] / totalUsage).ToString("P2"));
                float emissionHeight = (float)(emissions[i] * emissionScale);
                float quantityHeight = (float)(quantities[i] * quantityScale);

                int x = area.Left + 10 + i * (barWidth + 5);//柱子间隔  barWidth + 5
                int emissionY = area.Top + maxChartHeight - (int)emissionHeight;
                int quantityY = area.Top + maxChartHeight - (int)quantityHeight;

                Color currentColor = colors[i % colors.Length];

                //绘制碳排放的实心条形和边框
                g.FillRectangle(new SolidBrush(currentColor), x, emissionY, barWidth / 2, emissionHeight);
                g.DrawRectangle(Pens.Black, x, emissionY, barWidth / 2, emissionHeight);

                //绘制使用量的网格线条形区域 和边框，2025.09.20以前的，可以参照mainMaterMachine.aspx.cs进行修改
                using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(120, currentColor)))
                {
                    g.FillRectangle(solidBrush, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //绘制背景
                }

                using (Pen gridPen = new Pen(Color.FromArgb(150, Color.Black), 1f))
                {
                    for (int j = 1; j <= 3; j++) 
                    {
                        int gridY = quantityY + (int)(quantityHeight * j / 4);
                        g.DrawLine(gridPen, x + barWidth / 2, gridY, x + barWidth, gridY); //绘制4条竖线
                    }

                    for (int j = 1; j <= 3; j++)
                    {
                        int gridX = x + barWidth / 2 + (int)(barWidth / 2 * j / 4);
                        g.DrawLine(gridPen, gridX, quantityY, gridX, quantityY + quantityHeight); //绘制4条横线
                    }
                }
                g.DrawRectangle(Pens.Black, x + barWidth / 2, quantityY, barWidth / 2, quantityHeight); //绘制边框线

                float emissionPercentage = (float)(emissions[i] / totalEmission); //原来是unitTotalEmission
                string emissionPercentText = String.Format("{0:P2}", emissionPercentage); //如果只保留两位小数，可用“F2”或“0.00”格式

                float quantityPercentage = (float)(quantities[i] / totalUsage);
                string quantityPercentText = String.Format("{0:P2}", quantityPercentage); //如果只保留两位小数，可用“F2”或“0.00”格式
                //绘制碳排放百分比
                if (emissions[i] > 0)
                {
                    SizeF emissionPercentSize = g.MeasureString(emissionPercentText, percentFont);
                    float emissionTextY = emissionY - emissionPercentSize.Height - 10;
                    /// 修改碳排放百分比位置（向右移动）emissionPercentSize.Width / 2 +1
                    g.DrawString(emissionPercentText, percentFont, Brushes.Black,
                        x + barWidth / 4 - emissionPercentSize.Width / 2 + 1, emissionTextY + 5);
                }
                //绘制使用量百分比
                if (quantities[i] > 0)
                {
                    SizeF quantityPercentSize = g.MeasureString(quantityPercentText, percentFont);
                    float quantityTextY = quantityY - quantityPercentSize.Height - 10;
                    // 修改使用量百分比位置quantityPercentSize.Width / 2 + 5
                    g.DrawString(quantityPercentText, percentFont, Brushes.Black,
                        x + barWidth * 3 / 4 - quantityPercentSize.Width / 2 + 5, quantityTextY + 5);
                }

                string labelText = labels[i];
                //Font labelFontToUse = labelText.Length > 15 ? smallBottomFont : bottomlblFont;

                //处理材料或机械名称换行
                int namelen = labelText.Length; //2025.09.20修改
                if (namelen > 22)
                    labelText = labelText.Substring(0, 11) + "\n" + labelText.Substring(11, 11);
                else if (namelen >= 12)
                {
                    if (namelen % 2 == 0)
                        labelText = labelText.Substring(0, namelen / 2) + "\n" + labelText.Substring(namelen / 2, namelen / 2);
                    else
                        labelText = labelText.Substring(0, namelen / 2) + "\n" + labelText.Substring(namelen / 2, namelen / 2 + 1);
                }
                
                SizeF labelSize = g.MeasureString(labelText, bottomlblFont);
                // 计算旋转中心点
                float centerX = x + barWidth / 2;
                float centerY = area.Top + maxChartHeight + 55; //修改让x轴标签整体下移，原来是+45，2025.09.20

                // 保存当前图形状态
                GraphicsState state = g.Save();

                // 设置旋转变换
                g.TranslateTransform(centerX, centerY);
                g.RotateTransform(-45);
                g.TranslateTransform(-centerX, -centerY);

                // 绘制旋转后的文本
                g.DrawString(labelText, bottomlblFont, Brushes.Black, centerX - labelSize.Width / 2 - 10, centerY - labelSize.Height / 2); //2025.09.20，X轴的位置多了-10

                // 恢复图形状态
                g.Restore(state);
            }

            Font titleFont = new Font("Microsoft YaHei", 11, FontStyle.Bold);//“材料top10”“机械top10”的字体
            SizeF titleSize = g.MeasureString(title, titleFont);
            //调整“材料top10”“机械top10”位置area.Width - titleSize.Width) / 2+20左右，area.Top - 35上下
            g.DrawString(title, titleFont, Brushes.DarkBlue,
                area.Left + (area.Width - titleSize.Width) / 2 + 60, area.Top - 35);
        }

        //绘制每个小图的图例
        private void DrawCompactLegend(Graphics g, DataSet data, Rectangle area, Color[] colors, bool isMaterial)
        {
            if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                return;
            //图例字体的大小
            Font dataFont = new Font("Microsoft YaHei", 8);
            string emissionField = GetEmissionFieldFromDataSet(data);
            string quantityField = GetQuantityFieldFromDataSet(data);
            string unit;

            int itemHeight = 30;
            int maxItems = Math.Min(data.Tables[0].Rows.Count, (area.Height) / itemHeight);
            int rightOffset = 30;
            //图例标题字体的大小
            string legendTitle = isMaterial ? "材料TOP10" : "机械TOP10";
            g.DrawString(legendTitle, new Font("Microsoft YaHei", 8, FontStyle.Bold), Brushes.DarkBlue, area.Left + rightOffset, area.Top-10);

            for (int i = 0; i < maxItems; i++)
            {
                DataRow row = data.Tables[0].Rows[i];
                double emission = Convert.ToDouble(row[emissionField]);
                double quantity = Convert.ToDouble(row[quantityField]);
                unit = row["unit"].ToString();

                Color currentColor = colors[i % colors.Length];
                int yPos = area.Top + 10 + i * itemHeight;

                int legendBlockSize = 20;
                //绘制碳排放实心图例方块
                g.FillRectangle(new SolidBrush(currentColor), area.Left + rightOffset, yPos, legendBlockSize, legendBlockSize);
                g.DrawRectangle(Pens.Black, area.Left + rightOffset, yPos, legendBlockSize, legendBlockSize);

                //绘制使用量条形方块
                using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(120, currentColor)))
                {
                    g.FillRectangle(solidBrush, area.Left + rightOffset + legendBlockSize + 2, yPos, legendBlockSize, legendBlockSize);
                }
                using (Pen gridPen = new Pen(Color.FromArgb(150, Color.Black), 1f))
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        int gridY = yPos + (int)(legendBlockSize * j / 3);
                        g.DrawLine(gridPen, area.Left + rightOffset + legendBlockSize + 2, gridY,
                                  area.Left + rightOffset + legendBlockSize * 2 + 2, gridY);
                    }

                    for (int j = 1; j <= 2; j++)
                    {
                        int gridX = area.Left + rightOffset + legendBlockSize + 2 + (int)(legendBlockSize * j / 3);
                        g.DrawLine(gridPen, gridX, yPos, gridX, yPos + legendBlockSize);
                    }
                }
                g.DrawRectangle(Pens.Black, area.Left + rightOffset + legendBlockSize + 2, yPos, legendBlockSize, legendBlockSize);

                //绘制图例文字
                string emissionText = string.Format("碳排放: {0:N2} t", emission/1000); //原来是kg，现在t，所以/1000，2025.09.20
                g.DrawString(emissionText, dataFont, Brushes.Black, area.Left + rightOffset + (legendBlockSize * 2) + 6, yPos-2);

                string quantityText = string.Format("使用量: {0:N2}{1}", quantity,unit);
                g.DrawString(quantityText, dataFont, Brushes.Black, area.Left + rightOffset + (legendBlockSize * 2) + 6, yPos + 12);
            }
        }

        private void DrawNoDataMessage(Graphics g, string message, Rectangle area)
        {
            Font messageFont = new Font("Microsoft YaHei", 10, FontStyle.Italic);
            SizeF messageSize = g.MeasureString(message, messageFont);
            g.DrawString(message, messageFont, Brushes.Gray,
                area.Left + (area.Width - messageSize.Width) / 2,
                area.Top + (area.Height - messageSize.Height) / 2);
        }

        private string GetEmissionFieldFromDataSet(DataSet data)
        {
            if (data.Tables[0].Columns.Contains("temp_emission")) return "temp_emission";
            if (data.Tables[0].Columns.Contains("subgrade_emission")) return "subgrade_emission";
            if (data.Tables[0].Columns.Contains("pavement_emission")) return "pavement_emission";
            if (data.Tables[0].Columns.Contains("bridge_emission")) return "bridge_emission";
            if (data.Tables[0].Columns.Contains("tunnel_emission")) return "tunnel_emission";
            if (data.Tables[0].Columns.Contains("crossing_emission")) return "crossing_emission";
            if (data.Tables[0].Columns.Contains("traffic_emission")) return "traffic_emission";
            if (data.Tables[0].Columns.Contains("greening_emission")) return "greening_emission";
            if (data.Tables[0].Columns.Contains("other_emission")) return "other_emission";
            return "total_emission"; //这一行应该不要，2025.09.20
        }

        private string GetQuantityFieldFromDataSet(DataSet data)
        {
            if (data.Tables[0].Columns.Contains("temp_project")) return "temp_project";
            if (data.Tables[0].Columns.Contains("subgrade_project")) return "subgrade_project";
            if (data.Tables[0].Columns.Contains("pavement_project")) return "pavement_project";
            if (data.Tables[0].Columns.Contains("bridge_project")) return "bridge_project";
            if (data.Tables[0].Columns.Contains("tunnel_project")) return "tunnel_project";
            if (data.Tables[0].Columns.Contains("crossing_project")) return "crossing_project";
            if (data.Tables[0].Columns.Contains("traffic_project")) return "traffic_project";
            if (data.Tables[0].Columns.Contains("greening_project")) return "greening_project";
            if (data.Tables[0].Columns.Contains("other_project")) return "other_project";
            return "total_quantity"; //这一行应该不要，2025.09.20
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

        /// <summary>
        /// 以下测试使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTestSum_Click(object sender, EventArgs e)
        {
            int selectTableid;
            if (!int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }

            StringBuilder result = new StringBuilder();
            result.AppendLine("=== 总和测试结果 ===");

            double totalUsage = resultInfoBll.GetTotalUsageQuantity(selectTableid);
            result.AppendLine("总使用量: " + totalUsage);
            result.AppendLine();

            string[] unitProjects = {
                "临时工程", "路基工程", "路面工程", "桥涵工程", 
                "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
            };

            foreach (string unit in unitProjects)
            {
                double materialSum = resultInfoBll.GetTotalMaterialQuantityByUnit(selectTableid, unit);
                double machineSum = resultInfoBll.GetTotalMachineQuantityByUnit(selectTableid, unit);

                result.AppendLine("【" + unit + "】");
                result.AppendLine("材料总使用量: " + materialSum);
                result.AppendLine("机械总使用量: " + machineSum);
                result.AppendLine();

                System.Diagnostics.Debug.WriteLine(unit + ": 材料=" + materialSum + ", 机械=" + machineSum);
            }

            Label2.Text = result.ToString().Replace(Environment.NewLine, "<br/>");
            Label2.Visible = true;
            MessageBox.Show(this, result.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectTableid = 1;
            Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Bold);
            Brush textBrush = Brushes.Blue;
            Bitmap bm = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bm);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            graphics.DrawRectangle(new Pen(Color.Red, 5), 50.0f, 50.0f, 500.0f, 500.0f);
            graphics.DrawString("16.69%", percentFont, textBrush, 410.396973f, 172.226852f);
            graphics.DrawString("16.69%", percentFont, textBrush, 100.396973f, 172.226852f);
            graphics.DrawString("16.69%", percentFont, textBrush, 200.396973f, 172.226852f);

            string targetFile = ".\\UpFile\\" + selectTableid + "Test.png";
            bm.Save(Server.MapPath(targetFile), System.Drawing.Imaging.ImageFormat.Png);
            Image1.ImageUrl = targetFile;
            bm.Dispose();
            graphics.Dispose();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {

        }
    }
}