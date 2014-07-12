<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="PublicWebsite.Product"
	MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
<table style="text-align:center;">
			<tr>
				<td style="text-align:center;">
					<div id="LOT1">
						<a href="lotions.aspx" class="center">Lotions</a> | <a href="lipbalms.aspx" class="center">Lip Balms</a> | <a href="giftbags.aspx" class="center">Gift Bags</a> | <a href="misc.aspx" class="center">Other Accessories</a><br>
						<asp:Literal ID="lotionsList" runat="server" />
					</div>
				<div id="LOT2">
						<table style="width:400px;">
							<asp:Literal ID="productInformation" runat="server" />
						</table>
					</div>
				</td>
			</tr>
		</table>
</asp:Content>