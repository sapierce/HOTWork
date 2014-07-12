using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace HOTDAL
{
    public class SDASQL : SqlConnector
    {
        private static string sdaConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTSDA"].ConnectionString;
        private static string sdaConnectionStringKey = "HOTSDA";

        #region Functions
        public SDASQL(String connectionStringKey) : base(sdaConnectionStringKey) { }

        #region SELECT
        public DataTable ExecuteGET_SCHEDULE(string scheduleDay)
        {
            const string SPName = "SELECT BEG_TIME, END_TIME FROM CLS_TIME WHERE TIME_DAY = @P_DAY";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, scheduleDay));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_DAILY_COURSE(string scheduleDay, string scheduleTime)
        {
            const string SPName = "SELECT * FROM COURSE_DOMN WHERE CRSE_DAY = @P_DAY AND CRSE_TIME = @P_TIME AND CRSE_ACTV = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, scheduleDay));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, scheduleTime));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_DAILY_CLASS(int courseID, string scheduleDay)
        {
            const string SPName = "SELECT * FROM CLASS_DOMN WHERE COURSE_ID = @P_CRSE_ID AND CLASS_DATE = @P_DAY";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, scheduleDay));
            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_SDA_ARTS()
        {
            const string SPName = "SELECT * FROM ART_DOMN WHERE ART_ACTIVE = 1 AND ART_SCHOOL_ID = 1 ORDER BY ART_TITLE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ART_INFO(int artID)
        {
            const string SPName = "SELECT * FROM ART_DOMN WHERE ART_ID = @P_ART_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_SDA_BELTS()
        {
            const string SPName = "SELECT * FROM BELT_DOMN A LEFT OUTER JOIN ART_DOMN B ON A.ART_ID = B.ART_ID WHERE BELT_ACTIVE = 1 AND ART_SCHOOL_ID = 1 ORDER BY B.ART_ID, BELT_LEVEL";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_BELT_INFO(int beltID)
        {
            const string SPName = "SELECT * FROM BELT_DOMN WHERE BELT_ID = @P_BELT_ID AND BELT_ACTIVE = 1 ORDER BY ART_ID, BELT_LEVEL";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_BELT_INFO_BY_ART(int artID)
        {
            const string SPName = "SELECT * FROM BELT_DOMN WHERE BELT_ACTIVE = 1 AND ART_ID = @P_ART_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_TIPS()
        {
            const string SPName = "SELECT * FROM TIP_DOMN WHERE TIP_ACTIVE = 1 ORDER BY BELT_ID, TIP_LEVEL";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_TIP_INFO(int tipID)
        {
            const string SPName = "SELECT * FROM TIP_DOMN WHERE TIP_ACTIVE = 1 AND TIP_ID = @P_TIP_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TIP_ID", MySqlDbType.Int32, tipID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_TIP_INFO_BY_BELT(int beltID)
        {
            const string SPName = "SELECT * FROM TIP_DOMN WHERE BELT_ID = @P_BELT_ID AND TIP_ACTIVE = 1 ORDER BY BELT_ID, TIP_LEVEL";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_COURSE_STUDENTS(int courseID)
        {
            const string SPName = "SELECT * FROM STDT_CRSE_XREF A INNER JOIN STDT_INFO B ON A.STDT_ID = B.STDT_ID WHERE CRSE_ID = @P_CRSE_ID AND STDT_ACTIVE = 1  AND ACTIVE = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ATTEND(int courseID, int studentID)
        {
            const string SPName = "SELECT * FROM STDT_ATTEND WHERE CLASS_ID = @P_CLS_ID AND STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CLS_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ATTENDANCE(int courseID, int studentID)
        {
            const string SPName = "SELECT * FROM STDT_ATTEND A LEFT OUTER JOIN CLASS_DOMN B ON A.CLASS_ID = B.CLASS_ID INNER JOIN COURSE_DOMN C ON B.COURSE_ID = C.CRSE_ID "
            + "WHERE STDT_ID = @P_STDT_ID AND B.COURSE_ID = @P_CRSE_ID ORDER BY CLASS_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ATTENDANCE_BY_DATE(string courseDate)
        {
            const string SPName = "SELECT A.CLASS_ID, B.CRSE_TITLE, A.CLASS_DATE, B.CRSE_TIME, D.ATTEND, C.STDT_LNAME, C.STDT_FNAME "
            + "FROM ((COURSE_DOMN A INNER JOIN CLASS_DOMN B ON A.COURSE_ID = B.COURSE_ID) INNER JOIN STDT_INFO C ON A.STDT_ID = C.STDT_ID) "
            + "INNER JOIN STDT_ATTEND D ON B.CLASS_ID = D.CLASS_ID WHERE A.CLASS_DATE = @P_CRSE_DT "
            + "ORDER BY B.CRSE_TITLE ASC , A.CLASS_DATE ASC , C.STDT_LNAME ASC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_DT", MySqlDbType.VarChar, courseDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT * FROM STDT_INFO WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_STUDENTS()
        {
            const string SPName = "SELECT * FROM STDT_INFO WHERE STDT_ACTIVE = 1 ORDER BY STDT_LNAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_BY_NAME(string firstName, string lastName, int schoolID)
        {
            const string SPName = "SELECT STDT_ID, STDT_LNAME, STDT_FNAME, STDT_REG_ID, STDT_SCHOOL_ID FROM STDT_INFO WHERE STDT_LNAME LIKE @P_LAST_NAME AND STDT_FNAME LIKE @P_FIRST_NAME AND STDT_SCHOOL_ID = @P_SCHOOL_ID ORDER BY STDT_LNAME, STDT_FNAME ASC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, lastName + "%"));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, firstName + "%"));
            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.VarChar, schoolID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ART_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT * FROM STDT_ART_XREF A LEFT OUTER JOIN BELT_DOMN B ON A.BELT_ID = B.BELT_ID LEFT OUTER JOIN ART_DOMN C ON A.ART_ID = C.ART_ID LEFT OUTER JOIN TIP_DOMN D ON A.TIP_ID = D.TIP_ID WHERE STDT_ID = @P_STDT_ID AND COMP_DATE = '9999-12-31 00:00:00'";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ART_BY_XREF_ID(int xrefID)
        {
            const string SPName = "SELECT * FROM STDT_ART_XREF A LEFT OUTER JOIN BELT_DOMN B ON A.BELT_ID = B.BELT_ID LEFT OUTER JOIN ART_DOMN C ON A.ART_ID = C.ART_ID LEFT OUTER JOIN TIP_DOMN D ON A.TIP_ID = D.TIP_ID WHERE A.XREF_ID = @P_XREF_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_XREF_ID", MySqlDbType.Int32, xrefID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ART_BY_ART_ID(int artID, int studentID)
        {
            const string SPName = "SELECT * FROM STDT_ART_XREF A LEFT OUTER JOIN BELT_DOMN B ON A.BELT_ID = B.BELT_ID LEFT OUTER JOIN ART_DOMN C ON A.ART_ID = C.ART_ID LEFT OUTER JOIN TIP_DOMN D ON A.TIP_ID = D.TIP_ID WHERE A.ART_ID = @P_ART_ID AND STDT_ID = @P_STDT_ID AND COMP_DATE = '9999-12-31 00:00:00'";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ART_HISTORY_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT * FROM STDT_ART_XREF A LEFT OUTER JOIN BELT_DOMN B ON A.BELT_ID = B.BELT_ID " + 
                "LEFT OUTER JOIN ART_DOMN C ON A.ART_ID = C.ART_ID LEFT OUTER JOIN TIP_DOMN D ON A.TIP_ID = D.TIP_ID " + 
                "WHERE STDT_ID = @P_STDT_ID AND COMP_DATE <> '9999-12-31 00:00:00' ORDER BY COMP_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_ART_PREVIOUS_HISTORY_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT D.ART_ID, D.ART_TITLE, B.BELT_ID, B.BELT_TITLE, C.TIP_ID, C.TIP_TITLE, A.STDT_WHO, A.STDT_DATE FROM " + 
                "(((STDT_HIST A INNER JOIN BELT_DOMN B ON A.STDT_BELT_ID = B.BELT_ID) INNER JOIN ART_DOMN D ON D.ART_ID = B.ART_ID) " + 
                "INNER JOIN TIP_DOMN C ON A.STDT_TIP_ID = C.TIP_ID) WHERE A.STDT_ID = @P_STDT_ID ORDER BY A.STDT_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_PHONE_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT * FROM STDT_NBRS WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_PHONE_BY_PHONE_ID(int numberId)
        {
            const string SPName = "SELECT * FROM STDT_NBRS WHERE NBR_ID = @P_NBR_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NBR_ID", MySqlDbType.Int32, numberId));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_COURSES_BY_STUDENT_ID(int studentID, string type, int repeating)
        {
            const string SPName = "SELECT * FROM STDT_CRSE_XREF A INNER JOIN COURSE_DOMN B ON A.CRSE_ID = B.CRSE_ID " + 
                "WHERE STDT_ID = @P_STDT_ID AND CRSE_ACTV = 1 AND ACTIVE = 1 AND CLASS_OR_LESSON = @P_TYPE AND CRSE_RPT = @P_RPT " + 
                "ORDER BY CASE WHEN CRSE_DAY = 'Sunday' THEN 1 WHEN CRSE_DAY = 'Monday' THEN 2 WHEN CRSE_DAY = 'Tuesday' THEN 3 " + 
                "WHEN CRSE_DAY = 'Wednesday' THEN 4 WHEN CRSE_DAY = 'Thursday' THEN 5 WHEN CRSE_DAY = 'Friday' THEN 6 WHEN " + 
                "CRSE_DAY = 'Saturday' THEN 7 END ASC, CRSE_TIME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));
            parameters.Add(makeInputParameter("P_RPT", MySqlDbType.VarChar, repeating.ToString()));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_TERMS()
        {
            const string SPName = "SELECT * FROM TERM_DOMN WHERE TERM_ACTIVE = 1 ORDER BY BELT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_TERM_BY_TERM_ID(int termID)
        {
            const string SPName = "SELECT * FROM TERM_DOMN WHERE TERM_ACTIVE = 1 AND TERM_ID = @P_TERM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TERM_ID", MySqlDbType.Int32, termID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_INSTRUCTORS()
        {
            const string SPName = "SELECT * FROM INST_DOMN WHERE INST_DISP = 1 ORDER BY INST_LNAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_INSTRUCTOR_BY_ID(int instructorID)
        {
            const string SPName = "SELECT * FROM INST_DOMN WHERE INST_ID = @P_INST_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_INST_ID", MySqlDbType.Int32, instructorID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_CLASS_BY_ID(int classID)
        {
            const string SPName = "SELECT * FROM CLASS_DOMN WHERE CLASS_ID = @P_CLS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CLS_ID", MySqlDbType.Int32, classID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_COURSES()
        {
            const string SPName = "SELECT * FROM COURSE_DOMN WHERE CRSE_ACTV = 1 AND CRSE_RPT = 1 AND CLASS_OR_LESSON = 'C' ORDER BY CRSE_DAY, CRSE_TIME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_COURSE_BY_ID(int courseID)
        {
            const string SPName = "SELECT * FROM COURSE_DOMN WHERE CRSE_ID = @P_CRSE_ID AND CRSE_ACTV = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region INSERT
        public bool ExecuteINSERT_CLASS(long courseID, string scheduleDay)
        {
            const string SPName = "INSERT INTO CLASS_DOMN (COURSE_ID, CLASS_DATE) VALUES (@P_CRSE_ID, @P_DAY)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, scheduleDay));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_CLASS_ATTEND(int courseID, int studentID)
        {
            const string SPName = "INSERT INTO STDT_ATTEND (CLASS_ID, STDT_ID, ATTEND) VALUES (@P_CLS_ID, @P_STDT_ID, 0)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CLS_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_COURSE_STUDENT(long courseID, int studentID)
        {
            const string SPName = "INSERT INTO STDT_CRSE_XREF (STDT_ID, CRSE_ID, ACTIVE) VALUES (@P_STDT_ID, @P_CRSE_ID, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_STUDENT(string firstName, string lastName, string address, string city, string state, string zipCode, string emergencyContact, string birthDate, string paymentDate, string paymentPlan, Double paymentAmount)
        {
            const string SPName = "INSERT INTO STDT_INFO (STDT_LNAME, STDT_FNAME, STDT_ADDR, STDT_CITY, STDT_STATE, STDT_ZIP, STDT_EMER, STDT_BRTH_DATE, STDT_PYMT_DT, STDT_PYMT_PLAN, STDT_PYMT_AMT, STDT_NOTE, STDT_PASS, STDT_PAID, STDT_ACTIVE) " +
            "VALUES (@P_LST_NME, @P_FRST_NME, @P_ADDR, @P_CITY, @P_STATE, @P_ZIP, @P_EMG_CNTC, @P_BRTH_DT, @P_PYMT_DT, @P_PYMT_PLN, @P_PYMT_AMT, '', '1','1','1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LST_NME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_FRST_NME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_ADDR", MySqlDbType.VarChar, address));
            parameters.Add(makeInputParameter("P_CITY", MySqlDbType.VarChar, city));
            parameters.Add(makeInputParameter("P_STATE", MySqlDbType.VarChar, state));
            parameters.Add(makeInputParameter("P_ZIP", MySqlDbType.VarChar, zipCode));
            parameters.Add(makeInputParameter("P_EMG_CNTC", MySqlDbType.VarChar, emergencyContact));
            parameters.Add(makeInputParameter("P_BRTH_DT", MySqlDbType.VarChar, birthDate));
            parameters.Add(makeInputParameter("P_PYMT_DT", MySqlDbType.VarChar, paymentDate));
            parameters.Add(makeInputParameter("P_PYMT_PLN", MySqlDbType.VarChar, paymentPlan));
            parameters.Add(makeInputParameter("P_PYMT_AMT", MySqlDbType.VarChar, paymentAmount.ToString()));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_STUDENT_PHONE(int studentId, string relationship, string phoneNumber)
        {
            const string SPName = "INSERT INTO STDT_NBRS (STDT_ID, STDT_RELT, STDT_NUM) VALUES" + 
                "(@P_ID, @P_PHONE, @P_RELT)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, studentId));
            parameters.Add(makeInputParameter("P_PHONE", MySqlDbType.VarChar, phoneNumber));
            parameters.Add(makeInputParameter("P_RELT", MySqlDbType.VarChar, relationship));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_STUDENT_ART(long studentID, int artID)
        {
            const string SPName = "INSERT INTO STDT_ART_XREF (STDT_ID, ART_ID) VALUES (@P_STDT_ID, @P_ART_ID)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_STUDENT_NEW_ART(long studentID, int artID, int beltID, int tipID, int classCount)
        {
            const string SPName = "INSERT INTO STDT_ART_XREF (STDT_ID, ART_ID, BELT_ID, TIP_ID, CLASS_COUNT) VALUES (@P_STDT_ID, @P_ART_ID, @P_BELT_ID, @P_TIP_ID, @P_CLS_CNT)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));
            parameters.Add(makeInputParameter("P_TIP_ID", MySqlDbType.Int32, tipID));
            parameters.Add(makeInputParameter("P_CLS_CNT", MySqlDbType.Int32, classCount));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_TERM(string englishTerm, string chineseTerm, int beltID)
        {
            const string SPName = "INSERT INTO TERM_DOMN (TERM_TXT_ENG, TERM_TXT_CHN, BELT_ID, TERM_ACTIVE)" +
            "VALUES (@P_ENG_TERM, @P_CHN_TERM, @P_BELT_ID, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ENG_TERM", MySqlDbType.VarChar, englishTerm));
            parameters.Add(makeInputParameter("P_CHN_TERM", MySqlDbType.VarChar, chineseTerm));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_INSTRUCTOR(string firstName, string lastName, string type, string bio)
        {
            const string SPName = "INSERT INTO INST_DOMN (INST_FNAME, INST_LNAME, INST_STAT, INST_BIO, INST_DISP) VALUES " +
            "(@P_FIRST_NAME, @P_LAST_NAME, @P_TYPE, @P_BIO, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));
            parameters.Add(makeInputParameter("P_BIO", MySqlDbType.VarChar, bio));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_ART(string artTitle, int schoolID)
        {
            const string SPName = "INSERT INTO ART_DOMN (ART_TITLE, ART_SCHOOL_ID, ART_ACTIVE) VALUES (@P_ART_TITLE, @P_SCHOOL_ID, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_TITLE", MySqlDbType.VarChar, artTitle));
            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_BELT(string beltTitle, int beltLevel, int artID, string classOrTip, int classCount)
        {
            const string SPName = "INSERT INTO BELT_DOMN (BELT_TITLE, BELT_LEVEL, ART_ID, CLASS_TIP, CLASS_CNT, BELT_ACTIVE) " +
            "VALUES (@P_TITLE, @P_LEVEL, @P_ART_ID, @P_CLS_TIP, @P_CLS_CNT, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TITLE", MySqlDbType.VarChar, beltTitle));
            parameters.Add(makeInputParameter("P_LEVEL", MySqlDbType.Int32, beltLevel));
            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_CLS_TIP", MySqlDbType.VarChar, classOrTip));
            parameters.Add(makeInputParameter("P_CLS_CNT", MySqlDbType.Int32, classCount));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_TIP(string title, int level, int beltID, string lastTip)
        {
            const string SPName = "INSERT INTO TIP_DOMN (TIP_TITLE, TIP_LEVEL, BELT_ID, LAST_TIP, TIP_ACTIVE)" +
            "VALUES (@P_TITLE, @P_LEVEL, @P_BELT_ID, @P_LAST_TIP, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TITLE", MySqlDbType.VarChar, title));
            parameters.Add(makeInputParameter("P_LEVEL", MySqlDbType.Int32, level));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));
            parameters.Add(makeInputParameter("P_LAST_TIP", MySqlDbType.VarChar, lastTip));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_COURSE(string courseTitle, int firstArt, int secondArt, string courseDay, string courseTime, int instructorID, int repeating, string classLesson)
        {
            const string SPName = "INSERT INTO COURSE_DOMN (CRSE_TITLE, CRSE_ART_1, CRSE_ART_2, CRSE_DAY, CRSE_TIME, CRSE_INST, CRSE_RPT, CLASS_OR_LESSON, CRSE_ACTV)" +
            "VALUES (@P_CRSE_TITLE, @P_FRST_ART, @P_SNCD_ART, @P_CRSE_DAY, @P_CRSE_TIME, @P_INST_ID, @P_REPEAT, @P_CLS_LSN, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_TITLE", MySqlDbType.VarChar, courseTitle));
            parameters.Add(makeInputParameter("P_FRST_ART", MySqlDbType.Int32, firstArt));
            parameters.Add(makeInputParameter("P_SNCD_ART", MySqlDbType.Int32, secondArt));
            parameters.Add(makeInputParameter("P_CRSE_DAY", MySqlDbType.VarChar, courseDay));
            parameters.Add(makeInputParameter("P_CRSE_TIME", MySqlDbType.VarChar, courseTime));
            parameters.Add(makeInputParameter("P_INST_ID", MySqlDbType.Int32, instructorID));
            parameters.Add(makeInputParameter("P_REPEAT", MySqlDbType.VarChar, repeating));
            parameters.Add(makeInputParameter("P_CLS_LSN", MySqlDbType.VarChar, classLesson));

            return returnLastInsert(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_STUDENT_CHECK_IN(int classID, int studentID)
        {
            const string SPName = "UPDATE STDT_ATTEND SET ATTEND = 1 WHERE STDT_ID = @P_STDT_ID AND CLASS_ID = @P_CLS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CLS_ID", MySqlDbType.Int32, classID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_BY_ID(int studentID, string firstName, string lastName, string address, string city, string state, string zipCode, string emergencyContact, string birthDate, string paymentDate, string paymentPlan, Double paymentAmount, string studentNote, int studentPass, int studentPaid)
        {
            const string SPName = "UPDATE STDT_INFO SET STDT_LNAME = @P_LST_NME, STDT_FNAME = @P_FRST_NME, STDT_ADDR = @P_ADDR, STDT_CITY = @P_CITY," +
            "STDT_STATE = @P_STATE, STDT_ZIP = @P_ZIP, STDT_EMER = @P_EMG_CNTC, STDT_BRTH_DATE = @P_BRTH_DT, STDT_PYMT_DT = @P_PYMT_DT, " +
            "STDT_PYMT_PLAN = @P_PYMT_PLN, STDT_PYMT_AMT = @P_PYMT_AMT, STDT_NOTE = @P_NOTE, STDT_PASS = @P_PASS, STDT_PAID = @P_PAID WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_LST_NME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_FRST_NME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_ADDR", MySqlDbType.VarChar, address));
            parameters.Add(makeInputParameter("P_CITY", MySqlDbType.VarChar, city));
            parameters.Add(makeInputParameter("P_STATE", MySqlDbType.VarChar, state));
            parameters.Add(makeInputParameter("P_ZIP", MySqlDbType.VarChar, zipCode));
            parameters.Add(makeInputParameter("P_EMG_CNTC", MySqlDbType.VarChar, emergencyContact));
            parameters.Add(makeInputParameter("P_BRTH_DT", MySqlDbType.VarChar, birthDate));
            parameters.Add(makeInputParameter("P_PYMT_DT", MySqlDbType.VarChar, paymentDate));
            parameters.Add(makeInputParameter("P_PYMT_PLN", MySqlDbType.VarChar, paymentPlan));
            parameters.Add(makeInputParameter("P_PYMT_AMT", MySqlDbType.VarChar, paymentAmount.ToString()));
            parameters.Add(makeInputParameter("P_NOTE", MySqlDbType.VarChar, studentNote));
            parameters.Add(makeInputParameter("P_PASS", MySqlDbType.VarChar, studentPass.ToString()));
            parameters.Add(makeInputParameter("P_PAID", MySqlDbType.VarChar, studentPaid.ToString()));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_ART_COMPLETE(long XrefID, string CompleteDate)
        {
            const string SPName = "UPDATE STDT_ART_XREF SET COMP_DATE = @P_CPLT_DT WHERE XREF_ID = @P_XREF_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CPLT_DT", MySqlDbType.Int32, CompleteDate));
            parameters.Add(makeInputParameter("P_XREF_ID", MySqlDbType.VarChar, XrefID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_ART(int artID, int beltID, int tipID, int classCount, long XrefID)
        {
            const string SPName = "UPDATE STDT_ART_XREF SET ART_ID = @P_ART_ID, BELT_ID = @P_BELT_ID, TIP_ID = @P_TIP_ID, CLASS_COUNT = @P_CLS_CNT WHERE XREF_ID = @P_XREF_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_XREF_ID", MySqlDbType.Int32, XrefID));
            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.VarChar, artID));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.VarChar, beltID));
            parameters.Add(makeInputParameter("P_TIP_ID", MySqlDbType.VarChar, tipID));
            parameters.Add(makeInputParameter("P_CLS_CNT", MySqlDbType.VarChar, classCount));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_PHONE(int phoneId, string relationship, string phoneNumber)
        {
            const string SPName = "UPDATE STDT_NBRS SET STDT_RELT = @P_RELT, STDT_NUM = @P_PHONE WHERE NBR_ID = @P_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, phoneId));
            parameters.Add(makeInputParameter("P_PHONE", MySqlDbType.VarChar, phoneNumber));
            parameters.Add(makeInputParameter("P_RELT", MySqlDbType.VarChar, relationship));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_TERM(int termID, string englishTerm, string chineseTerm, int beltID)
        {
            const string SPName = "UPDATE TERM_DOMN SET TERM_TXT_ENG = @P_ENG_TERM, TERM_TXT_CHN = @P_CHN_TERM, BELT_ID = @P_BELT_ID WHERE TERM_ID = @P_TERM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TERM_ID", MySqlDbType.Int32, termID));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));
            parameters.Add(makeInputParameter("P_CHN_TERM", MySqlDbType.VarChar, chineseTerm));
            parameters.Add(makeInputParameter("P_ENG_TERM", MySqlDbType.VarChar, englishTerm));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_RENEWAL(int studentID, string renewalDate, string studentPlan)
        {
            const string SPName = "UPDATE STDT_INFO SET STDT_PYMT_DT = @P_DATE, STDT_PYMT_PLAN = @P_PLAN WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_PLAN", MySqlDbType.VarChar, studentPlan));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, renewalDate));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_INSTRUCTOR_BY_INSTRUCTOR_ID(int instructorID, string firstName, string lastName, string type, string bio)
        {
            const string SPName = "UPDATE INST_DOMN SET INST_FNAME = @P_FIRST_NAME, INST_LNAME = @P_LAST_NAME, INST_STAT = @P_TYPE, INST_BIO = @P_BIO WHERE INST_ID = @P_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));
            parameters.Add(makeInputParameter("P_BIO", MySqlDbType.VarChar, bio));
            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, instructorID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_ART(int artID, string artTitle)
        {
            const string SPName = "UPDATE ART_DOMN SET ART_TITLE = @P_ART_TITLE WHERE ART_ID = @P_ART_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_ART_TITLE", MySqlDbType.VarChar, artTitle));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_BELT(int beltID, string beltTitle, int beltLevel, int artID, string classOrTip, int classCount)
        {
            const string SPName = "UPDATE BELT_DOMN SET BELT_TITLE = @P_TITLE, BELT_LEVEL = @P_LEVEL, ART_ID = @P_ART_ID, CLASS_TIP = @P_CLS_TIP, CLASS_CNT = @P_CLS_CNT WHERE BELT_ID = @P_BELT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));
            parameters.Add(makeInputParameter("P_TITLE", MySqlDbType.VarChar, beltTitle));
            parameters.Add(makeInputParameter("P_LEVEL", MySqlDbType.Int32, beltLevel));
            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_CLS_TIP", MySqlDbType.VarChar, classOrTip));
            parameters.Add(makeInputParameter("P_CLS_CNT", MySqlDbType.Int32, classCount));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_TIP(int tipID, string tipTitle, int tipLevel, int beltID, string lastTip)
        {
            const string SPName = "UPDATE TIP_DOMN SET TIP_TITLE = @P_TITLE, TIP_LEVEL = @P_LEVEL, BELT_ID = @P_BELT_ID, LAST_TIP = @P_LAST_TIP WHERE TIP_ID = @P_TIP_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TIP_ID", MySqlDbType.Int32, tipID));
            parameters.Add(makeInputParameter("P_TITLE", MySqlDbType.VarChar, tipTitle));
            parameters.Add(makeInputParameter("P_LEVEL", MySqlDbType.Int32, tipLevel));
            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));
            parameters.Add(makeInputParameter("P_LAST_TIP", MySqlDbType.VarChar, lastTip));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_COURSE(int courseID, string courseTitle, int firstArt, int secondArt, string courseDay, string courseTime, int instructorID, string repeating)
        {
            const string SPName = "UPDATE COURSE_DOMN SET CRSE_TITLE = @P_CRSE_TITLE, CRSE_ART_1 = @P_FRST_ART, CRSE_ART_2 = @P_SNCD_ART, CRSE_DAY = @P_CRSE_DAY, CRSE_TIME = @P_CRSE_TIME, CRSE_INST = @P_INST_ID, CRSE_RPT = @P_REPEAT WHERE CRSE_ID = @P_CRSE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_CRSE_TITLE", MySqlDbType.VarChar, courseTitle));
            parameters.Add(makeInputParameter("P_FRST_ART", MySqlDbType.Int32, firstArt));
            parameters.Add(makeInputParameter("P_SNCD_ART", MySqlDbType.Int32, secondArt));
            parameters.Add(makeInputParameter("P_CRSE_DAY", MySqlDbType.VarChar, courseDay));
            parameters.Add(makeInputParameter("P_CRSE_TIME", MySqlDbType.VarChar, courseTime));
            parameters.Add(makeInputParameter("P_INST_ID", MySqlDbType.Int32, instructorID));
            parameters.Add(makeInputParameter("P_REPEAT", MySqlDbType.VarChar, repeating));

            return modifyData(parameters, SPName);
        }

        #endregion

        #region DELETE
        public bool ExecuteDELETE_COURSE_STUDENT(int courseID, int studentID)
        {
            const string SPName = "UPDATE STDT_CRSE_XREF SET ACTIVE = 0 WHERE STDT_ID = @P_STDT_ID AND CRSE_ID = @P_CRSE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_STUDENT(int studentID)
        {
            const string SPName = "UPDATE STDT_DOMN SET STDT_ACTIVE = 0 WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_STUDENT_PHONE(int phoneId)
        {
            const string SPName = "DELETE FROM STDT_NBRS WHERE NBR_ID = @P_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, phoneId));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_TERM(int termID)
        {
            const string SPName = "UPDATE TERM_DOMN SET TERM_ACTIVE = 0 WHERE TERM_ID = @P_TERM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TERM_ID", MySqlDbType.Int32, termID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_INSTRUCTOR(int instructorID)
        {
            const string SPName = "UPDATE INST_DOMN SET INST_DISP = False WHERE INST_ID = @P_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, instructorID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_ART(int artID)
        {
            const string SPName = "UPDATE ART_DOMN SET ART_ACTIVE = 0 WHERE ART_ID = @P_ART_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_BELT(int beltID)
        {
            const string SPName = "UPDATE BELT_DOMN SET BELT_ACTIVE = 0 WHERE BELT_ID = @P_BELT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BELT_ID", MySqlDbType.Int32, beltID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_TIP(int tipID)
        {
            const string SPName = "UPDATE TIP_DOMN SET TIP_ACTIVE = 0 WHERE TIP_ID = @P_TIP_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TIP_ID", MySqlDbType.Int32, tipID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_COURSE(int courseID)
        {
            const string SPName = "UPDATE COURSE_DOMN SET CRSE_ACTV = 0 WHERE CRSE_ID = @P_CRSE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CRSE_ID", MySqlDbType.Int32, courseID));

            return modifyData(parameters, SPName);
        }
        #endregion


        #endregion

    }

    public class SDAProdSQL : SqlConnector
    {
        private static string pointOfSaleConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTSDAPOS"].ConnectionString;
        private static string sdaPointOfSaleConnectionStringKey = "HOTSDAPOS";

        public SDAProdSQL(String connectionStringKey) : base(sdaPointOfSaleConnectionStringKey) { }

        #region SQL Calls
        #region SELECT
        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_STUDENT_ID(int studentID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_PAID, TRNS_TTL, TRNS_BGHT, TRNS_DATE, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_OTH FROM TRNS_DOMN WHERE TRNS_BGHT = @P_STDT_ID ORDER BY TRNS_ID DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_TRANSACTION_ID(int transactionID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_DATE, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH "
            + "FROM TRNS_DOMN WHERE TRNS_ID = @P_TRNS_ID ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTION_ITEMS_BY_TRANSACTION_ID(int transactionID)
        {
            const string SPName = "SELECT XREF_ID, PROD_ID, PROD_QTY, PROD_NME, PROD_PRICE, PROD_TAX FROM TRNS_XREF "
            + "WHERE TRNS_ID = @P_TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_ITEMS()
        {
            const string SPName = "SELECT * FROM PROD_DOMN A INNER JOIN PROD_INV C ON A.PROD_ID = C.PROD_ID WHERE A.PROD_DISP = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ITEM_BY_ITEM_ID(int itemID)
        {
            const string SPName = "SELECT PROD_NAME,PROD_PRICE,PROD_TYPE,PROD_TAX,PROD_FILE_NAME,PROD_DESC,PROD_SALE_ONLINE,PROD_SALE_STORE,PROD_SALE_PRICE,PROD_CODE,"
            + "A.PROD_LOC, A.PROD_ID,PROD_SUB_TYPE,PROD_DISP_ONLINE,PROD_DISP_STORE,PROD_CNT,A.PROD_DISP FROM PROD_DOMN A INNER JOIN PROD_INV C ON A.PROD_ID = C.PROD_ID "
            + "WHERE A.PROD_DISP = 1 AND A.PROD_ID = @P_ITEM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ITEM_ID", MySqlDbType.Int32, itemID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ITEM_BY_BARCODE(string barcode)
        {
            const string SPName = "SELECT PROD_NAME,PROD_PRICE,PROD_TYPE,PROD_TAX,PROD_FILE_NAME,PROD_DESC,PROD_SALE_ONLINE,PROD_SALE_STORE,PROD_SALE_PRICE,PROD_CODE,"
            + "A.PROD_LOC, A.PROD_ID,PROD_SUB_TYPE,A.PROD_DISP,PROD_DISP_ONLINE,PROD_DISP_STORE,PROD_CNT FROM PROD_DOMN A INNER JOIN PROD_INV C ON A.PROD_ID = C.PROD_ID "
            + "WHERE A.PROD_DISP = 1 AND PROD_CODE = @P_BAR_CODE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, barcode));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE(string transactionDate)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID " +
                "FROM TRNS_DOMN WHERE TRNS_DATE = @P_DATE ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, transactionDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE_EMPLOYEE(string date, int employeeID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID " +
                "FROM TRNS_DOMN WHERE TRNS_DATE = @P_DATE AND TRNS_SELL = @P_EMP_ID ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));
            parameters.Add(makeInputParameter("P_EMP_ID", MySqlDbType.Int32, employeeID));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_ITEM_INVENTORY(long itemID, int itemCount)
        {
            const string SPName = "UPDATE PROD_INV SET PROD_CNT = @P_PROD_CNT, PROD_INV_TMST = CURRENT_TIMESTAMP WHERE PROD_ID = @P_PROD_ID AND PROD_LOC = 'W'";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_CNT", MySqlDbType.Int32, itemID));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, itemCount));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_TRANSACTION(int transactionID, string seller, string total, string date, string payment, int isVoid, int isPaid)
        {
            const string SPName = "UPDATE TRNS_DOMN SET TRNS_SELL = @P_SELLER, TRNS_TTL = @P_TOTAL, TRNS_DATE = @P_DATE, TRNS_PYMT = @P_PYMT, TRNS_VOID = @P_VOID, "
            + "TRNS_PAID = @P_PAID WHERE TRNS_ID = @P_TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));
            parameters.Add(makeInputParameter("P_SELLER", MySqlDbType.VarChar, seller));
            parameters.Add(makeInputParameter("P_TOTAL", MySqlDbType.VarChar, total));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));
            parameters.Add(makeInputParameter("P_PYMT", MySqlDbType.VarChar, payment));
            parameters.Add(makeInputParameter("P_PAID", MySqlDbType.Int32, isPaid));
            parameters.Add(makeInputParameter("P_VOID", MySqlDbType.Int32, isVoid));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_ITEM(int productID, string name, string price, string type, string subtype, int tax, string description, int saleOnline,
            int saleStore, string salePrice, string barCode, string location, int prodOnline, int prodInStore, int prodActive)
        {
            const string SPName = "UPDATE PROD_DOMN SET PROD_NAME = @P_NAME,PROD_PRICE = @P_PRICE,PROD_TYPE = @P_TYPE,PROD_SUB_TYPE = @P_SUB_TYPE,PROD_TAX = @P_TAX,PROD_DESC = @P_DESC"
            + ",PROD_SALE_ONLINE = @P_SALE_ONLINE,PROD_SALE_STORE = @P_SALE_STORE,PROD_SALE_PRICE = @P_SALE_PRICE,PROD_CODE = @P_BAR_CODE,PROD_LOC = @P_LOCATION," +
            "PROD_DISP_ONLINE = @P_DISP_ONLINE, PROD_DISP_STORE = @P_DISP_STORE, PROD_DISP = @P_DISP WHERE PROD_ID = @P_PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, productID));
            parameters.Add(makeInputParameter("P_NAME", MySqlDbType.VarChar, name));
            parameters.Add(makeInputParameter("P_PRICE", MySqlDbType.VarChar, price));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));
            parameters.Add(makeInputParameter("P_SUB_TYPE", MySqlDbType.VarChar, subtype));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.Int32, tax));
            parameters.Add(makeInputParameter("P_DESC", MySqlDbType.VarChar, description));
            parameters.Add(makeInputParameter("P_SALE_ONLINE", MySqlDbType.Int32, saleOnline));
            parameters.Add(makeInputParameter("P_SALE_STORE", MySqlDbType.Int32, saleStore));
            parameters.Add(makeInputParameter("P_SALE_PRICE", MySqlDbType.VarChar, salePrice));
            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, barCode));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_DISP_ONLINE", MySqlDbType.Int32, prodOnline));
            parameters.Add(makeInputParameter("P_DISP_STORE", MySqlDbType.Int32, prodInStore));
            parameters.Add(makeInputParameter("P_DISP", MySqlDbType.Int32, prodActive));

            return modifyData(parameters, SPName);
        }

        #endregion

        #region INSERT
        public long ExecuteINSERT_TRANSACTION(int employeeID, string cartTotal, int studentID, string location, string paymentType, string date, string tax, string note)
        {
            const string SPName = "INSERT INTO TRNS_DOMN (TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_DATE, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH) "
            + "VALUES (@P_EMPL_ID, @P_TOTAL, @P_STDT_ID, @P_LOC, @P_TYPE,@P_DATE, @P_TAX, 0, 1, @P_NOTE)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_TOTAL", MySqlDbType.VarChar, cartTotal));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_LOC", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, paymentType));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.VarChar, tax));
            parameters.Add(makeInputParameter("P_NOTE", MySqlDbType.VarChar, note));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_TRANSACTION_ITEM(long transactionID, int itemID, int itemQuantity, string itemName, string itemPrice, int itemTax)
        {
            const string SPName = "INSERT INTO TRNS_XREF (TRNS_ID, PROD_ID, PROD_QTY, PROD_NME, PROD_PRICE, PROD_TAX) VALUES (@P_TRNS_ID,@P_ITEM_ID,@P_ITEM_QTY,@P_ITEM_NME,@P_ITEM_PRICE,@P_ITEM_TAX)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));
            parameters.Add(makeInputParameter("P_ITEM_ID", MySqlDbType.Int32, itemID));
            parameters.Add(makeInputParameter("P_ITEM_QTY", MySqlDbType.Int32, itemQuantity));
            parameters.Add(makeInputParameter("P_ITEM_NME", MySqlDbType.VarChar, itemName));
            parameters.Add(makeInputParameter("P_ITEM_PRICE", MySqlDbType.VarChar, itemPrice));
            parameters.Add(makeInputParameter("P_ITEM_TAX", MySqlDbType.Int32, itemTax));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_TRANSACTION_NOTE(int studentID, string note)
        {
            const string SPName = "UPDATE STDT_INFO SET STDT_NOTE = @P_NOTE WHERE STDT_ID = @P_STDT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));
            parameters.Add(makeInputParameter("P_NOTE", MySqlDbType.VarChar, note));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_PRODUCT(string Name, string Price, string Type, string SubType, int Tax, string Description, int SaleOnline, int SaleStore,
            string SalePrice, string Barcode, string Location, int ProductDisplayOnline, int ProductDisplayStore)
        {
            const string SPName = "INSERT INTO PROD_DOMN (PROD_NAME,PROD_PRICE,PROD_TYPE,PROD_SUB_TYPE,PROD_TAX,PROD_DESC,PROD_SALE_ONLINE,PROD_SALE_STORE," +
                "PROD_SALE_PRICE,PROD_CODE,PROD_LOC,PROD_DISP_ONLINE,PROD_DISP_STORE,PROD_DISP)" +
                "VALUES (@P_NAME,@P_PRICE,@P_TYPE,@P_SUB_TYPE,@P_TAX,@P_FILE_NAME,@P_DESC,@P_SALE_ONLINE," + 
                "@P_SALE_STORE,@P_SALE_PRICE,@P_BAR_CODE,@P_LOC,@P_DISP_ONLINE,@P_DISP_STORE,1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NAME", MySqlDbType.VarChar, Name));
            parameters.Add(makeInputParameter("P_PRICE", MySqlDbType.VarChar, Price));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, Type));
            parameters.Add(makeInputParameter("P_SUB_TYPE", MySqlDbType.VarChar, SubType));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.Int32, Tax));
            parameters.Add(makeInputParameter("P_DESC", MySqlDbType.VarChar, Description));
            parameters.Add(makeInputParameter("P_SALE_ONLINE", MySqlDbType.Int32, SaleOnline));
            parameters.Add(makeInputParameter("P_SALE_STORE", MySqlDbType.Int32, SaleStore));
            parameters.Add(makeInputParameter("P_SALE_PRICE", MySqlDbType.VarChar, SalePrice));
            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, Barcode));
            parameters.Add(makeInputParameter("P_LOC", MySqlDbType.VarChar, Location));
            parameters.Add(makeInputParameter("P_DISP_ONLINE", MySqlDbType.Int32, ProductDisplayOnline));
            parameters.Add(makeInputParameter("P_DISP_STORE", MySqlDbType.Int32, ProductDisplayStore));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_ITEM_INVENTORY(long itemID, int itemCount)
        {
            const string SPName = "INSERT INTO PROD_INV (PROD_CNT, PROD_INV_TMST, PROD_ID, PROD_LOC, PROD_DISP) VALUES (@P_PROD_CNT, CURRENT_TIMESTAMP, @P_PROD_ID,'W',1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_CNT", MySqlDbType.Int32, itemCount));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, itemID));

            return modifyData(parameters, SPName);
        }
        #endregion

        #region DELETE
        public bool ExecuteDELETE_TRANSACTION_ITEM(int transactionItemID)
        {
            const string SPName = "DELETE FROM TRNS_XREF WHERE XREF_ID = @P_XREF_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_XREF_ID", MySqlDbType.Int32, transactionItemID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_ITEM(int itemID)
        {
            const string SPName = "UPDATE PROD_DOMN SET PROD_DISP = 0 WHERE PROD_ID = @P_ID; UPDATE PROD_INV SET PROD_DISP = 0 WHERE PROD_ID = @P_ID;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ID", MySqlDbType.Int32, itemID));

            return modifyData(parameters, SPName);
        }
        
        #endregion
        #endregion
    }
}
