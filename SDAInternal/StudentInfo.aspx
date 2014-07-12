<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentInfo.aspx.cs" Inherits="HOTSelfDefense.StudentInfo" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="headerContent" runat="server" ContentPlaceHolderID="headerPlaceHolder">
    <script type="text/javascript">
        function confirmDelete(classId) {
            var answer = confirm("Are you sure you want to remove this student from the class?")
            if (answer) {
                window.location = "<% =HOTBAL.SDAConstants.STUDENT_DELETE_CLASS_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>&CID=" + classId;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table style="width: 90%;">
        <tr>
            <td style="vertical-align: top; width: 50%;">
                <table class="defense" style="vertical-align: top; width: 75%;">
                    <thead>
                        <tr>
                            <th colspan="2">Student Information</th>
                        </tr>
                    </thead>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">ID:</td>
                        <td>
                            <asp:Label ID="studentId" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Name:</td>
                        <td>
                            <asp:Label ID="studentName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Address:</td>
                        <td>
                            <asp:Label ID="studentAddress" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Birthday:</td>
                        <td>
                            <asp:Label ID="studentBirthday" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Emergency Contact:</td>
                        <td>
                            <asp:Label ID="emergencyContact" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Passing:</td>
                        <td>
                            <asp:Label ID="studentPassing" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Paying:</td>
                        <td>
                            <asp:Label ID="studentPaying" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Payment Amount:</td>
                        <td>
                            <asp:Label ID="paymentAmount" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Payment Plan:</td>
                        <td>
                            <asp:Label ID="paymentPlan" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Payment Date:</td>
                        <td>
                            <asp:Label ID="paymentDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Notes:</td>
                        <td>
                            <asp:Label ID="studentNotes" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader" style="width: 50%;">Active:</td>
                        <td>
                            <asp:Label ID="studentActive" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="accordion">
                                <h3>Phone Numbers</h3>
                                <div>
                                    <table class="defense">
                                        <tr>
                                            <th>Relation</th>
                                            <th>Number</th>
                                            <th>
                                                <br />
                                            </th>
                                        </tr>
                                        <asp:Literal ID="phoneList" runat="server" />
                                        <tr>
                                            <td colspan="3">
                                                <a href="<% =HOTBAL.SDAConstants.STUDENT_INFO_PHONES_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>">Add a Number</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <script>
                                $("#accordion").accordion({ collapsible: true, active: false });
                            </script>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="vertical-align: top; width: 100%; text-align: center;">
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="editInformation" runat="server" Text="Edit Student Information" OnClick="editInformation_Click" CssClass="ui-button" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="studentTransactions" runat="server" Text="View Student transactions" OnClick="studentTransactions_Click" CssClass="ui-button" />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%;">
                <table class="defense" style="vertical-align: top; width: 100%;">
                    <tr>
                        <th colspan="4">Art Information (<a href="<% =HOTBAL.SDAConstants.STUDENT_ART_HISTORY_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"]%>">History</a>)
                        </th>
                        <th>
                            <asp:Button ID="addArt" runat="server" Text="Add Arts" OnClick="addArt_Click" CssClass="ui-button" />
                        </th>
                    </tr>
                    <tr>
                        <td class="centerAlignHeader">Art</td>
                        <td class="centerAlignHeader">Belt</td>
                        <td class="centerAlignHeader">Tip</td>
                        <td class="centerAlignHeader">Class Count</td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                    </tr>
                    <asp:Literal ID="studentArtList" runat="server" />
                </table>
                <br />
                <table class="defense" style="vertical-align: top; width: 100%;">
                    <tr>
                        <th colspan="6">Recurring Classes</th>
                        <th>
                            <asp:Button ID="addClass" runat="server" Text="Add Class" OnClick="addClass_Click" CssClass="ui-button" />
                        </th>
                    </tr>
                    <tr>
                        <td class="centerAlignHeader">Day</td>
                        <td class="centerAlignHeader">Time</td>
                        <td class="centerAlignHeader">Art</td>
                        <td class="centerAlignHeader">Class</td>
                        <td class="centerAlignHeader">Instructor</td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                    </tr>
                    <asp:Literal ID="recurringClasses" runat="server" />
                </table>
                <br />
                <table class="defense" style="vertical-align: top; width: 100%;">
                    <tr>
                        <th colspan="6">Recurring Private Lessons</th>
                        <th>
                            <asp:Button ID="addLesson" runat="server" Text="Add Lesson" OnClick="addLesson_Click" CssClass="ui-button" />
                        </th>
                    </tr>
                    <tr>
                        <td class="centerAlignHeader">Date</td>
                        <td class="centerAlignHeader">Time</td>
                        <td class="centerAlignHeader">Art</td>
                        <td class="centerAlignHeader">Class</td>
                        <td class="centerAlignHeader">Instructor</td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                    </tr>
                    <asp:Literal ID="recurringLessons" runat="server" />
                </table>
                <br />
                <table class="defense" style="vertical-align: top; width: 100%;">
                    <tr>
                        <th colspan="6">Non-Recurring Classes/Lessons</th>
                    </tr>
                    <tr>
                        <td class="centerAlignHeader">Date</td>
                        <td class="centerAlignHeader">Time</td>
                        <td class="centerAlignHeader">Art</td>
                        <td class="centerAlignHeader">Class</td>
                        <td class="centerAlignHeader">Instructor</td>
                        <td class="centerAlignHeader">
                            <br />
                        </td>
                    </tr>
                    <asp:Literal ID="otherClassesLessons" runat="server" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>