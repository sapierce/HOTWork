<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentAttendance.aspx.cs" Inherits="HOTSelfDefense.StudentAttendancePage" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense">
        <tr>
            <th>
                <asp:Label ID="studentName" runat="server" />
                Attendance for
                <asp:Label ID="courseName" runat="server" /></th>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="attendanceList" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>