<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="SDAFederation.admin.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table class="standardTable">
        <tr>
            <td class="standardHeader" colspan='2'>Product Information</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Name:</td>
            <td class="standardField">
                <asp:TextBox ID='productName' runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Type:</td>
            <td class="standardField">
                <asp:DropDownList ID='productType' runat="server">
                    <asp:ListItem Value="MTL">Martial Arts</asp:ListItem>
                    <asp:ListItem Value="DIS">Discount</asp:ListItem>
                    <asp:ListItem Value="OTH">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">SubType:</td>
            <td class="standardField">
                <asp:DropDownList ID='productSubType' runat="server">
                    <asp:ListItem Value="BQ">Martial Arts Banquet</asp:ListItem>
                    <asp:ListItem Value="FF">Federation Fees</asp:ListItem>
                    <asp:ListItem Value="PF">Promotion Fees</asp:ListItem>
                    <asp:ListItem Value="DS">Discount</asp:ListItem>
                    <asp:ListItem Value="OT">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Price:</td>
            <td class="standardField">
                <asp:TextBox ID='productPrice' runat="server" Text="0.00" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Taxable?</td>
            <td class="standardField">
                <asp:CheckBox ID='isTaxable' runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Bar Code:</td>
            <td class="standardField">
                <asp:TextBox ID='barCode' runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Description:</td>
            <td class="standardField">
                <asp:TextBox ID='productDescription' TextMode="MultiLine" runat="server" Rows='3' Columns='30' /></td>
        </tr>
        <tr>
            <td class="leftAlignHeader">Inventory</td>
            <td class="standardField">
                <asp:TextBox ID="productInventory" runat="server" Text="0" /></td>
        </tr>
        <tr>
            <td class="leftAlignHeader" colspan='2'>Sales</td>
        </tr>
        <tr>
            <td class="rightAlignHeader">In Store?</td>
            <td class="standardField">
                <asp:CheckBox ID='onSaleInStore' runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Sale Price:</td>
            <td class="standardField">
                <asp:TextBox ID='salePrice' runat="server" Text="0.00" /></td>
        </tr>
        <tr>
            <td class="standardField" colspan="2">
                <asp:Button ID="editProduct" runat="server" Text="Edit Item" OnClick="editProduct_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
