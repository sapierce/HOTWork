<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HOTTropicalTans.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <asp:Panel ID="mainSearch" runat="server">
        <table>
            <tr>
                <td align="right">
                    <table class="tanning">
                        <thead>
                            <tr>
                                <th colspan="2">Search for customer
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="rightAlignHeader">Last Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="lastName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="rightAlignHeader">First Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="firstName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button OnClick="customerNameSearch_Click" Text="Search" runat="server" ID="customerNameSearch" />
                                </td>
                            </tr>                                                        
                        </tbody>
                    </table>
                </td>
                <td align="left" valign="top">
                    <table class="tanning">
                        <thead>
                            <tr>
                                <th colspan="2">Customers By Last Name
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td align="center">
                                <asp:LinkButton Text="A" runat="server" OnClick="customerLetter_Click" ID="lastNameA" />
                                |
										<asp:LinkButton Text="B" runat="server" OnClick="customerLetter_Click" ID="lastNameB" />
                                |
										<asp:LinkButton Text="C" runat="server" OnClick="customerLetter_Click" ID="lastNameC" />
                                |
										<asp:LinkButton Text="D" runat="server" OnClick="customerLetter_Click" ID="lastNameD" />
                                |
										<asp:LinkButton Text="E" runat="server" OnClick="customerLetter_Click" ID="lastNameE" />
                                |
										<asp:LinkButton Text="F" runat="server" OnClick="customerLetter_Click" ID="lastNameF" />
                                |
										<asp:LinkButton Text="G" runat="server" OnClick="customerLetter_Click" ID="lastNameG" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton Text="H" runat="server" OnClick="customerLetter_Click" ID="lastNameH" />
                                |
										<asp:LinkButton Text="I" runat="server" OnClick="customerLetter_Click" ID="lastNameI" />
                                |
										<asp:LinkButton Text="J" runat="server" OnClick="customerLetter_Click" ID="lastNameJ" />
                                |
										<asp:LinkButton Text="K" runat="server" OnClick="customerLetter_Click" ID="lastNameK" />
                                |
										<asp:LinkButton Text="L" runat="server" OnClick="customerLetter_Click" ID="lastNameL" />
                                |
										<asp:LinkButton Text="M" runat="server" OnClick="customerLetter_Click" ID="lastNameM" />
                                |
										<asp:LinkButton Text="N" runat="server" OnClick="customerLetter_Click" ID="lastNameN" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton Text="O" runat="server" OnClick="customerLetter_Click" ID="lastNameO" />
                                |
										<asp:LinkButton Text="P" runat="server" OnClick="customerLetter_Click" ID="lastNameP" />
                                |
										<asp:LinkButton Text="Q" runat="server" OnClick="customerLetter_Click" ID="lastNameQ" />
                                |
										<asp:LinkButton Text="R" runat="server" OnClick="customerLetter_Click" ID="lastNameR" />
                                |
										<asp:LinkButton Text="S" runat="server" OnClick="customerLetter_Click" ID="lastNameS" />
                                |
										<asp:LinkButton Text="T" runat="server" OnClick="customerLetter_Click" ID="lastNameT" />
                                |
										<asp:LinkButton Text="U" runat="server" OnClick="customerLetter_Click" ID="lastNameU" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton Text="V" runat="server" OnClick="customerLetter_Click" ID="lastNameV" />
                                |
										<asp:LinkButton Text="W" runat="server" OnClick="customerLetter_Click" ID="lastNameW" />
                                |
										<asp:LinkButton Text="X" runat="server" OnClick="customerLetter_Click" ID="lastNameX" />
                                |
										<asp:LinkButton Text="Y" runat="server" OnClick="customerLetter_Click" ID="lastNameY" />
                                |
										<asp:LinkButton Text="Z" runat="server" OnClick="customerLetter_Click" ID="lastNameZ" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <table class="tanning">
                        <thead>
                            <tr>
                                <th colspan="2">Product Search
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="rightAlignHeader">Product Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="productName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button OnClick="productSearch_Click" Text="Search" runat="server" ID="productSearch" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td align="left" valign="top">
                    <table class="tanning">
                        <thead>
                            <tr>
                                <th colspan="2">Tan Log Between:
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="rightAlignHeader">Beginning Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="beginDate" Text="9999-12-31" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="rightAlignHeader">Ending Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="endDate" Text="9999-12-31" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="tanLog" Text="Search" runat="server" OnClick="tanLog_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="searchResults" runat="server">
        <table class='tanning'>
            <thead><tr><th colspan='2'>Search Results</th></tr></thead>
            <asp:Label ID="outputResults" runat="server" />
        </table>
    </asp:Panel>
</asp:Content>