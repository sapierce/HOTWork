<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="AddBelt.aspx.cs" Inherits="SDAFederation.admin.AddBelt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:ValidationSummary ID="validation" DisplayMode="BulletList" EnableClientScript="true" ShowSummary="true" ValidationGroup="addBelt" runat="server" HeaderText="Please correct the following issues:" />
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" ControlToValidate="beltTitle" ErrorMessage="Please enter a belt name" ValidationGroup="addBelt" runat="server" />
    <h1>Add a Belt</h1>
    School: <asp:DropDownList ID="federationSchools" runat="server" OnSelectedIndexChanged="federationSchools_SelectedIndexChanged" AutoPostBack="true" /><br />
    Art: <asp:DropDownList ID="schoolArts" runat="server" /><br />
    Belt: <asp:TextBox ID="beltTitle" runat="server" ValidationGroup="addBelt" /><br />
    Belt Level: <asp:TextBox ID="beltLevel" runat="server" Text="0" /><br />
    <asp:Button ID="addBelt" runat="server" Text="Add Belt" CausesValidation="true" OnClick="addBelt_Click" />
</asp:Content>
