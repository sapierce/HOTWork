<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lotions.aspx.cs" Inherits="PublicWebsite.Lotions"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div class="product">
        <table style="text-align: center;">
            <tr>
                <td style="text-align: center;">
                    <div id="LOT1">
                        Lotions | 
                        <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>" class="center">Lip Balms</a> | 
                        <a href="<%= HOTBAL.TansConstants.GIFT_BAG_PUBLIC_URL %>" class="center">Gift Bags</a> | 
                        <a href="<%= HOTBAL.TansConstants.MISC_PUBLIC_URL %>" class="center">Other Accessories</a><br />
                        <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LM" class="center">Mystic</a> | 
                        <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LN" class="center">Non-Tingle</a> | 
                        <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LT" class="center">Tingle</a> | 
                        <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LS" class="center">Samples</a> | 
                        <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LO" class="center">Moisturizers</a><br />
                    </div>
                    <div id="LOT3">
                        <table>
                            <tr>
                                <td style="text-align: center;">
                                    <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LT">
                                        <img src="images/tingle.gif" border="0" alt="Tingle Lotions" /></a>
                                </td>
                                <td style="text-align: center;">
                                    <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LN">
                                        <img src="images/nontingle.gif" border="0" alt="Non-Tingle Lotions" /></a>
                                </td>
                                <td rowspan='2' style="text-align: center; vertical-align: middle;">
                                    <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LS">
                                        <img src="images/sample.gif" border="0" alt="Lotion Samples" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LM">
                                        <img src="images/mystic.gif" border="0" alt="Mystic Lotions" /></a>
                                </td>
                                <td style="text-align: center;">
                                    <a href="<%= HOTBAL.TansConstants.LOTIONS_PUBLIC_URL %>?Type=LO">
                                        <img src="images/moisturizer.gif" border="0" alt="Moisturizers" /></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>