<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Problems.aspx.cs" Inherits="HOTSelfDefense.Problems" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the sendProblem button is pressed
            $("#<%= this.sendProblem.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error message
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
    <!-- Display errors associated with validating reporting information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="problemValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="reportProblem" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Problem/Comment Validation -->
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" runat="server" ControlToValidate="reporterName" ErrorMessage="Please enter a name" EnableClientScript="true" SetFocusOnError="true" ValidationGroup="reportProblem" />
    <asp:RequiredFieldValidator ID="commentRequired" Display="None" runat="server" ControlToValidate="reportComment" ErrorMessage="Please enter a comment." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="reportProblem" />

    <table class="defense">
        <tr>
            <th colspan="2">Report Problems/Comments</th>
        </tr>
        <tr>
            <td class="rightAlignHeader">To:</td>
            <td>Stephanie (Problems@hottropicaltans.com)</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Your Name:</td>
            <td>
                <!-- Who is reporting the problem/comment? -->
                <asp:TextBox ID="reporterName" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Comments/Problem:</td>
            <td>
                <!-- What is the problem/comment? -->
                <asp:TextBox ID="reportComment" TextMode="MultiLine" Rows="4" Columns="25" runat="server" />
        </tr>
        <tr>
            <td colspan="2">
                <!-- Send the problem/comment after validation -->
                <asp:Button ID="sendProblem" runat="server" CssClass="ui-button" Text="Submit" OnClick="sendProblem_Click" CausesValidation="true" ValidationGroup="reportProblem" /></td>
        </tr>
    </table>
</asp:Content>