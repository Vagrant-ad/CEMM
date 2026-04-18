<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebTest0613.aspx.cs" Inherits="CEMM.Web.sgf.WebTest0613" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>导入Excel表计算</title>
    <style>
        .pagination {
            display: inline-block;
            margin-top: 10px;
        }
        .pagination a {
            color: black;
            float: left;
            padding: 8px 16px;
            text-decoration: none;
            border: 1px solid #ddd;
            margin: 0 4px;
        }
        .pagination a.active {
            background-color: #507CD1;
            color: white;
            border: 1px solid #507CD1;
        }
        .pagination a:hover:not(.active) {
            background-color: #ddd;
        }
        .hidden {
            display: none;
        }
        table {
            width: 100%;
        }
        .km-input {
            margin-right: 10px;
            padding: 4px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;
        <span style="margin-left:10px;">施工千米数:</span>
        <asp:TextBox ID="txtKm" runat="server" CssClass="km-input" TextMode="Number" step="0.01" min="0.01" value="1"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="导入并计算" OnClick="Button1_Click" BackColor="#009933" ForeColor="White"/>
        &nbsp;&nbsp; <asp:Button ID="Button3" runat="server" Text="导出Excel" OnClick="Button3_Click" BackColor="#009933" ForeColor="White"/>
        &nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="导出CSV" BackColor="#009933" ForeColor="White"/>
&nbsp;
        <asp:Label ID="lblExportProgress" runat="server" BackColor="#0099FF" Text="暂无计算/导出"></asp:Label>
        <asp:Button ID="Button2" runat="server" Text="返  回" style="margin-left: 29px" PostBackUrl="~/start.aspx" BackColor="#009933" ForeColor="White" OnClick="Button2_Click"/>
    </div>
    <div></div>
    <div style="width: 100%; height: 184px">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333"  >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <div id="pagination" class="pagination"></div>
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>

    <script>
        // 页面加载完成后初始化分页
        window.onload = function () {
            // 检查GridView是否存在且有数据
            var gridView = document.getElementById('<%= GridView1.ClientID %>');
            if (gridView && gridView.rows.length > 1) { // 大于1是因为第一行是表头
                setupPagination(gridView, 40); // 每页显示40行
            }
        };

        function setupPagination(table, rowsPerPage) {
            // 隐藏所有行（除了表头）
            var rows = table.rows;
            for (var i = 1; i < rows.length; i++) {
                rows[i].classList.add('hidden');
            }

            // 计算总页数
            var pageCount = Math.ceil((rows.length - 1) / rowsPerPage);

            // 创建分页控件
            var pagination = document.getElementById('pagination');
            pagination.innerHTML = '';

            // 添加"上一页"按钮
            var prevLink = document.createElement('a');
            prevLink.href = '#';
            prevLink.innerHTML = '&laquo;';
            prevLink.addEventListener('click', function (e) {
                e.preventDefault();
                var current = document.querySelector('.pagination a.active');
                if (current) {
                    var pageNum = parseInt(current.textContent);
                    if (pageNum > 1) {
                        showPage(table, pageNum - 1, rowsPerPage);
                        updateActivePage(pageNum - 1);
                    }
                }
            });
            pagination.appendChild(prevLink);

            // 添加页码按钮
            for (var i = 1; i <= pageCount; i++) {
                var link = document.createElement('a');
                link.href = '#';
                link.textContent = i;
                if (i === 1) {
                    link.classList.add('active');
                }
                link.addEventListener('click', (function (page) {
                    return function (e) {
                        e.preventDefault();
                        showPage(table, page, rowsPerPage);
                        updateActivePage(page);
                    };
                })(i));
                pagination.appendChild(link);
            }

            // 添加"下一页"按钮
            var nextLink = document.createElement('a');
            nextLink.href = '#';
            nextLink.innerHTML = '&raquo;';
            nextLink.addEventListener('click', function (e) {
                e.preventDefault();
                var current = document.querySelector('.pagination a.active');
                if (current) {
                    var pageNum = parseInt(current.textContent);
                    if (pageNum < pageCount) {
                        showPage(table, pageNum + 1, rowsPerPage);
                        updateActivePage(pageNum + 1);
                    }
                }
            });
            pagination.appendChild(nextLink);

            // 显示第一页
            showPage(table, 1, rowsPerPage);
        }

        function showPage(table, pageNum, rowsPerPage) {
            var rows = table.rows;

            // 隐藏所有行（除了表头）
            for (var i = 1; i < rows.length; i++) {
                rows[i].classList.add('hidden');
            }

            // 显示当前页的行
            var start = (pageNum - 1) * rowsPerPage + 1;
            var end = Math.min(start + rowsPerPage, rows.length);

            for (var i = start; i < end; i++) {
                rows[i].classList.remove('hidden');
            }
        }

        function updateActivePage(pageNum) {
            var links = document.querySelectorAll('.pagination a');
            for (var i = 0; i < links.length; i++) {
                links[i].classList.remove('active');
                if (links[i].textContent == pageNum) {
                    links[i].classList.add('active');
                }
            }
        }
    </script>
</body>
</html>