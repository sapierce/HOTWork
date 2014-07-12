<%@ page title="" language="C#" masterpagefile="Site.Master" autoeventwireup="true" codebehind="Products.aspx.cs" inherits="MobileSite.Products" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div data-role="collapsible-set" data-theme="a" data-content-theme="a">
        <div data-role="collapsible">
            <h3>Lotions</h3>
            <ul data-role="listview" data-ajax="false" data-inset="true" data-theme="a" data-icon="arrow-r">
                <li><a href="Lotions.aspx?Type=LM">Mystic Lotions</a></li>
                <li><a href="Lotions.aspx?Type=LT">Tingle Lotions</a></li>
                <li><a href="Lotions.aspx?Type=LN">Non-Tingle Lotions</a></li>
                <li><a href="Lotions.aspx?Type=LO">Moisturizers</a></li>
            </ul>
        </div>
        <div data-role="collapsible">
            <h3>Lip Balms</h3>
            <p><asp:Label ID="lipBalmList" runat="server" /></p>
        </div>
        <div data-role="collapsible">
            <h3>Gift Bags</h3>
            <p><asp:Label ID="giftBagList" runat="server" /></p>
        </div>
        <div data-role="collapsible">
            <h3>Other Accessories</h3>
            <p><asp:Label ID="otherAccessories" runat="server" /></p>
        </div>
    </div>
</asp:Content>