<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionDetails.aspx.cs" Inherits="SDAPOS.TransactionDetails" MasterPageFile="SDAPOS.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align='center'><asp:label id="lblError" class="error" runat="server"/></p>
			<table class='bcc' align='center'>
				<tr><td colspan='2' class='header'>Transaction <asp:label id="lblTrnsID" runat="server" /></td></tr>
				<tr>
					<td class='rheader'>Date:</td>
					<td class='reg'><asp:textbox id="txtTrnsDate" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Buyer:</td>
					<td class='reg'><asp:label id="lblTrnsBuyer" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Seller:</td>
					<td class='reg'><asp:textbox id="txtTrnsSeller" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Payment:</td>
					<td class='reg'><asp:dropdownlist id="sltTrnsPymt" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Total:</td>
					<td class='reg'><asp:textbox id="txtTrnsTtl" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Items:</td>
					<td class='reg'><asp:label id="lblItems" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Paid?</td>
					<td class='reg'><asp:checkbox id="chkTrnsPaid" runat="server" /></td>
				</tr>
				<tr>
					<td class='rheader'>Void?</td>
					<td class='reg'><asp:checkbox id="chkTrnsVoid" runat="server" /></td>
				</tr>
				<tr>
					<td><asp:Label ID="lblStudentID" runat="server" style="display: none" /></td>
				</tr>
				<tr>
					<td class='reg' colspan='2'><asp:button id="btnTrnsEdit" runat="server" onClick="onclick_btnTrnsEdit" text="Edit Transaction" /></td>
				</tr>
				<tr>
					<td class='reg' colspan='2'><asp:button id="btnTrnsReceipt" runat="server" onClick="onclick_btnTrnsReceipt" text="Get Receipt" /></td>
				</tr>
			</table>
</asp:Content>