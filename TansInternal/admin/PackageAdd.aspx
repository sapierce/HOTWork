<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true"
    CodeBehind="PackageAdd.aspx.cs" Inherits="HOTTropicalTans.PackageAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the addLesson button is pressed
            $("#<%= this.addPackage.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error messages
                    $("#<%= this.panError.ClientID %>").dialog({
                        resizable: false,
                        width: 420,
                        modal: true,
                        dialogClass: "no-close",
                        buttons: [
                        {
                            text: "OK",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                        ]
                    });
                }
            });
        });
        $(function () {
            $("#icons").tooltip({ content: "<b>About Lengths</b><br />" + 
                "<ul><li><b>7 Days</b> - One Week Package</li>" + 
                "<li><b>13/14 Days</b> - Two Week Package</li>" + 
                "<li><b>29/30 Days</b> - One Month Package</li>" + 
                "<li><b>90 Days</b> - Three Month Package</li>" + 
                "<li><b>120 Days</b> - Semester Package</li>" + 
                "<li><b>180 Days</b> - Six Month Package</li>" +
                "<li><b>365 Days</b> - One Year Package</li>" + 
                "<li><b>Others</b> - &lt;Entered # of Days&gt; Package</li></ul>" });
        });
        $(function(){
		    $('.ui-state-default').hover(
				function(){ $(this).addClass('ui-state-hover'); }, 
				function(){ $(this).removeClass('ui-state-hover'); }
			);
		});
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <!-- Display errors associated with validating Package information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
        <strong>The following errors were found:</strong>
        <asp:ValidationSummary ID="packageValidation" runat="server" CssClass="ui-state-error-text" ShowSummary="true" ValidationGroup="addPackage" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
    </asp:Panel>
    <asp:RequiredFieldValidator ID="packageTypeRequired" Display="None" runat="server" ControlToValidate="packageType" ErrorMessage="Please select a package type." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" InitialValue="" />
    <asp:RequiredFieldValidator ID="packageLengthRequired" Display="None" runat="server" ControlToValidate="packageLength" ErrorMessage="Please enter a package length." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" />
    <asp:RequiredFieldValidator ID="packagePriceRequired" Display="None" runat="server" ControlToValidate="packagePrice" ErrorMessage="Please enter a package price." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" />
    <asp:RequiredFieldValidator ID="packageBarCodeRequired" Display="None" runat="server" ControlToValidate="packageBarCode" ErrorMessage="Please enter a package barcode." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" />
    <asp:RegularExpressionValidator ID="packagePriceFormat" Display="None" runat="server" ControlToValidate="packagePrice" ErrorMessage="Please enter price in 00.00 format." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" ValidationExpression="^(?=.)\d{0,6}(\.\d{1,2})" />
    <asp:RegularExpressionValidator ID="packageLengthFormat" Display="None" runat="server" ControlToValidate="packageLength" ErrorMessage="Please enter length in whole numbers." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addPackage" ValidationExpression="^\d{0,6}" />
    
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Add Package
                </th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Full Name:
            </td>
            <td>
                <asp:TextBox ID="packageName" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Short Name:
            </td>
            <td>
                <asp:TextBox ID="packageShort" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Bed Type:
            </td>
            <td>
                <asp:DropDownList ID="packageType" runat="server" ValidationGroup="addPackage">
                    <asp:ListItem Value="">-Select Bed Type-</asp:ListItem>
                    <asp:ListItem Value="BB">Super Bed</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="PH">PowerHouse</asp:ListItem>
                    <asp:ListItem Value="SB">Regular Bed</asp:ListItem>
                    <asp:ListItem Value="OT">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Number of Tans:
            </td>
            <td>
                <asp:TextBox ID="packageTanCount" runat="server" Text="0" /><br />
                <asp:CheckBox ID="unlimitedTans" runat="server" Text="Unlimited Tans" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                <ul id="icons" class="ui-widget icon-collection">
                <li class="ui-state-default ui-corner-all" title=""><span class="ui-icon ui-icon-help"></span></li>
                    </ul>&nbsp;Length:
            </td>
            <td>
                <asp:TextBox ID="packageLength" runat="server" OnTextChanged="packageLength_TextChanged" AutoPostBack="true" ValidationGroup="addPackage" />&nbsp;day(s)
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Price:
            </td>
            <td>
                <asp:TextBox ID="packagePrice" runat="server" ValidationGroup="addPackage" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Bar Code:
            </td>
            <td>
                <asp:TextBox ID="packageBarcode" runat="server" ValidationGroup="addPackage" />
            </td>
        </tr>
        <tr>
            <td class="leftAlignHeader" colspan="2">Available
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Online?
            </td>
            <td>
                <asp:CheckBox ID="availableOnline" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">In Store?
            </td>
            <td>
                <asp:CheckBox ID="availableInStore" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="leftAlignHeader" colspan="2">On Sale...
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Online?
            </td>
            <td>
                <asp:CheckBox ID="saleOnlineOnly" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">In Store?
            </td>
            <td>
                <asp:CheckBox ID="saleInStoreOnly" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Sale Price:
            </td>
            <td>
                <asp:TextBox ID="salePrice" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="addPackage" Text="Add Package" OnClick="addPackage_Click" runat="server" ValidationGroup="addPackage" CausesValidation="true" />
            </td>
        </tr>
    </table>
</asp:Content>