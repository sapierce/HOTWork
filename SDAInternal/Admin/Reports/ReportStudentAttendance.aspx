<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportStudentAttendance.aspx.cs" Inherits="HOTSelfDefense.Admin.Reports.ReportStudentAttendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Class Attendance for <%=Request.QueryString["txtADate"]%></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataGrid ID="dgrStudentAttendance" runat="server" />
    </div>
    </form>
</body>
</html>
