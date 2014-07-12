<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="EditStudentArt.aspx.cs" Inherits="SDAFederation.EditStudentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="centerRoundedDiv">
        <asp:Label ID="editArtLabel" runat="server" Text="Art:" CssClass="rightAlignHeaderLabel" /> <asp:DropDownList ID="editArt" runat="server" /><br />
        <asp:Label ID="editBeltLabel" runat="server" Text="Belt:" CssClass="rightAlignHeaderLabel" /> <asp:DropDownList ID="editBelt" runat="server" /><br />
        <br />
        <asp:Button ID="updateArt" runat="server" Text="Update Information" OnClick="updateArt_Click" />
    </div>
</asp:Content>
