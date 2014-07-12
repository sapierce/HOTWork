<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfo.aspx.cs" Inherits="HOTTropicalTans.CustomerInfo"
    MasterPageFile="HOTTropicalTans.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Main">
    <table width='500' class='tanning'>
        <thead>
            <tr>
                <th colspan="3">Customer Information
                </th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">ID:
            </td>
            <td>
                <asp:Label ID="customerID" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Name:
            </td>
            <td>
                <asp:Label ID="customerName" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Fitzpatrick Number:
            </td>
            <td>
                <asp:Label ID="fitzpatrickNumber" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Join Date:
            </td>
            <td>
                <asp:Label ID="joinDate" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Plan:
            </td>
            <td>
                <asp:Label ID="planName" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Plan Renewal Date:
            </td>
            <td>
                <asp:Label ID="renewalDate" runat="server" />
            </td>
            <td>
                <a href="<% =HOTBAL.TansConstants.CUSTOMER_BILL_HISTORY_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>">History</a>
            </td>
        </tr>
        <asp:Literal ID="specialRenewalInformation" runat="server" />
        <tr>
            <td class="rightAlignHeader">Remarks:
            </td>
            <td>
                <asp:Label ID="remarks" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Email:
            </td>
            <td>
                <asp:Label ID="emailAddress" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <asp:Literal ID="notes" runat="server" />
        <tr>
            <td class="rightAlignHeader">Lotion note?
            </td>
            <td>
                <asp:CheckBox ID="lotionCheck" runat="server" />
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Online User?
            </td>
            <td>
                <asp:CheckBox ID="onlineUser" runat="server" />
            </td>
            <td>
                <asp:Label ID="onlineUserInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Signed up online?
            </td>
            <td>
                <asp:CheckBox ID="signUpOnline" runat="server" />
            </td>
            <td>
                <asp:Label ID="signUpOnlineInfo" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table class="standardTable">
        <tr>
            
            <td align='center' valign='top'>
                <asp:Button ID="addMassageAppointment" runat="server" OnClick="addMassageAppointment_Click" Style="width: 175px;" Text="Add Massage Appointment" />
            </td>
            <td align='center' valign='top'>
                <asp:Button ID="addTanningAppointment" runat="server" OnClick="addTanningAppointment_Click" Style="width: 175px;" Text="Add Tanning Appointment" />
            </td>
        </tr>
        <tr>
            <td align='center' valign='top'>
                <asp:Button ID="editCustomer" runat="server" OnClick="editCustomer_Click" Style="width: 175px;" Text="Edit/Delete Customer" />
            </td>
            <td align='center' valign='top' colspan="2">
                <asp:Button ID="transactionList" runat="server" Text="Transactions" OnClick="transactionList_Click" Style="width: 175px;" />
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td style="vertical-align: top;">
                <table class='tanning'>
                    <thead>
                        <tr>
                            <th colspan='5'>Last 10 Tans</th>
                            <th colspan='2'><a href='<% =HOTBAL.TansConstants.CUSTOMER_TAN_HISTORY_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>'>Full Tan History</a></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="centerAlignHeader">
                                <br />
                            </td>
                            <td class="centerAlignHeader">Date</td>
                            <td class="centerAlignHeader">Time</td>
                            <td class="centerAlignHeader">Bed</td>
                            <td class="centerAlignHeader">Length</td>
                            <td class="centerAlignHeader">
                                <br />
                            </td>
                            <td class="centerAlignHeader">
                                <br />
                            </td>
                        </tr>
                        <asp:Literal ID="tanLog" runat="server" />
                    </tbody>
                </table>
            </td>
            <td style="vertical-align: top;">
                <%--<table class='tanning'>
                    <thead>
                    <tr>
                        <th colspan='4'>Last 10 Massages</th>
                        <th colspan='2'><a href='<% =HOTBAL.TansConstants.CUSTOMER_MASSAGE_HISTORY_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>'>Full Massage History</a></th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                        <td class="centerAlignHeader">Date</td>
                        <td class="centerAlignHeader">Time</td>
                        <td class="centerAlignHeader">Length</td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                    </tr>--%>
                <asp:Literal ID="massageLog" runat="server" />
                <%--</tbody>
                </table>--%>
            </td>
        </tr>
    </table>
</asp:Content>