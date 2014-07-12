<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentRegister.aspx.cs"
    Inherits="PublicWebsite.MembersArea.CurrentRegister" MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div id="existingMember">
        <asp:Label ID="errorMessage" runat="server" CssClass="errorLabel" /><br />
        <asp:ValidationSummary ID="lookupValidation" runat="server" ValidationGroup="lookupUser" ShowSummary="true" /><br />
        <asp:RequiredFieldValidator ID="requiredFirstName" runat="server" ErrorMessage="Customer first name is required." ControlToValidate="firstName" ValidationGroup="lookupUser" Display="None" />
        <asp:RequiredFieldValidator ID="requiredLastName" runat="server" ErrorMessage="Customer last name is required." ControlToValidate="lastName" ValidationGroup="lookupUser" Display="None" />
        <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z -']+$" ID="validFirstName" runat="server" ErrorMessage="Customer first name can only contain A-Z." ControlToValidate="firstName" ValidationGroup="lookupUser" Display="None" />
        <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z -']+$" ID="validLastName" runat="server" ErrorMessage="Customer last name can only contain A-Z." ControlToValidate="lastName" ValidationGroup="lookupUser" Display="None" />
        <asp:ValidationSummary ID="currentRegisterValidation" runat="server" ValidationGroup="addUser" ShowSummary="true" /><br />
        <asp:RequiredFieldValidator ID="requiredUserName" runat="server" ErrorMessage="User name is required." ControlToValidate="userName" ValidationGroup="addUser" Display="None" />
        <asp:RequiredFieldValidator ID="requiredPassword" runat="server" ErrorMessage="Password is required." ControlToValidate="password" ValidationGroup="addUser" Display="None" />
        <asp:RequiredFieldValidator ID="requiredPasswordConfirm" runat="server" ErrorMessage="Password confirmation is required." ControlToValidate="passwordConfirm" ValidationGroup="addUser" Display="None" />
        <asp:RequiredFieldValidator ID="requiredEmailAddress" runat="server" ErrorMessage="E-mail address is required." ControlToValidate="emailAddress" ValidationGroup="addUser" Display="None" />
        <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z0-9]+$" ID="validUserName" runat="server" ErrorMessage="User name can only contain alphanumeric characters." ControlToValidate="userName" ValidationGroup="addUser" Display="None" />
        <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z0-9]+$" ID="validPassword" runat="server" ErrorMessage="Password can only contain alphanumeric characters." ControlToValidate="password" ValidationGroup="addUser" Display="None" />
        <asp:RegularExpressionValidator ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b" ID="validEmail" runat="server" ErrorMessage="Email address is formatted incorrectly." ControlToValidate="emailAddress" ValidationGroup="addUser" Display="None" />
        <asp:CompareValidator ID="comparePasswords" ControlToValidate="password" runat="server" ErrorMessage="Passwords fields do not match." EnableClientScript="true" ControlToCompare="passwordConfirm" Type="String" Operator="Equal" ValidationGroup="addUser" Display="None" />
        
        <asp:Panel ID="signUpSearch" runat="server">
            <div id="currentSignUp">
                <table>
                    <tr>
                        <td colspan="2">
                            <h4>Existing Member Sign-Up</h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>First Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="firstName" runat="server" ValidationGroup="lookupUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Last Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="lastName" runat="server" ValidationGroup="lookupUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top;" colspan="2">
                            <asp:Button ID="findCustomer" runat="server" Text="Submit" OnClick="findCustomer_OnClick" ValidationGroup="lookupUser" CausesValidation="true" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="confirmSingleMember" runat="server">
            <div id="confirmSingle">
                <table>
                    <tr>
                        <td colspan="2">
                            <h4>Existing Member Sign-Up</h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <b>Is this you?</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>First Name:</b>
                        </td>
                        <td>
                            <asp:Label ID="confirmFirstName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Last Name:</b>
                        </td>
                        <td>
                            <asp:Label ID="confirmLastName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Join Date:</b>
                        </td>
                        <td>
                            <asp:Label ID="confirmJoinDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Current Plan:</b>
                        </td>
                        <td>
                            <asp:Label ID="confirmPlan" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">If so, please select a username and password.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otherwise, please check your spelling and try again.
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="confirmMultipleMember" runat="server">
            <div id="confirmMultiple">
                <table>
                    <tr>
                        <td colspan="2">Is one of these you? If so, please select your account and enter a username and
                            password.<br />
                            <br />
                            <asp:RadioButtonList ID="multipleUsers" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Otherwise, please check your spelling and try again.
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="selectUser" runat="server">
            <div id="userSignUp">
                <table>
                    <tr>
                        <td style="text-align: right;">
                            <b>User Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="userName" runat="server" ValidationGroup="addUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Password:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="password" runat="server" TextMode="password" ValidationGroup="addUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Confirm Password:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="passwordConfirm" runat="server" TextMode="password" ValidationGroup="addUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>E-mail Address:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="emailAddress" runat="server" ValidationGroup="addUser" AutoCompleteType="None" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>Would you like to receive e-mails regarding specials?</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="receiveSpecials" runat="server" Checked="true" ValidationGroup="addUser" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <b>I have read and understand the site <a href="/TermsOfService.aspx">Terms of Service</a> and <a href="/PrivacyPolicy.aspx">Privacy Policy</a>:</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="readTermsOfService" runat="server" ValidationGroup="addUser" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" colspan="2">
                            <asp:Button ID="addNewUser" runat="server" Text="Submit" OnClick="addNewUser_OnClick" ValidationGroup="addUser" CausesValidation="true" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="existingUser" runat="server">
            <div id="Exists">
                Our records show that you have already signed up for an online account. Would you
                like to be <a href="ForgotPassword.aspx">reminded of the information</a>?
            </div>
        </asp:Panel>
        <asp:Panel ID="unknownUser" runat="server">
            <div id="NotExists">
                Sorry, we are unable to locate your member information. Please check the name and
                try again, or if you are a new customer (have NEVER tanned with HOT Tropical Tans
                before) please sign up <a href="NewRegister.aspx">here</a>.
            </div>
        </asp:Panel>
    </div>
</asp:Content>