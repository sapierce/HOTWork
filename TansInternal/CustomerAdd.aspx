<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="HOTTropicalTans.CustomerAdd" %>

<asp:Content ID="addCustomerHeader" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <style>
        input.error {
    border: 1px solid red;
}
label.error {
    background: url("images/unchecked.gif") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    margin-left: 0.3em;
    padding-left: 16px;
}
label.valid {
    background: url("images/checked.gif") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    display: block;
    height: 16px;
    width: 16px;
}
    </style>
</asp:Content>
<asp:Content ID="addCustomerContent" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Add New Customer</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Last Name:</td>
                <td>
                    <asp:TextBox ID="lastName" runat="server" MaxLength="50" class="lastName" />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">First Name:</td>
                <td>
                    <asp:TextBox ID="firstName" runat="server" MaxLength="50" class="firstName" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Fitzpatrick Number:</td>
                <td>
                    <asp:DropDownList ID="fitzpatrickNumber" runat="server" class="fitzNumber">
                        <asp:ListItem Value="">-Choose-</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Join Date:</td>
                <td>
                    <asp:TextBox ID="joinDate" runat="server" Enabled="false" MaxLength="10" class="joinDate" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Plan:</td>
                <td>
                    <asp:DropDownList ID="plans" runat="server" OnSelectedIndexChanged="plans_SelectedIndexChanged" AutoPostBack="true" class="package" />
                </td>
            </tr>
            <tr>
                <td class="centerAlignHeader" style="width: 50%;">
                    -OR-
                </td>
                <td class="centerAlignHeader">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Special:</td>
                <td>
                    <asp:DropDownList ID="specials" runat="server" OnSelectedIndexChanged="specials_SelectedIndexChanged" AutoPostBack="true" class="special" />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Renewal Date:</td>
                <td>
                    <asp:TextBox ID="renewalDate" runat="server" Enabled="false" MaxLength="10" class="renewalDate" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Remarks or Special Notes:<br /> <span class="detailInformation">(bed preference, water wash, etc.)</span></td>
                <td>
                    <asp:TextBox ID="remarks" runat="server" TextMode="MultiLine" Rows="3" MaxLength="200" class="notes" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="addNewCustomer" runat="server" Text="Add Customer" OnClick="addNewCustomer_Click" />
                </td>
            </tr>
        </tbody>
    </table>
    <script>
        // initialize tooltipster on text input elements
        $('#aspnetForm input[type="text"]').tooltipster({
            trigger: 'custom',
            onlyOne: false,
            position: 'right',
            theme: 'tooltipster-light'
        });

        // initialize tooltipster on select input elements
        $('#aspnetForm select').tooltipster({
            trigger: 'custom',
            onlyOne: false,
            position: 'right',
            theme: 'tooltipster-light'
        });

        $("#aspnetForm").validate({
            errorPlacement: function (error, element) {
                $(element).tooltipster('update', $(error).text());
                $(element).tooltipster('show');
            },
            success: function (label, element) {
                $(element).tooltipster('hide');
            }
        });

        $(".lastName").rules("add", {
            required: true,
            messages: {
                required: "Please enter in a last name.",
                maxlength: 50
            }
        });
        $(".firstName").rules("add", {
            required: true,
            messages: {
                required: "Please enter in a first name.",
                maxlength: 50
            }
        });
        $(".fitzNumber").rules("add", {
            required: true,
            messages: {
                required: "Please select a Fitzpatrick number.",
                range: [0, 6],
                maxlength: 1,
                digits: true
            }
        });
        $(".joinDate").rules("add", {
            required: true,
            date: true,
            messages: {
                required: "Please enter in a join date.",
                maxlength: 10,
                date: "Entered information must be in date format."
            }
        });
        var planSelection = document.getElementById("<% =plans.ClientID %>");
        var specialSelection = document.getElementById("<% =specials.ClientID %>");
        var planValue = planSelection.options[planSelection.selectedIndex].value;
        var specialValue = specialSelection.options[specialSelection.selectedIndex].value;
        if (planValue == "") {
            if (specialValue == "") {
                $(".package").rules("add", {
                    required: true,
                    messages: {
                        required: "Please select a tanning package."
                    }
                });
            }
        }
        $(".renewalDate").rules("add", {
            required: true,
            date: true,
            messages: {
                required: "Please enter in a renewal date.",
                maxlength: 10,
                date: "Entered information must be in date format."
            }
        });
        $(".notes").rules("add", {
            required: false,
            messages: {
                maxlength: 200
            }
        });
    </script>
</asp:Content>