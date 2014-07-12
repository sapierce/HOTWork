<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="HOTTropicalTans.CustomerEdit" %>

<asp:Content ID="customerEditHead" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Allow the renewalDate to use the datepicker
            $("#<%=renewalDate.ClientID%>").datepicker();

            // When the editCustomer button is pressed
            $("#<%= this.editCustomer.ClientID %>").click(function () {
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

        function deleteCustomer() {
            if (confirm("Are you sure you want to delete this customer?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="customerEdit" ContentPlaceHolderID="Main" runat="server">
    <!-- Display errors associated with validating Appointment information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
        <strong>The following errors were found:</strong>
        <asp:ValidationSummary ID="custValidation" runat="server" CssClass="ui-state-error-text"
            ShowSummary="true" ValidationGroup="custEdit" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        <span></span>
    </asp:Panel>
    <!-- Customer Validation -->
    <asp:RequiredFieldValidator ID="lastNameRequired" runat="server" EnableClientScript="true" Display="None" ControlToValidate="lastName" ErrorMessage="Please enter a Last Name." ValidationGroup="custEdit" />
    <asp:RequiredFieldValidator ID="firstNameRequired" runat="server" EnableClientScript="true" Display="None" ControlToValidate="firstName" ErrorMessage="Please enter a First Name." ValidationGroup="custEdit" />
    <asp:RequiredFieldValidator ID="joinDateRequired" runat="server" EnableClientScript="true" Display="None" ControlToValidate="joinDate" ErrorMessage="Please enter a Join Date." ValidationGroup="custEdit" />
    <asp:RequiredFieldValidator ID="renewalDateRequired" runat="server" EnableClientScript="true" Display="None" ControlToValidate="renewalDate" ErrorMessage="Please enter a Renewal Date." ValidationGroup="custEdit" />
    <asp:RequiredFieldValidator ID="specialDateRequired" runat="server" EnableClientScript="true" Display="None" ControlToValidate="specialDate" ErrorMessage="Please enter a Special Level Renewal Date." ValidationGroup="custEdit" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z-' ]{1,60}$" ID="validFirstName" runat="server" ErrorMessage="First names can only contain less than 60 alphanumeric characters." ControlToValidate="firstName" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z-' ]{1,60}$" ID="validLastName" runat="server" ErrorMessage="Last names can only contain less than 60 alphanumeric characters." ControlToValidate="lastName" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[0-9]{1}$" ID="validFitzNumber" runat="server" ErrorMessage="Fitzpatrick number can only be a single digit." ControlToValidate="fitzpatrickNumber" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validJoinDate" runat="server" ErrorMessage="Join date is invalid (MM/DD/YYYY)." ControlToValidate="joinDate" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validRenewalDate" runat="server" ErrorMessage="Renewal date is invalid (MM/DD/YYYY)." ControlToValidate="renewalDate" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validSpecialDate" runat="server" ErrorMessage="Special Level Renewal date is invalid (MM/DD/YYYY)." ControlToValidate="specialDate" ValidationGroup="custEdit" Visible="false" />
    <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z0-9]{1,200}$" ID="validRemarks" runat="server" ErrorMessage="Remarks can only contain less than 200 alphanumeric characters." ControlToValidate="remarkInfo" ValidationGroup="custEdit" Visible="false" />

    <table class="tanning" width="400">
        <thead>
            <tr>
                <th colspan="2">Edit Customer</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Last Name:
            </td>
            <td>
                <asp:TextBox ID="lastName" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">First Name:
            </td>
            <td>
                <asp:TextBox ID="firstName" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Fitzpatrick Number:
            </td>
            <td>
                <asp:DropDownList ID="fitzNumber" runat="server" ValidationGroup="custEdit">
                    <asp:ListItem Text="0" Value="0" />
                    <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="4" Value="4" />
                    <asp:ListItem Text="5" Value="5" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Join Date:
            </td>
            <td>
                <asp:TextBox ID="joinDate" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Plan:
            </td>
            <td>
                <asp:DropDownList ID="planList" runat="server" /><br />
                <asp:DropDownList ID="specialList" runat="server" /><br />
                <asp:DropDownList ID="specialLevelsList" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Plan Renewal Date:
            </td>
            <td>
                <asp:TextBox ID="renewalDate" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Special Level Renewal Date:
            </td>
            <td>
                <asp:TextBox ID="specialDate" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Remarks:
            </td>
            <td>
                <asp:TextBox ID="remarkInfo" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Lotion Note:
            </td>
            <td>
                <asp:CheckBox ID="lotionCheck" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">No Online Payments:
            </td>
            <td>
                <asp:CheckBox ID="onlineCheck" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Verification:<br /> <span class="detailInformation">(to change expiration date)</span>
            </td>
            <td>
                <asp:TextBox TextMode="password" ID="changeExpiration" runat="server" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="expirationLabel" runat="server" Visible="false" />
                <asp:Button Text="Edit Customer" runat="server" OnClick="editCustomer_Click" ID="editCustomer" CausesValidation="true" ValidationGroup="custEdit" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="deleteCustomer" runat="server" OnClientClick="return deleteCustomer();return false;" OnClick="deleteCustomer_Click" Style="width: 175px;" Text="Delete Customer" />
            </td>
        </tr>
    </table>
</asp:Content>