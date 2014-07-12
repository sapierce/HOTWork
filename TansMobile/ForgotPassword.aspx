<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MobileSite.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ID="vsPassword" runat="server" ShowSummary="true" ValidationGroup="password" />
    <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="password" ControlToValidate="emailAddress"
        runat="server" ErrorMessage="Please enter an e-mail address."  />
    <asp:RegularExpressionValidator ID="revEmail" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b"
        ValidationGroup="password" ControlToValidate="emailAddress" runat="server" ErrorMessage="Invalid e-mail format. Please check your entry." />
    <asp:Panel ID="forgotPasswordPanel" runat="server">
    <div id="forgotPassword">
        <table>
            <tr>
                <td colspan='2'>
                    <h3>
                        Forgotten Password</h3>
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <b>Enter in the e-mail address registered and a new password will be sent to you.</b>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <b>E-mail Address:</b>
                </td>
                <td>
                    <asp:TextBox ID='emailAddress' runat="server" AutoCompleteType="None" ValidationGroup="password" />
                </td>
            </tr>
            <tr>
                <td colspan='2' style="vertical-align: top;">
                    <asp:Button ID="getPassword" Text="Submit" runat="server" OnClick="getPassword_onSubmit" ValidationGroup="password" CausesValidation="true" data-mini="true" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    <asp:Panel ID="responsePanel" runat="server">
    <div id="forgotPasswordResponse">
        <table>
            <tr>
                <td colspan='2'>
                    <h3>
                        Forgotten Password</h3>
                </td>
            </tr>
            <tr>
                <td colspan='2' style="text-align: center;">
                    <asp:Label ID="errorMessage" CssClass="errorLabel" Wrap="true" Rows="3" TextMode="multiline" runat="server" />
                    <asp:Label ID="responseMessage" CssClass="responseLabel" Wrap="true" Rows="3" TextMode="multiline" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan='2' style="vertical-align: top;">
                    <a href="Logon.aspx">Go Back To Log On</a>
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
</asp:Content>
