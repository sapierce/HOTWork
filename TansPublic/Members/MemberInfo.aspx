<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberInfo.aspx.cs" Inherits="PublicWebsite.MembersArea.MemberInfo"
    MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <asp:Label ID="errorMessage" CssClass="errorLabel" runat="server" />
    <div id="memberInfo">
        <table>
            <tr>
                <td colspan='2'>
                    <h3>
                        Member Information</h3>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <b>Name:</b>
                </td>
                <td>
                    <asp:Label ID="customerName" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <b>Join Date:</b>
                </td>
                <td>
                    <asp:Label ID="joinDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
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
                <td style="text-align:right;">
                    <b>Current Plan:</b>
                </td>
                <td>
                    <asp:Label ID="currentPlan" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <b>E-mail Address:</b>
                </td>
                <td>
                    <asp:Label ID="emailAddress" runat="server" /><%--&nbsp;&nbsp;&nbsp;<asp:Label ID="isVerified" runat="server" />
                    <asp:Label ID="isNotVerified" runat="server" />--%>
                </td>
            </tr>
        </table>
    </div>
    <div id="memberLinks">
        <p style="text-align:center;">
            <a href='MemberUpdate.aspx' class="center">Change Your Information</a><br>
            <a href='AddAppointment.aspx' class="center">Add an Appointment</a>
        </p>
    </div>
    <div id="memberTanning">
        <table style="width: 360px;" class="bcc">
            <tr>
                <td colspan="5" class="header">
                    Upcoming Tans
                </td>
            </tr>
            <tr>
                <td>
                    <b>Date</b>
                </td>
                <td>
                    <b>Time</b>
                </td>
                <td>
                    <b>Bed</b>
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <asp:Label ID="upcomingTans" runat="server" />
        </table>
        <br />
        <br />
        <table style="width: 360px;" class="bcc">
            <tr>
                <td colspan="5" class="header">
                    Last Five Tans
                </td>
            </tr>
            <tr>
                <td>
                    <b>Date</b>
                </td>
                <td>
                    <b>Time</b>
                </td>
                <td>
                    <b>Bed</b>
                </td>
                <td>
                    <b>Length</b>
                </td>
            </tr>
            <asp:Label ID="previousTans" runat="server" />
        </table>
    </div>
</asp:Content>
