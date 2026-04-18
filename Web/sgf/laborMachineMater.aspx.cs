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
    public partial class laborMachineMater : System.Web.UI.Page
    {
        CEMM.BLL.computeResultTabInfo resultTabInfoBll = new BLL.computeResultTabInfo();
        CEMM.BLL.computeResultInfo resultInfoBll = new BLL.computeResultInfo();

        int width = 1160, height = 720;//绘图的区域大小
        /*
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
        }*/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTableDropdown(""); // 初始加载所有数据
            }
        }

        /// <summary>
        /// 绘制饼状图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPie_Click(object sender, EventArgs e)
        {
            CEMM.Model.computeResultInfo resultInfoMdl = new Model.computeResultInfo();
            int selectTableid;
            string targetFile, suffix = "pie03LaborMachineMater.png";
            if (int.TryParse(ddlTable.SelectedValue, out selectTableid))
            {
                string folderPath = Server.MapPath(".\\UpFile\\");
                string fileName = selectTableid + suffix;
                string filePath = Path.Combine(folderPath, fileName);
                if ((File.Exists(filePath)))
                {
                    targetFile = ".\\UpFile\\" + selectTableid + suffix;
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
            Bitmap bm = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bm);
            // 设置背景色为白色
            graphics.Clear(Color.White);
            // 设置高质量绘图
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double[] quantities = new double[4] { 0, 0, 0, 0 };
            try
            {
                quantities[0] = resultInfoBll.GetLaborCEmission(selectTableid); //人工&机械工碳排放
                quantities[2] = resultInfoBll.GetMateCEmission(selectTableid); //材料生产碳排放
                quantities[3] = resultInfoBll.GetTransCEmission(selectTableid); //材料运输碳排放
                quantities[1] = Convert.ToDouble(resultInfoMdl.total_emission) - quantities[0] - quantities[2] - quantities[3]; //施工机械碳排放
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
            string[] labels = { "人工&机械工", "施工机械", "材料生产", "材料运输" };

            // 定义饼图绘制区域
            Rectangle pieRect = new Rectangle(100, 60, 560, 560);
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
            //Color[] colors = {
            //    Color.FromArgb(0, 180, 0),     // 绿色
            //    Color.FromArgb(0, 0, 255),     // 蓝色
            //    Color.FromArgb(255, 0, 128),    // 粉色
            //    Color.FromArgb(128, 64, 64),     // 褐色
            //    Color.FromArgb(255, 128, 0),    // 桔色
            //    Color.FromArgb(128, 0, 128),     // 紫色
            //    Color.FromArgb(0, 128, 255),    // 天蓝色
            //    Color.FromArgb(255, 255, 0),    // 黄色
            //    Color.FromArgb(128, 128, 0),    // 墨绿和黑色之间
            //    Color.FromArgb(255, 128, 64),     // 黄和粉之间
            //    Color.FromArgb(255, 128, 64),     // 墨绿色
            //    Color.FromArgb(128, 128, 128),     // 灰色
            //    Color.FromArgb(255, 128, 192),     // 淡粉色
            //};
            Color[] colors = {
                Color.FromArgb(19, 149, 0),     // 绿色
                Color.FromArgb(0, 139, 255),     // 蓝色
                Color.FromArgb(235, 47, 142),    // 粉色，前三种是一种组合
                Color.FromArgb(251, 235, 0),    // 金黄色

                Color.FromArgb(113, 0, 46),     //酱紫色
                Color.FromArgb(189, 165, 173),     // 浅灰
                Color.FromArgb(164, 122, 0),     //aa，浅卡奇色  
                Color.FromArgb(0, 156, 149),     //bb，蓝绿浅
                Color.FromArgb(175, 227, 255),  //cc,淡蓝色
                Color.FromArgb(108, 73, 0),     //aa，深卡奇色
                Color.FromArgb(0, 102, 198),     //bb，蓝绿深
                Color.FromArgb(115, 172, 255),  //cc,中蓝色
                Color.FromArgb(85, 65, 72),     // 紫褐色
                //Color.FromArgb(255, 255, 0),    // 黄色
                Color.FromArgb(255, 128, 64),     // 黄和粉之间
                Color.FromArgb(128, 128, 0),    // 墨绿和黑色之间
                Color.FromArgb(0, 64, 0),     // 墨绿色
                Color.FromArgb(128, 128, 128),     // 灰色
                Color.FromArgb(255, 128, 192),     // 淡粉色
                Color.FromArgb(255, 128, 0),    // 桔色
                Color.FromArgb(128, 0, 128),     // 紫色
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
                DrawLegend(graphics, labels, colors, values, total, new Point(area.Right + 40, area.Top + 20));
                // 添加图标题
                string titleStr = "人工&机械工、施工机械、材料生产、材料运输的碳排放统计";
                SizeF titleSize = graphics.MeasureString(titleStr, titleFont);
                graphics.DrawString(titleStr, titleFont, Brushes.DarkBlue, new PointF(area.Left + (area.Width - titleSize.Width) / 2, 10));
                //graphics.DrawString("人工&机械工、施工机械、材料生产、材料运输的碳排放统计", titleFont, Brushes.DarkBlue, new PointF(area.Left + 170, 10));
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
                    graphics.DrawString(percentText, percentFont, textBrush, labelX, labelY); //第二次点会出错
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                throw new ArgumentException("AddPercentageLabel：" + ex.ToString());
            }
        }
        private void DrawLegend(Graphics graphics, string[] labels, Color[] colors,
                               double[] values, double total, Point startPoint)
        {
            try
            {
                int boxSize = 20;
                int spacing = 40;

                using (Font labelFont = new Font("Microsoft YaHei", 10))
                using (Font percentFont = new Font("Microsoft YaHei", 10, FontStyle.Italic))
                {
                    Font titleFont = new Font("Microsoft YaHei", 12, FontStyle.Bold);
                    Brush textBrush = Brushes.Black;
                    string legendText;
                    for (int i = 0; i < labels.Length; i++)
                    {
                        // 计算百分比
                        double percentage = values[i] / total; //如果将参数double[] percents改为double[] values，则 percentage = values[i] / total * 100;
                        string percentText = String.Format("{0:P2}", percentage); //上一行不* 100，因为本行是"P2"的格式
                        // 绘制颜色框
                        Rectangle colorRect = new Rectangle(startPoint.X, startPoint.Y + i * spacing, boxSize, boxSize);
                        using (Brush brush = new SolidBrush(colors[i % colors.Length]))
                        {
                            graphics.FillRectangle(brush, colorRect);
                        }
                        graphics.DrawRectangle(Pens.Black, colorRect);
                        // 绘制标签+数值
                        PointF textPoint = new PointF(startPoint.X + boxSize + 10, startPoint.Y + i * spacing);
                        legendText = String.Format("{0}：{1:N2}", labels[i], values[i]); //N2带千分位，保留2位小数；
                        graphics.DrawString(legendText, labelFont, textBrush, textPoint);
                        // 绘制百分比
                        PointF percentPoint = new PointF(startPoint.X + boxSize + 210, startPoint.Y + i * spacing);
                        if (percentage < 0.028) //10/360=0.028,角度小于10的红色显示
                            graphics.DrawString(percentText, percentFont, Brushes.Red, percentPoint);
                        else
                            graphics.DrawString(percentText, percentFont, Brushes.Gray, percentPoint);
                    }
                    // 添加标题
                    graphics.DrawString("工程类型（单位：kg）", titleFont, Brushes.DarkBlue, startPoint.X, startPoint.Y - spacing);
                    graphics.DrawString("占比", titleFont, Brushes.DarkBlue, startPoint.X + 210 + 20, startPoint.Y - spacing);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
                throw new ArgumentException("DrawLegend：" + ex.ToString());
            }
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }


    }
}