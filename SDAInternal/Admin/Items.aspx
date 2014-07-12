<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="HOTSelfDefense.Admin.Items" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align='center'>
        <asp:Label ID="lblError" class="error" runat="server" /></p>
    <table class='bcc' align='center'>
        <tr>
            <td class='header' colspan='2'>Product Information</td>
        </tr>
        <tr>
            <td class='rheader'>Name:</td>
            <td class='reg'>
                <asp:TextBox ID='txtProdName' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>Type:</td>
            <td class='reg'>
                <asp:DropDownList ID='sltProdType' runat="server">
                    <asp:ListItem Value="TSH">T-Shirt</asp:ListItem>
                    <asp:ListItem Value="MTL">Martial Arts</asp:ListItem>
                    <asp:ListItem Value="DIS">Discount</asp:ListItem>
                    <asp:ListItem Value="OTH">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class='rheader'>SubType:</td>
            <td class='reg'>
                <asp:DropDownList ID='sltProdSubType' runat="server">
                    <asp:ListItem Value="BQ">Martial Arts Banquet</asp:ListItem>
                    <asp:ListItem Value="MT">Martial Arts Uniforms</asp:ListItem>
                    <asp:ListItem Value="ML">Martial Arts Lessons (plan)</asp:ListItem>
                    <asp:ListItem Value="MD">Martial Arts Lessons (non-plan)</asp:ListItem>
                    <asp:ListItem Value="MG">Martial Arts Gear</asp:ListItem>
                    <asp:ListItem Value="MW">Martial Arts Weapons</asp:ListItem>
                    <asp:ListItem Value="PF">Promotion Fees</asp:ListItem>
                    <asp:ListItem Value="DS">Discount</asp:ListItem>
                    <asp:ListItem Value="OT">Other</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class='rheader'>Price:</td>
            <td class='reg'>
                <asp:TextBox ID='txtProdPrice' runat="server" Text="0.00" /></td>
        </tr>
        <tr>
            <td class='rheader'>Taxable?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkProdTax' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>Bar Code:</td>
            <td class='reg'>
                <asp:TextBox ID='txtProdCode' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>Description:</td>
            <td class='reg'>
                <asp:TextBox ID='txtProdDesc' TextMode="MultiLine" runat="server" Rows='3' Columns='30' /></td>
        </tr>
        <tr>
            <td class='lheader' colspan='2'>Available</td>
        </tr>
        <tr>
            <td class='rheader'>Online?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkOnOn' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>In Store?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkInOn' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>Nowhere?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkNone' runat="server" /></td>
        </tr>
        <tr>
            <td class='lheader'>Inventory</td>
            <td class='reg'>
                <asp:TextBox ID="txtWacoInv" runat="server" Text="0" /></td>
        </tr>
        <tr>
            <td class='lheader' colspan='2'>Sales</td>
        </tr>
        <tr>
            <td class='rheader'>Online?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkSlOn' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>In Store?</td>
            <td class='reg'>
                <asp:CheckBox ID='chkSlIn' runat="server" /></td>
        </tr>
        <tr>
            <td class='rheader'>Sale Price:</td>
            <td class='reg'>
                <asp:TextBox ID='txtProdSale' runat="server" Text="0.00" /></td>
        </tr>
        <tr>
            <td class="reg" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Item" OnClick="btnAdd_onClick" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit Item" OnClick="btnEdit_onClick" /><br />
                <br />
                <br />
                <br />
                <asp:Button ID="btnDelete" runat="server" Text="Delete Item" OnClick="btnDelete_onClick" />
            </td>
        </tr>
    </table>
</asp:Content>