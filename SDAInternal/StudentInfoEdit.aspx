<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentInfoEdit.aspx.cs"
    Inherits="HOTSelfDefense.StudentInfoEdit" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=paymentDate.ClientID%>").datepicker();
        });
        function deleteStudent() {
            if (confirm("Are you sure you want to delete this student?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:ValidationSummary ID="editValidation" runat="server" CssClass="ui-state-error" DisplayMode="BulletList" ShowSummary="true" ValidationGroup="editStudent" HeaderText="The following errors were found:" />
    <asp:RequiredFieldValidator ID="firstNameRequired" Display="None" runat="server" ControlToValidate="firstName" ErrorMessage="Please enter a first name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" />
    <asp:RequiredFieldValidator ID="lastNameRequired" Display="None" runat="server" ControlToValidate="lastName" ErrorMessage="Please enter a last name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" />
    <asp:RequiredFieldValidator ID="birthdayRequired" Display="None" runat="server" ControlToValidate="birthdayDate" ErrorMessage="Please enter a birthdate." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" />
    <asp:RequiredFieldValidator ID="planRequired" Display="None" runat="server" ControlToValidate="paymentPlan" ErrorMessage="Payment Plan is required" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" InitialValue="None" />
    <asp:RequiredFieldValidator ID="amountRequired" Display="None" runat="server" ControlToValidate="paymentAmount" ErrorMessage="Payment Amount is required" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" />
    <asp:RequiredFieldValidator ID="paymentDateRequired" Display="None" runat="server" ControlToValidate="paymentDate" ErrorMessage="Payment Date is required" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="firstNameExpression" Display="None" runat="server" ControlToValidate="firstName" ErrorMessage="Only alphabetical characters are allowed in the first name." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="lastNameExpression" Display="None" runat="server" ControlToValidate="lastName" ErrorMessage="Only alphabetical characters are allowed in the last name." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="addressExpression" Display="None" runat="server" ControlToValidate="address" ErrorMessage="Only alphanumerical characters are allowed in the address." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([0-9 a-zA-Z'\-])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="cityExpression" Display="None" runat="server" ControlToValidate="city" ErrorMessage="Only alphabetical characters are allowed in the city." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="stateExpression" Display="None" runat="server" ControlToValidate="state" ErrorMessage="Only two alphabetical characters are allowed in the state." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([A-Z])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="zipCodeExpression" Display="None" runat="server" ControlToValidate="zipCode" ErrorMessage="Only numbers allowed in zip code" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^\d{5}" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="contactExpression" Display="None" runat="server" ControlToValidate="emergencyContact" ErrorMessage="Only alphabetical characters are allowed in the emergency contact." EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^([ \u00c0-\u01ffa-zA-Z'\-])+$" ValidationGroup="editStudent" />
    <asp:RegularExpressionValidator ID="birthdayExpression" Display="None" runat="server" ControlToValidate="birthdayDate" ErrorMessage="Birth Date is not formatted correctly (MM-DD-YYYY)" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" ValidationGroup="editStudent" />
    <table class="defense" style="width: 500px;">
        <tr>
            <th colspan="2">Edit Student Information
            </th>
        </tr>
        <tr>
            <td class="rightAlignHeader">ID:
            </td>
            <td>
                <asp:Label ID="studentId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">First Name:
            </td>
            <td>
                <asp:TextBox ID="firstName" runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Last Name:
            </td>
            <td>
                <asp:TextBox ID="lastName" runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Address:
            </td>
            <td>
                <asp:TextBox ID="address" runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">City:
            </td>
            <td>
                <asp:TextBox ID="city" runat="server" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">State:
            </td>
            <td>
                <asp:TextBox ID="state" runat="server" MaxLength="2" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Zip:
            </td>
            <td>
                <asp:TextBox ID="zipCode" runat="server" MaxLength="11" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Birthday:
            </td>
            <td>
                <asp:TextBox ID="birthdayDate" runat="server" MaxLength="10" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Emergency Contact:
            </td>
            <td>
                <asp:TextBox ID="emergencyContact" runat="server" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Passing:
            </td>
            <td>
                <asp:CheckBox ID="isPassing" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Paying:
            </td>
            <td>
                <asp:CheckBox ID="isPaid" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Payment Plan:
            </td>
            <td>
                <asp:TextBox ID="paymentPlan" runat="server" MaxLength="10" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Payment Amount:
            </td>
            <td>$<asp:TextBox ID="paymentAmount" runat="server" MaxLength="10" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Next Payment Due:
            </td>
            <td>
                <asp:TextBox ID="paymentDate" runat="server" MaxLength="10" /><br />
                <span class="detailInformation">If lifetime account, date should be 9999-12-31.</span>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Notes:
            </td>
            <td>
                <asp:TextBox ID="studentNote" runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Active:
            </td>
            <td>
                <asp:CheckBox ID="isActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="editCustomer" Text="Update Information" runat="server" OnClick="editCustomer_Click" CssClass="ui-button" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="deleteCustomer" runat="server" OnClientClick="return deleteCustomer();return false;" OnClick="deleteCustomer_Click" Text="Delete Customer" CssClass="ui-button" />
            </td>
        </tr>
    </table>
</asp:Content>