<%@ Page Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="FullTransactionLog.aspx.cs" Inherits="HOTSelfDefense.Admin.Reports.FullTransactionLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>Full Transaction Log for <asp:Literal ID="titleDate" runat="server" />
		</title>
	</head>
	<body>
		<form id="frmTrnsFull" runat="server">
			<p align='center'><asp:label id="errorMessage" class="error" runat="server" /></p>
			<table width="75%" border="1" bordercolor="#000000">
				<tr>
					<td colspan='7'><h3><asp:Label ID="headerText" Runat="server"/></h3>
					</td>
				</tr>
				<tr>
					<td colspan='7'><h4>Tanning</h4>
					</td>
				</tr>
				<tr>
					<td><br /></td>
					<td><b>Bought By</b></td>
					<td><b>Items</b></td>
					<td><b>Location</b></td>
					<td><b>Seller</b></td>
					<td><b>Method</b></td>
					<td><b>Total</b></td>
				</tr>
                <asp:Literal ID="litTanningSales" runat="server" />
			</table>
			<table width="75%" border="1" bordercolor="#000000">
				<tr>
					<td colspan='7'><h4>Tanning Totals</h4>
					</td>
				</tr>
                <asp:Literal ID="litTanningTotals" runat="server" />
			</table>
			<br /><br />
			<table width="75%" border="1" bordercolor="#000000">
				<tr>
					<td colspan='7'><h4>Martial Arts</h4>
					</td>
				</tr>
				<tr>
					<td><br /></td>
					<td><b>Bought By</b></td>
					<td><b>Items</b></td>
					<td><b>Location</b></td>
					<td><b>Seller</b></td>
					<td><b>Method</b></td>
					<td><b>Total</b></td>
				</tr>
                <asp:Literal ID="litMartialArtSales" runat="server" />
			</table>
			<table width="75%" border="1" bordercolor="#000000">
				<tr>
					<td colspan='7'><h4>Martial Arts Totals</h4>
					</td>
				</tr>
                <asp:Literal ID="litMartialArtTotals" runat="server" />
			</table>
			<br />
			<br />
			<table width="75%" border="1" bordercolor="#000000">
				<tr>
					<td colspan='7'><h4>Complete Totals</h4>
					</td>
				</tr>
                <asp:Literal ID="litCompleteTotals" runat="server" />
			</table>
			<P></P>
		</form>
	</body>
</html>
