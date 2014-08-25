<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeNotes.aspx.cs" Inherits="HOTTropicalTans.EmployeeNotes1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table style="text-align:center;">
        <tr>
            <td style="align-items: center;">
                <asp:Button ID="addNote" runat="server" OnClick="addNote_Click" Text="Send Note" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 50%;">
                <asp:Literal ID="employeeNoteList" runat='server' />
            </td>
        </tr>
    </table>
</asp:Content>