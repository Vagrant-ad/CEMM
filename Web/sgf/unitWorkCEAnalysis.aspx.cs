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


namespace CEMM.Web.sgf
{
    public partial class unitWorkCEAnalysis : System.Web.UI.Page
    {
        CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
        CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();
        
        int width = 1160, height = 720;//绘图的区域大小
                
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //MessageBox.Show(this, filePath); //test use
                if ((File.Exists(filePath)))
                {
                    targetFile = ".\\UpFile\\" + selectTableid + suffix;
                    //Label2.Text = targetFile;  //test use
                    //MessageBox.Show(this, "直接加载");  //test use
                    Image1.ImageUrl = string.Format("{0}?t={1}", targetFile, DateTime.Now.Ticks);  //避免取缓存数据
                    return;
                }

                resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
            }
            else
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }

            //if (int.TryParse(ddlTable.SelectedValue, out selectTableid))
            //{
            //    resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
            //}
            //else
            //{
            //    MessageBox.Show(this, "tableid数据错误！");
            //    return;
            //}

            Bitmap bm = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bm);
            // 设置背景色为白色
            graphics.Clear(Color.White);
            // 设置高质量绘图
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double[] quantities = new double[9]{0,0,0,0,0,0,0,0,0};
            //double[] quantitiesTest = new double[2] {0, 0};
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
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            string[] labels = {
                "临时工程", "路基工程", "路面工程", "桥涵工程", 
                "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
            };
            //string[] labelsTest = {
            //    "临时工程", "路基工程" };

            // 定义饼图绘制区域
            Rectangle pieRect = new Rectangle(160, 60, 560, 560);
            // 调用绘制饼图函数
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
            Image1.ImageUrl = string.Format("{0}?t={1}", targetFile, DateTime.Now.Ticks);  //避免取缓存数据
            bm.Dispose();
            graphics.Dispose();
        }

        private void DrawPieChart(Graphics graphics, double[] values, string[] labels, Rectangle area)
        {
            if (values == null || labels == null || values.Length != labels.Length)
            {
                //MessageBox.Show(this, "DrawPieChart参数数据无效!");
                //return;
                throw new ArgumentException("DrawPieChart参数数据无效");
            }
                
            // 计算总和
            double total = 0;
            double[] percents = new double[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 
            
            foreach (double value in values) total += value;
            if (total == 0) return;
            // 预定义颜色
            Color[] colors = {
                Color.FromArgb(255, 128, 128),    // 临时工程
                Color.FromArgb(255, 255, 0),     // 路基工程
                Color.FromArgb(255, 128, 64),     // 路面工程
                Color.FromArgb(0, 255, 255),     // 隧道工程
                Color.FromArgb(0, 128, 192),    // 桥涵工程
                Color.FromArgb(255, 0, 255),     // 交叉工程
                Color.FromArgb(128, 64, 64),    // 交通工程
                Color.FromArgb(0, 255, 0),    // 绿化环保工程
                Color.FromArgb(128, 0, 128)     // 其他工程
            };
            // 创建新的字体资源（避免使用已释放的资源）
            using (Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Bold))
            using (Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold))
            {
                // 绘制饼图
                float startAngle = 0;
                Brush textBrush = Brushes.White;

                for (int i = 0; i < values.Length; i++)
                {
                    percents[i] = values[i] / total;
                    // 计算当前扇形的角度
                    float sweepAngle = (float)(360 * percents[i]);
                    // 绘制扇形
                    using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                    {
                        graphics.FillPie(brush, area, startAngle - 90, sweepAngle); //调整90度使0°位于顶部
                    }
                    // 绘制边框
                    graphics.DrawPie(Pens.Black, area, startAngle - 90, sweepAngle); //调整90度使0°位于顶部
                    // 添加百分比标签
                    AddPercentageLabel(graphics, percents[i], total, startAngle, sweepAngle, area, percentFont, textBrush);
                    startAngle += sweepAngle;
                }
                // 绘制图例
                //DrawLegend(graphics, labels, colors, values, total, new Point(area.Right + 20, area.Top)); //原来的
                DrawLegend(graphics, labels, colors, percents, total, new Point(area.Right + 40, area.Top + 20));
                // 添加图标题
                string titleStr = "单位工程碳排放统计";
                SizeF titleSize = graphics.MeasureString(titleStr, titleFont);
                graphics.DrawString(titleStr, titleFont, Brushes.DarkBlue, new PointF(area.Left + (area.Width - titleSize.Width) / 2, 10));
                //graphics.DrawString("单位工程碳排放统计", titleFont, Brushes.DarkBlue, new PointF(area.Left + 170, 10));
            }

        }
        private void AddPercentageLabel(Graphics graphics, double percent, double total,
                                       float startAngle, float sweepAngle, Rectangle area, Font percentFont, Brush textBrush)
        {
            try
            {
                // 计算百分比
                double percentage = percent;  //percentage = value / total * 100;
                string percentText = String.Format("{0:P2}", percentage);
                // 计算标签位置（扇形中心）
                float centerX = area.X + area.Width / 2;
                float centerY = area.Y + area.Height / 2;
                float radius = Math.Min(area.Width, area.Height) / 3;
                float midAngle = startAngle + sweepAngle / 2;
                // 将角度转换为弧度（调整90度使0°位于顶部）
                double radian = (midAngle - 90) * (Math.PI / 180);
                radian += 2 * Math.PI; // 加上2π
                radian %= 2 * Math.PI; // 取模，确保结果在0到2π之间
                float labelX = centerX + (float)(radius * Math.Cos(radian));
                float labelY = centerY + (float)(radius * Math.Sin(radian));
                // 测量文本大小
                SizeF textSize = graphics.MeasureString(percentText, percentFont);
                // 调整位置使文本居中
                labelX -= textSize.Width / 2;
                labelY -= textSize.Height / 2;
                if (sweepAngle > 10)  //角度大于10的才显示，小于10的不显示
                {
                    // 为标签添加背景增强可读性
                    RectangleF backgroundRect = new RectangleF(labelX - 2, labelY - 1, textSize.Width + 4, textSize.Height + 2);
                    // 绘制半透明背景
                    using (Brush bgBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                    {
                        graphics.FillEllipse(bgBrush, backgroundRect);
                    }
                    // 绘制百分比文本
                    graphics.DrawString(percentText, percentFont, textBrush, labelX, labelY); 
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
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
                
                using(Font labelFont = new Font("Microsoft YaHei", 10))
                using (Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Italic))
                {
                    Font titleFont = new Font("Microsoft YaHei", 12, FontStyle.Bold);
                    Brush textBrush = Brushes.Black;
                    for (int i = 0; i < labels.Length; i++)
                    {
                        // 计算百分比
                        double percentage = percents[i]; //如果将参数double[] percents改为double[] values，则 percentage = values[i] / total * 100;
                        string percentText = String.Format("{0:P2}", percentage);
                        // 绘制颜色框
                        Rectangle colorRect = new Rectangle(startPoint.X, startPoint.Y + i * spacing, boxSize, boxSize);
                        using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                        {
                            graphics.FillRectangle(brush, colorRect);
                        }
                        graphics.DrawRectangle(Pens.Black, colorRect);
                        // 绘制标签
                        PointF textPoint = new PointF(startPoint.X + boxSize + 10, startPoint.Y + i * spacing);
                        graphics.DrawString(labels[i], labelFont, textBrush, textPoint);
                        // 绘制百分比
                        PointF percentPoint = new PointF(startPoint.X + boxSize + 150, startPoint.Y + i * spacing);
                        if (percentage < 0.028) //10/360=0.028,角度小于10的红色显示
                            graphics.DrawString(percentText, percentFont, Brushes.Red, percentPoint);
                        else
                            graphics.DrawString(percentText, percentFont, Brushes.Gray, percentPoint);
                    }
                    // 添加标题
                    graphics.DrawString("工程类型", titleFont, Brushes.DarkBlue, startPoint.X, startPoint.Y - 30);
                    graphics.DrawString("占比", titleFont, Brushes.DarkBlue, startPoint.X + 150 + 20, startPoint.Y - 30);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                throw new ArgumentException("DrawLegend：" + ex.ToString());
            }
        }
        /// <summary>
        /// 绘制柱状图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnBar_Click(object sender, EventArgs e)
        {
            CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
            int selectTableid;
            //string targetFile;

            if (int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                resultInfoMdl = resultInfoBll.GetModel2("tpfzj", selectTableid);
            }
            else
            {
                MessageBox.Show(this, "请选择正确的数据表！");
                return;
            }
            // 工程碳排放量
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
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            // 工程名称
            string[] labels = {
                "临时工程", "路基工程", "路面工程", "桥涵工程", 
                "隧道工程", "交叉工程", "交通工程", "绿化环保工程", "其他工程"
            };

            try
            {
                // 调用绘图函数
                byte[] chartBytes = DrawBarChart(labels, quantities);

                // 将图像显示在Image控件中
                //Image1.Visible = true;
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(chartBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }
        
        private byte[] DrawBarChart(string[] labels, double[] quantities)
        {
            // 1. 计算总量和百分比
            double total = 0;
            foreach (double q in quantities) total += q;
            double[] percentages = new double[quantities.Length];
            for (int i = 0; i < quantities.Length; i++)
            {
                percentages[i] = Math.Round(quantities[i] * 100.0 / total, 2);
            }

            // 2. 设置画布尺寸
            //int width = 900;
            //int height = 600;
            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 3. 设置背景和抗锯齿
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // 4. 设置绘图区域边距
                int margin = 40;
                int chartWidth = width - 2 * margin;
                int chartHeight = height - 2 * margin - 120 - margin / 2; //-120底部留出图例空间,(- margin / 2)留出顶部图标题的空间
                int barMaxHeight = chartHeight;  //chartHeight-60;如果调整柱形的高度，在此调整

                // 5. 定义颜色
                //Color[] colors = {
                //    Color.RoyalBlue, Color.DarkGray, Color.DarkOrange, Color.Purple,
                //    Color.Teal, Color.Crimson, Color.Gold, Color.LimeGreen, Color.DeepPink };
                Color[] colors = {
                
                Color.FromArgb(164, 122, 0),     //aa，浅卡奇色
                Color.FromArgb(189, 165, 173),     // 浅灰
                Color.FromArgb(85, 65, 72),     // 紫褐色
                Color.FromArgb(0, 156, 149),     //bb，蓝绿浅
                Color.FromArgb(0, 102, 198),     //bb，蓝绿深
                Color.FromArgb(235, 47, 142),    // 粉色
                Color.FromArgb(115, 172, 255),  //cc,中蓝色
                Color.FromArgb(5, 150, 0),     // 绿色  （第8项绿色交通，必须为绿色）
                Color.FromArgb(251, 235, 0),    // 金黄色

                Color.FromArgb(175, 227, 255),  //cc,淡蓝色
                Color.FromArgb(108, 73, 0),     //aa，深卡奇色
                Color.FromArgb(0, 140, 255),     // 蓝色            
                Color.FromArgb(113, 0, 46),     //酱紫色
                                               
                //Color.FromArgb(255, 255, 0),    // 黄色
                Color.FromArgb(255, 128, 64),     // 黄和粉之间
                Color.FromArgb(128, 128, 0),    // 墨绿和黑色之间
                Color.FromArgb(0, 64, 0),     // 墨绿色
                Color.FromArgb(128, 128, 128),     // 灰色
                Color.FromArgb(255, 128, 192),     // 淡粉色
                Color.FromArgb(255, 128, 0),    // 桔色            
                Color.FromArgb(128, 0, 128),     // 紫色
            };

                // 6. 绘制柱状图
                int barWidth = chartWidth / (labels.Length * 2); // 柱子宽度
                int spacing = barWidth; // 柱子间距

                // 计算最大高度用于缩放
                double maxQuantity = 0;
                foreach (double q in quantities) if (q > maxQuantity) maxQuantity = q;
                double scale = barMaxHeight / maxQuantity; //原来的：(float)chartHeight / maxQuantity;

                // 绘制每个柱子
                for (int i = 0; i < labels.Length; i++)
                {
                    int barHeight = (int)(quantities[i] * scale);
                    int x = margin + i * (barWidth + spacing);
                    int y = margin + chartHeight - barHeight + margin / 2;//之前只有前3个，最后一个(+ margin/2)为了留出顶部图标题的空间

                    // 绘制柱子
                    using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                    {
                        g.FillRectangle(brush, x, y, barWidth, barHeight);
                        g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);
                    }

                    Font percentFont = new Font("Microsoft YaHei", 10);
                    // 在柱子顶部显示百分比
                    string percentText = percentages[i] + "%";
                    SizeF textSize = g.MeasureString(percentText, percentFont);
                    g.DrawString(percentText, percentFont, Brushes.Black,x + (barWidth - textSize.Width) / 2, y - 20);
                    //在柱子底部显示标签
                    SizeF labelSize = g.MeasureString(labels[i], percentFont);
                    g.DrawString(labels[i], percentFont, Brushes.Black,
                        x + (barWidth - labelSize.Width) / 2, chartHeight + margin + 10 + margin / 2);//(+ margin/2)为了留出顶部图标题的空间
                }

                // 7. 绘制图例
                int legendX = margin;
                int legendY = height - 120;
                int swatchSize = 15;
                int lineHeight = 25;
                Font legenFont = new Font("Microsoft YaHei", 10);

                for (int i = 0; i < labels.Length; i++)
                {
                    // 绘制颜色方块
                    using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                    {
                        g.FillRectangle(brush, legendX, legendY, swatchSize, swatchSize);
                        g.DrawRectangle(Pens.Black, legendX, legendY, swatchSize, swatchSize);
                    }

                    // 绘制工程名称和工程量
                    string legendText = String.Format("{0}：{1:N2}", labels[i], quantities[i]); //N0带千分位，保留0位小数；$"{labels[i]}: {quantities[i]:N0}"；string formatTemplate = "{0}: {1}";如果前面这样定义了，里面第一个参数就写formatTemplate
                    g.DrawString(legendText, legenFont, Brushes.Black, legendX + swatchSize + 5, legendY);

                    /*### 更复杂的格式控制示例：
                     如果需要在数字部分添加格式（比如将数字格式化为两位小数），在字符串插值中可以这样写：
                     ```csharp 6.0以上
                     $"{labels[i]}: {quantities[i]:F2}"
                     ```
                     用`String.Format`实现：
                     ```csharp
                     String.Format("{0}: {1:F2}", labels[i], quantities[i])
                     */

                    // 换行逻辑：每行显示3个图例
                    if ((i + 1) % 3 == 0)
                    {
                        legendX = margin;
                        legendY += lineHeight;
                    }
                    else
                    {
                        legendX += 280; // 每个图例项水平间距
                    }
                }
                // 8. 添加图标题
                Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);
                string titleStr="单位工程碳排放统计";
                SizeF titleSize = g.MeasureString(titleStr, titleFont);
                g.DrawString(titleStr, titleFont, Brushes.DarkBlue, new PointF((chartWidth-titleSize.Width) / 2, 10));

                // 9. 保存图像到内存流
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
        //未使用
        private void CleanupOldImages()
        {
            try
            {
                string tempPath = Server.MapPath("~/TempImages/");
                if (!Directory.Exists(tempPath)) return;

                // 删除超过10分钟的旧图片
                foreach (string file in Directory.GetFiles(tempPath, "chart_*.png"))
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.CreationTime < DateTime.Now.AddMinutes(-10))
                    {
                        fi.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning("清理图片错误: {0}",ex);
            }
        }

        //btnTest
        protected void Button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this, ddlTable.SelectedValue + "," + ddlTable.SelectedItem.Text);

            int selectTableid=1;
            Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Bold);
            Brush textBrush = Brushes.Blue;
            Bitmap bm = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bm);
            // 设置背景色为白色
            graphics.Clear(Color.White);
            // 设置高质量绘图
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

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filterText = txtFilter.Text.Trim();

            if (string.IsNullOrEmpty(filterText))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请输入筛选关键词！');", true);
                return;
            }

            // 老式字符串拼接方式
            string safeFilterText = filterText.Replace("'", "''");
            string whereClause = "tableName LIKE '%" + safeFilterText + "%'";
            DataSet ds = resultTabInfoBll.GetList(whereClause);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                // 简单明了的老式写法
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
                // 老式字符串拼接方式
                string safeFilterText = filterText.Replace("'", "''");
                string whereClause = "tableName LIKE '%" + safeFilterText + "%'";
                ds = resultTabInfoBll.GetList(whereClause);

                // 检查筛选结果
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


    }
}