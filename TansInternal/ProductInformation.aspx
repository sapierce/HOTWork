<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="ProductInformation.aspx.cs" Inherits="HOTTropicalTans.ProductInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
			<tr>
				<th colspan="3">
					Product Information
				</th>
			</tr>
        </thead>
        <tbody>
            <tr><td colspan="3"><asp:Label ID="noProduct" runat="server" /></td></tr>
            <tr>
                <td rowspan="6" style="vertical-align: top;">
                    <asp:Literal ID="productImage" runat="server" />
                </td>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Product Name
                </td>
                <td style="vertical-align: top;">
                    <asp:Label ID="productName" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Product Type
                </td>
                <td style="vertical-align: top;">
                    <asp:Label ID="productType" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Product Price
                </td>
                <td style="vertical-align: top;">
                    Non-members: <asp:Label ID="productPrice" runat="server" /><br />
                    Members: <asp:Label ID="productSalePrice" runat="server" /><br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Total Price<br />
                    <span class="detailInformation"><asp:Label ID="productTaxed" runat="server" /></span>
                </td>
                <td style="vertical-align: top;">
                    Non-members: <asp:Label ID="productTotalPrice" runat="server" /><br />
                    Members: <asp:Label ID="productTotalSalePrice" runat="server" /><br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Current Inventory
                </td>
                <td style="vertical-align: top;">
                    <asp:Label ID="productCount" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="rightAlignHeader">
                    Description
                </td>
                <td style="vertical-align: top;">
                    <asp:Label ID="productDescription" runat="server" Width="400" />
                </td>
            </tr>
        </tbody>
	</table>
</asp:Content>
