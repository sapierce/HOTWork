<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SDAFederation._Default" MasterPageFile="Federation.Master" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" ID="default" runat="server">
    <div class="centerRoundedDiv">
        <h1>Log In</h1>
        School: <asp:DropDownList ID="federationSchools" runat="server" /><br /><br />
        <asp:Button ID="login" runat="server" Text="Log In" OnClick="login_Click" />
    </div>
</asp:Content>
