<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ClassPrint.aspx.cs" Inherits="HOTSelfDefense.ClassPrint" %>

<!DOCTYPE html>
<html>
<head>
    <title>Class Roster</title>
    <style>
        body {
            font-family: Helvetica, Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif;
            font-size: 12px;
            text-align: left;
        }

        table {
            border-spacing: 10px;
        }

        th {
            border-bottom: 1px solid #000000;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            vertical-align: bottom;
            padding: 2px;
        }

        td {
            border-right: 1px solid #000000;
            border-bottom: 1px solid #000000;
            padding: 5px;
            vertical-align: middle;
        }

        .error {
            color: red;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <!-- Output error messages -->
        <asp:Label ID="errorMessage" class="error" runat="server" /><br />
        <table>
            <tr>
                <th>Class:</th>
                <td>
                    <!-- Output the class/lesson title -->
                    <asp:Label ID="classTitle" runat="server" /></td>
            </tr>
            <tr>
                <th>Art:</th>
                <td>
                    <!-- Output the class/lesson art(s) -->
                    <asp:Label ID="classArt" runat="server" /></td>
            </tr>
            <tr>
                <th>Instructor:</th>
                <td>
                    <!-- Output the class/lesson instructor -->
                    <asp:Label ID="classInstructor" runat="server" /></td>
            </tr>
        </table>
        <br />
        <br />
        <!-- Build a table list of students in this class/lesson -->
        <table>
            <tr>
                <th>Check In</th>
                <th>Student Name</th>
                <th>Note</th>
                <th>Belt</th>
                <th>Tip/Class</th>
                <th>Belt</th>
                <th>Tip/Class</th>
            </tr>
            <!-- Output a table list of students in this class/lesson -->
            <asp:Literal ID="printRoster" runat="server" />
        </table>
    </form>
</body>
</html>