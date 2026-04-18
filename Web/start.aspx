<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="start.aspx.cs" Inherits="CEMM.Web.sgf.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>界面导航</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f8fcf7;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            text-align: center;
            padding: 30px;
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            width: 100%;
            /* 添加以下代码确保居中 */
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
        h1 {
            color: #2e7d32;
            margin-bottom: 30px;
        }
        .nav-buttons {
            display: flex;
            flex-direction: column;
            gap: 20px;
        }
        .nav-button {
            position: relative;
            padding: 15px 30px;
            font-size: 18px;
            font-weight: 600;
            border: none;
            border-radius: 8px;
            color: white;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            text-align: center;
            width: 100%;
        }
        .nav-button i {
            position: absolute;
            left: 20px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 24px;
        }
        .btn-import {
            background: linear-gradient(to right, #1b5e20, #2e7d32);
        }
        .btn-import:hover {
            background: linear-gradient(to right, #15521b, #1f6824);
            transform: translateY(-3px);
            box-shadow: 0 6px 15px rgba(46, 125, 50, 0.4);
        }
        .btn-calculate {
            top: 0px;
            left: 0px;
            background: linear-gradient(to right, #1565c0, #0d47a1) !important;
        }
        .btn-calculate:hover {
            background: linear-gradient(to right, #11539a, #0a3a7a);
            transform: translateY(-3px);
            box-shadow: 0 6px 15px rgba(13, 71, 161, 0.4);
        }
        /* 给不同按钮设置不同的图标 */
        .btn-import::before {
            content: "\f1c3"; /* Font Awesome Excel 图标 */
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            margin-right: 10px;
        }

        .btn-calculate::before {
            content: "\f542"; /* Font Awesome 项目图标 */
            font-family: "Font Awesome 6 Free";
            font-weight: 900;
            margin-right: 10px;
        }
        .description {
            margin-top: 10px;
            font-size: 14px;
            color: #666;
            text-align: left;
            padding: 0 10px;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1><i class="fas fa-route"></i> 辽宁省高速公路项目施工期碳排放管理系统</h1>
            <div class="nav-buttons">
                <asp:Button ID="btnImport" runat="server" Text="Excel导入计算" 
                    CssClass="nav-button btn-import" 
                    PostBackUrl="~/sgf/WebTest0613.aspx" BackColor="#009933" />
                <div class="description">通过Excel文件导入数据，批量计算碳排放总量</div>
                
                <asp:Button ID="btnCalculate" runat="server" Text="根据项目表计算" 
                    CssClass="nav-button btn-calculate" 
                    PostBackUrl="~/sgf/WebForm1.aspx" BackColor="#009933" />
                <div class="description">选择具体工程项目表，按项计算碳排放量</div>

                <asp:Button ID="btnCEAnalysis" runat="server" Text="碳排放可视分析" 
                    CssClass="nav-button btn-import" 
                    PostBackUrl="~/sgf/CEmisAnalysis.aspx" BackColor="#009933" />
                <div class="description">碳排放计算结果可视分析</div>
                 <asp:Button ID="btnmachineCEFactor2" runat="server" Text="数据维护管理" 
                    CssClass="nav-button btn-calculate" 
                    PostBackUrl="~/sgf/sqlserver.aspx" BackColor="#009933" />
                <div class="description">数据库的更新维护</div>

            </div>
        </div>
    </form>
</body>
</html>