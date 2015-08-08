using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentInfo : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sdaMethods = new HOTBAL.SDAMethods();
        HOTBAL.FederationMethods federationMethods = new HOTBAL.FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Student Details";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            HOTBAL.Student studentInfo = new HOTBAL.Student();

            if (Request.QueryString["ID"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int incomingStudentId = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    studentInfo = sdaMethods.GetStudentInformation(incomingStudentId);

                    if (String.IsNullOrEmpty(studentInfo.ErrorMessage))
                    {
                        studentId.Text = studentInfo.StudentId.ToString();
                        studentName.Text = studentInfo.FirstName + " " + studentInfo.LastName + (String.IsNullOrEmpty(studentInfo.Suffix) ? "" : ", " + studentInfo.Suffix);
                        studentSchool.Text = federationMethods.GetSchoolBySchoolID(studentInfo.SchoolId).SchoolName;
                        studentBirthday.Text = functionsClass.FormatSlash(studentInfo.BirthDate);
                        emergencyContact.Text = studentInfo.EmergencyContact;
                        studentPassing.Text = (studentInfo.IsPassing == true ? "Yes" : "No");
                        studentPaying.Text = (studentInfo.IsPaying == true ? "Yes" : "No");
                        paymentAmount.Text = studentInfo.PaymentAmount.ToString("C");
                        paymentPlan.Text = studentInfo.PaymentPlan;
                        paymentDate.Text = functionsClass.FormatSlash(studentInfo.PaymentDate);
                        studentNotes.Text = studentInfo.Note;
                        studentActive.Text = (studentInfo.IsActive == true ? "Yes" : "No");

                        buildStudentAddress(studentInfo.StreetAddress, studentInfo.City, studentInfo.State, studentInfo.ZipCode);

                        studentArtInformation(incomingStudentId);
                        studentPhoneNumbers(incomingStudentId);
                        studentRecurringClasses(incomingStudentId);
                        studentPrivateLessons(incomingStudentId);
                        studentOtherCourses(incomingStudentId);
                    }
                    else
                        errorLabel.Text = studentInfo.ErrorMessage;
                }
                else
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
            }
            else
                errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
        }

        private void studentPhoneNumbers(int ID)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            List<HOTBAL.StudentPhone> studentPhones = sdaMethods.GetStudentPhones(ID);

            if (studentPhones != null)
            {
                if (studentPhones.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentPhones[0].ErrorMessage))
                    {
                        foreach (HOTBAL.StudentPhone phone in studentPhones)
                        {
                            phoneList.Text += "<tr>" + 
                                "<td style='color: #000000;'>" + phone.RelationshipToStudent + "</td>" +
                                "<td style='color: #000000;'>" + phone.PhoneNumber + "</td>" +
                                "<td style='color: #000000;'><a href='" + HOTBAL.SDAConstants.STUDENT_INFO_PHONES_INTERNAL_URL + 
                                "?ID=" + Request.QueryString["ID"].ToString() + "&NID=" + phone.PhoneId.ToString() + "'>Update</a></td>" + 
                                "</tr>";
                        }
                    }
                    else
                        errorLabel.Text = studentPhones[0].ErrorMessage;
                }
                else
                    phoneList.Text = "<tr><td colspan='3'>No Phone Numbers Found.</td></tr>";
            }
            else
                phoneList.Text = "<tr><td colspan='3'>No Phone Numbers Found.</td></tr>";
        }

        private void studentArtInformation(int ID)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            List<HOTBAL.StudentArt> studentArts = sdaMethods.GetStudentArts(ID);

            if (studentArts != null)
            {
                if (studentArts.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentArts[0].ErrorMessage))
                    {
                        foreach (HOTBAL.StudentArt art in studentArts)
                        {
                            studentArtList.Text += "<tr>" +
                                "<td>" + art.ArtTitle + "</td>" +
                                "<td>" + art.BeltTitle + "</td>" +
                                "<td>" + art.TipTitle + "</td>" +
                                "<td>" + art.ClassCount.ToString() + "</td>" +
                                "<td valign='top'><a href='" + HOTBAL.SDAConstants.STUDENT_EDIT_ART_INTERNAL_URL + "?ID=" +
                                Request.QueryString["ID"].ToString().ToString() + "&XID=" + art.StudentArtId + "'>Update</a></td>" +
                                "</tr>";
                        }
                    }
                    else
                        errorLabel.Text = studentArts[0].ErrorMessage;
                }
                else
                    studentArtList.Text = "<tr><td colspan='5'>No Arts Found.</td></tr>";
            }
            else
                studentArtList.Text = "<tr><td colspan='5'>No Arts Found.</td></tr>";
        }

        private void studentRecurringClasses(int ID)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            List<HOTBAL.Course> studentCourses = sdaMethods.GetStudentCourses(ID, "C", 1);

            if (studentCourses != null)
            {
                if (studentCourses.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentCourses[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Course course in studentCourses)
                        {
                            recurringClasses.Text += "<tr>" + 
                                "<td>" + course.Day + "</td>" + 
                                "<td>" + course.Time + "</td>" + 
                                "<td>" + sdaMethods.GetArtTitle(course.FirstArtId) + (course.SecondArtId == 0 ? "" : "/" + sdaMethods.GetArtTitle(course.SecondArtId)) + "</td>" + 
                                "<td><a href='" + HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + course.CourseId.ToString() + "'>" + course.CourseTitle + "</a></td>" + 
                                "<td>" + sdaMethods.GetInstructorByID(course.InstructorId).FirstName + " " + sdaMethods.GetInstructorByID(course.InstructorId).LastName + "</td>" + 
                                "<td align='center'><a href='" + HOTBAL.SDAConstants.STUDENT_ATTENDANCE_INTERNAL_URL + "?CID=" + course.CourseId.ToString() + 
                                "&ID=" + Request.QueryString["ID"].ToString().ToString() + "'>Attendance</a></td>" +
                                "<td align='center'><a href='confirmDelete(" + course.CourseId.ToString() + ");return false;'>Remove</a></td>" + 
                                "</tr>";
                        }
                    }
                    else
                        errorLabel.Text = studentCourses[0].ErrorMessage;
                }
                else
                    recurringClasses.Text += "<tr><td colspan='6'>No recurring classes.</td></tr>";
            }
            else
                recurringClasses.Text += "<tr><td colspan='6'>No recurring classes.</td></tr>";
        }

        private void studentPrivateLessons(int ID)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            List<HOTBAL.Course> studentLessons = sdaMethods.GetStudentCourses(ID, "L", 1);

            if (studentLessons != null)
            {
                if (studentLessons.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentLessons[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Course lesson in studentLessons)
                        {
                            recurringLessons.Text += "<tr>" + 
                                "<td>" + lesson.Day + "</td>" + 
                                "<td>" + lesson.Time + "</td>" + 
                                "<td>" + sdaMethods.GetArtTitle(lesson.FirstArtId) + (lesson.SecondArtId == 0 ? "" : "/" + sdaMethods.GetArtTitle(lesson.SecondArtId)) + "</td>" + 
                                "<td><a href='" + HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + lesson.CourseId.ToString() + "'>" + lesson.CourseTitle + "</a></td>" + 
                                "<td>" + sdaMethods.GetInstructorByID(lesson.InstructorId).FirstName + " " + sdaMethods.GetInstructorByID(lesson.InstructorId).LastName + "</td>" + 
                                "<td align='center'><a href='" + HOTBAL.SDAConstants.STUDENT_ATTENDANCE_INTERNAL_URL + "?CID=" + lesson.CourseId.ToString() + "&ID=" + 
                                Request.QueryString["ID"].ToString().ToString() + "'>Attendance</a></td>" +
                                "<td align='center'><a href='confirmDelete(" + lesson.CourseId.ToString() + "),return false;'>Remove</a></td>" + 
                                "</tr>";
                        }
                    }
                    else
                    {
                        errorLabel.Text = studentLessons[0].ErrorMessage;
                    }
                }
                else
                    recurringLessons.Text += "<tr><td colspan='7'>No recurring private lessons.</td></tr>";
            }
            else
                recurringLessons.Text += "<tr><td colspan='7'>No recurring private lessons.</td></tr>";
        }

        private void studentOtherCourses(int ID)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            List<HOTBAL.Course> studentCourses = sdaMethods.GetStudentCourses(ID, "L", 0);

            if (studentCourses != null)
            {
                if (studentCourses.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentCourses[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Course course in studentCourses)
                        {
                            otherClassesLessons.Text += "<tr>" + 
                                "<td>" + course.Day + "</td>" + 
                                "<td>" + course.Time + "</td>" + 
                                "<td>" + sdaMethods.GetArtTitle(course.FirstArtId) + (course.SecondArtId == 0 ? "" : "/" + sdaMethods.GetArtTitle(course.SecondArtId)) + "</td>" + 
                                "<td><a href='" + HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + course.CourseId.ToString() + "'>" + course.CourseTitle + "</a></td>" + 
                                "<td>" + sdaMethods.GetInstructorByID(course.InstructorId).FirstName + " " + sdaMethods.GetInstructorByID(course.InstructorId).LastName + "</td>" + 
                                "<td align='center'><a href='" + HOTBAL.SDAConstants.DELETE_CLASS_INTERNAL_URL + "?CID=" + course.CourseId.ToString() + 
                                "&ID=" + Request.QueryString["ID"].ToString().ToString() + "'>Delete</a></td>" + 
                                "</tr>";
                        }
                    }
                    else
                    {
                        errorLabel.Text = studentCourses[0].ErrorMessage;
                    }
                }
                else
                    otherClassesLessons.Text += "<tr><td colspan='6'>No non-recurring lessons.</td></tr>";
            }
            else
                otherClassesLessons.Text += "<tr><td colspan='6'>No non-recurring lessons.</td></tr>";

            studentCourses = sdaMethods.GetStudentCourses(ID, "C", 0);

            if (studentCourses != null)
            {
                if (studentCourses.Count > 0)
                {
                    if (String.IsNullOrEmpty(studentCourses[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Course c in studentCourses)
                        {
                            otherClassesLessons.Text += "<tr><td>" + c.Day + "</td><td>"
                                + c.Time + "</td><td>" + sdaMethods.GetArtTitle(c.FirstArtId)
                                + (c.SecondArtId == 0 ? "" : "/" + sdaMethods.GetArtTitle(c.SecondArtId))
                                + "</td><td><a href='" + HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + c.CourseId.ToString() + "'>" + c.CourseTitle + "</a>"
                                + "</td><td>" + sdaMethods.GetInstructorByID(c.InstructorId).FirstName
                                + " " + sdaMethods.GetInstructorByID(c.InstructorId).LastName
                                + "</td><td align='center'><a href='" + HOTBAL.SDAConstants.DELETE_CLASS_INTERNAL_URL + "?ID=" + c.CourseId.ToString()
                                + "'>Delete</a></td></tr>";
                        }
                    }
                    else
                    {
                        errorLabel.Text = studentCourses[0].ErrorMessage;
                    }
                }
                else
                    otherClassesLessons.Text += "<tr><td colspan='6'>No non-recurring classes.</td></tr>";
            }
            else
                otherClassesLessons.Text += "<tr><td colspan='6'>No non-recurring classes.</td></tr>";
        }

        private void buildStudentAddress(string address, string city, string state, string zip)
        {
            if ((String.IsNullOrEmpty(address)) &&
                (String.IsNullOrEmpty(city)) &&
                (String.IsNullOrEmpty(state)) &&
                (String.IsNullOrEmpty(zip)))
            {
                studentAddress.Text = "No address found";
            }
            else
            {
                if (!String.IsNullOrEmpty(address))
                    studentAddress.Text = address;

                if (!String.IsNullOrEmpty(state))
                    if (!String.IsNullOrEmpty(city))
                        if (!String.IsNullOrEmpty(studentAddress.Text))
                            studentAddress.Text += "<br />" + city + ", " + state;
                        else
                            studentAddress.Text = city + ", " + state;
                    else
                        if (!String.IsNullOrEmpty(studentAddress.Text))
                            studentAddress.Text += "<br />" + state;
                        else
                            studentAddress.Text = state;
                else
                    if (!String.IsNullOrEmpty(city))
                        if (!String.IsNullOrEmpty(studentAddress.Text))
                            studentAddress.Text += "<br />" + city;
                        else
                            studentAddress.Text = city;

                if (!String.IsNullOrEmpty(zip))
                    if (!String.IsNullOrEmpty(studentAddress.Text))
                        studentAddress.Text += "<br />" + zip;
                    else
                        studentAddress.Text = zip;
            }
        }

        protected void editInformation_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_EDIT_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
        }

        protected void studentTransactions_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.STUDENT_TRANS_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
        }

        protected void addArt_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.STUDENT_ADD_ART_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
        }

        protected void addClass_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.STUDENT_ADD_CLASS_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
        }

        protected void addLesson_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADD_CLASS_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
        }

        protected void addTransaction_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAPOSConstants.CART_URL + "?ID=" + Request.QueryString["ID"] + "&Action=");
        }
    }
}