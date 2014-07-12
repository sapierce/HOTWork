<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentArtEdit.aspx.cs" Inherits="HOTSelfDefense.StudentArtEdit" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the editArt button is pressed
            $("#<%= this.editArt.ClientID %>").click(function () {
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
    <!-- Display errors associated with validating Art information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="artValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="editArt" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Art Validation -->
    <asp:RequiredFieldValidator ID="artUpdaterRequired" Display="None" runat="server" ControlToValidate="artUpdater" ErrorMessage="Please enter who authorized this update." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editArt" />

    <table>
        <thead>
            <tr>
                <th colspan="2">Add Student Art</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Art:</td>
                <td>
                    <asp:DropDownList ID="studentArt" runat="server" Enabled="false" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Belt:</td>
                <td>
                    <asp:DropDownList ID="studentBelt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studentBelt_SelectedIndexChanged" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">
                    <asp:Label ID="tipOrClass" runat="server" /></td>
                <td>
                    <asp:DropDownList ID="studentTip" runat="server" Visible="false" /><asp:TextBox ID="classCount" runat="server" Visible="false" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Authorizer:</td>
                <td>
                    <asp:TextBox ID="artUpdater" runat="server" /></td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Button ID="editArt" runat="server" Text="Update Art" OnClick="editArt_Click" CausesValidation="true" ValidationGroup="editArt" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>