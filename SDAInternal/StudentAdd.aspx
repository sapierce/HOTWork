<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentAdd.aspx.cs" Inherits="HOTSelfDefense.StudentAdd" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // When the addStudent button is pressed
            $("#<%= this.addStudent.ClientID %>").click(function () {
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
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <!-- Display errors associated with validating Student information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="addValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addStudent" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Student Validation -->
    <asp:RequiredFieldValidator ID="firstNameRequired" Display="None" runat="server" ControlToValidate="firstName" ErrorMessage="Please enter a first name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" />
    <asp:RequiredFieldValidator ID="lastNameRequired" Display="None" runat="server" ControlToValidate="lastName" ErrorMessage="Please enter a last name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" />
    <asp:RequiredFieldValidator ID="birthdayRequired" Display="None" runat="server" ControlToValidate="birthdayDate" ErrorMessage="Please enter a birthdate." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" />
    <asp:RequiredFieldValidator ID="planIntervalRequired" Display="None" runat="server" ControlToValidate="intervalCount" ErrorMessage="Please enter a payment interval." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" />
    <asp:RequiredFieldValidator ID="planRequired" Display="None" runat="server" ControlToValidate="paymentInterval" ErrorMessage="Please select a payment plan." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" InitialValue="None" />
    <asp:RequiredFieldValidator ID="artRequired" Display="None" runat="server" ControlToValidate="artList" ErrorMessage="Please select an art." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" InitialValue="0" />
    <asp:RequiredFieldValidator ID="amountRequired" Display="None" runat="server" ControlToValidate="paymentAmount" ErrorMessage="Please enter a payment amount." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="firstNameExpression" Display="None" runat="server" ControlToValidate="firstName" ErrorMessage="Only alphabetical characters are allowed in the first name." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="lastNameExpression" Display="None" runat="server" ControlToValidate="lastName" ErrorMessage="Only alphabetical characters are allowed in the last name." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="addressExpression" Display="None" runat="server" ControlToValidate="address" ErrorMessage="Only alphanumerical characters are allowed in the address." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([0-9 a-zA-Z'\-])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="cityExpression" Display="None" runat="server" ControlToValidate="city" ErrorMessage="Only alphabetical characters are allowed in the city." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="stateExpression" Display="None" runat="server" ControlToValidate="state" ErrorMessage="Only two alphabetical characters are allowed in the state." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([A-Z])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="zipCodeExpression" Display="None" runat="server" ControlToValidate="zipCode" ErrorMessage="Only numbers allowed in zip code" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^\d{5}" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="contactExpression" Display="None" runat="server" ControlToValidate="emergencyContact" ErrorMessage="Only alphabetical characters are allowed in the emergency contact." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="birthdayExpression" Display="None" runat="server" ControlToValidate="birthdayDate" ErrorMessage="Birth Date is not formatted correctly (MM-DD-YYYY)" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="planIntervalExpression" Display="None" runat="server" ControlToValidate="intervalCount" ErrorMessage="Only numbers allowed in Payment Plan interval" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^\d{0,3}" ValidationGroup="addStudent" />
    <asp:RegularExpressionValidator ID="amountExpression" Display="None" runat="server" ControlToValidate="paymentAmount" ErrorMessage="Payment Amount is not formatted correctly (0.00)" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^[0-9]+(\.[0-9][0-9])" ValidationGroup="addStudent" />

    <table class="defense" style="width: 500px;">
        <tr>
            <th colspan="2">Add Student Information
            </th>
        </tr>
        <tr>
            <td class="rightAlignHeader">First Name:</td>
            <td>
                <!-- Student First Name (required) -->
                <asp:TextBox ID="firstName" runat="server" ValidationGroup="addStudent" MaxLength="50" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Last Name:</td>
            <td>
                <!-- Student Last Name (required) -->
                <asp:TextBox ID="lastName" runat="server" ValidationGroup="addStudent" MaxLength="50" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Address:</td>
            <td>
                <!-- Student Address -->
                <asp:TextBox ID="address" runat="server" ValidationGroup="addStudent" MaxLength="250" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">City:</td>
            <td>
                <!-- Student City -->
                <asp:TextBox ID="city" runat="server" ValidationGroup="addStudent" MaxLength="50" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">State:</td>
            <td>
                <!-- Student State -->
                <asp:TextBox ID="state" runat="server" ValidationGroup="addStudent" Text="TX" MaxLength="2" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Zip:</td>
            <td>
                <!-- Student Zip Code -->
                <asp:TextBox ID="zipCode" runat="server" ValidationGroup="addStudent" MaxLength="5" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Birthday:</td>
            <td>
                <!-- Student Birthdate -->
                <asp:TextBox ID="birthdayDate" runat="server" ValidationGroup="addStudent" MaxLength="10" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Emergency Contact:</td>
            <td>
                <!-- Student Emergency Contact -->
                <asp:TextBox ID="emergencyContact" runat="server" ValidationGroup="addStudent" MaxLength="50" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Art:</td>
            <td>
                <!-- Student Primary Art (required) -->
                <asp:DropDownList ID="artList" runat="server" ValidationGroup="addStudent" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Payment Schedule:</td>
            <td>
                <!-- Student Payment Interval Count (required) -->
                <asp:TextBox ID="intervalCount" runat="server" ValidationGroup="addStudent" MaxLength="3" size="2" Text="1" />

                <!-- Student Payment Interval (required) -->
                <asp:DropDownList ID="paymentInterval" runat="server" ValidationGroup="addStudent">
                    <asp:ListItem Value="None" Text="None" runat="server" />
                    <asp:ListItem Value="Lesson" Text="Lesson(s)" runat="server" />
                    <asp:ListItem Value="Week" Text="Week(s)" runat="server" />
                    <asp:ListItem Value="Month" Text="Month(s)" runat="server" />
                    <asp:ListItem Value="Year" Text="Year(s)" runat="server" />
                    <asp:ListItem Value="Life" Text="Lifetime" runat="server" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Payment Amount:</td>
            <td>$
                <!-- Student Payment Amount (required) -->
                <asp:TextBox ID="paymentAmount" runat="server" ValidationGroup="addStudent" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <!-- Add the student after validation -->
                <asp:Button ID="addStudent" Text="Add Student" runat="server" CssClass="ui-button" OnClick="addStudent_Click" CausesValidation="true" ValidationGroup="addStudent" /></td>
        </tr>
    </table>
</asp:Content>