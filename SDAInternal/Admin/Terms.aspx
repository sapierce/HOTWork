<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms.aspx.cs" Inherits="HOTSelfDefense.TermsPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align="center"><asp:label id="lblError" runat="server" /></p>
		<table align="center" class="bcc">
			<tr>
				<td class="lheader">Belt:</td>
				<td class="reg"><asp:dropdownlist id="ddlBeltName" runat="server" /></td>
			</tr>
            <tr>
				<td class="lheader">English:</td>
				<td class="reg"><asp:textbox id="txtEnglish" runat="server" /></td>
			</tr>
            <tr>
                <td class="lheader">Chinese:</td>
				<td class="reg"><asp:textbox id="txtChinese" runat="server" /></td>
            </tr>
			<tr>
				<td class="reg" colspan="2">
                    <asp:button id="btnAdd" runat="server" text="Add Term" onClick="btnAdd_onClick" />
                    <asp:button id="btnEdit" runat="server" text="Edit Term" onClick="btnEdit_onClick" /><br /><br /><br /><br />
                    <asp:button id="btnDelete" runat="server" text="Delete Term" onClick="btnDelete_onClick" />
                </td>
			</tr>
		</table>
</asp:Content>