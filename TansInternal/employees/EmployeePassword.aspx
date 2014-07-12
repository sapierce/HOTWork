<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeePassword.aspx.cs" Inherits="HOTTropicalTans.EmployeePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <p>
        <asp:ValidationSummary ID="passwordSummary" CssClass="validation" HeaderText="The following validation errors were found..."
            runat="server" DisplayMode="BulletList" ValidationGroup="employeePassword" ShowSummary="true" />
        <asp:CompareValidator ID="cvNewPassword" ControlToValidate="newPassword" ControlToCompare="confirmPassword"
            Type="String" Operator="Equal" Display="None" runat="server" EnableClientScript="true"
            ValidationGroup="employeePassword" ErrorMessage="New Password fields do not match." />
        <asp:Label ID="errorMessage" CssClass="error" runat="server" />
    </p>
    <table class='standardTable'>
        <tr>
            <td class='standardHeader' colspan='2'>Employee Password</td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>Employee Name
            </td>
            <td valign='top' class='standardField'>
                <asp:Label ID='employeeName' runat='server' />
            </td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>Employee ID
            </td>
            <td valign='top' class='standardField'>
                <asp:Label ID="employeeNumber" runat='server' />
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td class='standardHeader'>Current Password: </td>
            <td valign='top'>
                <asp:TextBox ID="currentPassword" TextMode="Password" runat="server" ValidationGroup="employeePassword" /></td>
        </tr>
        <tr>
            <td class='standardHeader'>New Password: </td>
            <td valign='top'>
                <asp:TextBox ID="newPassword" TextMode="Password" runat="server" ValidationGroup="employeePassword" /></td>
        </tr>
        <tr>
            <td class='standardHeader'>Confirm Password: </td>
            <td valign='top'>
                <asp:TextBox ID="confirmPassword" TextMode="Password" runat="server" ValidationGroup="employeePassword" /></td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button ID="changePassword" Text="Submit" runat="server" OnClick="changePassword_Click" ValidationGroup="employeePassword" CausesValidation="true" /></td>
        </tr>
    </table>
</asp:Content>