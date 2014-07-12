<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="PublicWebsite.Members"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div id='members'>
        Whether you're a current customer of HOT Tropical Tans or a new client, by signing
        up for an account online you can take advantage of these special offers:
        <div id="memberBenefits">
            * Three Month Unlimited tanning packages on all beds<br />
            * One Year Unlimited tanning packages on all beds<br />
            * Student Specials<br />
            * Exclusive online deals<br />
            * And much more!<br />
        </div>
        <br />
        In addition, you can schedule your appointments online and up to FIVE days in advance
        compared to the typical three day limit.<br />
        <br />
        <table style="width: 500px; text-align: center;">
            <tr>
                <td colspan='3' class='header'>
                    <asp:ValidationSummary ID="signUpSummary" runat="server" EnableClientScript="true" DisplayMode="BulletList" HeaderText="The following errors were found:" ValidationGroup="signUp" ShowSummary="true" />
                    <asp:RequiredFieldValidator ID="requiredStatement" runat="server" EnableClientScript="true" Display="None" ErrorMessage="Please enter a statement date." ControlToValidate="signUpSelect" ValidationGroup="signUp" />
                    <asp:RegularExpressionValidator ID="regularListType" runat="server" EnableClientScript="true" Display="None" ErrorMessage="Sign up type can only be NEW or CURRENT." ControlToValidate="signUpSelect" ValidationGroup="signUp" ValidationExpression="^(NEW)|(CURRENT)$" />
                    Ready to sign up?  Begin here:
                        <asp:DropDownList runat="server" ID="signUpSelect" ValidationGroup="signUp">
                            <asp:ListItem Text="I have previously tanned at HOT Tropical Tans." Value="CURRENT" />
                            <asp:ListItem Text="I have NEVER tanned at HOT Tropical Tans." Value="NEW" />
                        </asp:DropDownList>
                    <asp:Button runat="server" ID="signUpSubmit" Text="Sign up" OnClick="signUpSubmit_Click" />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <a href="clientform.pdf" class="center">Print off a Client Form and bring it in to HOT Tropical Tans</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>