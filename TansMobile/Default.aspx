<%@ Page Title="Home Page" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MobileSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div ID="main">
        <h1>Welcome to HOT Tropical Tans!</h1>
        <br />
        Waco's ONLY official Mystic Tan provider as well as a provider of superior regular
                tanning.<br />
        <br />
        Join today!<br />
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" OnClick="onClick_btnLogin"
            Text="Member Login" data-theme="a" data-mini="true" /><br />
        <asp:Button ID="btnAbout" runat="server" OnClick="onClick_btnAbout"
            Text="Salon Information" data-theme="a" data-mini="true" /><br />
        <asp:Button ID="btnProducts" runat="server" OnClick="onClick_btnProducts"
            Text="Available Products" data-theme="a" data-mini="true" /><br />
        <asp:Button ID="btnBeds" runat="server" OnClick="onClick_btnBeds"
            Text="Available Beds" data-theme="a" data-mini="true" /><br />
        <br />
    </div>
</asp:Content>