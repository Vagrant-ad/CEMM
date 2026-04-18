<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="CEMM.Web.computeResultInfo.Add" Title="增加页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		code
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtcode" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		formName
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtformName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		unit
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtunit" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		price
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtprice" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		emissionfactor
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtemissionfactor" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		total_quantity
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttotal_quantity" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		total_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttotal_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		temp_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttemp_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		temp_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttemp_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subgrade_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtsubgrade_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		subgrade_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtsubgrade_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		pavement_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtpavement_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		pavement_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtpavement_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		bridge_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtbridge_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		bridge_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtbridge_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tunnel_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttunnel_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tunnel_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttunnel_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		crossing_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtcrossing_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		crossing_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtcrossing_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		traffic_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttraffic_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		traffic_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttraffic_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		greening_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtgreening_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		greening_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtgreening_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		other_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtother_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		other_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtother_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		assistant_project
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtassistant_project" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		assistant_emission
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtassistant_emission" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		tableID
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txttableID" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
</table>

            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="保存"
                    OnClick="btnSave_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消"
                    OnClick="btnCancle_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
