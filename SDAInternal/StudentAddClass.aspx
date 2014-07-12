<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentAddClass.aspx.cs" Inherits="HOTSelfDefense.StudentAddClass" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
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
    <!-- Display errors associated with validating Class information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="classValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addStudent" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Class Validation -->
    <asp:RequiredFieldValidator ID="classRequired" Display="None" runat="server" ControlToValidate="classList" ErrorMessage="Please select a class." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addStudent" InitialValue="0" />

    <table class="defense">
        <thead>
            <tr>
                <th colspan="2">Add Student to Class</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Class:</td>
            <td>
                <!-- List of current recurring classes -->
                <asp:DropDownList ID="classList" runat="server" ValidationGroup="addStudent" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <!-- Add the class after validation -->
                <asp:Button ID="addStudent" runat="server" Text="Add Class" OnClick="addStudent_Click" CausesValidation="true" ValidationGroup="addStudent" /></td>
        </tr>
    </table>
</asp:Content>