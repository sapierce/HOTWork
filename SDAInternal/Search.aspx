<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Search.aspx.cs" Inherits="HOTSelfDefense.Search" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <!-- Display errors associated with validating Search information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="searchValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="search" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Search Validation -->
    <asp:RequiredFieldValidator ID="firstNameRequired" Display="None" runat="server" ControlToValidate="firstName" ErrorMessage="Please enter at least one character for first name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="search" />
    <asp:RequiredFieldValidator ID="lastNameRequired" Display="None" runat="server" ControlToValidate="lastName" ErrorMessage="Please enter at least one character for last name." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="search" />

    <asp:Panel ID="mainSearch" runat="server">
        <table style="margin-right: auto; margin-left: auto; padding: 10px;">
            <tr>
                <td style="width: 50%;">
                    <table class="defense">
                        <tr>
                            <th colspan="2">Search for Student
                            </th>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">Last Name:
                            </td>
                            <td>
                                <asp:TextBox ID='lastName' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">First Name:
                            </td>
                            <td>
                                <asp:TextBox ID="firstName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:CheckBox ID="activeOnlyName" runat="server" Text="Active Only?" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button OnClick="searchByName_Click" Text='Search' runat="server" ID="searchByName" CssClass="ui-button" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table class="defense">
                        <thead>
                            <tr>
                                <th colspan='2'>Students By Last Name
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton Text="A" runat="server" OnClick="customerLetter_Click" ID="lastNameA" />
                                |
										<asp:LinkButton Text="B" runat="server" OnClick="customerLetter_Click" ID="lastNameB" />
                                |
										<asp:LinkButton Text="C" runat="server" OnClick="customerLetter_Click" ID="lastNameC" />
                                |
										<asp:LinkButton Text="D" runat="server" OnClick="customerLetter_Click" ID="lastNameD" />
                                |
										<asp:LinkButton Text="E" runat="server" OnClick="customerLetter_Click" ID="lastNameE" />
                                |
										<asp:LinkButton Text="F" runat="server" OnClick="customerLetter_Click" ID="lastNameF" />
                                |
										<asp:LinkButton Text="G" runat="server" OnClick="customerLetter_Click" ID="lastNameG" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton Text="H" runat="server" OnClick="customerLetter_Click" ID="lastNameH" />
                                |
										<asp:LinkButton Text="I" runat="server" OnClick="customerLetter_Click" ID="lastNameI" />
                                |
										<asp:LinkButton Text="J" runat="server" OnClick="customerLetter_Click" ID="lastNameJ" />
                                |
										<asp:LinkButton Text="K" runat="server" OnClick="customerLetter_Click" ID="lastNameK" />
                                |
										<asp:LinkButton Text="L" runat="server" OnClick="customerLetter_Click" ID="lastNameL" />
                                |
										<asp:LinkButton Text="M" runat="server" OnClick="customerLetter_Click" ID="lastNameM" />
                                |
										<asp:LinkButton Text="N" runat="server" OnClick="customerLetter_Click" ID="lastNameN" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton Text="O" runat="server" OnClick="customerLetter_Click" ID="lastNameO" />
                                |
										<asp:LinkButton Text="P" runat="server" OnClick="customerLetter_Click" ID="lastNameP" />
                                |
										<asp:LinkButton Text="Q" runat="server" OnClick="customerLetter_Click" ID="lastNameQ" />
                                |
										<asp:LinkButton Text="R" runat="server" OnClick="customerLetter_Click" ID="lastNameR" />
                                |
										<asp:LinkButton Text="S" runat="server" OnClick="customerLetter_Click" ID="lastNameS" />
                                |
										<asp:LinkButton Text="T" runat="server" OnClick="customerLetter_Click" ID="lastNameT" />
                                |
										<asp:LinkButton Text="U" runat="server" OnClick="customerLetter_Click" ID="lastNameU" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton Text="V" runat="server" OnClick="customerLetter_Click" ID="lastNameV" />
                                |
										<asp:LinkButton Text="W" runat="server" OnClick="customerLetter_Click" ID="lastNameW" />
                                |
										<asp:LinkButton Text="X" runat="server" OnClick="customerLetter_Click" ID="lastNameX" />
                                |
										<asp:LinkButton Text="Y" runat="server" OnClick="customerLetter_Click" ID="lastNameY" />
                                |
										<asp:LinkButton Text="Z" runat="server" OnClick="customerLetter_Click" ID="lastNameZ" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="activeOnlyLast" runat="server" Text="Active Only?" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="searchResults" runat="server">
        <table class="defense">
            <tr>
                <th colspan="2">Student Results</th>
            </tr>
            <asp:Literal ID="searchResultsOutput" runat="server" />
        </table>
    </asp:Panel>
</asp:Content>