<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SDAFederation.admin._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="centerRoundedDiv">
        <asp:Label ID="schoolLabel" runat="server" CssClass="standardHeaderLabel" Text="School Information" /><br />
        <a href="AddSchool.aspx">Add a School</a><br />
        Edit/Delete a School: <br />
        <asp:DropDownList ID="federationSchools" runat="server" /><br />
        <asp:Button ID="editSchool" runat="server" Text="Edit/Delete School" OnClick="editSchool_Click" />
    </div>
    <div class="centerRoundedDiv">
        <asp:Label ID="artLabel" runat="server" CssClass="standardHeaderLabel" Text="Art Information" /><br />
        <a href="AddArt.aspx">Add an Art</a><br />
        Edit/Delete an Art: <br />
        <asp:DropDownList ID="artSchool" runat="server" OnSelectedIndexChanged="artSchool_SelectedIndexChanged" AutoPostBack="true" /><br />
        <asp:DropDownList ID="artArts" runat="server" /><br />
        <asp:Button ID="editArt" runat="server" Text="Edit/Delete Art" OnClick="editArt_Click" />
    </div>
    <div class="centerRoundedDiv">
        <asp:Label ID="beltLabel" runat="server" CssClass="standardHeaderLabel" Text="Belt Information" /><br />
        <a href="AddBelt.aspx">Add a Belt</a><br />
        Edit/Delete a Belt: <br />
        <asp:DropDownList ID="beltSchool" runat="server" OnSelectedIndexChanged="beltSchool_SelectedIndexChanged" AutoPostBack="true" /><br />
        <asp:DropDownList ID="beltArts" runat="server" OnSelectedIndexChanged="beltArts_SelectedIndexChanged" AutoPostBack="true" /><br />
        <asp:DropDownList ID="beltBelts" runat="server" /><br />
        <asp:Button ID="editBelt" runat="server" Text="Edit/Delete Belt" OnClick="editBelt_Click" />
    </div>
    <div class="centerRoundedDiv">
        <asp:Label ID="productLabel" runat="server" CssClass="standardHeaderLabel" Text="Product Information" /><br />
        <a href="AddItem.aspx">Add a Product</a><br />
        Edit/Delete a Product: <br />
        <asp:DropDownList ID="federationProducts" runat="server" /><br />
        <asp:Button ID="editProduct" runat="server" Text="Edit/Delete Product" OnClick="editProduct_Click" />
    </div>
</asp:Content>
