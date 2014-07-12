<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionReceipt.aspx.cs" Inherits="HOTPOS.TransactionReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
  <head>
	<title>Receipt</title>
 </head>
  <body>
	<form id="frmReceipt" method="post" runat="server">
		<asp:label id="errorMessage" runat="server" CssClass="error"/>
		<table>
			<tr>
				<td colspan="4">
					<asp:Label ID='address' runat="server" />
				</td>
			</tr>
			<tr>
				<td style="vertical-align:top;" colspan="4">
					<asp:Label ID='voidIndicator' runat="server" />
				</td>
			</tr>
			<tr>
				<td style="vertical-align:top;text-align:right;" colspan="2">
					Sold to:
				</td>
				<td style="vertical-align:top;" colspan="2">
					<asp:Label ID='customerName' runat='server' />
				</td>
			</tr>
			<tr>
				<td style="vertical-align:top;text-align:right;" colspan="2">
					Date:
				</td>
				<td style="vertical-align:top;" colspan='2'>
					<asp:Label ID='transactionDate' runat='server' />
				</td>
			</tr>
			<tr>
				<td><b>Item</b></td>
				<td><b>Quantity</b></td>
				<td><b>Unit Price</b></td>
				<td><b>Total Price</b></td>
			</tr>
			<asp:Literal ID='itemsList' runat='server' />
			<tr>
				<td style="text-align: right; vertical-align:top;" colspan="3">SubTotal:</td>
				<td valign='top'>
					<asp:Label ID='subTotal' runat='server' />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; vertical-align:top;" colspan="3">Tax:</td>
				<td style="text-align: left; vertical-align:top;">
					<asp:Label ID='tax' runat='server' />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; vertical-align:top;" colspan="3">Total:</td>
				<td style="text-align: left; vertical-align:top;">
					<asp:Label ID='total' runat='server' />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; vertical-align:top;" colspan="3">Payment by:</td>
				<td style="text-align: left; vertical-align:top;">
					<asp:Label ID='paymentMethod' runat='server' />
				</td>
			</tr>
			<tr><td colspan="4"><br /></td></tr>
			<tr><td colspan="4"><br /></td></tr>
			<tr><td style="text-align: right; vertical-align:top;" colspan="4"><a href="javascript:window.print()">Print This Receipt</a></td></tr>
			<tr>
				<td  style="text-align: right; vertical-align:top;" colspan="3"><asp:textbox id='emailAddress' runat='server' /></td>
				<td><asp:button id='emailReceipt' text='Email Receipt' runat='server' /></td>
			</tr>
		</table>
	</form>
  </body>
</html>