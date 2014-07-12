<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="SpecialAdd.aspx.cs" Inherits="HOTTropicalTans.admin.SpecialAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:ValidationSummary runat="server" ID="vsEmailAddress" CssClass="validation" HeaderText="The following validation errors were found..."
        DisplayMode="BulletList" ShowSummary="true" ValidationGroup="email" />
    <asp:RequiredFieldValidator ControlToValidate="specialName" Display="None" EnableClientScript="true"
        ErrorMessage="Please enter a Special Name" ID="rfvSpecialName" ValidationGroup="special"
        runat="server" />
    <asp:Label ID="lblError" CssClass="error" runat="server" />
    <table class='standardTable'>
        <tr>
            <td class='standardHeader' colspan='2'>Add Special
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Name:
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialName' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>One Word Name:
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialAbbrName' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Bed Type:
            </td>
            <td class='rightAlignHeader'>Length:
            </td>
        </tr>
        <tr>
            <td class='standardField'>
                <asp:DropDownList ID='specialType1' runat="server">
                    <asp:ListItem Value="">-Select Bed Type-</asp:ListItem>
                    <asp:ListItem Value="BB">Big Bed</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="PH">PowerHouse</asp:ListItem>
                    <asp:ListItem Value="SB">Small Bed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialLength1' runat="server" />
                day(s)
            </td>
        </tr>
        <tr>
            <td class='standardField'>
                <asp:DropDownList ID='specialType2' runat="server">
                    <asp:ListItem Value="">-Select Bed Type-</asp:ListItem>
                    <asp:ListItem Value="BB">Big Bed</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="PH">PowerHouse</asp:ListItem>
                    <asp:ListItem Value="SB">Small Bed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialLength2' runat="server" />
                day(s)
            </td>
        </tr>
        <tr>
            <td class='standardField'>
                <asp:DropDownList ID='specialType3' runat="server">
                    <asp:ListItem Value="">-Select Bed Type-</asp:ListItem>
                    <asp:ListItem Value="BB">Big Bed</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="PH">PowerHouse</asp:ListItem>
                    <asp:ListItem Value="SB">Small Bed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialLength3' runat="server" />
                day(s)
            </td>
        </tr>
        <tr>
            <td class='standardField'>
                <asp:DropDownList ID='specialType4' runat="server">
                    <asp:ListItem Value="">-Select Bed Type-</asp:ListItem>
                    <asp:ListItem Value="BB">Big Bed</asp:ListItem>
                    <asp:ListItem Value="MY">Mystic</asp:ListItem>
                    <asp:ListItem Value="PH">PowerHouse</asp:ListItem>
                    <asp:ListItem Value="SB">Small Bed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialLength4' runat="server" />
                day(s)
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Price:
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialPrice' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Bar Code:
            </td>
            <td class='standardField'>
                <asp:TextBox ID='specialBarCode' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='leftAlignHeader' colspan='2'>Available
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Online?
            </td>
            <td class='standardField'>
                <asp:CheckBox ID='availableOnline' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>In Store?
            </td>
            <td class='standardField'>
                <asp:CheckBox ID='availableInStore' runat="server" />
            </td>
        </tr>
        <tr>
            <td class='rightAlignHeader'>Online Summary/Description:</td>
            <td class='standardField'>
                <asp:TextBox ID='specialDescription' TextMode="MultiLine" runat="server" Rows='3' Columns='30' /></td>
        </tr>
        <tr>
            <td class='standardField' colspan='2'>
                <asp:Button ID="btnAddSpecial" Text="Add Special" OnClick="onClick_btnAddSpecial"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>