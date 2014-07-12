<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instructors.aspx.cs" Inherits="HOTSelfDefense.InstructorsPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align="center"><asp:label id="lblError" runat="server" /></p>
		<table align="center" class="bcc">
			<tr>
				<td class="lheader">First Name:</td>
				<td class="reg"><asp:textbox id="txtFName" runat="server" /></td>
			</tr>
			<tr>
				<td class="lheader">Last Name:</td>
				<td class="reg"><asp:textbox id="txtLName" runat="server" /></td>
			</tr>
			<tr>
				<td class="lheader">Type:</td>
				<td class="reg">
					<asp:dropdownlist id="ddlType" runat="server">
						<asp:listitem value="0">-Select a Type-</asp:listitem>
						<asp:listitem value="HI">Head Instructor</asp:listitem>
						<asp:listitem value="AI">Assistant Instructor</asp:listitem>
					</asp:dropdownlist>
				</td>
			</tr>
			<tr>
				<td class="lheader">Biography:</td>
				<td class="reg"><asp:textbox id="txtBio" runat="server" size="100" height="100" textmode="multiline" /></td>
			</tr>
			<tr>
				<td class="reg" colspan="2">
                    <asp:button id="btnAdd" runat="server" text="Add Instructor" onClick="btnAdd_onClick" />
                    <asp:button id="btnEdit" runat="server" text="Edit Instructor" onClick="btnEdit_onClick" /><br /><br /><br /><br />
                    <asp:button id="btnDelete" runat="server" text="Delete Instructor" onClick="btnDelete_onClick" />
                </td>
			</tr>
		</table>
</asp:Content>