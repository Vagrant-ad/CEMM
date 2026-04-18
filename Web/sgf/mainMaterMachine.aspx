<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainMaterMachine.aspx.cs" Inherits="CEMM.Web.sgf.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>主要材料、机械的碳排放量</title>
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
        }
        
        .container {
            min-width: 1500px; /*图是固定宽度，设置min-width与图的宽度一致，在小屏幕上右上角就不会缺角*/
            max-width: 1501px;
            margin: 0 auto;
            background-color: white;
            border-radius: 16px;
            box-shadow: 0 4px 20px rgba(0, 100, 80, 0.1);
            padding-bottom: 20px;
            
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
            width: 70%;
            height: 38px;
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
            overflow-x: auto; /* 水平滚动 */
            overflow-y: visible; 
            background-color: white;
            border-radius: 12px;
            padding: 5px 25px 20px;
            margin-top: 5px;
            border: 1px solid #e2e8f0;
        }
        /*后加的，2025.8.1
          width: 100%;       
          max-width: 100%;    
            */
        
        #Label2 {
            font-size: 18px;
            font-weight: 600;
            color: #4CAF50;
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 15px;
        }
        /*
        #Image1 {
            width: 100%;
            max-width: 100%;
            height: auto; 
            min-height: 400px; 
            background-color: #f8fafc;
            border: 2px dashed #e2e8f0;
            border-radius: 8px;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 10px;
            transition: all 0.3s ease;
            margin-bottom: 10px;
            object-fit: contain; 
        }*/
        /*---后加的，2025.8.1 
          height: auto; ---高度自适应，保持图片比例
          min-height: 400px; ---保留最小高度
          bject-fit: contain; ---确保图片完整显示，不拉伸变形    */
        
        .return-button {
            text-align: center;
            margin-top: 10px;
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
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <asp:Label ID="Label1" runat="server">
                    <i class="fas fa-leaf"></i>主要材料、机械的碳排放量
                </asp:Label>
            </div>
            
            <div class="content">
                <div class="control-panel">
                    <div class="control-row">
                        <div class="form-group">
                            <label for="ddlTable"><i class="fas fa-table"></i> 选择分析数据表</label>
                            <asp:DropDownList ID="ddlTable" runat="server">
                                <asp:ListItem Value="0">请选择数据表</asp:ListItem>
                            </asp:DropDownList>
                                <asp:TextBox ID="txtFilter" runat="server" CssClass="filter-input" 
                                    placeholder="输入关键词..." Height="33px"></asp:TextBox>
                            <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" 
                                CssClass="filter-button" Text="筛选" />
                            <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" 
                                CssClass="filter-button clear" Text="清空" />
                        </div>
                        
                        <div class="button-group">
                            <asp:Button ID="btnPie" runat="server" OnClick="btnPie_Click" CssClass="button pie" BackColor="#33CC33" Text="主要材料机械碳排放" />
                        </div>
                        <div class="return-button">
                            <asp:Button ID="Button1" runat="server" Text="返回" CssClass="button return" BackColor="#33CC33" PostBackUrl="~/sgf/CEmisAnalysis.aspx" />
                        </div>
                    </div>
                </div>
                
                <%--<div class="chart-container">--%>
                    <asp:Label ID="Label2" runat="server">
                        <i class="fas fa-chart-pie"></i>主要材料、机械的碳排放量
                    </asp:Label>
                    <hr />
                    <asp:Image ID="Image1" runat="server" align="center"/>
                <%--</div>--%>

            </div>
        </div>
    </form>
</body>
</html>