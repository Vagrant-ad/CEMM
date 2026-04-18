<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machineCEFactor2cz.aspx.cs" Inherits="CEMM.Web.sgf.machineCEFactor2cz" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>数据库操作</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', 'Microsoft YaHei', sans-serif;
            background-color: #f5f7fa;
            margin: 0;
            padding: 20px;
            color: #333;
        }
        
        .container {
            max-width: 1200px;
            margin: 0 auto;
        }
        
        .page-title {
            color: #2c3e50;
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #eaeaea;
        }
        
        .card {
            background: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
            margin-bottom: 20px;
        }
        
        .search-section {
            display: flex;
            align-items: center;
            gap: 15px;
            margin-bottom: 20px;
        }
        
        .search-input {
            flex-grow: 1;
            padding: 10px 15px;
            border: 1px solid #ddd;
            border-radius: 6px;
            font-size: 14px;
            transition: all 0.3s;
            height: 40px;
            box-sizing: border-box;
        }
        
        .search-input:focus {
            border-color: #4CAF50;
            outline: none;
            box-shadow: 0 0 0 2px rgba(76, 175, 80, 0.2);
        }
        
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            font-size: 14px;
            font-weight: 500;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            position: relative;
            height: 40px;
        }
        
        .btn-primary {
            background-color: #4CAF50;
            color: white;
            min-width: 100px;
            padding-left: 35px;
        }
        
        .btn-primary:hover {
            background-color: #3e8e41;
        }
        
        .btn-secondary {
            background-color: #3498db;
            color: white;
        }
        
        .btn-secondary:hover {
            background-color: #2980b9;
        }
        
        .btn-icon {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 14px;
        }
        
        .file-upload-section {
            display: flex;
            align-items: center;
            gap: 15px;
            margin-top: 20px;
        }
        
        .file-upload {
            flex-grow: 1;
        }
        
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
        }
        
        .grid-view th {
            background-color: #4CAF50;
            color: white;
            padding: 12px 15px;
            text-align: left;
            font-weight: 500;
        }
        
        .grid-view td {
            padding: 12px 15px;
            border-bottom: 1px solid #eaeaea;
        }
        
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        
        .grid-view tr:hover {
            background-color: #f1f1f1;
        }
        
        .grid-view .pager {
            padding: 10px;
            text-align: center;
            background-color: #f5f5f5;
            border-top: 1px solid #ddd;
        }
        
        .grid-view .pager a {
            padding: 5px 10px;
            margin: 0 3px;
            border-radius: 3px;
            color: #333;
            text-decoration: none;
        }
        
        .grid-view .pager a:hover {
            background-color: #e0e0e0;
        }
        
        .grid-view .pager span {
            padding: 5px 10px;
            margin: 0 3px;
            border-radius: 3px;
            background-color: #4CAF50;
            color: white;
        }
        
        .action-buttons {
            display: flex;
            gap: 5px;
        }
        
        .btn-sm {
            padding: 5px 10px;
            font-size: 12px;
            min-width: 0;
        }
        
        .btn-edit {
            background-color: #3498db;
            color: white;
        }
        
        .btn-edit:hover {
            background-color: #2980b9;
        }
        
        .btn-delete {
            background-color: #e74c3c;
            color: white;
        }
        
        .btn-delete:hover {
            background-color: #c0392b;
        }
        
        .no-data {
            text-align: center;
            padding: 20px;
            color: #777;
            font-style: italic;
        }
        
        .status-message {
            padding: 10px 15px;
            border-radius: 4px;
            margin-bottom: 20px;
            display: none;
        }
        
        .status-success {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }
        
        .status-error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
        
        /* 编辑模式样式 */
        .edit-mode input[type="text"] {
            width: 90%;
            padding: 5px;
            border: 1px solid #ddd;
            border-radius: 3px;
        }
        
        .edit-mode input[type="text"]:focus {
            border-color: #3498db;
            outline: none;
        }
        
        /* 命令按钮样式 */
        .command-buttons {
            display: flex;
            gap: 5px;
        }
        
        .command-buttons .btn {
            min-width: 60px;
            padding: 5px 10px;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="container">
            <h1 class="page-title">
                <i class="fas fa-database" style="margin-right: 10px;"></i>数据库操作
            </h1>
            
            <asp:Panel ID="pnlMessage" runat="server" CssClass="status-message" Visible="false">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </asp:Panel>
            
            <div class="card">
                <div class="search-section">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" 
                        placeholder="输入关键词搜索..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" 
                        OnClick="btnSearch_Click" Text="查  询" />
                    <i class="fas fa-search btn-icon"></i>
                </div>
                
                <div class="file-upload-section">
                    <asp:Button ID="btnDownloadTemplate" runat="server" 
                        CssClass="btn btn-secondary" 
                        OnClick="btnDownloadTemplate_Click" 
                        Text="下载模板" />
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="file-upload" accept=".csv" />
                   
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-secondary" 
                        OnClick="Button1_Click" Text="导入CSV文件" />
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-secondary" 
                        OnClick="Button4_Click" Text="未导入成功记录下载" />
                &nbsp;<asp:Button ID="Button2" runat="server" Text="返  回" style="margin-left: 29px"   BackColor="#009933" ForeColor="White" OnClick="Button2_Click"/>
                </div>
            </div>
            
            <div class="card">
                <asp:GridView ID="gvProjects1" runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="grid-view" 
                    OnSelectedIndexChanged="gvProjects1_SelectedIndexChanged" 
                    OnRowEditing="gvProjects1_RowEditing"
                    OnRowUpdating="gvProjects1_RowUpdating"
                    OnRowCancelingEdit="gvProjects1_RowCancelingEdit"
                    OnRowDeleting="gvProjects1_RowDeleting"
                    OnPageIndexChanging="gvProjects1_PageIndexChanging" 
                    OnRowDataBound="gvProjects1_RowDataBound"
                    DataKeyNames="mfid" 
                    AllowPaging="True"  
                    PageSize="20"       
                    PagerSettings-Mode="NumericFirstLast"  
                    PagerSettings-Position="Bottom"      
                    PagerSettings-FirstPageText="首页"     
                    PagerSettings-LastPageText="末页" 
                    PagerSettings-PageButtonCount="5">
                    <Columns>
                        <asp:BoundField DataField="mfid" HeaderText="ID" ReadOnly="True" ItemStyle-Width="70px" />
                        
                       
                       <asp:TemplateField HeaderText="名称" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("name") %>' CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                  
                        <asp:TemplateField HeaderText="代码" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("code") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("code") %>' CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                       
                        <asp:TemplateField HeaderText="规格型号" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblSpecific" runat="server" Text='<%# Eval("specific") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSpecific" runat="server" Text='<%# Bind("specific") %>' CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                    
                        <asp:TemplateField HeaderText="单位" ItemStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("unit") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("unit") %>' CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                
                        <asp:TemplateField HeaderText="能源因子" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblEnergyFactor" runat="server" 
                                    Text='<%# Eval("energyfactor") != null ? string.Format("{0:F4}", Eval("energyfactor")) : "" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEnergyFactor" runat="server" 
                                    Text='<%# Eval("energyfactor") != null ? string.Format("{0:F4}", Eval("energyfactor")) : "" %>' 
                                    CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
               
                        <asp:TemplateField HeaderText="机械因子" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblMachineFactor" runat="server" 
                                    Text='<%# Eval("machinefactor") != null ? string.Format("{0:F3}", Eval("machinefactor")) : "" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMachineFactor" runat="server" 
                                    Text='<%# Eval("machinefactor") != null ? string.Format("{0:F3}", Eval("machinefactor")) : "" %>' 
                                    CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
             
                        <asp:TemplateField HeaderText="标准" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblStandardId" runat="server" Text='<%# Eval("standardid") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStandardId" runat="server" Text='<%# Bind("standardid") %>' CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="能源类型" ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label ID="lblEnergyType" runat="server" 
                                    Text='<%# Eval("energytype") != null ? Eval("energytype").ToString() : "" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEnergyType" runat="server" 
                                    Text='<%# Eval("energytype") != null ? Eval("energytype").ToString() : "" %>' 
                                    CssClass="edit-field"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
        
                        <asp:TemplateField HeaderText="操作" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <div class="command-buttons">
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="修改" 
                                        CssClass="btn btn-sm btn-edit" />
                                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="删除" 
                                        CssClass="btn btn-sm btn-delete" 
                                        OnClientClick="return confirm('确定要删除这条记录吗？');" />
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="command-buttons">
                                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="保存" 
                                        CssClass="btn btn-sm btn-edit" />
                                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" 
                                        CssClass="btn btn-sm btn-delete" />
                                </div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="no-data">暂无数据</div>
                    </EmptyDataTemplate>
                    <PagerStyle CssClass="pager" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>