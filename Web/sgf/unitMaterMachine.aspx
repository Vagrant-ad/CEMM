<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unitMaterMachine.aspx.cs" Inherits="CEMM.Web.sgf.unitMaterMachine" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>单位工程主要材料、机械碳排放统计</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', 'Microsoft YaHei', sans-serif;
        }
        
        body {
            background: #f5fbf8;
            color: #333;
            line-height: 1.6;
            padding: 20px;
            min-height: 100vh;
            overflow-x: auto; /*2025.09.20*/
            /*overflow-x: hidden;  防止水平滚动 */
        }
        
        .container {
            min-width: 1860px; /*图是固定宽度，设置min-width与图的宽度一致，在小屏幕上右上角就不会缺角*/
            max-width: 1861px;
            margin: 0 auto;
            background-color: white;
            border-radius: 16px;
            box-shadow: 0 4px 20px rgba(0, 100, 80, 0.1);
            padding-bottom: 20px;
            /*overflow: hidden;  改为hidden防止溢出 */
        }
        
        .header {
            background: linear-gradient(90deg, #4CAF50 0%, #81C784 100%);
            color: white;
            padding:12px 20px;
            text-align: center;
            padding-bottom: 20px;
        }
        
        #Label1 {
            font-family: "Microsoft YaHei", "微软雅黑", "SimHei", "黑体", sans-serif;
            font-size: 28px;
            font-weight: 600;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 12px;
            margin: 0 auto;
            width: fit-content;
            line-height: 1.2;
        }
        
        .content {
            padding: 10px  30px;
            /*overflow: hidden;  改为hidden防止溢出 */
        }
        
        .control-panel {
            background-color: #f8fafc;
            border-radius: 12px;
            padding: 15px 25px;
            margin-bottom: 8px;
            border: 1px solid #e2e8f0;
            margin-top: 0px;
        }
        
        .control-row {
            display: flex;
            gap: 20px;
            align-items: flex-end;
            flex-wrap: wrap;
        }
        
        .form-group {
            flex: 1;
            min-width: 60px;
        }
        
        .form-group label {
            display: block;
            margin-bottom: 4px;
            font-weight: 500;
            font-size: 15px;
        }
        
        #ddlTable {
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            padding: 8px 16px;
            font-size: 14px;
            line-height: 1.2;
        }
        
        .button-group {
            display: flex;
            gap: 15px;
            height: 48px;
        }
        
        .button {
            cursor: pointer;
            font-weight: 600;
            min-width: 120px;
            height: 48px;
            background: linear-gradient(90deg, #4CAF50 0%, #81C784 100%);
            color: white;
            border-radius: 8px;
            border: none;
            padding: 0 20px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            font-size: 15px;
        }
        
        .chart-container {
            width: 100%;
            /* overflow-x: auto; 水平滚动 */  /*.chart-wrapper中“overflow-x: auto;”去掉后，加上后是原生滚动条，当.container中设置max-width时会出现chart的内置滚动条*/
            overflow-y: visible;
            border: 1px solid #e2e8f0;
            border-radius: 12px;
            background-color: white;
            padding: 15px;
            margin-top: 5px;
        }
        
        #Label2 {
            font-size: 18px;
            font-weight: 600;
            color: #4CAF50;
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 15px;
            background-color: #f8fafc;
        }
        
        .chart-wrapper {
            width: 100%;
            /*overflow-x: auto;*/ /*加上后是圆角滚动条，当.container中设置max-width时会出现chart的内置滚动条*/
            position: relative;
        }
        
        .chart-inner {
            min-width: 1550px; /* 原来是1555，改大了还可能不好使 */
            position: relative;
            margin: 0 auto; /* 居中显示 */
        }
    
        #Image1 {
            display: block;
            width: auto; /* 原来是1800px，固定宽度与画布一致，后来画布宽度调成了1860 */
            height: auto;
            max-width: 100%; /* 确保图像不会溢出 */
        }
    
        .return-button {
            text-align: center;
            margin-top: 10px;
        }
        
        .button.bar::before {
            content: "\f201";
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            margin-right: 8px;
        }

        .button.pie::before {
            content: "\f200";
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            margin-right: 8px;
        }

        .button.return::before {
            content: "\f060";
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            margin-right: 8px;
        }
        
        /* 筛选控件样式 */
        .filter-input {
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            padding: 8px 12px;
            font-size: 14px;
            margin-top: 8px;
            width: 200px;
        }
        
        .filter-button {
            cursor: pointer;
            background: #4CAF50;
            color: white;
            border: none;
            border-radius: 6px;
            padding: 8px 16px;
            margin-left: 8px;
            font-size: 14px;
        }
        
        .filter-button.clear {
            background: #f44336;
        }
        
        @media (max-width: 900px) {
            .control-row {
                flex-direction: column;
                align-items: stretch;
                gap: 15px;
            }
            
            .button-group {
                width: 100%;
                flex-direction: column;
                height: auto;
            }
            
            .button {
                width: 100%;
            }
            
            #Label1 {
                font-size: 28px;
            }
            
            .filter-input {
                width: 100%;
                margin-top: 10px;
            }
            
            .filter-button {
                margin-top: 10px;
                width: 100%;
                margin-left: 0;
            }
            
            .chart-inner {
                min-width: 100%; /* 在小屏幕上使用100%宽度 */
            }
            
            #Image1 {
                width: 100%; /* 在小屏幕上使用100%宽度 */
            }
        }
        
        /* 滚动条样式 */
        .chart-wrapper::-webkit-scrollbar {
            height: 12px;
        }
    
        .chart-wrapper::-webkit-scrollbar-track {
            background: #f1f1f1;
            border-radius: 6px;
        }
    
        .chart-wrapper::-webkit-scrollbar-thumb {
            background: #c1c1c1;
            border-radius: 6px;
        }
    
        .chart-wrapper::-webkit-scrollbar-thumb:hover {
            background: #a8a8a8;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="container">
            <div class="header">
                <asp:Label ID="Label1" runat="server">
                    <i class="fas fa-leaf"></i>单位工程主要材料、机械碳排放统计
                </asp:Label>
            </div>
            
            <div class="content">
                <div class="control-panel">
                    <div class="control-row">
                        <div class="form-group">
                            <label for="ddlTable"><i class="fas fa-table"></i> 选择分析数据表</label>
                            <asp:DropDownList ID="ddlTable" runat="server" Width="43%" Font-Size="11pt">
                                <asp:ListItem Value="0" Selected="True">请选择数据表</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtFilter" runat="server" CssClass="filter-input" 
                                placeholder="输入关键词..." Height="38px"></asp:TextBox>
                            <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                                CssClass="filter-button" Text="筛选" />
                            <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" 
                                CssClass="filter-button clear" Text="清空" />
                        </div>
                        
                        <div class="button-group">
                            <asp:Button ID="btnBar" runat="server" OnClick="btnBar_Click" CssClass="button bar" Text="柱状图" BackColor="#00CC00" />
                            <asp:Button ID="btnPie" runat="server" OnClick="btnPie_Click" CssClass="button pie" Text="饼状图" BackColor="#00CC00" style="display:none" />
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="button" Text="测试" BackColor="#00CC00"  style="display:none;"/>
                            <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="button return" BackColor="#33CC33"  PostBackUrl="~/sgf/CEmisAnalysis.aspx"/>
                          
                            
                        </div>
                    </div>
                </div>
                
                <div class="chart-container">
                    <asp:Label ID="Label2" runat="server">
                        <i class="fas fa-chart-pie"></i>碳排放分布柱状图（图例位于下方，单位：kg）
                    </asp:Label>
                    <hr />

                    <div class="chart-wrapper">
                        <div class="chart-inner">
                            <asp:Image ID="Image1" runat="server"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>