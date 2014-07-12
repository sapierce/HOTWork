<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeNotes.aspx.cs" Inherits="HOTTropicalTans.EmployeeNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <p style='text-align: center;'>
        <asp:Label ID="errorMessage" class="error" runat="server" /><br />
        <asp:Label ID="noteResponse" runat="server" />
    </p>
    <table>
        <tr>
            <td colspan='2'>
                <h3>Leave Note</h3>
            </td>
        </tr>
        <tr>
            <td>To:</td>
            <td>Michelle</td>
        </tr>
        <tr>
            <td valign='top'>Note:</td>
            <td>
                <asp:TextBox ID='commentFromEmployee' TextMode="MultiLine" Rows='5' Columns='30' runat="server" /></td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button ID="leaveNote" runat="server" Text="Submit" OnClick="leaveNote_OnClick" /></td>
        </tr>
    </table>
</asp:Content>