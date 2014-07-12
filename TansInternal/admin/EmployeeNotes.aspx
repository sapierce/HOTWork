<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeNotes.aspx.cs" Inherits="HOTTropicalTans.EmployeeNotes1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="errorMessage" CssClass="error" runat="server" />
    <table style="text-align:center;">
        <tr>
            <td align='center'>
                <asp:Button ID="addNote" Visible='true' runat='server' OnClick="addNote_OnClick" Text='Send Note' />
            </td>
        </tr>
        <tr>
            <td style="text-align:center;width:50%;">
                <asp:Literal ID="employeeNoteList" runat='server' />
            </td>
        </tr>
    </table>
</asp:Content>