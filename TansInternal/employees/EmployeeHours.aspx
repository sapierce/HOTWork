<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeHours.aspx.cs" Inherits="HOTTropicalTans.EmployeeHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
   <asp:Label ID="errorMessage" CssClass="error" runat="server" />
    <table class='standardTable'>
        <tr>
            <td class='standardHeader' colspan='2'>Add Time</td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>Employee Name
            </td>
            <td valign='top' class='standardField'>
                <asp:Label ID='employeeName' runat='server' />
            </td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>Date
            </td>
            <td valign='top' class='standardField'>
                <asp:TextBox ID="shiftDate" runat='server' />
            </td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>Start Time
            </td>
            <td valign='top' class='standardField'>
                <asp:TextBox ID="shiftStartTime" runat='server' />
            </td>
        </tr>
        <tr>
            <td valign='top' class='rightAlignHeader'>End Time
            </td>
            <td valign='top' class='standardField'>
                <asp:TextBox ID="shiftEndTime" runat='server' />
            </td>
        </tr>
        <tr>
            <td align='left' class='standardField' colspan='2'>
                <asp:Button ID='editHours' runat='server' Text='Edit Time' OnClick="editHours_onClick" />
            </td>
        </tr>
    </table>
</asp:Content>