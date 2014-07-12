<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tips.aspx.cs" Inherits="HOTSelfDefense.TipsPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align="center"><asp:label id="lblError" runat="server" /></p>
		<table align="center" class="bcc">
			<%--<tr>
				<td class="lheader">Art:</td>
				<td class="reg"><asp:dropdownlist id="ddlArtName" runat="server" /></td>
			</tr>--%>
			<tr>
				<td class="lheader">Belt:</td>
				<td class="reg"><asp:dropdownlist id="ddlBeltName" runat="server" /></td>
			</tr>
			<tr>
				<td class="lheader">Tip:</td>
				<td class="reg"><asp:textbox id="txtTipName" runat="server" /></td>
			</tr>
            <tr>
				<td class="lheader">Tip Level:</td>
				<td class="reg"><asp:textbox id="txtTipLevel" runat="server" /></td>
			</tr>
            <tr>
                <td class="lheader">Last Tip?</td>
				<td class="reg"><asp:checkbox id="chkLastTip" runat="server" /></td>
            </tr>
			<tr>
				<td class="reg" colspan="2">
                    <asp:button id="btnAdd" runat="server" text="Add Tip" onClick="btnAdd_onClick" />
                    <asp:button id="btnEdit" runat="server" text="Edit Tip" onClick="btnEdit_onClick" /><br /><br /><br /><br />
                    <asp:button id="btnDelete" runat="server" text="Delete Tip" onClick="btnDelete_onClick" />
                </td>
			</tr>
		</table>
</asp:Content>