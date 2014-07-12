<%@ Page Title="About Us" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="MobileSite.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="about">
        <b>Waco Location:</b><br />
        710 N Valley Mills Drive<br />
        Waco, TX&nbsp;&nbsp;&nbsp;76710<br />
        <br />
        Phone: 254-399-9944<br />
        <br />
        <a href="https://maps.google.com/maps?q=710+North+Valley+Mills+Drive,+Waco,+TX&layer=c&z=17&iwloc=A&sll=31.529280,-97.177805&cbp=13,184.5,0,0,0&cbll=31.529471,-97.177787&hl=en&ved=0CAoQ2wU&sa=X&ei=0QhNUOWXJ8qrwQH95oHYAw">Map it!</a>
    </div>
    <div id="hours">
        <asp:Literal ID="hoursOfOperation" runat="server" /><br />
        <i>Last appointments are taken 30 minutes before closing</i>
    </div>
</asp:Content>