<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="HOTTropicalTans.CustomerAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the addClass button is pressed
            $("#<%= this.addCustomer.ClientID %>").click(function () {
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
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <!-- Display errors associated with validating Appointment information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="custValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addCustomer" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>
    <!-- Appointment Validation -->
    <asp:RequiredFieldValidator ID="hasFirstName" runat="server" ErrorMessage="Please enter the customer's first name." ControlToValidate="firstName" ValidationGroup="addCustomer" Visible="false" />
    <asp:RequiredFieldValidator ID="hasLastName" runat="server" ErrorMessage="Please enter the customer's last name." ControlToValidate="lastName" ValidationGroup="addCustomer" Visible="false" />
    <asp:RequiredFieldValidator ID="hasFitzNumber" runat="server" ErrorMessage="Please select the customer's Fitzpatrick number." ControlToValidate="fitzpatrickNumber" ValidationGroup="addCustomer" Visible="false" />
    <asp:RequiredFieldValidator ID="hasJoinDate" runat="server" ErrorMessage="Please enter the customer's join date." ControlToValidate="joinDate" ValidationGroup="addCustomer" Visible="false" />
    <asp:RequiredFieldValidator ID="hasRenewalDate" runat="server" ErrorMessage="Please enter the customer's renewal date." ControlToValidate="renewalDate" ValidationGroup="addCustomer" Visible="false" />
    <asp:RequiredFieldValidator ID="hasPlans" runat="server" ErrorMessage="Please select the customer's plan." ControlToValidate="plans" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z-' ]{1,60}$" ID="validFirstName" runat="server" ErrorMessage="First names can only contain less than 60 alphanumeric characters." ControlToValidate="firstName" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z-' ]{1,60}$" ID="validLastName" runat="server" ErrorMessage="Last names can only contain less than 60 alphanumeric characters." ControlToValidate="lastName" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[0-9]{1}$" ID="validFitzNumber" runat="server" ErrorMessage="Fitzpatrick number can only be a single digit." ControlToValidate="fitzpatrickNumber" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validJoinDate" runat="server" ErrorMessage="Join date is invalid (MM/DD/YYYY)." ControlToValidate="joinDate" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validRenewalDate" runat="server" ErrorMessage="Renewal date is invalid (MM/DD/YYYY)." ControlToValidate="renewalDate" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[0-9:]{1,6}$" ID="validPlan" runat="server" ErrorMessage="Plan can only contain less than 200 alphanumeric characters." ControlToValidate="plans" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z0-9]{1,200}$" ID="validNotes" runat="server" ErrorMessage="Special notes can only contain less than 200 alphanumeric characters." ControlToValidate="specialNotes" ValidationGroup="addCustomer" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z0-9]{1,200}$" ID="validRemarks" runat="server" ErrorMessage="Remarks can only contain less than 200 alphanumeric characters." ControlToValidate="remarks" ValidationGroup="addCustomer" Visible="false" />
    
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Add New Customer</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Last Name:</td>
                <td>
                    <asp:TextBox ID="lastName" runat="server" ValidationGroup="addCustomer" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">First Name:</td>
                <td>
                    <asp:TextBox ID="firstName" runat="server" ValidationGroup="addCustomer" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Fitzpatrick Number:</td>
                <td>
                    <asp:DropDownList ID="fitzpatrickNumber" runat="server" ValidationGroup="addCustomer">
                        <asp:ListItem Value="0">-Choose-</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Join Date:</td>
                <td>
                    <asp:TextBox ID="joinDate" runat="server" ValidationGroup="addCustomer" Enabled="false" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Plan:</td>
                <td>
                    <asp:DropDownList ID="plans" runat="server" OnSelectedIndexChanged="plans_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="addCustomer" />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Renewal Date:</td>
                <td>
                    <asp:TextBox ID="renewalDate" runat="server" Enabled="false" ValidationGroup="addCustomer" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Remarks or Special Notes:<br /> <span class="detailInformation">(bed preference, water wash, etc.)</span></td>
                <td>
                    <asp:TextBox ID="remarks" runat="server" TextMode="MultiLine" Rows="3" MaxLength="200" ValidationGroup="addCustomer" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="addCustomer" runat="server" Text="Add Customer" OnClick="addCustomer_Click" ValidationGroup="addCustomer" CausesValidation="true" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>