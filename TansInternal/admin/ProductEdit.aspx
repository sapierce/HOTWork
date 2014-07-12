<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="HOTTropicalTans.admin.ProductEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        function deleteProduct() {
            if (confirm("Are you sure you want to delete this product?")) {
                return true;
            }
            else {
                return false;
            }
        }
        // When the addClass button is pressed
        $("#<%= this.editProduct.ClientID %>").click(function () {
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
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <!-- Display errors associated with validating Product information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="prodValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="editProd" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>
    <!-- Appointment Validation -->
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" runat="server" ControlToValidate="productName" ErrorMessage="Please enter a product name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editProd" InitialValue="" />
    <asp:RequiredFieldValidator ID="priceRequired" Display="None" runat="server" ControlToValidate="productPrice" ErrorMessage="Please enter a product price." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editProd" />
    <asp:RequiredFieldValidator ID="codeRequired" Display="None" runat="server" ControlToValidate="productBarCode" ErrorMessage="Please enter a bar code." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editProd" InitialValue="" />
    
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Edit Product</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Name:</td>
            <td>
                <asp:TextBox ID="productName" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Type:</td>
            <td>
                <asp:DropDownList ID="productType" runat="server" ValidationGroup="editProd">
                    <asp:ListItem Value="ACC">Accessories</asp:ListItem>
                    <asp:ListItem Value="DIS">Discounts</asp:ListItem>
                    <asp:ListItem Value="LTN">Lotions</asp:ListItem>
                    <asp:ListItem Value="OTH">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">SubType:</td>
            <td>
                <asp:DropDownList ID="productSubType" runat="server" ValidationGroup="editProd">
                    <asp:ListItem Value="DS">Discount</asp:ListItem>
                    <asp:ListItem Value="GB">Gift Bag</asp:ListItem>
                    <asp:ListItem Value="LB">Lip Balm</asp:ListItem>
                    <asp:ListItem Value="LM">Lotion Mystic</asp:ListItem>
                    <asp:ListItem Value="LN">Lotion Non-Tingle</asp:ListItem>
                    <asp:ListItem Value="LS">Lotion Sample</asp:ListItem>
                    <asp:ListItem Value="LT">Lotion Tingle</asp:ListItem>
                    <asp:ListItem Value="LO">Moisturizer</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="OT">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Price:</td>
            <td>
                <asp:TextBox ID="productPrice" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Bar Code:</td>
            <td>
                <asp:TextBox ID="productBarCode" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Description:</td>
            <td>
                <asp:TextBox ID="productDescription" TextMode="MultiLine" runat="server" Rows="3" Columns="30" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Taxable?</td>
            <td>
                <asp:CheckBox ID="productTaxed" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="leftAlignHeader" colspan="2">On Sale</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Online?</td>
            <td>
                <asp:CheckBox ID="saleOnline" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">In Store?</td>
            <td>
                <asp:CheckBox ID="saleInStore" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Sale Price:</td>
            <td>
                <asp:TextBox ID="salePrice" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="leftAlignHeader" colspan="2">Available</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Online?</td>
            <td>
                <asp:CheckBox ID="availableOnline" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">In Store?</td>
            <td>
                <asp:CheckBox ID="availableInStore" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <%--<tr>
            <td class="rightAlignHeader">Which store(s)?</td>
            <td>
                <asp:DropDownList ID="sltStore" runat="server" ValidationGroup="editProd">
                    <asp:ListItem Value="B">Both</asp:ListItem>
                    <asp:ListItem Value="W">Waco Only</asp:ListItem>
                    <asp:ListItem Value="H">Hewitt Only</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td class="leftAlignHeader" colspan="2">Inventory</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Waco:</td>
            <td>
                <asp:TextBox ID="wacoInventory" runat="server" ValidationGroup="editProd" /></td>
        </tr>
        <%--<tr>
            <td class="rightAlignHeader">Hewitt:</td>
            <td>
                <asp:TextBox ID="txtHewittInv" runat="server" /></td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <asp:Button ID="editProduct" Text="Edit Product" OnClick="editProduct_Click" runat="server" ValidationGroup="editProd" CausesValidation="true" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="deleteProduct" Text="Delete Product" OnClientClick="return deleteProduct();return false;" OnClick="deleteProduct_Click" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
