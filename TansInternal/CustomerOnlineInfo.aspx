<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true"
    CodeBehind="CustomerOnlineInfo.aspx.cs" Inherits="HOTTropicalTans.CustomerOnlineInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <script type="text/javascript">
		<!--
    function confirmation() {
        var answer = confirm("Are you sure you wish to delete this online account? Confirmation will be sent to the e-mail address on file.")
        return answer;
    }
    //-->
    </script>

    <table width="500" class="tanning">
        <thead>
            <tr>
                <th colspan="2">Online Account Information</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">User Name:
                </td>
                <td>
                    <asp:Label ID="userName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">E-mail Address:
                </td>
                <td>
                    <asp:Label ID="emailAddress" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Reset Password:
                </td>
                <td>
                    <asp:Button ID="sendPassword" runat="server" Text="Reset Password" OnClick="sendPassword_Click" /><br />
                    <div class="detailInformation">Will be sent to e-mail address on file</div>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Change E-mail Address:
                </td>
                <td>
                    <asp:TextBox ID="newEmailAddress" runat="server" />&nbsp;&nbsp;
				<asp:Button ID="changeEmail" runat="server" Text="Change E-mail" OnClick="changeEmail_Click" /><br />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">
                    <br />
                </td>
                <td>
                    <asp:Button ID="deleteAccount" OnClientClick="confirmation()" OnClick="deleteAccount_Click" Text="Delete Online Account" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <asp:Panel ID="signUpInfo" runat="server">
        <br />
        <br />
        <table width="500" class="tanning">
            <thead>
                <tr>
                    <th colspan="2">Client Record
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="rightAlignHeader">Name:
                    </td>
                    <td>
                        <asp:Label ID="name" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Date of Birth:
                    </td>
                    <td>
                        <asp:Label ID="dateOfBirth" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Fitzpatrick Number:
                    </td>
                    <td>
                        <asp:Label ID="fitzPatrickNumber" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Address:
                    </td>
                    <td>
                        <asp:Label ID="address" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">City:
                    </td>
                    <td>
                        <asp:Label ID="city" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">State:
                    </td>
                    <td>
                        <asp:Label ID="state" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Zip Code:
                    </td>
                    <td>
                        <asp:Label ID="zipCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Phone Number:
                    </td>
                    <td>
                        <asp:Label ID="phoneNumber" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Family history of skin cancer?
                    </td>
                    <td>
                        <asp:CheckBox ID="familyHistory" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Personal history of skin cancer?
                    </td>
                    <td>
                        <asp:CheckBox ID="personalHistory" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Acknowledged customer notice?
                    </td>
                    <td>
                        <asp:CheckBox ID="acknowledgedNotice" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rightAlignHeader">Customer Signature:
                    </td>
                    <td>
                        <asp:Label ID="signature" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Content>