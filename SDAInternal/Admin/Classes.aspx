<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="HOTSelfDefense.ClassesPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align='center'><asp:label id="lblError" class="error" runat="server" /></p>
		<table align='center' border='1' bordercolor='#000000'>
			<tr>
				<td class='rheader'><b>Class Art</b></td>
				<td><asp:dropdownlist id='sltArtFirst' runat='server' /></td>
			</tr>
			<tr>
				<td class='rheader'><b>Secondary Class Art</b></td>
				<td><asp:dropdownlist id="sltArtSecond" runat='server' /></td>
			</tr>
			<tr>
				<td class='rheader'><b>Time:</b></td>
				<td><asp:textbox id='txtTime' size='4' runat='server' /></td>
			</tr>
			<tr>
				<td class='rheader'><b>Instructor:</b></td>
				<td><asp:dropdownlist id='sltInstructor' runat='server' /></td>
			</tr>
			<tr>
				<td colspan='2'>
					<table class='bcc'>
						<tr>
							<td class='rheader'><b>Title:</b></td>
							<td><asp:textbox runat='server' ID='txtTitle' /></td>
						</tr>
						<tr>
							<td class='rheader'><b>Recurring?</b></td>
							<td><asp:dropdownlist id='sltRecurringClass' runat='server' /></td>
						</tr>
						<tr>
							<td colspan="2"><asp:button id="btnEdit" runat="server" text="Edit Class" onClick="btnEdit_onClick" /></td><br /><br /><br /><br />
						</tr>
						<tr>
							<td colspan="2"><asp:button id="btnDelete" runat="server" text = "Delete Class" onClick="btnDelete_onClick" /></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
</asp:Content>