<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberVerify.aspx.cs" Inherits="PublicWebsite.MembersArea.MemberVerify"
    MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <asp:Label ID="errorMessage" class="error" runat="server" />
    <div id="memberInfo">
        <table>
            <tr>
                <td colspan='2'>
                    <h3>
                       Validate Your E-Mail Address</h3>
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    By validating your e-mail address, you will be able to receive e-mail reminders of your tanning appointments and package expiration dates.
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <b>Email:</b>
                </td>
                <td>
                    <asp:Label ID="verificationResponse" runat="server" />
                    <asp:Label ID="emailAddress" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Button ID="sendEmail" runat="server" OnClick="sendEmail_Click" Text="Send Verification" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
