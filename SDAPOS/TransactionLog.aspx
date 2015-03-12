<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionLog.aspx.cs" Inherits="SDAPOS.TransactionLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Transaction Log for
        <asp:Literal ID="logDate" runat="server" />
    </title>
    <style type="text/css">
        body {
            font-family: Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            font-size: medium;
            color: black;
            background-color: white;
        }
        table {
            border: 1px solid #000000;
        }

        tr {
            border: 1px solid #000000;
        }

        td {
            border: 1px solid #000000;
        }
    </style>
</head>
<body>
    <form id="fullTransaction" method="post" runat="server">
        <span style="align-content: center; text-align: center;">
            <asp:Label ID="errorMessage" CssClass="error" runat="server" />
        </span>
        <table width="75%">
            <tr>
                <td colspan="9">
                    <asp:Label ID="headerTitle" runat="server" Font-Size="Larger" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <span style="font-size: large; text-align: center; font-weight: bold;">Tanning</span>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td><strong>Date</strong></td>
                <td><strong>Bought By</strong></td>
                <td><strong>Items</strong></td>
                <td><strong>Seller</strong></td>
                <td><strong>Payment Method</strong></td>
                <td><strong>Sub-Total</strong></td>
                <td><strong>Tax</strong></td>
                <td><strong>Total</strong></td>
            </tr>
            <asp:Literal ID="tanningSales" runat="server" />
        </table>
        <table width="75%">
            <asp:Literal ID="tanningTotals" runat="server" />
        </table>
        <br />
        <br />
        <table width="75%">
            <tr>
                <td colspan="9">
                    <span style="font-size: large; text-align: center; font-weight: bold;">Martial Arts</span>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td><strong>Date</strong></td>
                <td><strong>Bought By</strong></td>
                <td><strong>Items</strong></td>
                <td><strong>Seller</strong></td>
                <td><strong>Payment Method</strong></td>
                <td><strong>Sub-Total</strong></td>
                <td><strong>Tax</strong></td>
                <td><strong>Total</strong></td>
            </tr>
            <asp:Literal ID="martialArtSales" runat="server" />
        </table>
        <table width="75%">
            <asp:Literal ID="martialArtTotals" runat="server" />
        </table>
        <br />
        <br />
        <table width="75%">
            <asp:Literal ID="completeTotals" runat="server" />
        </table>
    </form>
</body>
</html>