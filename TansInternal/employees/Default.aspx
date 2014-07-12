<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HOTTropicalTans.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table class='tanning'>
        <thead>
            <tr>
                <th colspan="2">
                    Employee Login
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign='top' class='rightAlignHeader'>Employee ID
                </td>
                <td valign='top'>
                    <asp:TextBox ID='employeeNumber' runat='server' />
                </td>
            </tr>
            <tr>
                <td valign='top' class='rightAlignHeader'>Password
                </td>
                <td valign='top'>
                    <asp:TextBox ID='employeePassword' TextMode="Password" runat='server' /><br />
                    <div class="detailInformation">
                        - Leave blank if this is your first time logging in.<br />
                        - If you have forgotten your password and need<br />
                        to reset it, contact Michelle.<br />
                    </div>
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <asp:Button ID='submit' runat='server' Text='Information' OnClick="submit_OnClick" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>