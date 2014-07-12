<%@ Page Title="Member Login" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Logon.aspx.cs" Inherits="MobileSite.Logon" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="login">
        <table>
            <tr>
                <td colspan="2">
                    <h3>Please Login</h3>
                    <asp:validationsummary id="validLogin" runat="server" displaymode="BulletList" cssclass="errorLabel" headertext="The following errors were found:" enableclientscript="true" showsummary="true" validationgroup="mobileLogon" />
                    <asp:requiredfieldvalidator id="requiredUserName" runat="server" controltovalidate="loginUser" display="None" enableclientscript="true" text="Please enter a user name." validationgroup="mobileLogon" />
                    <asp:requiredfieldvalidator id="requiredPassword" runat="server" controltovalidate="loginPassword" display="None" enableclientscript="true" text="Please enter a password." validationgroup="mobileLogon" />
                </td>
            </tr>
            <tr>
                <td class="label">User:
                </td>
                <td>
                    <asp:textbox id="loginUser" size='15' runat="server" validationgroup="mobileLogon" autocompletetype="None" />
                </td>
            </tr>
            <tr>
                <td class="label">Password:
                </td>
                <td>
                    <asp:textbox id="loginPassword" size='15' textmode="password" runat="server" validationgroup="mobileLogon" autocompletetype="None" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="label">
                    <asp:button id="submitloging" text="Login" runat="server" onclick="submitLogin_OnClick" validationgroup="mobileLogon" causesvalidation="true" data-mini="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right;">
                    <span style="font-size: 11px"><a href="ForgotPassword.aspx" class="center">Forgot Password?</a></span>
                </td>
            </tr>
            <%--<tr>
                <td align='left' colspan="2">
                    <font style="font-size: 11px">Don't have an online account? <a href="MemberRegistration.aspx"
                        class="center">Get one here!</a></font>
                </td>
            </tr>--%>
        </table>
    </div>
</asp:Content>