<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeAdd.aspx.cs" Inherits="HOTTropicalTans.admin.EmployeeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="errorMessage" CssClass="error" runat="server" />
    <table style="text-align: center;" class='standardTable'>
        <tr>
            <td class='standardHeader' colspan='2'>Add Employee Information</td>
        </tr>
        <tr>
            <td style="vertical-align: top;" class='rightAlignHeader'>Employee First Name:
            </td>
            <td style="vertical-align: top;">
                <asp:TextBox ID='employeeFirstName' runat='server' />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" class='rightAlignHeader'>Employee Last Name:
            </td>
            <td style="vertical-align: top;">
                <asp:TextBox ID="employeeLastName" runat='server' />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" class='rightAlignHeader'>Employee ID:
            </td>
            <td style="vertical-align: top;">
                <asp:TextBox ID="employeeNumber" runat='server' />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Button ID='addEmployee' runat='server' Text='Add Employee' OnClick="addEmployee_OnClick" />
            </td>
        </tr>
    </table>
</asp:Content>