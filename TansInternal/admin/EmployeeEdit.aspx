<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeEdit.aspx.cs" Inherits="HOTTropicalTans.admin.EmployeeEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        function deleteEmployee() {
            if (confirm("Are you sure you want to delete this employee?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:label id="errorMessage" CssClass="error" runat="server"/>
		<table style="text-align:center;" class='standardTable'>
			<tr>
				<td class='standardHeader' colspan='2'>Edit Employee Information</td>
			</tr>
			<tr>
				<td style="vertical-align:top;" class='rightAlignHeader'>
					Employee First Name
				</td>
				<td valign='top'>
					<asp:textbox id='firstName' runat='server' />
				</td>
			</tr>
			<tr>
				<td style="vertical-align:top;" class='rightAlignHeader'>
					Employee First Name
				</td>
				<td valign='top'>
					<asp:textbox id="lastName" runat='server' />
				</td>
			</tr>
			<tr>
				<td style="vertical-align:top;" class='rightAlignHeader'>
					Employee ID
				</td>
				<td valign='top'>
					<asp:label id="employeeNumber" runat='server' />
				</td>
			</tr>
			<tr>
				<td align='left'>
					<asp:button id='resetPassword' onClick="resetPassword_OnClick" runat='server' text='Reset Password' />
				</td>
			</tr>
			<tr>
				<td align='left'>
					<asp:button id='editEmployee' onClick="editEmployee_OnClick" runat='server' text='Edit Employee' />
				</td>
			</tr>
			<tr>
				<td align='left'>
					<asp:button id="deleteEmployee" OnClientClick="return deleteEmployee();return false;" onClick="deleteEmployee_OnClick" runat='server' text='Delete Employee' />
				</td>
			</tr>
		</table>
</asp:Content>
