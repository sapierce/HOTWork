<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="MemberInfo.aspx.cs" Inherits="MobileSite.MemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="memberInformation">
        <table>
            <tr>
                <td colspan='2'>
                    <h3>
                        Member Information</h3>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>Name:</b>
                </td>
                <td>
                    <asp:Label ID="customerName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>Join Date:</b>
                </td>
                <td>
                    <asp:Label ID="joinDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>Renewal Date:</b>
                </td>
                <td>
                    <asp:Label ID="renewalDate" runat="server" /><br />
                    <asp:Label ID="renewalPrompt"
                        Text="<a href='/packages.aspx' class='center'>Renew your package today!</a>"
                        Visible="false" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>Current Plan:</b>
                </td>
                <td>
                    <asp:Label ID="customerPlan" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>E-mail Address:</b>
                </td>
                <td>
                    <asp:Label ID="emailAddress" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Literal ID="verifiedEmail" runat="server" />
                    <asp:Label ID="nonVerifiedEmail" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="memberLinks">
        <p align="center">
            <%--<a href='MemberUpdate.aspx' class="center">Change Your Information</a><br>--%>
            <a href='AddAppointment.aspx' class="center">Add an Appointment</a>
        </p>
    </div>
    <div id="memberTanning">
        <table border="0" width='300' class="table">
            <thead>
                <th colspan="5" class="tableHeader">
                    Upcoming Tans
                </th>
            </thead>
            <tr>
                <td class="tableCellHeader">
                    Date
                </td>
                <td class="tableCellHeader">
                    Time
                </td>
                <td class="tableCellHeader">
                    Bed
                </td>
                <td class="tableCellHeader">
                    <br />
                </td>
            </tr>
            <asp:Label ID="lblUpcomingTans" runat="server" />
        </table>
        <br />
        <br />
        <table border="0" width='300' class="table">
            <thead>
                <th colspan="5" class="tableHeader">
                    Last Five Tans
                </th>
            </thead>
            <tr>
                <td class="tableCellHeader">
                    Date
                </td>
                <td class="tableCellHeader">
                    Time
                </td>
                <td class="tableCellHeader">
                    Bed
                </td>
                <td class="tableCellHeader">
                    Length
                </td>
            </tr>
            <asp:Label ID="lblPreviousTans" runat="server" />
        </table>
    </div>
</asp:Content>
