<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="CEMM.Web.computeResultInfo.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		resultid
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblresultid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		code
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcode" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		formName
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblformName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		unit
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblunit" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		price
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblprice" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		emissionfactor
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblemissionfactor" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		total_quantity
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltotal_quantity" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		total_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltotal_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		temp_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltemp_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		temp_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltemp_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subgrade_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsubgrade_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subgrade_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsubgrade_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		pavement_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblpavement_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		pavement_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblpavement_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		bridge_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblbridge_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		bridge_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblbridge_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tunnel_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltunnel_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tunnel_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltunnel_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		crossing_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcrossing_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		crossing_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcrossing_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		traffic_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltraffic_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		traffic_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltraffic_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		greening_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblgreening_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		greening_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblgreening_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		other_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblother_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		other_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblother_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		assistant_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblassistant_project" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		assistant_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblassistant_emission" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tableID
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltableID" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




