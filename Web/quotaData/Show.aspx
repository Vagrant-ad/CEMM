<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="CEMM.Web.quotaData.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		srid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsrid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subitermid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsubitermid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subitermsrid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsubitermsrid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subitermname
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsubitermname" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		toolid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltoolid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		toolquant
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltoolquant" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		jcjs
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbljcjs" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		zljs
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblzljs" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		dygx
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbldygx" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		isuse
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblisuse" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




