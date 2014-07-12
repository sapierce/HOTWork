<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="SitePasswords.aspx.cs" Inherits="HOTTropicalTans.admin.SitePasswords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Manage Passwords</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td colspan="2" class="centerAlignHeader">Administration Password</td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Old Password:</td>
                <td>
                    <asp:TextBox ID="oldAdminPwd" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">New Password:</td>
                <td>
                    <asp:TextBox ID="newAdminPwd" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Confirm New Password:</td>
                <td>
                    <asp:TextBox ID="newAdminConfirm" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="submitAdminChange" runat="server" Text="Change Password" OnClick="submitAdminChange_Click" /></td>
            </tr>
            <tr>
                <td colspan="2" class="centerAlignHeader">Renewal Change Password</td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Old Password:</td>
                <td>
                    <asp:TextBox ID="oldRenewPwd" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">New Password:</td>
                <td>
                    <asp:TextBox ID="newRenewPwd" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Confirm New Password:</td>
                <td>
                    <asp:TextBox ID="newRenewConfirm" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="submitRenew" runat="server" Text="Change Password" OnClick="submitRenew_Click" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>