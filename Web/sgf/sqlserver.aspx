<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sqlserver.aspx.cs" Inherits="CEMM.Web.sgf.sqlserver" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>碳排放统计分析</title>
    <!-- 引入Font Awesome图标库 -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        :root {
            --primary-color: #2e7d32;
            --primary-light: #e8f5e9;
            --primary-lighter: #f8fbf3;
            --accent-color: #388e3c;
            --text-color: #333;
            --card-shadow: 0 3px 10px rgba(0, 0, 0, 0.08);
            --card-hover-shadow: 0 6px 18px rgba(56, 142, 60, 0.15);
        }
        
        body {
            font-family: 'Microsoft YaHei', 'Segoe UI', Arial, sans-serif;
            background-color: #f5faf5;
            margin: 0;
            padding: 0;
            color: var(--text-color);
            line-height: 1.5;
        }
        
        .container {
            max-width: 1000px;
            margin: 30px auto;
            background-color: white;
            padding: 30px;
            border-radius: 14px;
            box-shadow: var(--card-shadow);
            position: relative;
        }
        
        .header {
            color: var(--primary-color);
            text-align: center;
            margin-bottom: 30px;
            font-size: 32px;
            font-weight: 700;
            padding-bottom: 15px;
            position: relative;
            text-shadow: 1px 1px 2px rgba(0,0,0,0.1);
            letter-spacing: 1px;
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 15px;
        }
        
        .header:after {
            content: "";
            position: absolute;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
            width: 120px;
            height: 4px;
            background: linear-gradient(90deg, transparent, var(--accent-color), transparent);
            border-radius: 2px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        
        .title-icon {
            color: var(--accent-color);
            font-size: 36px;
            animation: pulse 2s infinite;
            text-shadow: 0 0 8px rgba(46, 125, 50, 0.3);
        }
        
        @keyframes pulse {
            0% { transform: scale(1); }
            50% { transform: scale(1.1); }
            100% { transform: scale(1); }
        }
        
        /* 两列布局 */
        .options-container {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
        }
        
        .option-card {
            border: none;
            border-radius: 10px;
            padding: 18px;
            background-color: var(--primary-lighter);
            transition: all 0.25s ease;
            cursor: pointer;
            box-shadow: var(--card-shadow);
            display: flex;
            align-items: center;
            min-height: 50px;
            position: relative;
            overflow: hidden;
        }
        
        .option-card:hover {
            transform: translateY(-3px);
            box-shadow: var(--card-hover-shadow);
            background-color: white;
            border: 1px solid #c8e6c9;
        }
        
        .option-card:hover .option-icon {
            transform: scale(1.08);
            background-color: var(--primary-color);
            color: white;
        }
       
        .option-title {
            font-weight: 600;
            color: var(--primary-color);
            margin: 0;
            font-size: 20px;
            line-height: 1.4;
            transition: all 0.25s ease;
            letter-spacing: 0.3px;
        }
        
        .option-card:hover .option-title {
            color: var(--accent-color);
        }
        
        .option-icon {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 42px;
            height: 42px;
            background-color: white;
            border-radius: 50%;
            margin-right: 18px;
            color: var(--primary-color);
            font-size: 20px;
            font-weight: bold;
            flex-shrink: 0;
            transition: all 0.25s ease;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
        }
        
        .option-card:before {
            content: "";
            position: absolute;
            top: 0;
            left: 0;
            width: 4px;
            height: 0;
            background: var(--accent-color);
            transition: height 0.3s ease;
        }
        
        .option-card:hover:before {
            height: 100%;
        }

        /* 返回按钮样式 */
        .back-button {
            display: inline-block;
            margin-top: 30px;
            padding: 12px 24px;
            background-color: var(--primary-color);
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.25s ease;
            box-shadow: var(--card-shadow);
            text-decoration: none;
        }

        .back-button:hover {
            background-color: var(--accent-color);
            transform: translateY(-2px);
            box-shadow: var(--card-hover-shadow);
        }

        .back-button i {
            margin-right: 8px;
        }
        
        @media (max-width: 900px) {
            .options-container {
                grid-template-columns: 1fr;
            }
            
            .container {
                padding: 25px;
                margin: 20px;
            }
            
            .header {
                font-size: 28px;
                flex-direction: column;
                gap: 10px;
            }
            
            .title-icon {
                font-size: 32px;
            }
        }
        
        @media (max-width: 480px) {
            .option-card {
                padding: 18px;
                min-height: 70px;
            }
            
            .option-icon {
                margin-right: 15px;
                width: 38px;
                height: 38px;
                font-size: 18px;
            }
            
            .option-title {
                font-size: 18px;
            }
            
            .header {
                font-size: 24px;
            }
            
            .title-icon {
                font-size: 28px;
            }

            .back-button {
                padding: 10px 20px;
                font-size: 15px;
            }
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="container">
            <div class="header">
                <i class="fas fa-chart-line title-icon"></i>
                <span>数据维护管理</span>
                <i class="fas fa-leaf title-icon"></i>
            </div>
            
            <div class="options-container">
                <!-- machineCEFactor2 -->
                <div class="option-card" onclick="window.location.href='machineCEFactor2cz.aspx'">
                    <span class="option-icon">1</span>
                    <div>
                        <div class="option-title">碳排放因子数据库维护管理</div>
                    </div>
                </div>
               
                
            </div>

            <!-- 返回按钮 -->
            <div style="text-align: center;">
                <a id="A1" href="~/start.aspx" runat="server" class="back-button">
                    <i class="fas fa-arrow-left"></i>返回首页
                </a>
            </div>
        </div>
    </form>
</body>
</html>
