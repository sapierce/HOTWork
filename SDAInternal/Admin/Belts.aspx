<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Belts.aspx.cs" Inherits="HOTSelfDefense.BeltsPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align='center'><asp:label id="lblError" class="error" runat="server" /></p>
		<table align='center' border='1' bordercolor='#000000'>
		    <tr>
		        <td valign='top' colspan='2' bgcolor='#4682B4'>
		            <b>Edit Belt<br /></b>
		        </td>
		    </tr>
			<tr>
				<td class='rheader'><b>Art:</b></td>
				<td><asp:dropdownlist id='sltArt' runat='server' /></td>
			</tr>
			<tr>
				<td class='rheader'><b>Belt:</b></td>
				<td><asp:textbox id="txtBelt" runat='server' /></td>
			</tr>
            <tr>
				<td class='rheader'><b>Classes or Tips:</b></td>
				<td>
                    <asp:dropdownlist id="sltClassOrTip" runat='server'>
                        <asp:ListItem Text="Classes" Value="C" />
                        <asp:ListItem Text="Tips" Value="T" />
                    </asp:dropdownlist>
                </td>
			</tr>
			<tr>
				<td class='rheader'><b>Classes Required:</b></td>
				<td><asp:textbox id='txtClass' size='4' runat='server' text='0' /></td>
			</tr>
			<tr>
			    <td bgcolor='#4682B4'>
			        <b>Belt Level:<br />(0=White)</b></td>
			    <td>
			        <asp:textbox id="txtBeltLevel" runat='server' Text="0" />
			    </td>
			</tr>
			<tr>
				<td class="reg" colspan="2">
                    <asp:button id="btnAdd" runat="server" text="Add Belt" onClick="btnAdd_onClick" />
                    <asp:button id="btnEdit" runat="server" text="Edit Belt" onClick="btnEdit_onClick" /><br /><br /><br /><br />
                    <asp:button id="btnDelete" runat="server" text="Delete Belt" onClick="btnDelete_onClick" />
                </td>
			</tr>
		</table>
</asp:Content>