<%@ Page Title="利用已有数据计算" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CEMM.Web.sgf.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>利用已有数据计算</title>
    <link href="../Style/Style.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <style type="text/css">
        .body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: black;
            background-color: #f8fcf7;
            line-height: 1.6;
        }
        .container {
            max-height: 1200px;
            margin:0 auto;
            padding: 5px 20px;
        }
      
        .breadcrumb {
            background-color: rgba(46, 125, 50, 0.1);
            padding: 12px 20px;
            border-radius: 8px;
            font-size: 15px;
            margin-bottom: 0px;/* 减小间距 */
            color: #2e7d32;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
            gap: 10px;
}

          .breadcrumb a {
                color: #2e7d32;
                text-decoration: none;
                transition: all 0.3s ease;
                font-weight: 500;
            }

            .breadcrumb a:hover {
                color: #1b5e20;
                text-decoration: underline;
            }

            .breadcrumb .current {
                color: #1b5e20;
                font-weight: 600;
            }

            .breadcrumb .divider {
                color: rgba(46, 125, 50, 0.6);
            }

            .fa-home {
                margin-right: 5px;
                font-size: 16px;
            }

        .divTbg {
            background: #2e7d32;
            border-radius: 12px;
            padding: 0;
            margin-bottom: 0.4px;/* 减小间距 */
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            margin-top:10px;
        }
        .divTbg1{
            background: #2e7d32;
            border-radius: 12px;
            padding: 0;
            margin-bottom: 0.4px;/* 减小间距 */
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            margin-top:10px;

        }
        .divTbg:hover,.divTbg1:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
        }
        
        .divTbgInfo,.divTbgInfo1 {
            padding: 10px 25px;/* 减小内边距 */
            display: flex;
            align-items: center;
            gap: 15px;
            background: rgba(255, 255, 255, 0.08);
        }
        
        .divTbgTitle,.divTbgTitle1 {
            color: white;
            font-size: 15px;
            font-weight: 600;
            letter-spacing: 0.5px;
            flex-grow: 1;
        }
        .content {
            padding: 10px;/* 减小内边距 */
        }
        
        .section {
            background: white;
            border-radius: 12px;
            padding: 3px; /* 减小内边距 */
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.05);
            margin-bottom: 5px; /* 减小间距 */
            width: 1145px;
            display: flex; /* 添加flex布局 */
            align-items: center; /* 垂直居中 */
            justify-content: center; /* 水平居中 */
            margin-top: 5px;
            height: 40px;
            margin-left: auto;
            margin-right: auto;
            
        }
        .section:first-of-type {
            margin-top: 8px;
        }

        .section table {
            width: 100%;
            height: 5%; /* 确保表格填充容器 */
        }
        .section table tr {
            display: flex;
            align-items: center; /* 行内内容垂直居中 */
            height: 100%; /* 行填充容器高度 */
        }
        .section table td {
            display: flex;
            align-items: center; /* 单元格内容垂直居中 */
            height: 100%; /* 单元格填充行高度 */
        }
.search-button, .filter-button ,.calculator-button{
        border-style: none;
            border-color: inherit;
            border-width: medium;
            position: relative; /* 为内部图标定位做准备 */
            color: white;
            padding: 8px 20px 8px 40px; /* 左侧留出图标空间 *//* 减小内边距 */
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 10px rgba(46, 125, 50, 0.4);
            white-space: nowrap;
            min-width: 110px; /* 适应"确定筛选"文字 */
            height: auto;
            text-align: center;
            display: inline-block;
            align-items: center; /* 按钮内容垂直居中 */
            justify-content: center;
            top: 0px;
            left: 0px;
            background: linear-gradient(to right, #1b5e20, #2e7d32);
        }

        .calculator-button:hover {
            
            box-shadow: 0 6px 15px rgba(46, 125, 50, 0.5);
            transform: translateY(-2px);
        }
.search-button:hover {
            background: linear-gradient(to right, #1b5e20, #2e7d32);box-shadow: 0 6px 15px rgba(46, 125, 50, 0.5);
           transform: translateY(-2px);

}

.filter-button:hover {
    
    box-shadow: 0 6px 15px rgba(46, 125, 50, 0.5);
    transform: translateY(-2px);
}
.search-icon, .filter-icon ,.calculator-icon{
    position: absolute;
    left: 15px; /* 调整位置与按钮内边距匹配 */
    top: 50%;
    transform: translateY(-50%);
    color: white;
    font-weight: bold;
    pointer-events: none;
    font-size: 1.3rem;/* 稍微减小图标大小 */
     z-index: 2; /* 确保图标在按钮上方 */
   
}
.search-button-container, 
.filter-button-container,
.calculator-button-container { 
    position: relative;
    display: inline-block;
    z-index: 1; /* 添加z-index确保图标在按钮上方 */
}
.button-container {
            min-width: 120px; /* 统一按钮容器宽度 */
        }
.uniform-control {
    /* 统一宽度 */
    /* 统一高度 */
    padding: 3px 12px;/* 减小内边距 */
    font-size: 0.9rem;/* 稍微减小字号 */
    border: 1px solid #ddd;
    border-radius: 6px;
    box-sizing: border-box;
    transition: border-color 0.3s ease;
    background-color: #fff;
    height: 32px
}

.uniform-control:focus {
    border-color: #4a90e2;
    outline: none;
    box-shadow: 0 0 0 2px rgba(74, 144, 226, 0.2);
}

/* 下拉列表特定样式 */
.dropdown-control {
    appearance: none; /* 移除默认样式 */
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' fill='%23666' viewBox='0 0 24 24'%3E%3Cpath d='M7 10l5 5 5-5z'/%3E%3C/svg%3E");
    background-repeat: no-repeat;
    background-position: right 15px center;
    background-size:30px 30px; 
    padding-right: 35px; /* 为下拉箭头留出空间 */
}
/* 添加悬停效果 */
.uniform-control:hover {
    border-color: #bbb;
}

/* 添加占位符样式 */
.uniform-control::placeholder {
    color: #999;
    font-style: italic;
}

/* 下拉列表选项样式 */
.dropdown-control option {
    padding: 8px 15px;
}

/* 为下拉列表添加悬停效果 */
.dropdown-control:hover {
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' fill='%23444' viewBox='0 0 24 24'%3E%3Cpath d='M7 10l5 5 5-5z'/%3E%3C/svg%3E");
}



.gridview{
   
            background: white;
            border-radius: 12px;
            padding: 15px; /* 减小内边距 */
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.05);
            margin-bottom: 2px; /* 减小间距 */
            width: 1145px;
            display: flex; /* 添加flex布局 */
            align-items: center; /* 垂直居中 */
            justify-content: center; /* 水平居中 */
            margin: 0 auto;
            margin-top: 10px;
            font-size: 1.1rem;
        }


/* 表头特别处理 */
.gridview th {
    font-size: 1.1rem;
    font-weight: 600;/*加粗*/
}
/*表格内字体*/
.gridview td {
    font-size: 1rem; /* 内容字体大小 */
}
/* 输入框文字 */
.gridview .construction-input {
    font-size: 1rem;
    padding: 8px 10px; /* 同时调大输入框内边距以适应更大字体 */
}
.form-control {
    width: 90%;
    padding: 8px 12px;
    border: 1px solid #ddd;
    border-radius: 4px;
    box-sizing: border-box;
    margin: 0 auto; /* 添加居中 */
    display: block;/* 添加块级显示 */
}
        .style3
        {
            width: 39%;
        }
        .auto-style2 {
            width: 15%;
            height: 37px;
        }
        .auto-style3 {
            width: 15%;
            height: 48px;
        }
        .divTbgTitle {
            width: 1179px;
            height: 15px;
        }
        .auto-style7 {
            width: 30%;
            height: 100%;
            display: flex; 
            align-items: center; 
            justify-content: flex-start;
        }
        .auto-style11 {
            width: 1px;
            height: 86px;
        }
        .auto-style12 {
            height: 86px;
            align-items: center; /* 内容垂直居中 */
        }
        .auto-style15 {
            width: 20%;
            height: 48px;
        }
                 
        
        

        
/*请输入查询内容*/
        /* 统一所有信息标签的样式 */
.uniform-info-label {
    min-width: 200px;
    height: 48px;
    padding: 5px 5px;
    font-size: 15px;
    font-family: 'Microsoft YaHei', sans-serif;
    color: #2d3748;
    background: #f0f7f1;
    border-radius: 10px;
    box-shadow: 0 3px 8px rgba(0, 0, 0, 0.05);
    display: flex;
    align-items: center;
    gap: 12px;
}
.empty-value {
    font-size: 15px; /* 放大字体 */
    font-weight:normal; /* 加粗 */
    color: #666; /* 灰色文字更明显 */
    display: flex; /* 启用Flex布局 */
    text-align: left;
}
    /* 填充父容器高度 */
        
        .auto-style22 {
            min-width: 200px;
            height: 48px;
            padding: 5px 10px;
            font-size: 15px;
            font-family: 'Microsoft YaHei', sans-serif;
            color: #2d3748;
            background: #f0f7f1;
            border-radius: 10px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
            gap: 12px;
            width: 105px;
        }
        .auto-style23 {
            width: 28%;
            height: 22px;
            display: flex;
            align-items: center;
            justify-content: flex-start;
        }
        .auto-style24 {
            width: 38%;
            height: 48px;
        }
        .auto-style25 {
            width: 16%;
            height: 48px;
        }
        .auto-style26 {
            min-width: 200px;
            height: 48px;
            padding: 5px 5px;
            font-size:15px;
            font-family: 'Microsoft YaHei', sans-serif;
            color: #2d3748;
            background: #f0f7f1;
            border-radius: 10px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
            gap: 12px;
            width: 104px;
        }
        .auto-style27 {
            min-width: 200px;
            height: 48px;
            padding: 5px 5px;
            font-size: 15px;
            font-family: 'Microsoft YaHei', sans-serif;
            color: #2d3748;
            background: #f0f7f1;
            border-radius: 10px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
            gap: 12px;
            width: 209px;
        }
        .auto-style28 {
            min-width: 200px;
            height: 100%;
            padding: 5px 5px;
            font-size: 15px;
            font-family: 'Microsoft YaHei', sans-serif;
            color: #2d3748;
            background: #f0f7f1;
            border-radius: 10px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
            gap: 12px;
            width: 527px;
            justify-content: center;
        }
        .SkyButtonFocus {
            margin-left: 26px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="breadcrumb">
             <a href="#"><i class="fas fa-home"></i> 碳排放管理</a>
            <span class="divider">></span>
            <a href="#">碳排计算</a>
        </div>
    </div>
    
    
   
 <div class="divTbg">
                <div class="divTbgInfo">
            <div class="divTbgTitle1">
                <i class="fas fa-search"></i> 查询筛选</div>
                </div>
      </div>
  
                   
                  
 <div class="section">              
         <table style="width: 100%;" cellpadding="2" cellspacing="1" >
        <tr>
            <td align="left" class="auto-style26">
                <i class="fas fa-filter"></i> 请输入查询内容：</td>
           <td width="20%" class="auto-style15">
               <asp:TextBox ID="txtSearch"  runat="server"  CssClass="uniform-control"  MaxLength="20"  placeholder="输入关键词搜索..." Height="37px"></asp:TextBox>
           </td>
            <td class="auto-style3">
                <div class="search-button-container">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="search-button" OnClick="btnSearch_Click" BackColor="#006600"  />
                    <i class="fas fa-search search-icon"></i>
                </div>
            </td>

            <td class="auto-style24">
                <asp:DropDownList ID="DropDownList1" runat="server"  CssClass="uniform-control dropdown-control"  style="margin-bottom: 3px" Height="37px" Width="400px"></asp:DropDownList>
            </td>
            
            <td class="auto-style25">
                <div class="filter-button-container">
                    <asp:Button ID="btnConform" runat="server" Text="确定筛选" CssClass="filter-button" OnClick="btnConform_Click" BackColor="#006600" />
                    <i class="fas fa-filter filter-icon"></i>
                </div>
           </td>
        </tr>
    </table>
    </div>
    <div class="section">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" >
        <tr>
            <td align="left" class="auto-style27">
                 <i class="fas fa-info-circle"></i> 基础工作量信息：
            </td>
            <td class="auto-style23">
                <asp:Label ID="lblBaseQuant" runat="server"  CssClass="empty-value" Width="301px" >         当前无值</asp:Label>
            </td>
           

            <td align="left"class="auto-style22">
                 <label><i class="fas fa-edit"></i> 请输入施工量倍数：</label>
             </td>   
            
            
            
            <td class="auto-style15">
               
                <asp:TextBox ID="TextBox1" runat="server" Width="168px" MaxLength="20" placeholder="1" CssClass="uniform-control" Height="37px" Text="1"></asp:TextBox>
              
            </td>
            <td class="button-container">
                 <div class="calculator-button-container">
                    
                    <asp:Button ID="btnCompute" runat="server" Text="确认计算" CssClass="calculator-button" OnClick="btnCompute_Click" BackColor="#006600"/>
                      <i class="fas fa-calculator calculator-icon"></i>
                    
                </div>
      
                
            </td>
        </tr>
    </table>

    </div>
    <div class ="section"> 
    <table style="width: 100%;" cellpadding="2" cellspacing="1" >
        <tr>
            <td align="left" class="auto-style28">
              <i class="fas fa-chart-line"></i>最终碳排放量：
                </td>
              <td width="20%" class="auto-style7">
                 <asp:Label ID="endquant" runat="server" CssClass="empty-value" Width="301px" >当前无值</asp:Label>
            </td>
          
        </tr>
    </table>
    </div>
    
        <div>
    <div class="divTbg1">
        <div class="divTbgInfo1">
            <div class="divTbgTitle1">
                <i class="fas fa-filter"></i> 施工信息展示</div>
        </div>
    </div>
<div class="gridview">
        <table id="CModuleInfo-corner" style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:GridView ID="gvProjects1" runat="server" 
                        AutoGenerateColumns="False"
                        CssClass="grid-view" 
                        Width="100%" 
                        OnSelectedIndexChanged="gvProjects1_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="选择" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="xzThis" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="subitermid" HeaderText="工程表编号" 
                                 />
                                
                            <asp:BoundField DataField="subitermsrid" HeaderText="子项序号" 
                                />
                                
                            <asp:BoundField DataField="subitermname" HeaderText="子项名称" 
                                 />
                                
                            <asp:BoundField DataField="name" HeaderText="工具/材料名称" 
                                 />
                                
                            <asp:BoundField DataField="toolquant" HeaderText="基数消耗数量" 
                                DataFormatString="{0:N2}"  
                                />
                                
                            <asp:TemplateField HeaderText="施工数量" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConstructionQuant1" runat="server"  
                                        Text=''
                                        CssClass="construction-input" placeholder="输入施工数据...">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle ForeColor="black" />
                        <RowStyle CssClass="grid-row" />
                        <AlternatingRowStyle BackColor="#f8f9fa" />
                        <EmptyDataTemplate>
                            <div class="no-data">暂无工程数据</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
 <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
        <tr>
            <td class="auto-style11">
            </td>
            <td align="left" class="auto-style12">
                <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="SkyButtonFocus" OnClick="btnDelete_Click" />
                <asp:Button ID="btnDelete0" runat="server" Text="返回" CssClass="SkyButtonFocus"  PostBackUrl="~/start.aspx" BackColor="#009933" ForeColor="White"/>
            </td>
        </tr>
    </table>
    </div>


</asp:Content>



