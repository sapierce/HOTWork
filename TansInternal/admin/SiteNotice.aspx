<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="SiteNotice.aspx.cs" Inherits="HOTTropicalTans.admin.SiteNotice" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=startDate.ClientID%>").datepicker();
            $("#<%=endDate.ClientID%>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">New Site Notice
                </th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Notice:
            </td>
            <td>
                <asp:TextBox TextMode="MultiLine" runat="server" ID="noticeText" Rows="4" ValidationGroup="siteNotice" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Start Date:
            </td>
            <td>
                <asp:TextBox ID="startDate" runat="server" ValidationGroup="siteNotice" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">End Date:
            </td>
            <td>
                <asp:TextBox ID="endDate" runat="server" ValidationGroup="siteNotice" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="noticeID" runat="server" Value="0" />
                <asp:Button Text="Add Notice" runat="server" OnClick="addNotice_OnClick" ID="addNotice" ValidationGroup="siteNotice" />
            </td>
        </tr>
    </table>
</asp:Content>