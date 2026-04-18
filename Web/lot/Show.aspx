<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="CEMM.Web.lot.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		lotid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		lotname
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotname" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		lotstartpos
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotstartpos" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		lotendpos
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotendpos" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		projectid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblprojectid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		Construparty
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblConstruparty" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		lotstartdate
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotstartdate" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		lotenddate
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbllotenddate" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




