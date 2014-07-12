<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionReceipt.aspx.cs" Inherits="SDAPOS.TransactionReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
  <head>
    <title>Receipt</title>
 </head>
  <body>
	<form id="frmReceipt" method="post" runat="server">
		<p align='center'><asp:label id="lblError" runat="server"/></p>
		<table>
			<tr>
			    <td colspan='3'>
			        <asp:Label ID='lblAddress' runat="server" />
			        <br />
			    </td>
		    </tr>
		    <tr>
		        <td valign='top' colspan='3'>
		            <asp:Label ID='lblVoid' runat="server" />
		        </td>
		    </tr>
		    <tr>
		        <td valign='top' align='right'>
		            Sold to:
		        </td>
		        <td valign='top' colspan='2'>
		            <asp:Label ID='lblName' runat='server' />
		        </td>
		    </tr>
		    <tr>
		        <td><b>Item</b></td>
		        <td><b>Quantity</b></td>
		        <td><b>Unit Price</b></td>
		    </tr>
		    <asp:Literal ID='litItems' runat='server' />
		    <tr>
		        <td valign='top' align='right' colspan='2'>SubTotal:</td>
		        <td valign='top'>
		            <asp:Label ID='lblSubTotal' runat='server' />
		        </td>
		    </tr>
		    <tr>
		        <td valign='top' align='right' colspan='2'>Tax:</td>
		        <td valign='top'>
		            <asp:Label ID='lblTax' runat='server' />
		        </td>
		    </tr>
		    <tr>
		        <td valign='top' align='right' colspan='2'>Total:</td>
		        <td valign='top'>
		            <asp:Label ID='lblTotal' runat='server' />
		        </td>
		    </tr>
		    <tr>
		        <td valign='top' align='right' colspan='2'>Payment by:</td>
		        <td valign='top'>
		            <asp:Label ID='lblPayment' runat='server' />
		        </td>
		    </tr>
		    <tr><td valign='top' align='left' colspan='2'><br /></td></tr>
		    <tr><td valign='top' align='left' colspan='2'><br /></td></tr>
		    <tr><td valign='top' align='left' colspan='2'><a href="javascript:window.print()">Print This Receipt</a></td></tr>
		    <tr>
		        <td valign='top' align='left'><asp:textbox id='txtEmail' runat='server' /></td>
		        <td><asp:button id='btnSubmit' text='Email Receipt' runat='server' /></td>
		    </tr>
		</table>
    </form>
  </body>
</html>