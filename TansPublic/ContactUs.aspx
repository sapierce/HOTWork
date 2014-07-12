<%@ Page Title="" Language="C#" MasterPageFile="~/PublicWebsite.Master" AutoEventWireup="true"
    CodeBehind="ContactUs.aspx.cs" Inherits="PublicWebsite.ContactUs" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="contactHeaderContent" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the submitComment button is pressed
            $("#<%= this.submitComment.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error messages
                    $("#<%= this.panError.ClientID %>").dialog({
                        resizable: false,
                        width: 420,
                        modal: true
                    });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="contactContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <!-- Display errors associated with validating Contact information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="problemSummary" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="problems" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>
    <!-- Contact Validation -->
    <asp:RequiredFieldValidator ID="nameRequired" runat="server" ControlToValidate="enteredName" EnableClientScript="true" ErrorMessage="Please enter a name." ValidationGroup="problems" Display="None" />
    <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="enteredEmail" EnableClientScript="true" ErrorMessage="Please enter an e-mail address" ValidationGroup="problems" Display="None" />
    <asp:RequiredFieldValidator ID="aboutRequired" runat="server" ControlToValidate="enteredComment" EnableClientScript="true" ErrorMessage="Please enter a comment or problem" ValidationGroup="problems" Display="None" />
    <asp:RequiredFieldValidator ID="messageRequired" runat="server" ControlToValidate="commentAbout" EnableClientScript="true" ErrorMessage="Please select what your comment is regarding" ValidationGroup="problems" Display="None" />
    <asp:RegularExpressionValidator ID="emailExpression" runat="server" ControlToValidate="enteredEmail" EnableClientScript="true" ErrorMessage="E-mail address is not in a valid format" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b" ValidationGroup="problems" Display="None" />
    <asp:Panel ID="enterComments" runat="server">
        <div id="contactUs">
            <table class="tanning" style="width: 400px;">
                <thead>
                    <tr>
                        <th class="centerAlignHeader" colspan="2">Contact Us</th>
                    </tr>
                </thead>
                <tr>
                    <td colspan="2">
                        <h3>Question, concern or comment? Give us a call at 254-399-9944, or complete the form below and we’ll contact you as soon as possible. </h3>
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Your Name:
                    </td>
                    <td>
                        <asp:TextBox ID="enteredName" runat="server" AutoCompleteType="DisplayName" ValidationGroup="problems" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">E-mail:
                    </td>
                    <td>
                        <asp:TextBox ID="enteredEmail" runat="server" AutoCompleteType="Email" ValidationGroup="problems" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Regarding:
                    </td>
                    <td>
                        <asp:DropDownList ID="commentAbout" runat="server" ValidationGroup="problems">
                            <asp:ListItem Text="General" Value="General" />
                            <asp:ListItem Text="Store Information" Value="Store" />
                            <asp:ListItem Text="Package Question" Value="Package" />
                            <asp:ListItem Text="Account Question" Value="Account" />
                            <asp:ListItem Text="Product Information" Value="Product" />
                            <asp:ListItem Text="Technical Question" Value="Technical" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;" class="rightAlignHeader">Message:
                    </td>
                    <td>
                        <asp:TextBox ID="enteredComment" TextMode="MultiLine" runat="server" Rows="6" Columns="75" ValidationGroup="problems" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;" class="rightAlignHeader">Captcha:
                    </td>
                    <td>
                        <recaptcha:RecaptchaControl
                            ID="recaptcha"
                            runat="server"
                            PublicKey="6Lc4DdoSAAAAAPyZ1ev-1bU9MjKZC0J-3kxw5vvb"
                            PrivateKey="6Lc4DdoSAAAAAHOyjOV7ExD8bvIHDvdsa0m0XOWv" />
                    </td>
                </tr>
                <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="submitComment" runat="server" OnClick="submitComment_Click" Text="Submit" ValidationGroup="problems" CausesValidation="true" />
                    </td>
                </tr>
                    </tfoot>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="responsePanel" runat="server">
        <div id="member">
            <table class="tanning">
                <tr>
                    <td class="centerAlignHeader">Thank you! Our staff will get back to you as soon as possible.
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>