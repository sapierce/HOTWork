<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentAddLesson.aspx.cs" Inherits="HOTSelfDefense.StudentAddLesson" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align='center'><asp:label id="lblError" class="error" runat="server" /></p>
	<table align='center' border='1' bordercolor='#000000'>
		<tr>
			<td colspan='2' class='header'>Add Class</td>
		</tr>
        <tr>
			<td class='rheader'><b>Title:</b></td>
			<td><asp:textbox id='txtTitle' size='50' runat='server' /></td>
		</tr>
		<tr>
			<td class='rheader'><b>Class Art:</b></td>
			<td><asp:dropdownlist id='sltArtFirst' runat='server' /></td>
		</tr>
		<tr>
			<td class='rheader'><b>Secondary Class Art:</b></td>
			<td><asp:dropdownlist id="sltArtSecond" runat='server' /></td>
		</tr>
		<tr>
			<td class='rheader'><b>Begin Date:</b></td>
			<td><div id="overDiv" style="Z-INDEX:1000; VISIBILITY:hidden; POSITION:absolute"></div>
				<asp:textbox id='txtDate' runat='server' Width="105" /></td>
		</tr>
		<tr>
			<td class='rheader'><b>Time:</b></td>
			<td><asp:textbox id='txtTime' size='4' runat='server' /><asp:dropdownlist id='sltAMPM' runat='server' /></td>
		</tr>
		<tr>
			<td class='rheader'><b>Instructor:</b></td>
			<td><asp:dropdownlist id='sltInstructor' runat='server' /></td>
		</tr>
        <tr>
							<td class='rheader'><b>Recurring?</b></td>
							<td><asp:dropdownlist id='sltRecurringLesson' runat='server' /></td>
						</tr>
						<tr>
							<td class='rheader'><b>Student:</b></td>
							<td><asp:dropdownlist runat="server" id='sltStudentList' /></td>
						</tr>
						<tr>
							<td colspan="2"><asp:Button ID="btnAddLEsson" Text='Add Lesson' runat="server" /></td>
						</tr>
					</table>
</asp:Content>