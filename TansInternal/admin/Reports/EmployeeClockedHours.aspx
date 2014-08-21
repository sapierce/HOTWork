<%@ Page Title="" Language="C#" MasterPageFile="../../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeClockedHours.aspx.cs" Inherits="HOTTropicalTans.admin.EmployeeClockedHours" %>

<asp:Content ID="clockedHoursHeader" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="clockedHoursContent" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th class="centerAlignHeader" colspan="3">
                    <asp:Label ID='selectedDates' runat='server' />
                </th>
            </tr>
        </thead>
        <tr>
            <td class="centerAlignHeader" colspan="3">
                <asp:DropDownList ID='dateRanges' runat="server" />
                <asp:Button ID='findHours' runat='server' Text='Go' />
            </td>
        </tr>
        <tr>
            <td class="centerAlignHeader">
                Employee
            </td>
            <td class="centerAlignHeader">
                Total Hours
            </td>
            <td class="centerAlignHeader">
                Total Sales
            </td>
        </tr>
        <asp:Literal ID='hoursList' runat="server" />
    </table>
</asp:Content>