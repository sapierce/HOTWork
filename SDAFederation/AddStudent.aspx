<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="SDAFederation.AddStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table class="standardTable">
        <tr>
            <td colspan='2' class="standardHeader">
                <b>Add Student</b>
            </td>
        </tr>
		<tr>
			<td class="rightAlignHeader">Federation ID:</td>
			<td class="standardField"><asp:textbox ID='federationID' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">First Name:</td>
			<td class="standardField"><asp:textbox ID='firstName' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Last Name:</td>
			<td class="standardField"><asp:textbox ID='lastName' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Address:</td>
			<td class="standardField"><asp:textbox ID='address' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">City:</td>
			<td class="standardField"><asp:textbox ID='city' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">State:</td>
			<td class="standardField"><asp:textbox ID='state' runat='server' MaxLength="2" /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Zip:</td>
			<td class="standardField"><asp:textbox ID='zip' runat='server' MaxLength="5" /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Birthday:</td>
			<td class="standardField"><asp:textbox ID='birthDate' runat='server' MaxLength="10" /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Emergency Contact:</td>
			<td class="standardField"><asp:textbox ID='emergencyContact' runat='server' /></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Art:</td>
			<td class="standardField"><asp:dropdownlist id="artSelection" runat='server' OnSelectedIndexChanged="artSelection_SelectedIndexChanged" AutoPostBack="true"/></td>
		</tr>
		<tr>
			<td class="rightAlignHeader">Belt:</td>
			<td class="standardField"><asp:dropdownlist id="beltSelection" runat='server' /></td>
		</tr>
		<tr>
			<td colspan="2" class="standardField"><asp:button id="addStudent" text="Add Student" OnClick="addStudent_Click" runat="server" /></td>
		</tr>
	</table>
</asp:Content>
