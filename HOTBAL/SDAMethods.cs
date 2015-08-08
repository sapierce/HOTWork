using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using HOTDAL;

namespace HOTBAL
{
    public class SDAMethods
    {
        private static string sdaConnectionStringKey = "HOTSDA";
        private SDASQL sdaDataAccess = new SDASQL(sdaConnectionStringKey);
        private SDAProdSQL prodSDADataAccess = new SDAProdSQL(sdaConnectionStringKey);
        private FederationSQL fedDataAccess = new FederationSQL(sdaConnectionStringKey);
        HOTFunctions functionsClass = new HOTFunctions();
        int arrCount = 0, count = 0;
        
        /// <summary>
        /// Logs the error messages to the database
        /// </summary>
        /// <param name="errorMessage">Exception object</param>
        /// <param name="sqlCommand">Additional information</param>
        /// <param name="errorLocation">Where the error occurred</param>
        public void LogErrorMessage(Exception errorMessage, string sqlCommand, string errorLocation)
        {
            functionsClass.SendErrorMail(errorLocation, errorMessage, sqlCommand);
        }

        public string GetSchedule(DateTime ScheduleDate)
        {
            string scheduleResponse = String.Empty;
            string scheduleDay = ScheduleDate.DayOfWeek.ToString().Substring(0, 3);

            try
            {
                //Get the hours for day
                DataTable scheduleTable = sdaDataAccess.ExecuteGET_SCHEDULE(scheduleDay);

                if (scheduleTable.Rows.Count > 0)
                {
                    DateTime bTime = Convert.ToDateTime(scheduleTable.Rows[0]["BEG_TIME"].ToString());
                    DateTime eTime = Convert.ToDateTime(scheduleTable.Rows[0]["END_TIME"].ToString());
                    DateTime cTime = bTime;
                    count = 0;
                    arrCount = 0;

                    //Response.Write(cTime & "-" & eTime & "<br />")
                    while (cTime < eTime)
                    {
                        try
                        {
                            DataTable courseTable = sdaDataAccess.ExecuteGET_DAILY_COURSE(scheduleDay, cTime.ToShortTimeString());

                            if (courseTable.Rows.Count > 0)
                            {
                                // There are courses scheduled for the day and time
                                foreach (DataRow row in courseTable.Rows)
                                {
                                    //Get the actual class
                                    try
                                    {
                                        DataTable classTable = sdaDataAccess.ExecuteGET_DAILY_CLASS(Convert.ToInt32(row["CRSE_ID"].ToString()), functionsClass.FormatDash(ScheduleDate));

                                        if (classTable.Rows.Count > 0)
                                        {
                                            // Class already set up, get the ID
                                            if (arrCount == 0)
                                            {
                                                scheduleResponse += "<tr><td class='rightAlignHeader'>" + cTime.ToShortTimeString() + "</td><td><a href='" + SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + classTable.Rows[0]["CLASS_ID"].ToString() + "'>";
                                                
                                                if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    scheduleResponse += "Private Lesson for ";

                                                scheduleResponse += row["CRSE_TITLE"].ToString();

                                                if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                {
                                                    scheduleResponse += " - " + GetArtTitle(Convert.ToInt32(row["CRSE_ART_1"].ToString()));

                                                    if (row["CRSE_ART_2"].ToString() != "0")
                                                    {
                                                        scheduleResponse += "/" + GetArtTitle(Convert.ToInt32(row["CRSE_ART_2"].ToString()));
                                                    }
                                                }
                                                scheduleResponse += "</a></td></tr>";
                                            }
                                            else
                                            {
                                                scheduleResponse += "<tr><td class='rightAlignHeader'></td><td><a href='" + SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + classTable.Rows[0]["CLASS_ID"].ToString() + "'>";
                                                if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    scheduleResponse += "Private Lesson for ";

                                                scheduleResponse += row["CRSE_TITLE"].ToString();

                                                if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                {
                                                    scheduleResponse += " - " + GetArtTitle(Convert.ToInt32(row["CRSE_ART_1"].ToString()));

                                                    if (row["CRSE_ART_2"].ToString() != "0")
                                                    {
                                                        scheduleResponse += "/" + GetArtTitle(Convert.ToInt32(row["CRSE_ART_2"].ToString()));
                                                    }
                                                }
                                                scheduleResponse += "</a></td></tr>";
                                            }
                                        }
                                        else
                                        {
                                            // No class set up yet, add it
                                            bool addResponse = AddClass(functionsClass.FormatDash(ScheduleDate), Convert.ToInt32(row["CRSE_ID"].ToString()));
                                            if (addResponse)
                                            {
                                                classTable = sdaDataAccess.ExecuteGET_DAILY_CLASS(Convert.ToInt32(row["CRSE_ID"].ToString()), functionsClass.FormatDash(ScheduleDate));

                                                if (arrCount == 0)
                                                {
                                                    scheduleResponse += "<tr><td class='rightAlignHeader'>" + cTime.ToShortTimeString() + "</td><td><a href='" + SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + classTable.Rows[0]["CLASS_ID"].ToString() + "'>";
                                                    if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    {
                                                        scheduleResponse += "Private Lesson for ";
                                                    }
                                                    scheduleResponse += row["CRSE_TITLE"].ToString();

                                                    if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    {
                                                        scheduleResponse += " - " + GetArtTitle(Convert.ToInt32(row["CRSE_ART_1"].ToString()));
                                                        if (row["CRSE_ART_2"].ToString() != "0")
                                                        {
                                                            scheduleResponse += "/" + GetArtTitle(Convert.ToInt32(row["CRSE_ART_2"].ToString()));
                                                        }
                                                    }
                                                    scheduleResponse += "</a></td></tr>";
                                                }
                                                else
                                                {
                                                    scheduleResponse += "<tr><td class='rightAlignHeader'></td><td><a href='" + SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + classTable.Rows[0]["CLASS_ID"].ToString() + "'>";
                                                    if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    {
                                                        scheduleResponse += "Private Lesson for ";
                                                    }
                                                    scheduleResponse += row["CRSE_TITLE"].ToString();

                                                    if (row["CLASS_OR_LESSON"].ToString() == "L")
                                                    {
                                                        scheduleResponse += " - " + GetArtTitle(Convert.ToInt32(row["CRSE_ART_1"].ToString()));
                                                        if (row["CRSE_ART_2"].ToString() != "0")
                                                        {
                                                            scheduleResponse += "/" + GetArtTitle(Convert.ToInt32(row["CRSE_ART_2"].ToString()));
                                                        }
                                                    }
                                                    scheduleResponse += "</a></td></tr>";
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogErrorMessage(ex, ScheduleDate.ToShortDateString(), "SDAMethods: GetSchedule: Class");
                                        scheduleResponse = SDAMessages.ERROR_GENERIC;
                                    }

                                    arrCount++;
                                }
                                // Add extra empty row
                                scheduleResponse += "<tr><td class='rightAlignHeader'></td><td><a href='" + SDAConstants.ADD_CLASS_INTERNAL_URL + "?Time=" + cTime.ToShortTimeString() + "&Date=" + functionsClass.FormatDash(ScheduleDate) + "'>----</a></td></tr>";
                            }
                            else
                            {
                                // Return empty row
                                scheduleResponse += "<tr><td class='rightAlignHeader'>" + cTime.ToShortTimeString() + "</td><td><a href='" + SDAConstants.ADD_CLASS_INTERNAL_URL + "?Time=" + cTime.ToShortTimeString() + "&Date=" + functionsClass.FormatDash(ScheduleDate) + "'>----</a></td></tr>";
                            }
                            arrCount = 0;
                        }
                        catch (Exception ex)
                        {
                            LogErrorMessage(ex, ScheduleDate.ToShortDateString(), "SDAMethods: GetSchedule: Course");
                            scheduleResponse = SDAMessages.ERROR_GENERIC;
                        }

                        cTime = cTime.AddMinutes(30);

                        count++;
                        if ((cTime == eTime.AddMinutes(-30)) || count > 100)
                            cTime = eTime;
                    }
                }
                else
                {
                    // No hours scheduled for today
                    scheduleResponse = "No hours";
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ScheduleDate.ToShortDateString(), "SDAMethods: GetSchedule");
                scheduleResponse = SDAMessages.ERROR_GENERIC;
            }

            return scheduleResponse;
        }

        public List<string> GetAvailableTimes(DateTime scheduleDate)
        {
            List<string> scheduleTimes = new List<string>();
            string scheduleDay = scheduleDate.DayOfWeek.ToString().ToUpper().Substring(0,3);

            try
            {
                // Get the hours for day
                DataTable scheduleTable = sdaDataAccess.ExecuteGET_SCHEDULE(scheduleDay);

                if (scheduleTable.Rows.Count > 0)
                {
                    DateTime bTime = Convert.ToDateTime(scheduleTable.Rows[0]["BEG_TIME"].ToString());
                    DateTime eTime = Convert.ToDateTime(scheduleTable.Rows[0]["END_TIME"].ToString());
                    DateTime cTime = bTime;
                    count = 0;

                    //Response.Write(cTime & "-" & eTime & "<br />")
                    while (cTime < eTime)
                    {
                        scheduleTimes.Add(cTime.ToShortTimeString());
                            
                        cTime = cTime.AddMinutes(30);
                    }
                }
                else
                {
                    // No hours scheduled for today
                    scheduleTimes.Add("No Times Available");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, scheduleDate.ToShortDateString(), "SDAMethods: GetAvailableTimes");
                scheduleTimes.Add(SDAMessages.ERROR_GENERIC);
            }

            return scheduleTimes;
        }

        public bool AddClass(string ScheduleDate, long CourseID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_CLASS(CourseID, functionsClass.FormatDash(Convert.ToDateTime(ScheduleDate)));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseID.ToString(), "SDAMethods: AddClass");
            }

            return addResponse;
        }

        public string GetArtTitle(int ArtID)
        {
            string artTitle = "Unknown";

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_ART_INFO(ArtID);

                if (artTable.Rows.Count > 0)
                {
                    artTitle = artTable.Rows[0]["ART_TITLE"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ArtID.ToString(), "SDAMethods: GetArtTitle");
            }

            return artTitle;
        }

        public List<Student> GetStudentsByClass(int CourseID)
        {
            // Get students in course
            List<Student> responseStudents = new List<Student>();

            try
            {
                DataTable courseStudents = sdaDataAccess.ExecuteGET_COURSE_STUDENTS(CourseID);

                if (courseStudents.Rows.Count > 0)
                {
                    foreach (DataRow row in courseStudents.Rows)
                    {
                        Student studentList = new Student();
                        studentList = GetStudentInformation(Convert.ToInt32(row["STDT_ID"]));
                        responseStudents.Add(studentList);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseID.ToString(), "SDAMethods: GetStudentsByClass");
                Student studentList = new Student();
                studentList.ErrorMessage = SDAMessages.ERROR_GENERIC;
                responseStudents.Add(studentList);
            }

            return responseStudents;
        }

        public bool IsStudentCheckedIn(int ClassID, int StudentID)
        {
            bool response = false;

            try
            {
                DataTable checkTable = sdaDataAccess.ExecuteGET_STUDENT_ATTEND(ClassID, StudentID);

                if (checkTable.Rows.Count > 0)
                {
                    if (checkTable.Rows[0]["ATTEND"].ToString().Trim() == "False")
                        response = false;
                    else
                        response = true;
                }
                else
                {
                    //Add student record to class
                    response = AddStudentAttendance(ClassID, StudentID);
                    if (response)
                        response = false;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ClassID.ToString(), "SDAMethods: IsStudentCheckedIn");
            }

            return response;
        }

        public bool AddStudentAttendance(int ClassID, int StudentID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_CLASS_ATTEND(ClassID, StudentID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ClassID.ToString(), "SDAMethods: AddStudentAttendance");
            }

            return addResponse;
        }

        public bool AddStudentCourse(long CourseID, int StudentID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_COURSE_STUDENT(CourseID, StudentID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseID.ToString(), "SDAMethods: AddStudentCourse");
            }

            return addResponse;
        }

        public bool DeleteStudentCourse(int CourseID, int StudentID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteDELETE_COURSE_STUDENT(CourseID, StudentID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseID.ToString(), "SDAMethods: DeleteStudentCourse");
            }

            return addResponse;
        }

        public bool CheckInStudent(int ClassID, int StudentID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteUPDATE_STUDENT_CHECK_IN(ClassID, StudentID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ClassID.ToString(), "SDAMethods: CheckInStudent");
            }

            return addResponse;
        }

        public Student GetStudentInformation(int studentID)
        {
            //Get student information
            Student responseStudent = new Student();

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_STUDENT_BY_STUDENT_ID(studentID);

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        responseStudent.StudentId = Convert.ToInt32(row["STDT_ID"].ToString().Trim());
                        if (Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim()) == 1)
                            responseStudent.RegistrationId = row["STDT_ID"].ToString().Trim();
                        else
                            responseStudent.RegistrationId = row["STDT_REG_ID"].ToString();
                        responseStudent.FirstName = row["STDT_FNAME"].ToString();
                        responseStudent.LastName = row["STDT_LNAME"].ToString();
                        responseStudent.Suffix = row["STDT_SUFFIX"].ToString();
                        responseStudent.StreetAddress = row["STDT_ADDR"].ToString();
                        responseStudent.City = row["STDT_CITY"].ToString();
                        responseStudent.State = row["STDT_STATE"].ToString();
                        responseStudent.ZipCode = (row["STDT_ZIP"].ToString() == "0" ? "" : row["STDT_ZIP"].ToString());
                        responseStudent.EmergencyContact = row["STDT_EMER"].ToString();
                        responseStudent.PaymentDate = Convert.ToDateTime(row["STDT_PYMT_DT"].ToString());
                        responseStudent.PaymentPlan = row["STDT_PYMT_PLAN"].ToString();
                        responseStudent.PaymentAmount = Convert.ToDouble(row["STDT_PYMT_AMT"].ToString());
                        responseStudent.IsPaying = (row["STDT_PAID"].ToString() == "True" ? true : false);
                        responseStudent.IsPassing = (row["STDT_PASS"].ToString() == "True" ? true : false);
                        responseStudent.BirthDate = Convert.ToDateTime(row["STDT_BRTH_DATE"].ToString());
                        responseStudent.Note = row["STDT_NOTE"].ToString();
                        responseStudent.IsActive = (row["STDT_ACTIVE"].ToString() == "True" ? true : false);
                        responseStudent.SchoolId = Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim());
                    }
                }
                else
                {
                    responseStudent.ErrorMessage = SDAMessages.NO_STUDENTS_CLASS;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: GetStudentInformation");
            }

            return responseStudent;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> studentResponse = new List<Student>();

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_ALL_STUDENTS();

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        Student students = new Student();
                        students.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        students.FirstName = row["STDT_FNAME"].ToString();
                        students.LastName = row["STDT_LNAME"].ToString();
                        students.Suffix = row["STDT_SUFFIX"].ToString();
                        studentResponse.Add(students);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllStudents");
                Student students = new Student();
                students.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentResponse.Add(students);
            }

            return studentResponse;
        }

        public long AddNewStudent(string FirstName, string LastName, string Suffix, string Address, string City, string State, string ZipCode, string EmergencyContact, string SchoolId, DateTime BirthDate, DateTime PaymentDate, string PaymentPlan, Double PaymentAmount, Int32 ArtID)
        {
            long studentID = 0;

            try
            {
                studentID = sdaDataAccess.ExecuteINSERT_STUDENT(FirstName, LastName, Suffix, Address, City, State, ZipCode, EmergencyContact, SchoolId, functionsClass.FormatDash(BirthDate), functionsClass.FormatDash(DateTime.Now), PaymentPlan, PaymentAmount);
                
                if (studentID > 0)
                    sdaDataAccess.ExecuteINSERT_STUDENT_ART(studentID, ArtID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: AddNewStudent");
            }

            return studentID;
        }

        public bool UpdateStudent(int ID, string FirstName, string LastName, string Address, string Suffix, string City, string State, string ZipCode, string EmergencyContact, string SchoolId, DateTime BirthDate, DateTime PaymentDate, string PaymentPlan, Double PaymentAmount, string Note, bool Active, bool Paid, bool Pass)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteUPDATE_STUDENT_BY_ID(ID, FirstName, LastName, Suffix, Address, City, State, ZipCode,
                    EmergencyContact, SchoolId, functionsClass.FormatDash(BirthDate), functionsClass.FormatDash(PaymentDate), PaymentPlan, 
                    PaymentAmount, Note, (Pass.ToString() == "True" ? 1 : 0), (Paid.ToString() == "True" ? 1 : 0));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: UpdateStudent");
            }

            return addResponse;
        }

        public bool UpdateStudentArt(long StudentID, long XrefID, int ArtID, int BeltID, int TipID, int ClassCount, DateTime CompleteDate, string Approver, string Type)
        {
            bool addResponse = false;

            try
            {
                if (Type == "C")
                {
                    //if belt or tip change, automatically mark previous complete and generate new row (C)
                    addResponse = sdaDataAccess.ExecuteUPDATE_STUDENT_ART_COMPLETE(XrefID, functionsClass.FormatDash(CompleteDate));
                    if (addResponse)
                        addResponse = sdaDataAccess.ExecuteINSERT_STUDENT_NEW_ART(StudentID, ArtID, BeltID, TipID, ClassCount);
                }
                else if (Type == "U")
                {
                    //otherwise, update current xref row (U)
                    addResponse = sdaDataAccess.ExecuteUPDATE_STUDENT_ART(ArtID, BeltID, TipID, ClassCount, XrefID);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID + "-" + XrefID, "SDAMethods: UpdateStudentArt");
            }

            return addResponse;
        }

        public bool AddStudentArt(int StudentID, int ArtID, int BeltID, int TipID, int ClassCount, DateTime CompleteDate, string Approver)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_STUDENT_NEW_ART(StudentID, ArtID, BeltID, TipID, ClassCount);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID.ToString(), "SDAMethods: AddStudentArt");
            }

            return addResponse;
        }

        public bool DeleteStudent(int StudentID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_STUDENT(StudentID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID.ToString(), "SDAMethods: DeleteStudent");
            }

            return deleteResponse;
        }

        public List<Student> GetStudentsByName(string FirstName, string LastName, int SchoolID)
        {
            List<Student> studentResponse = new List<Student>();

            try
            {
                DataTable studentsTable = sdaDataAccess.ExecuteGET_STUDENT_BY_NAME(FirstName, LastName, SchoolID, false);

                if (studentsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Student students = new Student();
                        students.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        if (Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim()) == 1)
                            students.RegistrationId = row["STDT_ID"].ToString().Trim();
                        else
                            students.RegistrationId = row["STDT_REG_ID"].ToString().Trim();
                        students.RegistrationId = row["STDT_REG_ID"].ToString();
                        students.FirstName = row["STDT_FNAME"].ToString();
                        students.LastName = row["STDT_LNAME"].ToString();
                        students.Suffix = row["STDT_SUFFIX"].ToString();
                        students.IsActive = (row["STDT_ACTIVE"].ToString() == "1" ? true : false);
                        studentResponse.Add(students);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: GetStudentsByName");
                Student students = new Student();
                students.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentResponse.Add(students);
            }

            return studentResponse;
        }

        public List<Student> GetStudentsByName(string FirstName, string LastName, int SchoolID, bool activeOnly)
        {
            List<Student> studentResponse = new List<Student>();

            try
            {
                DataTable studentsTable = sdaDataAccess.ExecuteGET_STUDENT_BY_NAME(FirstName, LastName, SchoolID, activeOnly);

                if (studentsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Student students = new Student();
                        students.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        if (Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim()) == 1)
                            students.RegistrationId = row["STDT_ID"].ToString().Trim();
                        else
                            students.RegistrationId = row["STDT_REG_ID"].ToString().Trim();
                        students.RegistrationId = row["STDT_REG_ID"].ToString();
                        students.FirstName = row["STDT_FNAME"].ToString();
                        students.LastName = row["STDT_LNAME"].ToString();
                        students.Suffix = row["STDT_SUFFIX"].ToString();
                        students.IsActive = (row["STDT_ACTIVE"].ToString() == "1" ? true : false);
                        studentResponse.Add(students);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: GetStudentsByName");
                Student students = new Student();
                students.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentResponse.Add(students);
            }

            return studentResponse;
        }

        public List<Student> GetStudentsByLastName(string LastLetter, int SchoolID)
        {
            List<Student> studentResponse = new List<Student>();

            try
            {
                DataTable studentsTable = sdaDataAccess.ExecuteGET_STUDENT_BY_NAME("", LastLetter, SchoolID, false);

                if (studentsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Student students = new Student();
                        students.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        students.FirstName = row["STDT_FNAME"].ToString();
                        students.LastName = row["STDT_LNAME"].ToString();
                        students.Suffix = row["STDT_SUFFIX"].ToString();
                        students.IsActive = (row["STDT_ACTIVE"].ToString() == "1" ? true : false);
                        studentResponse.Add(students);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, LastLetter, "SDAMethods: GetStudentsByLastName");
                Student students = new Student();
                students.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentResponse.Add(students);
            }

            return studentResponse;
        }

        public List<Student> GetStudentsByLastName(string LastLetter, int SchoolID, bool activeOnly)
        {
            List<Student> studentResponse = new List<Student>();

            try
            {
                DataTable studentsTable = sdaDataAccess.ExecuteGET_STUDENT_BY_NAME("", LastLetter, SchoolID, activeOnly);

                if (studentsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Student students = new Student();
                        students.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        students.FirstName = row["STDT_FNAME"].ToString();
                        students.LastName = row["STDT_LNAME"].ToString();
                        students.Suffix = row["STDT_SUFFIX"].ToString();
                        students.IsActive = (row["STDT_ACTIVE"].ToString() == "1" ? true : false);
                        studentResponse.Add(students);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, LastLetter, "SDAMethods: GetStudentsByLastName");
                Student students = new Student();
                students.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentResponse.Add(students);
            }

            return studentResponse;
        }

        public List<StudentArt> GetStudentArts(int studentID)
        {
            List<StudentArt> studentArts = new List<StudentArt>();
            count = 0;

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_STUDENT_ART_BY_STUDENT_ID(studentID);

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        StudentArt artInfo = new StudentArt();
                        artInfo.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        artInfo.ArtTitle = row["ART_TITLE"].ToString();
                        if (String.IsNullOrEmpty(row["BELT_TITLE"].ToString()))
                        {
                            artInfo.BeltId = 0;
                            artInfo.BeltTitle = "No belt found";
                        }
                        else
                        {
                            artInfo.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                            artInfo.BeltTitle = row["BELT_TITLE"].ToString();
                        }
                        artInfo.StudentArtId = Convert.ToInt32(row["XREF_ID"].ToString());
                        artInfo.ClassOrTip = row["CLASS_TIP"].ToString();
                        artInfo.ClassCount = Convert.ToInt32((String.IsNullOrEmpty(row["CLASS_COUNT"].ToString()) ? "0" : row["CLASS_COUNT"].ToString()));
                        if (String.IsNullOrEmpty(row["TIP_TITLE"].ToString()))
                        {
                            artInfo.TipId = 0;
                            artInfo.TipTitle = "No tip found";
                        }
                        else
                        {
                            artInfo.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                            artInfo.TipTitle = row["TIP_TITLE"].ToString();
                        }

                        //if ((artInfo[count].ClassOrTip == "C") && (row["CLASS_COUNT"].ToString() != "0"))
                        //{
                        //Count the number of classes taken since last belt update
                        //    //SELECT * FROM STDT_ART_XREF WHERE STDT_ID = AND ART_ID = 
                        //    //SELECT * FROM CLASS_DOMN LEFT OUTER JOIN STDT_ATTEND WHERE CLASS_DATE >= historydate AND STDT_ID = studentID
                        //Count
                        //if count != ClassCount
                        //update class count (DB & object)
                        //}

                        studentArts.Add(artInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: GetStudentArts");
                StudentArt artInfo = new StudentArt();
                artInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentArts.Add(artInfo);
            }

            return studentArts;
        }

        public StudentArt GetStudentArt(int XrefID)
        {
            StudentArt studentArt = new StudentArt();
            count = 0;

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_STUDENT_ART_BY_XREF_ID(XrefID);

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        studentArt.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        studentArt.ArtTitle = row["ART_TITLE"].ToString();
                        if (String.IsNullOrEmpty(row["BELT_TITLE"].ToString()))
                        {
                            studentArt.BeltId = 0;
                            studentArt.BeltTitle = "No belt found";
                        }
                        else
                        {
                            studentArt.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                            studentArt.BeltTitle = row["BELT_TITLE"].ToString();
                        }
                        studentArt.StudentArtId = Convert.ToInt32(row["XREF_ID"].ToString());
                        studentArt.ClassOrTip = row["CLASS_TIP"].ToString();
                        studentArt.ClassCount = Convert.ToInt32((String.IsNullOrEmpty(row["CLASS_COUNT"].ToString()) ? "0" : row["CLASS_COUNT"].ToString()));
                        if (String.IsNullOrEmpty(row["TIP_TITLE"].ToString()))
                        {
                            studentArt.TipId = 0;
                            studentArt.TipTitle = "No tip found";
                        }
                        else
                        {
                            studentArt.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                            studentArt.TipTitle = row["TIP_TITLE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, XrefID.ToString(), "SDAMethods: GetStudentArt");
                studentArt.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return studentArt;
        }

        public StudentArt GetStudentArtByID(int ArtID, int StudentID)
        {
            StudentArt studentArt = new StudentArt();
            count = 0;

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_STUDENT_ART_BY_ART_ID(ArtID, StudentID);

                if (artTable.Rows.Count > 0)
                {
                    foreach (DataRow row in artTable.Rows)
                    {
                        studentArt.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        studentArt.ArtTitle = row["ART_TITLE"].ToString();
                        if (String.IsNullOrEmpty(row["BELT_TITLE"].ToString()))
                        {
                            studentArt.BeltId = 0;
                            studentArt.BeltTitle = "No belt found";
                        }
                        else
                        {
                            studentArt.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                            studentArt.BeltTitle = row["BELT_TITLE"].ToString();
                        }
                        studentArt.StudentArtId = Convert.ToInt32(row["XREF_ID"].ToString());
                        studentArt.ClassOrTip = row["CLASS_TIP"].ToString();
                        studentArt.ClassCount = Convert.ToInt32((String.IsNullOrEmpty(row["CLASS_COUNT"].ToString()) ? "0" : row["CLASS_COUNT"].ToString()));
                        if (String.IsNullOrEmpty(row["TIP_TITLE"].ToString()))
                        {
                            studentArt.TipId = 0;
                            studentArt.TipTitle = "No tip found";
                        }
                        else
                        {
                            studentArt.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                            studentArt.TipTitle = row["TIP_TITLE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ArtID.ToString(), "SDAMethods: GetStudentArtByID");
                studentArt.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return studentArt;
        }

        public List<StudentArt> GetStudentCompletedArts(int studentID)
        {
            List<StudentArt> studentArts = new List<StudentArt>();

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_STUDENT_ART_HISTORY_BY_STUDENT_ID(studentID);

                if (artTable.Rows.Count > 0)
                {
                    foreach (DataRow row in artTable.Rows)
                    {
                        StudentArt artInfo = new StudentArt();
                        artInfo.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        artInfo.ArtTitle = row["ART_TITLE"].ToString();
                        if (String.IsNullOrEmpty(row["BELT_TITLE"].ToString()))
                        {
                            artInfo.BeltId = 0;
                            artInfo.BeltTitle = "No belt found";
                        }
                        else
                        {
                            artInfo.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                            artInfo.BeltTitle = row["BELT_TITLE"].ToString();
                        }

                        if (String.IsNullOrEmpty(row["TIP_TITLE"].ToString()))
                        {
                            artInfo.TipId = 0;
                            artInfo.TipTitle = "No tip found";
                        }
                        else
                        {
                            artInfo.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                            artInfo.TipTitle = row["TIP_TITLE"].ToString();
                        }

                        artInfo.CompletionDate = row["COMP_DT"].ToString();

                        studentArts.Add(artInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: GetStudentCompletedArts");
                StudentArt artInfo = new StudentArt();
                artInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentArts.Add(artInfo);
            }

            return studentArts;
        }

        public List<StudentArt> GetStudentCompletedArtsPrevious(int studentID)
        {
            List<StudentArt> studentArts = new List<StudentArt>();

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_STUDENT_ART_PREVIOUS_HISTORY_BY_STUDENT_ID(studentID);

                if (artTable.Rows.Count > 0)
                {
                    foreach (DataRow row in artTable.Rows)
                    {
                        StudentArt artInfo = new StudentArt();
                        artInfo.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        artInfo.ArtTitle = row["ART_TITLE"].ToString();
                        if (String.IsNullOrEmpty(row["BELT_TITLE"].ToString()))
                        {
                            artInfo.BeltId = 0;
                            artInfo.BeltTitle = "No belt found";
                        }
                        else
                        {
                            artInfo.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                            artInfo.BeltTitle = row["BELT_TITLE"].ToString();
                        }
                        if (String.IsNullOrEmpty(row["TIP_TITLE"].ToString()))
                        {
                            artInfo.TipId = 0;
                            artInfo.TipTitle = "No tip found";
                        }
                        else
                        {
                            artInfo.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                            artInfo.TipTitle = row["TIP_TITLE"].ToString();
                        }

                        artInfo.CompletionDate = row["STDT_DATE"].ToString();

                        studentArts.Add(artInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: GetStudentCompletedArtsPrevious");
                StudentArt artInfo = new StudentArt();
                artInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                studentArts.Add(artInfo);
            }

            return studentArts;
        }

        public List<StudentPhone> GetStudentPhones(int studentID)
        {
            List<StudentPhone> responsePhones = new List<StudentPhone>();

            try
            {
                DataTable phoneTable = sdaDataAccess.ExecuteGET_STUDENT_PHONE_BY_STUDENT_ID(studentID);

                if (phoneTable.Rows.Count > 0)
                {
                    foreach (DataRow row in phoneTable.Rows)
                    {
                        StudentPhone phoneInfo = new StudentPhone();
                        phoneInfo.PhoneId = Convert.ToInt32(row["NBR_ID"].ToString());
                        phoneInfo.PhoneNumber = row["STDT_NUM"].ToString();
                        phoneInfo.RelationshipToStudent = row["STDT_RELT"].ToString();
                        phoneInfo.StudentId = row["STDT_ID"].ToString();
                        //phoneInfo.Active = (row["ACTIVE"].ToString() == "True" ? true : false);

                        responsePhones.Add(phoneInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: GetStudentPhones");
                StudentPhone phoneInfo = new StudentPhone();
                phoneInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                responsePhones.Add(phoneInfo);
            }

            return responsePhones;
        }

        public StudentPhone GetStudentPhoneById(int phoneId)
        {
            StudentPhone responsePhone = new StudentPhone();

            try
            {
                DataTable phoneTable = sdaDataAccess.ExecuteGET_STUDENT_PHONE_BY_PHONE_ID(phoneId);

                if (phoneTable.Rows.Count > 0)
                {
                    foreach (DataRow row in phoneTable.Rows)
                    {
                        responsePhone.PhoneId = Convert.ToInt32(row["NBR_ID"].ToString());
                        responsePhone.PhoneNumber = row["STDT_NUM"].ToString();
                        responsePhone.RelationshipToStudent = row["STDT_RELT"].ToString();
                        responsePhone.StudentId = row["STDT_ID"].ToString();
                        //responsePhone.Active = (row["ACTIVE"].ToString() == "True" ? true : false);
                    }
                }
                else
                {
                    responsePhone.PhoneId = phoneId;
                    responsePhone.PhoneNumber = "";
                    responsePhone.RelationshipToStudent = "";
                    responsePhone.StudentId = "";
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, phoneId.ToString(), "SDAMethods: GetStudentPhoneById");
                responsePhone.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return responsePhone;
        }

        public bool AddStudentPhone(int studentId, string relationShip, string phoneNumber)
        {
            try
            {
                return sdaDataAccess.ExecuteINSERT_STUDENT_PHONE(studentId, relationShip, phoneNumber);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentId.ToString(), "SDAMethods: AddStudentPhone");
                return false;
            }
        }

        public bool UpdateStudentPhoneById(int phoneId, string relationShip, string phoneNumber)
        {
            try
            {
                return sdaDataAccess.ExecuteUPDATE_STUDENT_PHONE(phoneId, relationShip, phoneNumber);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, phoneId.ToString(), "SDAMethods: UpdateStudentPhoneById");
                return false;
            }
        }

        public bool DeleteStudentPhoneById(int phoneId)
        {
            try
            {
                return sdaDataAccess.ExecuteDELETE_STUDENT_PHONE(phoneId);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, phoneId.ToString(), "SDAMethods: DeleteStudentPhoneById");
                return false;
            }
        }

        public List<Course> GetStudentCourses(int StudentID, string Type, int Repeating)
        {
            List<Course> responseCourses = new List<Course>();

            try
            {
                DataTable courseTable = sdaDataAccess.ExecuteGET_STUDENT_COURSES_BY_STUDENT_ID(StudentID, Type, Repeating);

                if (courseTable.Rows.Count > 0)
                {
                    foreach (DataRow row in courseTable.Rows)
                    {
                        Course courseInfo = GetCourseInformation(Convert.ToInt32(row["CRSE_ID"].ToString()));

                        responseCourses.Add(courseInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID.ToString(), "SDAMethods: GetStudentCourses");
                Course courseInfo = new Course();
                courseInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                responseCourses.Add(courseInfo);
            }

            return responseCourses;
        }

        public List<ClassAttendance> GetStudentAttendance(int StudentID, int CourseID)
        {
            List<ClassAttendance> studentAttend = new List<ClassAttendance>();
            count = 0;

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_STUDENT_ATTENDANCE(StudentID, CourseID);

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        ClassAttendance classAttend = new ClassAttendance();
                        classAttend.DidAttend = (row["ATTEND"].ToString() == "True" ? true : false);
                        classAttend.ClassDate = row["CLASS_DATE"].ToString();
                        classAttend.StudentId = Convert.ToInt32(StudentID);
                        studentAttend.Add(classAttend);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID.ToString(), "SDAMethods: GetStudentAttendance");
            }

            return studentAttend;
        }

        public List<ClassAttendance> GetStudentAttendanceByDate(string CourseDate)
        {
            List<ClassAttendance> studentAttend = new List<ClassAttendance>();
            count = 0;

            try
            {
                DataTable studentTable = sdaDataAccess.ExecuteGET_STUDENT_ATTENDANCE_BY_DATE(CourseDate);

                if (studentTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentTable.Rows)
                    {
                        ClassAttendance classAttend = new ClassAttendance();
                        classAttend.DidAttend = (row["ATTEND"].ToString() == "True" ? true : false);
                        classAttend.ClassDate = row["CLASS_DATE"].ToString();
                        classAttend.StudentId = Convert.ToInt32(row["STDT_ID"].ToString());
                        studentAttend.Add(classAttend);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseDate, "SDAMethods: GetStudentAttendanceByDate");
            }

            return studentAttend;
        }

        public List<Transaction> GetAllStudentTransactions(int StudentID)
        {
            List<Transaction> transactionsResponse = new List<Transaction>();
            count = 0;

            try
            {
                DataTable transactionTable = prodSDADataAccess.ExecuteGET_STUDENT_TRANSACTIONS_BY_STUDENT_ID(StudentID);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow row in transactionTable.Rows)
                    {
                        Transaction transactionInfo = new Transaction();
                        transactionInfo.TransactionDate = Convert.ToDateTime(row["TRNS_DATE"].ToString());
                        transactionInfo.TransactionId = Convert.ToInt32(row["TRNS_ID"].ToString());
                        transactionInfo.TransactionLocation = row["TRNS_LOC"].ToString();
                        transactionInfo.OtherInformation = row["TRNS_OTH"].ToString();
                        transactionInfo.IsTransactionPaid = (row["TRNS_PAID"].ToString() == "True" ? true : false);
                        transactionInfo.PaymentMethod = row["TRNS_PYMT"].ToString();
                        transactionInfo.SellerId = row["TRNS_SELL"].ToString();
                        transactionInfo.CustomerId = Convert.ToInt32(StudentID);
                        transactionInfo.TaxTotal = Convert.ToDouble(row["TRNS_TAX"].ToString());
                        transactionInfo.TransactionTotal = Convert.ToDouble(row["TRNS_TTL"].ToString());
                        transactionInfo.IsTransactionVoid = (row["TRNS_VOID"].ToString() == "True" ? true : false);
                        transactionsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    Transaction transactionInfo = new Transaction();
                    transactionInfo.TransactionId = 0;
                    transactionsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, StudentID.ToString(), "SDAMethods: GetAllStudentTransactions");
                Transaction transactionInfo = new Transaction();
                transactionInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                transactionsResponse.Add(transactionInfo);
            }

            return transactionsResponse;
        }

        public Transaction GetStudentTransaction(int TransactionID)
        {
            Transaction transactionResponse = new Transaction();
            count = 0;

            try
            {
                DataTable transactionTable = prodSDADataAccess.ExecuteGET_STUDENT_TRANSACTIONS_BY_TRANSACTION_ID(TransactionID);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow row in transactionTable.Rows)
                    {
                        transactionResponse.TransactionDate = Convert.ToDateTime(row["TRNS_DATE"].ToString());
                        transactionResponse.TransactionId = Convert.ToInt32(row["TRNS_ID"].ToString());
                        transactionResponse.TransactionLocation = row["TRNS_LOC"].ToString();
                        transactionResponse.OtherInformation = row["TRNS_OTH"].ToString();
                        transactionResponse.IsTransactionPaid = (row["TRNS_PAID"].ToString() == "True" ? true : false);
                        transactionResponse.PaymentMethod = row["TRNS_PYMT"].ToString();
                        transactionResponse.SellerId = row["TRNS_SELL"].ToString();
                        transactionResponse.CustomerId = Convert.ToInt32(row["TRNS_BGHT"].ToString());
                        transactionResponse.TaxTotal = Convert.ToDouble(row["TRNS_TAX"].ToString());
                        transactionResponse.TransactionTotal = Convert.ToDouble(row["TRNS_TTL"].ToString());
                        transactionResponse.IsTransactionVoid = (row["TRNS_VOID"].ToString() == "True" ? true : false);
                    }
                }
                else
                {
                    transactionResponse.ErrorMessage = SDAMessages.ERROR_GENERIC;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionID.ToString(), "SDAMethods: GetStudentTransaction");
                transactionResponse.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return transactionResponse;
        }

        public List<TransactionItem> GetTransactionItems(int TransactionID)
        {
            List<TransactionItem> transactionItemsResponse = new List<TransactionItem>();
            count = 0;

            try
            {
                DataTable transactionTable = prodSDADataAccess.ExecuteGET_STUDENT_TRANSACTION_ITEMS_BY_TRANSACTION_ID(TransactionID);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow row in transactionTable.Rows)
                    {
                        TransactionItem transactionInfo = new TransactionItem();
                        transactionInfo.TransactionItemId = Convert.ToInt32(row["XREF_ID"].ToString());
                        transactionInfo.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        transactionInfo.ProductId = Convert.ToInt32(row["PROD_ID"].ToString());
                        transactionInfo.ProductName = row["PROD_NME"].ToString();
                        transactionInfo.ItemQuantity = Convert.ToInt32(row["PROD_QTY"].ToString());
                        transactionInfo.IsTaxed = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        transactionInfo.TransactionId = Convert.ToInt32(TransactionID);

                        transactionItemsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    TransactionItem transactionInfo = new TransactionItem();
                    transactionInfo.TransactionItemId = 0;
                    transactionItemsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionID.ToString(), "SDAMethods: GetTransactionItems");
                TransactionItem transactionInfo = new TransactionItem();
                transactionInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                transactionItemsResponse.Add(transactionInfo);
            }

            return transactionItemsResponse;
        }

        public bool RemoveSDATransactionItems(int TransactionItemID)
        {
            bool transactionItemsResponse = false;
            count = 0;

            try
            {
                transactionItemsResponse = prodSDADataAccess.ExecuteDELETE_TRANSACTION_ITEM(TransactionItemID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionItemID.ToString(), "SDAMethods: RemoveSDATransactionItems");
            }

            return transactionItemsResponse;
        }

        public bool AddTransaction(List<CartItem> cartSummary, int employeeID, string cartTotal, int studentID, string location, string paymentType, string date, string tax, string note)
        {
            bool response = false;
            long transactionID = 0;

            try
            {
                transactionID = prodSDADataAccess.ExecuteINSERT_TRANSACTION(employeeID, cartTotal, studentID, location, paymentType, date, tax, note);

                //Add in the items for the transaction
                foreach (CartItem item in cartSummary)
                {
                    response = prodSDADataAccess.ExecuteINSERT_TRANSACTION_ITEM(transactionID, item.ItemId, item.ItemQuantity, item.ItemName, item.ItemPrice.ToString(), (item.ItemIsTaxed ? 1 : 0));

                    if (response)
                    {
                        if (item.ItemType == "MTL")
                        {
                            //Update renewals
                            string[] splitter = item.ItemName.Split(Convert.ToChar("-"));
                            string planDate;
                            if (splitter[0] == "ML")
                            {
                                string[] time = splitter[1].Split(Convert.ToChar(" "));
                                if (time[0] == "Monthly")
                                    planDate = functionsClass.FormatDash(DateTime.Now.AddDays(30));
                                else if (time[0] == "Weekly")
                                    planDate = functionsClass.FormatDash(DateTime.Now.AddDays(7));
                                else if (time[0] == "Lifetime")
                                    planDate = "9999-12-31";
                                else
                                    planDate = functionsClass.FormatDash(DateTime.Now);

                                response = sdaDataAccess.ExecuteUPDATE_STUDENT_RENEWAL(studentID, planDate, splitter[1].ToString());
                            }
                        }
                        else
                        {
                            //Get current product count
                            DataTable itemTable = prodSDADataAccess.ExecuteGET_ITEM_BY_ITEM_ID(item.ItemId);

                            if (itemTable.Rows.Count > 0)
                            {
                                string itemCount = itemTable.Rows[0]["PROD_CNT"].ToString();

                                //UpdateInventory
                                response = prodSDADataAccess.ExecuteUPDATE_ITEM_INVENTORY(item.ItemId, (Convert.ToInt32(itemCount) - 1));
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(note))
                {
                    response = prodSDADataAccess.ExecuteINSERT_TRANSACTION_NOTE(studentID, note);
                }

            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "SDAMethods: AddTransaction");
            }

            return response;
        }

        public bool UpdateTransaction(int transactionID, string seller, string total, DateTime date, string payment, bool isVoid, bool isPaid)
        {
            bool response = false;

            try
            {
                response = prodSDADataAccess.ExecuteUPDATE_TRANSACTION(transactionID, seller, total, functionsClass.FormatDash(date), payment, (isVoid == true ? 1 : 0), (isPaid == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, transactionID.ToString(), "SDAMethods: UpdateTransaction");
            }

            return response;
        }

        public List<Term> GetAllTerms()
        {
            List<Term> termsResponse = new List<Term>();

            try
            {
                DataTable termTable = sdaDataAccess.ExecuteGET_ALL_TERMS();

                if (termTable.Rows.Count > 0)
                {
                    foreach (DataRow row in termTable.Rows)
                    {
                        Term terms = new Term();
                        terms.TermId = Convert.ToInt32(row["TERM_ID"].ToString());
                        terms.EnglishTerm = row["TERM_TXT_ENG"].ToString();
                        terms.ChineseTerm = row["TERM_TXT_CHN"].ToString();
                        terms.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                        termsResponse.Add(terms);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllTerms");
                Term terms = new Term();
                terms.ErrorMessage = SDAMessages.ERROR_GENERIC;
                termsResponse.Add(terms);
            }

            return termsResponse;
        }

        public Term GetTermByID(int TermID)
        {
            Term termResponse = new Term();

            try
            {
                DataTable termTable = sdaDataAccess.ExecuteGET_TERM_BY_TERM_ID(TermID);

                if (termTable.Rows.Count > 0)
                {
                    termResponse.EnglishTerm = termTable.Rows[0]["TERM_TXT_ENG"].ToString();
                    termResponse.TermId = Convert.ToInt32(termTable.Rows[0]["TERM_ID"].ToString());
                    termResponse.ChineseTerm = termTable.Rows[0]["TERM_TXT_CHN"].ToString();
                    termResponse.BeltId = Convert.ToInt32(termTable.Rows[0]["BELT_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TermID.ToString(), "SDAMethods: GetTermByID");
                termResponse.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return termResponse;
        }

        public bool AddTerm(string English, string Chinese, Int32 BeltID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_TERM(English, Chinese, BeltID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, English, "SDAMethods: AddTerm");
            }

            return addResponse;
        }

        public bool UpdateTerm(int ID, string English, string Chinese, int BeltID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteUPDATE_TERM(ID, English, Chinese, BeltID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateTerm");
            }

            return addResponse;
        }

        public bool DeleteTerm(int ID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteDELETE_TERM(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteTerm");
            }

            return addResponse;
        }

        public List<Product> GetAllItems()
        {
            List<Product> itemsResponse = new List<Product>();
            count = 0;

            try
            {
                DataTable productTable = prodSDADataAccess.ExecuteGET_ALL_ITEMS();

                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow row in productTable.Rows)
                    {
                        Product items = new Product();
                        items.ProductId = Convert.ToInt32(row["PROD_ID"].ToString());
                        items.ProductType = row["PROD_TYPE"].ToString();
                        items.ProductCode = row["PROD_CODE"].ToString();
                        items.ProductName = row["PROD_NAME"].ToString();
                        items.IsAvailableOnline = (row["PROD_DISP_ONLINE"].ToString() == "True" ? true : false);
                        items.IsAvailableInStore = (row["PROD_DISP_STORE"].ToString() == "True" ? true : false);
                        items.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        items.ProductCount = Convert.ToInt32(row["PROD_CNT"].ToString());
                        items.IsOnSaleOnline = (row["PROD_SALE_ONLINE"].ToString() == "True" ? true : false);
                        items.ProductSalePrice = Convert.ToDouble(row["PROD_SALE_PRICE"].ToString());
                        items.IsTaxable = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        items.ProductSubType = row["PROD_SUB_TYPE"].ToString();
                        itemsResponse.Add(items);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllItems");
                Product items = new Product();
                //items[0].Error = SDAMessages.ERROR_GENERIC;
                itemsResponse.Add(items);
            }

            return itemsResponse;
        }

        public Product GetItemByID(int ItemID)
        {
            Product itemResponse = new Product();
            count = 0;

            try
            {
                DataTable productTable = prodSDADataAccess.ExecuteGET_ITEM_BY_ITEM_ID(ItemID);

                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow row in productTable.Rows)
                    {
                        itemResponse.ProductId = Convert.ToInt32(row["PROD_ID"].ToString());
                        itemResponse.ProductType = row["PROD_TYPE"].ToString();
                        itemResponse.ProductCode = row["PROD_CODE"].ToString();
                        itemResponse.ProductName = row["PROD_NAME"].ToString();
                        itemResponse.ProductFileName = row["PROD_FILE_NAME"].ToString();
                        itemResponse.ProductDescription = row["PROD_DESC"].ToString();
                        itemResponse.IsAvailableOnline = (row["PROD_DISP_ONLINE"].ToString() == "True" ? true : false);
                        itemResponse.IsAvailableInStore = (row["PROD_DISP_STORE"].ToString() == "True" ? true : false);
                        itemResponse.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        itemResponse.ProductCount = Convert.ToInt32(row["PROD_CNT"].ToString());
                        itemResponse.IsOnSaleOnline = (row["PROD_SALE_ONLINE"].ToString() == "True" ? true : false);
                        itemResponse.IsOnSaleInStore = (row["PROD_SALE_STORE"].ToString() == "True" ? true : false);
                        itemResponse.ProductSalePrice = Convert.ToDouble(row["PROD_SALE_PRICE"].ToString());
                        itemResponse.IsTaxable = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        itemResponse.ProductSubType = row["PROD_SUB_TYPE"].ToString();
                        itemResponse.IsActive = (row["PROD_DISP"].ToString() == "True" ? false : true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ItemID.ToString(), "SDAMethods: GetItemByID");
                //itemResponse.Error = SDAMessages.ERROR_GENERIC;
            }

            return itemResponse;
        }

        public Product GetItemByBarCode(string barCode)
        {
            Product itemResponse = new Product();

            try
            {
                DataTable productTable = prodSDADataAccess.ExecuteGET_ITEM_BY_BARCODE(barCode);

                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow row in productTable.Rows)
                    {
                        itemResponse.ProductId = Convert.ToInt32(row["PROD_ID"].ToString());
                        itemResponse.ProductType = row["PROD_TYPE"].ToString();
                        itemResponse.ProductCode = row["PROD_CODE"].ToString();
                        itemResponse.ProductName = row["PROD_NAME"].ToString();
                        itemResponse.ProductFileName = row["PROD_FILE_NAME"].ToString();
                        itemResponse.ProductDescription = row["PROD_DESC"].ToString();
                        itemResponse.IsAvailableOnline = (row["PROD_DISP_ONLINE"].ToString() == "True" ? true : false);
                        itemResponse.IsAvailableInStore = (row["PROD_DISP_STORE"].ToString() == "True" ? true : false);
                        itemResponse.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        itemResponse.ProductCount = Convert.ToInt32(row["PROD_CNT"].ToString());
                        itemResponse.IsOnSaleOnline = (row["PROD_SALE_ONLINE"].ToString() == "True" ? true : false);
                        itemResponse.IsOnSaleInStore = (row["PROD_SALE_STORE"].ToString() == "True" ? true : false);
                        itemResponse.ProductSalePrice = Convert.ToDouble(row["PROD_SALE_PRICE"].ToString());
                        itemResponse.IsTaxable = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        itemResponse.ProductSubType = row["PROD_SUB_TYPE"].ToString();
                        itemResponse.IsActive = (row["PROD_DISP"].ToString() == "True" ? false : true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, barCode, "SDAMethods: GetItemByBarCode");
            }

            return itemResponse;
        }

        public bool AddItem(string Name, Double Price, string SubType, bool Tax, string Description, bool SaleOnline, bool SaleStore, 
            Double SalePrice, string Barcode, string Location, string Type, bool AvailOnline, bool Active, bool AvailInStore, int Count)
        {
            bool addResponse = false;
            long productID = 0;

            try
            {
                productID = prodSDADataAccess.ExecuteINSERT_PRODUCT(Name, Price.ToString(), Type, SubType, (Tax == true ? 1 : 0), 
                    Description, (SaleOnline == true ? 1 : 0), (SaleStore == true ? 1 : 0), SalePrice.ToString(), Barcode, Location,
                    (AvailOnline == true ? 1 : 0), (AvailInStore == true ? 1 : 0));

                if (productID > 0)
                    addResponse = prodSDADataAccess.ExecuteINSERT_ITEM_INVENTORY(productID, count);
                else
                    LogErrorMessage(new Exception("NoProdID"), Name, "SDAMethods: AddItem");
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, Name, "SDAMethods: AddItem");
            }

            return addResponse;
        }

        public bool UpdateItem(int ID, string Name, Double Price, string SubType, bool Tax, string Description, bool SaleOnline, bool SaleStore, Double SalePrice, 
            string Barcode, string Location, string Type, bool AvailOnline, bool Active, bool AvailInStore, int Count)
        {
            bool addResponse = false;

            try
            {
                addResponse = prodSDADataAccess.ExecuteUPDATE_ITEM(ID, Name, Price.ToString(), Type, SubType, (Tax == true ? 1 : 0), Description, (SaleOnline == true ? 1 : 0),
                    (SaleStore == true ? 1 : 0), SalePrice.ToString(), Barcode, Location, (AvailOnline == true ? 1 : 0), (AvailInStore == true ? 1 : 0), (Active == true ? 1 : 0));

                if (addResponse)
                    addResponse = prodSDADataAccess.ExecuteUPDATE_ITEM_INVENTORY(ID, Count);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateItem");
            }

            return addResponse;
        }

        public bool DeleteItem(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = prodSDADataAccess.ExecuteDELETE_ITEM(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteItem");
            }

            return deleteResponse;
        }

        public List<Instructor> GetAllInstructors()
        {
            List<Instructor> instResponse = new List<Instructor>();

            try
            {
                DataTable instructorsTable = sdaDataAccess.ExecuteGET_ALL_INSTRUCTORS();

                if (instructorsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in instructorsTable.Rows)
                    {
                        Instructor insts = new Instructor();
                        insts.InstructorId = Convert.ToInt32(row["INST_ID"].ToString());
                        insts.FirstName = row["INST_FNAME"].ToString();
                        insts.LastName = row["INST_LNAME"].ToString();
                        insts.InstructorType = row["INST_STAT"].ToString();
                        insts.InstructorBiography = row["INST_BIO"].ToString();
                        instResponse.Add(insts);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllInstructors");
                Instructor insts = new Instructor();
                insts.ErrorMessage = SDAMessages.ERROR_GENERIC;
                instResponse.Add(insts);
            }

            return instResponse;
        }

        public Instructor GetInstructorByID(int InstructorID)
        {
            Instructor instructorInformation = new Instructor();

            try
            {
                DataTable instructorTable = sdaDataAccess.ExecuteGET_INSTRUCTOR_BY_ID(InstructorID);

                if (instructorTable.Rows.Count > 0)
                {
                    instructorInformation.FirstName = instructorTable.Rows[0]["INST_FNAME"].ToString();
                    instructorInformation.LastName = instructorTable.Rows[0]["INST_LNAME"].ToString();
                    instructorInformation.InstructorBiography = instructorTable.Rows[0]["INST_BIO"].ToString();
                    instructorInformation.InstructorType = instructorTable.Rows[0]["INST_STAT"].ToString();
                    instructorInformation.InstructorId = Convert.ToInt32(instructorTable.Rows[0]["INST_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                instructorInformation.ErrorMessage = HOTBAL.SDAMessages.NO_INSTRUCTORS;
                LogErrorMessage(ex, InstructorID.ToString(), "SDAMethods: GetInstructorByID");
            }

            return instructorInformation;
        }

        public bool AddInstructor(string FirstName, string LastName, string Type, string Bio)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_INSTRUCTOR(FirstName, LastName, Type, Bio);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: AddInstructor");
            }

            return addResponse;
        }

        public bool UpdateInstructor(int ID, string FirstName, string LastName, string Type, string Bio)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteUPDATE_INSTRUCTOR_BY_INSTRUCTOR_ID(ID, FirstName, LastName, Type, Bio);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateInstructor");
            }

            return addResponse;
        }

        public bool DeleteInstructor(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_INSTRUCTOR(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteInstructor");
            }

            return deleteResponse;
        }

        public List<Art> GetAllSDAArts()
        {
            List<Art> artResponse = new List<Art>();

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_ALL_SDA_ARTS();

                if (artTable.Rows.Count > 0)
                {
                    foreach (DataRow row in artTable.Rows)
                    {
                        Art arts = new Art();
                        arts.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        arts.ArtTitle = row["ART_TITLE"].ToString();
                        artResponse.Add(arts);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllArts");
                Art arts = new Art();
                arts.ErrorMessage = SDAMessages.ERROR_GENERIC;
                artResponse.Add(arts);
            }

            return artResponse;
        }

        public bool AddArt(string ArtTitle, int SchoolID)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_ART(ArtTitle, SchoolID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ArtTitle, "SDAMethods: AddArt");
            }

            return addResponse;
        }

        public Art GetArtByID(int ArtID)
        {
            Art artResponse = new Art();

            try
            {
                DataTable artTable = sdaDataAccess.ExecuteGET_ART_INFO(ArtID);

                if (artTable.Rows.Count > 0)
                {
                    artResponse.ArtTitle = artTable.Rows[0]["ART_TITLE"].ToString();
                    artResponse.ArtId = Convert.ToInt32(artTable.Rows[0]["ART_ID"].ToString());
                    artResponse.SchoolId = Convert.ToInt32(artTable.Rows[0]["ART_SCHOOL_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ArtID.ToString(), "SDAMethods: GetArtByID");
            }

            return artResponse;
        }

        public bool UpdateArt(int ID, string ArtTitle)
        {
            bool updateResponse = false;

            try
            {
                updateResponse = sdaDataAccess.ExecuteUPDATE_ART(ID, ArtTitle);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateArt");
            }

            return updateResponse;
        }

        public bool DeleteArt(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_ART(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteArt");
            }

            return deleteResponse;
        }

        public List<Belt> GetAllSDABelts()
        {
            List<Belt> beltResponse = new List<Belt>();
            count = 0;

            try
            {
                DataTable beltTable = sdaDataAccess.ExecuteGET_ALL_SDA_BELTS();

                foreach (DataRow row in beltTable.Rows)
                    {
                        Belt belts = new Belt();
                        belts.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                        belts.BeltTitle = row["BELT_TITLE"].ToString();
                        belts.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        belts.ClassCount = Convert.ToInt32(row["CLASS_CNT"].ToString());
                        belts.ClassOrTip = row["CLASS_TIP"].ToString();
                        belts.BeltLevel = row["BELT_LEVEL"].ToString();
                        beltResponse.Add(belts);
                    }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllBelts");
                Belt belts = new Belt();
                belts.ErrorMessage = SDAMessages.ERROR_GENERIC;
                beltResponse.Add(belts);
            }

            return beltResponse;
        }

        public Belt GetBeltByID(int BeltID)
        {
            Belt beltResponse = new Belt();

            try
            {
                DataTable beltTable = sdaDataAccess.ExecuteGET_BELT_INFO(BeltID);

                if (beltTable.Rows.Count > 0)
                {
                    beltResponse.BeltTitle = beltTable.Rows[0]["BELT_TITLE"].ToString();
                    beltResponse.BeltId = Convert.ToInt32(beltTable.Rows[0]["BELT_ID"].ToString());
                    beltResponse.ArtId = Convert.ToInt32(beltTable.Rows[0]["ART_ID"].ToString());
                    beltResponse.ClassCount = Convert.ToInt32(beltTable.Rows[0]["CLASS_CNT"].ToString());
                    beltResponse.ClassOrTip = beltTable.Rows[0]["CLASS_TIP"].ToString();
                    beltResponse.BeltLevel = beltTable.Rows[0]["BELT_LEVEL"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, BeltID.ToString(), "SDAMethods: GetBeltByID");
                beltResponse.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return beltResponse;
        }

        public List<Belt> GetArtBelts(int ArtID)
        {
            List<Belt> beltResponse = new List<Belt>();
            count = 0;

            try
            {
                DataTable beltTable = sdaDataAccess.ExecuteGET_BELT_INFO_BY_ART(ArtID);

                foreach (DataRow row in beltTable.Rows)
                    {
                        Belt belts = new Belt();
                        belts.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                        belts.BeltTitle = row["BELT_TITLE"].ToString();
                        belts.ArtId = Convert.ToInt32(row["ART_ID"].ToString());
                        belts.ClassCount = Convert.ToInt32(row["CLASS_CNT"].ToString());
                        belts.ClassOrTip = row["CLASS_TIP"].ToString();
                        belts.BeltLevel = row["BELT_LEVEL"].ToString();
                        beltResponse.Add(belts);
                    }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ArtID.ToString(), "SDAMethods: GetArtBelts");
                Belt belts = new Belt();
                belts.BeltId = 0;
                belts.ErrorMessage = SDAMessages.ERROR_GENERIC;
                beltResponse.Add(belts);
            }

            return beltResponse;
        }

        public bool AddBelt(string BeltTitle, int BeltLevel, int ArtID, string ClassOrTip, int ClassCount)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_BELT(BeltTitle, BeltLevel, ArtID, ClassOrTip, ClassCount);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, BeltTitle, "SDAMethods: AddBelt");
            }

            return addResponse;
        }

        public bool UpdateBelt(int ID, string BeltTitle, int BeltLevel, int ArtID, string ClassOrTip, int ClassCount)
        {
            bool updateResponse = false;

            try
            {
                updateResponse = sdaDataAccess.ExecuteUPDATE_BELT(ID, BeltTitle, BeltLevel, ArtID, ClassOrTip, ClassCount);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateBelt");
            }

            return updateResponse;
        }

        public bool DeleteBelt(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_BELT(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteBelt");
            }

            return deleteResponse;
        }

        public List<Tip> GetAllTips()
        {
            List<Tip> tipResponse = new List<Tip>();
            count = 0;

            try
            {
                DataTable tipsTable = sdaDataAccess.ExecuteGET_ALL_TIPS();

                if (tipsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in tipsTable.Rows)
                    {
                        Tip tips = new Tip();
                        tips.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                        tips.TipTitle = row["TIP_TITLE"].ToString();
                        tips.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                        tips.IsLastTip = (row["LAST_TIP"].ToString() == "True" ? true : false);
                        tips.TipLevel = row["TIP_LEVEL"].ToString();
                        tipResponse.Add(tips);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllTips");
                Tip tips = new Tip();
                tips.ErrorMessage = SDAMessages.ERROR_GENERIC;
                tipResponse.Add(tips);
            }

            return tipResponse;
        }

        public Tip GetTipByID(int TipID)
        {
            Tip tipResponse = new Tip();

            try
            {
                DataTable tipTable = sdaDataAccess.ExecuteGET_TIP_INFO(TipID);

                if (tipTable.Rows.Count > 0)
                {
                    tipResponse.TipTitle = tipTable.Rows[0]["TIP_TITLE"].ToString();
                    tipResponse.TipId = Convert.ToInt32(tipTable.Rows[0]["TIP_ID"].ToString());
                    tipResponse.BeltId = Convert.ToInt32(tipTable.Rows[0]["BELT_ID"].ToString());
                    tipResponse.TipLevel = tipTable.Rows[0]["TIP_LEVEL"].ToString();
                    tipResponse.IsLastTip = (tipTable.Rows[0]["LAST_TIP"].ToString() == "True" ? true : false);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TipID.ToString(), "SDAMethods: GetTipByID");
                tipResponse.ErrorMessage = SDAMessages.ERROR_GENERIC;
            }

            return tipResponse;
        }

        public List<Tip> GetBeltTips(int BeltID)
        {
            List<Tip> tipResponse = new List<Tip>();
            count = 0;

            try
            {
                DataTable tipTable = sdaDataAccess.ExecuteGET_TIP_INFO_BY_BELT(BeltID);

                foreach (DataRow row in tipTable.Rows)
                    {
                        Tip tips = new Tip();
                        tips.TipId = Convert.ToInt32(row["TIP_ID"].ToString());
                        tips.TipTitle = row["TIP_TITLE"].ToString();
                        tips.BeltId = Convert.ToInt32(row["BELT_ID"].ToString());
                        tips.IsLastTip = (row["LAST_TIP"].ToString() == "True" ? true : false);
                        tips.TipLevel = row["TIP_LEVEL"].ToString();
                        tipResponse.Add(tips);
                    }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, BeltID.ToString(), "SDAMethods: GetBeltTips");
                Tip tips = new Tip();
                tips.ErrorMessage = SDAMessages.ERROR_GENERIC;
                tipResponse.Add(tips);
            }

            return tipResponse;
        }

        public bool AddTip(string Title, int Level, int BeltID, string LastTip)
        {
            bool addResponse = false;

            try
            {
                addResponse = sdaDataAccess.ExecuteINSERT_TIP(Title, Level, BeltID, LastTip);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, Title, "SDAMethods: AddTip");
            }

            return addResponse;
        }

        public bool UpdateTip(int ID, string Title, int Level, int BeltID, string LastTip)
        {
            bool updateResponse = false;

            try
            {
                updateResponse = sdaDataAccess.ExecuteUPDATE_TIP(ID, Title, Level, BeltID, LastTip);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateTip");
            }

            return updateResponse;
        }

        public bool DeleteTip(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_TIP(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteTip");
            }

            return deleteResponse;
        }

        public Course GetClassInformation(int ClassID)
        {
            Course classInfo = new Course();
            int classID = 0;

            try
            {
                DataTable classTable = sdaDataAccess.ExecuteGET_CLASS_BY_ID(ClassID);

                if (classTable.Rows.Count > 0)
                {
                    classID = Convert.ToInt32(classTable.Rows[0]["COURSE_ID"].ToString());

                    DataTable courseTable = sdaDataAccess.ExecuteGET_COURSE_BY_ID(classID);

                    if (courseTable.Rows.Count > 0)
                    {
                        classInfo.FirstArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_1"].ToString());
                        classInfo.InstructorId = Convert.ToInt32(courseTable.Rows[0]["CRSE_INST"].ToString());
                        classInfo.SecondArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_2"].ToString());
                        classInfo.CourseTitle = courseTable.Rows[0]["CRSE_TITLE"].ToString();
                        classInfo.CourseId = Convert.ToInt32(classID);
                    }
                }
                else
                {
                    //Could be a course
                    DataTable courseTable = sdaDataAccess.ExecuteGET_COURSE_BY_ID(ClassID);

                    if (courseTable.Rows.Count > 0)
                    {
                        classInfo.FirstArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_1"].ToString());
                        classInfo.InstructorId = Convert.ToInt32(courseTable.Rows[0]["CRSE_INST"].ToString());
                        classInfo.SecondArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_2"].ToString());
                        classInfo.CourseTitle = courseTable.Rows[0]["CRSE_TITLE"].ToString();
                        classInfo.CourseId = Convert.ToInt32(ClassID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ClassID.ToString(), "SDAMethods: GetClassInformation");
            }

            return classInfo;
        }

        public Course GetCourseInformation(int CourseID)
        {
            Course classInfo = new Course();

            try
            {
                DataTable courseTable = sdaDataAccess.ExecuteGET_COURSE_BY_ID(CourseID);

                if (courseTable.Rows.Count > 0)
                {
                    classInfo.FirstArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_1"].ToString());
                    classInfo.InstructorId = Convert.ToInt32(courseTable.Rows[0]["CRSE_INST"].ToString());
                    classInfo.SecondArtId = Convert.ToInt32(courseTable.Rows[0]["CRSE_ART_2"].ToString());
                    classInfo.CourseTitle = courseTable.Rows[0]["CRSE_TITLE"].ToString();
                    classInfo.Day = courseTable.Rows[0]["CRSE_DAY"].ToString();
                    classInfo.Time = courseTable.Rows[0]["CRSE_TIME"].ToString();
                    classInfo.InstructorId = Convert.ToInt32(courseTable.Rows[0]["CRSE_INST"].ToString());
                    classInfo.ClassOrLesson = courseTable.Rows[0]["CLASS_OR_LESSON"].ToString();
                    classInfo.IsRepeating = (courseTable.Rows[0]["CRSE_RPT"].ToString() == "True" ? true : false);
                    classInfo.CourseId = Convert.ToInt32(CourseID);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseID.ToString(), "SDAMethods: GetCourseInformation");
            }

            return classInfo;
        }

        public List<Course> GetAllActiveRepeatingClasses()
        {
            List<Course> courseInfo = new List<Course>();

            try
            {
                DataTable courseTable = sdaDataAccess.ExecuteGET_ALL_COURSES();

                if (courseTable.Rows.Count > 0)
                {
                    foreach (DataRow row in courseTable.Rows)
                    {
                        Course courseList = new Course();
                        courseList.CourseId = Convert.ToInt32(row["CRSE_ID"].ToString());
                        courseList.CourseTitle = row["CRSE_TITLE"].ToString();
                        courseList.InstructorId = Convert.ToInt32(row["CRSE_INST"].ToString());
                        courseList.FirstArtId = Convert.ToInt32(row["CRSE_ART_1"].ToString());
                        courseList.SecondArtId = Convert.ToInt32(row["CRSE_ART_2"].ToString());
                        courseList.Day = row["CRSE_DAY"].ToString();
                        courseList.Time = row["CRSE_TIME"].ToString();
                        courseList.ClassOrLesson = row["CLASS_OR_LESSON"].ToString();
                        courseList.IsRepeating = (row["CRSE_RPT"].ToString() == "True" ? true : false);
                        courseInfo.Add(courseList);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "SDAMethods: GetAllActiveRepeatingClasses");
                Course courseList = new Course();
                courseList.ErrorMessage = SDAMessages.ERROR_GENERIC;
                courseInfo.Add(courseList);
            }

            return courseInfo;
        }

        public bool UpdateCourse(int ID, string Title, int FirstArt, int SecondArt, string Day, string Time, int Instructor, string Repeating)
        {
            bool updateResponse = false;

            try
            {
                updateResponse = sdaDataAccess.ExecuteUPDATE_COURSE(ID, Title, FirstArt, SecondArt, Day, Time, Instructor, Repeating);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: UpdateCourse");
            }

            return updateResponse;
        }

        public bool DeleteCourse(int ID)
        {
            bool deleteResponse = false;

            try
            {
                deleteResponse = sdaDataAccess.ExecuteDELETE_COURSE(ID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ID.ToString(), "SDAMethods: DeleteCourse");
            }

            return deleteResponse;
        }

        public long AddCourse(string CourseTitle, int FirstArt, int SecondArt, string Day, string Time, int Instructor, int Repeating, string ClassLesson)
        {
            long courseID = 0;

            try
            {
                courseID = sdaDataAccess.ExecuteINSERT_COURSE(CourseTitle, FirstArt, SecondArt, Day, Time, Instructor, Repeating, ClassLesson);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, CourseTitle, "SDAMethods: AddCourse");
            }

            return courseID;
        }

        public List<Transaction> GetAllMartialArtTransactions(string TransactionDate)
        {
            List<Transaction> transactionsResponse = new List<Transaction>();
            count = 0;

            try
            {
                DataTable transactionTable = prodSDADataAccess.ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE(TransactionDate);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow row in transactionTable.Rows)
                    {
                        Transaction transactionInfo = new Transaction();
                        transactionInfo.TransactionDate = Convert.ToDateTime(row["TRNS_DATE"].ToString());
                        transactionInfo.TransactionId = Convert.ToInt32(row["TRNS_ID"].ToString());
                        transactionInfo.TransactionLocation = row["TRNS_LOC"].ToString();
                        transactionInfo.OtherInformation = row["TRNS_OTH"].ToString();
                        transactionInfo.IsTransactionPaid = (row["TRNS_PAID"].ToString() == "True" ? true : false);
                        transactionInfo.PaymentMethod = (String.IsNullOrEmpty(row["TRNS_PYMT"].ToString()) ? "Unknown" : row["TRNS_PYMT"].ToString());
                        transactionInfo.SellerId = row["TRNS_SELL"].ToString();
                        transactionInfo.CustomerId = Convert.ToInt32(row["TRNS_BGHT"]);
                        transactionInfo.TaxTotal = Convert.ToDouble(row["TRNS_TAX"].ToString());
                        transactionInfo.TransactionTotal = Convert.ToDouble(row["TRNS_TTL"].ToString());
                        transactionInfo.IsTransactionVoid = (row["TRNS_VOID"].ToString() == "True" ? true : false);
                        transactionsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    Transaction transactionInfo = new Transaction();
                    transactionInfo.TransactionId = 0;
                    transactionsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionDate, "SDAMethods: GetAllMartialArtTransactions");
                Transaction transactionInfo = new Transaction();
                transactionInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                transactionsResponse.Add(transactionInfo);
            }

            return transactionsResponse;
        }

        public List<Transaction> GetEmployeeMartialArtTransactions(string Date, int EmployeeID)
        {
            List<Transaction> transactionsResponse = new List<Transaction>();
            count = 0;

            try
            {
                DataTable transactionTable = prodSDADataAccess.ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE_EMPLOYEE(Date, EmployeeID);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow row in transactionTable.Rows)
                    {
                        Transaction transactionInfo = new Transaction();
                        transactionInfo.TransactionDate = Convert.ToDateTime(row["TRNS_DATE"].ToString());
                        transactionInfo.TransactionId = Convert.ToInt32(row["TRNS_ID"].ToString());
                        transactionInfo.TransactionLocation = row["TRNS_LOC"].ToString();
                        transactionInfo.OtherInformation = row["TRNS_OTH"].ToString();
                        transactionInfo.IsTransactionPaid = (row["TRNS_PAID"].ToString() == "True" ? true : false);
                        transactionInfo.PaymentMethod = row["TRNS_PYMT"].ToString();
                        transactionInfo.SellerId = row["TRNS_SELL"].ToString();
                        transactionInfo.CustomerId = Convert.ToInt32(row["TRNS_BGHT"]);
                        transactionInfo.TaxTotal = Convert.ToDouble(row["TRNS_TAX"].ToString());
                        transactionInfo.TransactionTotal = Convert.ToDouble(row["TRNS_TTL"].ToString());
                        transactionInfo.IsTransactionVoid = (row["TRNS_VOID"].ToString() == "True" ? true : false);
                        transactionsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    Transaction transactionInfo = new Transaction();
                    transactionInfo.TransactionId = 0;
                    transactionsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, Date, "SDAMethods: GetEmployeeMaritalArtTransactions");
                Transaction transactionInfo = new Transaction();
                transactionInfo.ErrorMessage = SDAMessages.ERROR_GENERIC;
                transactionsResponse.Add(transactionInfo);
            }

            return transactionsResponse;
        }
    }
}
