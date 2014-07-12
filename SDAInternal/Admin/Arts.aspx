<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arts.aspx.cs" Inherits="HOTSelfDefense.ArtsPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<asp:Label ID="lblError" runat="server" />
<table align="center" class="bcc">
			<tr>
				<td class="lheader">Art:</td>
				<td class="reg"><asp:textbox id="txtArtName" runat="server" /></td>
			</tr>
			<tr>
				<td class="reg" colspan="2">
                    <asp:button id="btnAdd" runat="server" text="Add Art" onClick="btnAdd_onClick" />
                    <asp:button id="btnEdit" runat="server" text="Edit Art" onClick="btnEdit_onClick" /><br /><br /><br /><br />
                    <asp:button id="btnDelete" runat="server" text="Delete Art" onClick="btnDelete_onClick" />
                </td>
			</tr>
		</table>
</asp:Content>