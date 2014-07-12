<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberUpdate.aspx.cs" Inherits="PublicWebsite.MembersArea.MemberUpdate"
    MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <asp:ValidationSummary ID="newRegistrationSummary" runat="server" ShowSummary="true"
        ValidationGroup="update" CssClass="errorLabel" />
    <asp:CompareValidator ID="passwordCompare" runat="server" ErrorMessage="The entered passwords do not match."
        EnableClientScript="true" ControlToCompare="newPassword" ControlToValidate="newPasswordConfirm"
        ValidationGroup="update" ValueToCompare="newPasswordConfirm" Display="None" />
    <asp:RegularExpressionValidator ID="emailExpresson" runat="server" ErrorMessage="E-mail address is not in a valid format."
        EnableClientScript="true" ValidationGroup="update" ControlToValidate="emailAddress"
        Display="None" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b" />
    <div id="memberInfo">
        <table>
            <tr>
                <td>
                    <asp:Label ID="errorMessage" CssClass="errorLabel" runat="server" />
                    <table>
                        <tr>
                            <td colspan="2">
                                <h2>Change Member Information</h2>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left;">
                                <h3>Change E-mail Address</h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <b>E-mail</b>
                            </td>
                            <td>
                                <asp:TextBox ID="emailAddress" runat="server" AutoCompleteType="None" ValidationGroup="update" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left;">
                                <h3>Update Password</h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <b>Old Password:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="oldPassword" runat="server" TextMode="password" AutoCompleteType="None" ValidationGroup="update" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <b>New Password:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="newPassword" runat="server" TextMode="password" AutoCompleteType="None" ValidationGroup="update" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <b>New Password Confirm:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="newPasswordConfirm" runat="server" TextMode="password" AutoCompleteType="None" ValidationGroup="update" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left;">
                                <h3>Notifications</h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <b>Receive e-mail regarding specials?</b>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="receiveSpecials" runat="server"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <asp:Button ID="updateInformation" runat="server" Text="Update Information" OnClick="updateInformation_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
