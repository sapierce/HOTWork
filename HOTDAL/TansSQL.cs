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
    public class TansSQL : SqlConnector
    {
        private static string tansConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTTans"].ConnectionString;
        private static string tansConnectionStringKey = "HOTTans";
        
        #region SQLCalls
        public TansSQL(String connectionStringKey) : base(tansConnectionStringKey) { }

        #region SELECT
        public DataTable ExecuteCUSTOMER_INFO_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT A.CUST_FNAME, A.CUST_LNAME, A.CUST_JOIN, A.CUST_RENEWAL, A.CUST_PLAN, A.SPECIAL_FLAG, A.SPECIAL_DATE, A.SPECIAL_ID, A.CUST_RESTRICT, A.CUST_FPS, A.CUST_REMARK, " +
                "A.CUST_LOTION, A.CUST_ONLINE, A.CUST_NEW_ONLINE, A.CUST_ACTIVE, B.USER_MAIL, B.USER_SPECIAL, B.USER_NAME, B.VERIFY_IND, C.CUST_DOB, C.CUST_ADDR, C.CUST_CITY, C.CUST_ST, C.CUST_ZIP, " + 
                "C.CUST_PHONE, C.CUST_FHIST, C.CUST_HIST, C.CUST_WARN, C.CUST_WARN_TXT " + 
                "FROM TN_CUST_INFO A LEFT OUTER JOIN TN_CUST_ONLINE_INFO B ON A.USER_ID = B.TAN_UID LEFT OUTER JOIN TN_CUST_NEW_INFO C ON A.USER_ID = C.CUST_TAN_ID " + 
                "WHERE A.USER_ID = @P_USER_ID;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_INFO_BY_CUSTOMER_NAME(string customerFirstName, string customerLastName)
        {
            const string SPName = "SELECT USER_ID, CUST_LNAME, CUST_FNAME, CUST_JOIN, CUST_PLAN, CUST_ONLINE FROM TN_CUST_INFO WHERE CUST_LNAME LIKE @P_LAST_NAME AND " + 
                "CUST_FNAME LIKE @P_FIRST_NAME AND CUST_ACTIVE = '1' ORDER BY CUST_LNAME, CUST_FNAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, customerLastName + "%"));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, customerFirstName + "%"));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteONLINE_CUSTOMERS(bool newOnlineOnly)
        {
            string SPName = "SELECT USER_ID FROM TN_CUST_INFO WHERE CUST_ONLINE = '1' " +
                (newOnlineOnly ? " AND CUST_NEW_ONLINE = '1'" : "") +
                "ORDER BY USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteNEW_ONLINE_CUSTOMERS()
        {
            string SPName = "SELECT CUST_TAN_ID FROM TN_CUST_NEW_INFO ORDER BY CUST_TAN_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_TANS_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT TAN_ID, TAN_DATE, TAN_TIME, TAN_LOC, TAN_LENGTH, TAN_BED, TAN_ONLINE, ACTV_IND FROM TN_TAN_LOG_INFO WHERE USER_ID = @P_USER_ID ORDER BY TAN_DATE DESC;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_TAN_BY_CUSTOMER_ID_DATE(Int64 customerID, string tanDate)
        {
            const string SPName = "SELECT TAN_ID FROM TN_TAN_LOG_INFO WHERE TAN_DATE = @P_DATE AND USER_ID = @P_USER_ID AND ACTV_IND = 1 ORDER BY TAN_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_24_HOUR_CHECK(Int64 customerID, string tanDate)
        {
            const string SPName = "SELECT TAN_TIME, TAN_DATE, TAN_LENGTH FROM TN_TAN_LOG_INFO WHERE USER_ID = @P_USER_ID AND TAN_DATE = @P_DATE AND ACTV_IND = '1'";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_NOTES_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT NOTE_ID, NOTE_TXT, NOTE_DISP, NOTE_OWES, NOTE_OWED, NOTE_CHECK FROM TN_CUST_NOTE_INFO WHERE USER_ID = @P_USER_ID AND NOTE_DISP = 1;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_NOTES_BY_NOTE_ID(Int32 noteID)
        {
            const string SPName = "SELECT NOTE_ID, NOTE_TXT, NOTE_DISP, NOTE_OWES, NOTE_OWED, NOTE_CHECK FROM TN_CUST_NOTE_INFO WHERE NOTE_ID = @P_NOTE_ID AND NOTE_DISP = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NOTE_ID", MySqlDbType.Int32, noteID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_PUBLIC_LOGIN(string userName, string password)
        {
            const string SPName = "SELECT TAN_UID, PASS_TXT FROM TN_CUST_ONLINE_INFO WHERE USER_NAME = @P_USERNAME AND PASS_TXT = @P_PASSWORD";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USERNAME", MySqlDbType.VarChar, userName));
            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, password));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_TAN_ID_BY_USER_NAME(string userName)
        {
            const string SPName = "SELECT TAN_UID FROM TN_CUST_ONLINE_INFO WHERE USER_NAME = @P_USER_NAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_NAME", MySqlDbType.VarChar, userName));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_BY_EMAIL(string emailAddress)
        {
            const string SPName = "SELECT * FROM TN_CUST_ONLINE_INFO WHERE USER_MAIL = @P_EMAIL";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMAIL", MySqlDbType.VarChar, emailAddress));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_EMAIL_VERIFICATION(string emailAddress, string guid)
        {
            const string SPName = "SELECT TAN_UID, USER_ID, VERIFY_TMST FROM TN_CUST_ONLINE_INFO WHERE USER_MAIL = @P_EMAIL AND VERIFY_ID = @P_GUID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMAIL", MySqlDbType.VarChar, emailAddress));
            parameters.Add(makeInputParameter("P_GUID", MySqlDbType.VarChar, guid));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_BILLING_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT TAN_PURCHASE, TAN_RENEWAL, TAN_PACKAGE FROM TN_CUST_HIST_INFO WHERE USER_ID = @P_USER_ID ORDER BY TAN_PURCHASE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTIMES_BY_LOCATION_TYPE(string location, string type)
        {
            const string SPName = "SELECT TIME_DAY, BEG_TIME, END_TIME, TIME_WEB FROM TN_STORE_TIME_INFO WHERE TIME_LOC = @P_LOCATION AND TIME_TYPE = @P_TYPE;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTIMES_BY_LOCATION_DAY_TYPE(string location, string day, string type)
        {
            const string SPName = "SELECT BEG_TIME, END_TIME FROM TN_STORE_TIME_INFO WHERE TIME_LOC = @P_LOCATION AND TIME_WEB = @P_DAY AND TIME_TYPE = @P_TYPE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, day));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTAN_TIME_TAKEN(string tanBed, string tanDate, string location, string tanTime)
        {
            const string SPName = "SELECT TAN_ID, TAN_BED, TAN_DATE, TAN_LOC, TAN_TIME, TAN_LENGTH, ACTV_IND, TAN_ONLINE, USER_ID FROM TN_TAN_LOG_INFO WHERE TAN_BED = @P_BED AND TAN_DATE = @P_DATE AND TAN_LOC = @P_LOCATION AND TAN_TIME = @P_TIME AND ACTV_IND = 1 ORDER BY TAN_TIME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, tanBed));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, tanTime));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTAN_TIMES_TAKEN(string tanBed, string tanDate, string location)
        {
            const string SPName = "SELECT TAN_TIME FROM TN_TAN_LOG_INFO WHERE TAN_BED = @P_BED AND TAN_DATE = @P_DATE AND TAN_LOC = @P_LOCATION AND ACTV_IND = 1 ORDER BY TAN_TIME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, tanBed));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTAN_BY_TAN_ID(int tanID)
        {
            const string SPName = "SELECT TAN_ID, TAN_BED, TAN_DATE, TAN_LOC, TAN_TIME, TAN_LENGTH, ACTV_IND, TAN_ONLINE, USER_ID FROM TN_TAN_LOG_INFO WHERE TAN_ID = @P_TAN_ID AND ACTV_IND = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TAN_ID", MySqlDbType.Int32, tanID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTANS_BY_DATE(string tanBeginDate, string tanEndDate)
        {
            const string SPName = "SELECT TAN_ID, TAN_BED, TAN_DATE, TAN_LOC, TAN_TIME, TAN_LENGTH, ACTV_IND, TAN_ONLINE, USER_ID " +
                "FROM TN_TAN_LOG_INFO WHERE TAN_DATE >= @P_TAN_BEGIN AND TAN_DATE <= @P_TAN_END AND ACTV_IND = 1 ORDER BY TAN_DATE, TAN_TIME, TN_TAN_BED_DOMN";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TAN_BEGIN", MySqlDbType.VarChar, tanBeginDate));
            parameters.Add(makeInputParameter("P_TAN_END", MySqlDbType.VarChar, tanEndDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteALL_ACTIVE_SPECIALS()
        {
            const string SPName = "SELECT SPEC_ID, SPEC_NME, SPEC_SHORT_NME, PROD_ID, SPEC_ACTV, SPEC_LENGTH FROM TN_SPEC_DOMN WHERE SPEC_ACTV = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSPECIALS_BY_SPECIAL_NAME(string specialName)
        {
            const string SPName = "SELECT SPEC_ID, SPEC_NME, SPEC_SHORT_NME, PROD_ID, SPEC_ACTV, SPEC_LENGTH FROM TN_SPEC_DOMN WHERE SPEC_SHORT_NME = @P_SPECIAL_NAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SPECIAL_NAME", MySqlDbType.VarChar, specialName));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSPECIALS_BY_SPECIAL_ID(int specialID)
        {
            const string SPName = "SELECT SPEC_ID, SPEC_NME, SPEC_SHORT_NME, PROD_ID, SPEC_ACTV, SPEC_LENGTH FROM TN_SPEC_DOMN WHERE SPEC_ID = @P_SPECIAL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int32, specialID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSPECIAL_LEVELS_BY_SPECIAL_ID(int specialID)
        {
            const string SPName = "SELECT SPEC_LEVEL_ID, SPEC_ID, SPEC_LEVEL_BED, SPEC_LEVEL_LENGTH, SPEC_LEVEL_ORDER FROM TN_SPEC_INFO WHERE SPEC_ID = @P_SPECIAL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int32, specialID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSPECIAL_LEVEL_BY_LEVEL_ID(int specialLevelID)
        {
            const string SPName = "SELECT SPEC_LEVEL_ID, SPEC_ID, SPEC_LEVEL_BED, SPEC_LEVEL_LENGTH, SPEC_LEVEL_ORDER FROM TN_SPEC_INFO WHERE SPEC_LEVEL_ID = @P_LEVEL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LEVEL_ID", MySqlDbType.Int32, specialLevelID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSPECIAL_BY_PRODUCT_ID(int productId)
        {
            const string SPName = "SELECT A.SPEC_ID, A.SPEC_NME, A.SPEC_SHORT_NME, A.SPEC_LENGTH, A.PROD_ID, A.SPEC_ACTV, B.SPEC_LEVEL_ID, B.SPEC_LEVEL_LENGTH " +
                "FROM TN_SPEC_DOMN A INNER JOIN TN_SPEC_INFO B ON A.SPEC_ID = B.SPEC_ID WHERE PROD_ID = @P_PROD_ID AND B.SPEC_LEVEL_ORDER = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, productId));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteALL_ACTIVE_PLANS()
        {
            const string SPName = "SELECT PLAN_ID, PLAN_LONG, PLAN_SHORT, BED_TYPE, PLAN_ACTIVE, PLAN_LENGTH, PLAN_TAN_COUNT, PROD_ID FROM TN_PLAN_DOMN WHERE PLAN_ACTIVE = 1 ORDER BY BED_TYPE, PLAN_LENGTH";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePLAN_BY_PLAN_NAME(string planName)
        {
            const string SPName = "SELECT PLAN_ID, PLAN_LONG, PLAN_SHORT, BED_TYPE, PLAN_ACTIVE, PLAN_LENGTH, PLAN_TAN_COUNT, PROD_ID FROM TN_PLAN_DOMN WHERE PLAN_SHORT = @P_PLAN_NAME AND PLAN_ACTIVE = 1;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PLAN_NAME", MySqlDbType.VarChar, planName));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePLAN_BY_PLAN_ID(int planID)
        {
            const string SPName = "SELECT PLAN_ID, PLAN_LONG, PLAN_SHORT, BED_TYPE, PLAN_ACTIVE, PLAN_LENGTH, PLAN_TAN_COUNT, PROD_ID FROM TN_PLAN_DOMN WHERE PLAN_ID = @P_PLAN_ID AND PLAN_ACTIVE = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PLAN_ID", MySqlDbType.Int32, planID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePLAN_BY_PRODUCT_ID(int productID)
        {
            const string SPName = "SELECT PLAN_ID, PLAN_LONG, PLAN_SHORT, BED_TYPE, PLAN_ACTIVE, PLAN_LENGTH, PLAN_TAN_COUNT, PROD_ID FROM TN_PLAN_DOMN WHERE PROD_ID = @P_PRODUCT_ID AND PLAN_ACTIVE = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PRODUCT_ID", MySqlDbType.Int32, productID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteBEDS_BY_BED_ID(int bedID)
        {
            const string SPName = "SELECT BED_ID, BED_SHORT, BED_LONG, BED_TYPE, BED_DISP_INT, BED_DISP_EXT, BED_LOC, BED_ACTV FROM TN_TAN_BED_DOMN WHERE BED_ID = @P_BED_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_ID", MySqlDbType.Int32, bedID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEXTERNAL_ACTIVE_BEDS_BY_BED_TYPE(string bedType)
        {
            const string SPName = "SELECT BED_ID, BED_SHORT, BED_LONG, BED_TYPE, BED_DISP_INT, BED_DISP_EXT, BED_LOC, BED_ACTV FROM TN_TAN_BED_DOMN WHERE BED_TYPE = @P_BED_TYPE AND BED_DISP_EXT = 1 AND BED_ACTV = 1 ";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_TYPE", MySqlDbType.VarChar, bedType));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ALL_ACTIVE_BEDS()
        {
            const string SPName = "SELECT BED_ID, BED_SHORT, BED_LONG, BED_TYPE, BED_DISP_INT, BED_DISP_EXT, BED_LOC, BED_ACTV FROM TN_TAN_BED_DOMN WHERE BED_ACTV = 1 ORDER BY BED_ORDER";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteBEDS_BY_LOCATION(string bedLocation)
        {
            const string SPName = "SELECT BED_ID, BED_SHORT, BED_LONG, BED_TYPE, BED_DISP_INT, BED_DISP_EXT, BED_LOC, BED_ACTV FROM TN_TAN_BED_DOMN WHERE BED_LOC = @P_LOCATION AND BED_ACTV = 1 ORDER BY BED_ORDER";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, bedLocation));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteBED_SUMMARY_BY_DATE(int bedId, string summaryDate)
        {
            const string SPName = "SELECT TAN_BED, SUM(TAN_LENGTH) AS TOTAL_TIME, COUNT(TAN_ID) AS TANNER_COUNT FROM TN_TAN_LOG_INFO WHERE TAN_DATE = @P_SUMMARY_DT AND TAN_LENGTH <> 0 AND TAN_BED = @P_BED_ID GROUP BY TN_TAN_BED_DOMN DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_ID", MySqlDbType.Int32, bedId));
            parameters.Add(makeInputParameter("P_SUMMARY_DT", MySqlDbType.String, summaryDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteALL_ACTIVE_EMPLOYEES()
        {
            const string SPName = "SELECT EMPL_ID, EMPL_FNAME, EMPL_LNAME FROM TN_EMPL_DOMN WHERE EMPL_ID <> '1234' AND EMPL_DISP <> 0 ORDER BY EMPL_LNAME, EMPL_FNAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_BY_EMPLOYEE_ID(Int32 employeeID)
        {
            const string SPName = "SELECT EMPL_ID, EMPL_FNAME, EMPL_LNAME FROM TN_EMPL_DOMN WHERE EMPL_ID = @P_EMPL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_NOTES_TO_EMPLOYEES()
        {
            const string SPName = "SELECT NOTE_ID, NOTE_TO, NOTE_FROM, NOTE_TXT, NOTE_DATE FROM TN_EMPL_NOTE_INFO WHERE NOTE_TO <> '1' AND NOTE_DISP = '1' ORDER BY NOTE_ID DESC LIMIT 0, 10";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_NOTES_FROM_EMPLOYEES()
        {
            const string SPName = "SELECT NOTE_ID, NOTE_TO, NOTE_FROM, NOTE_TXT, NOTE_DATE FROM TN_EMPL_NOTE_INFO WHERE NOTE_TO = '1' AND NOTE_DISP = '1' ORDER BY NOTE_ID DESC LIMIT 0, 10";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteALL_ACTIVE_SITE_NOTICES()
        {
            const string SPName = "SELECT NOTICE_ID, NOTICE_TXT, NOTICE_START_DT, NOTICE_END_DT FROM TN_SITE_NTFY_INFO WHERE NOTICE_START_DT <= CURDATE() AND NOTICE_END_DT >= CURDATE()";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_LOGIN(int employeeID, string employeePassword)
        {
            const string SPName = "SELECT EMPL_ID FROM TN_EMPL_DOMN WHERE EMPL_ID = @P_EMPL_ID AND EMPL_PWD = @P_PASSWORD";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, employeePassword));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_CURRENT_SHIFT_BY_EMPLOYEE_ID(int employeeID)
        {
            const string SPName = "SELECT SHFT_ID, SHFT_DATE FROM TN_EMPL_SHFT_XREF WHERE EMPL_ID = @P_EMPL_ID AND SHFT_END_HOUR = '00:00:00'";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_SCHEDULE_BY_EMPLOYEE_ID(int employeeID, string startDate, string endDate)
        {
            const string SPName = "SELECT SCHD_ID, BEG_TIME, END_TIME, SCHD_LOC, SCHD_DATE, EMPL_ID FROM TN_EMPL_SCHD_INFO WHERE EMPL_ID = @P_EMPL_ID AND DISP_IND = '1' AND SCHD_DATE >= @P_START AND SCHD_DATE <= @P_END ORDER BY SCHD_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_WORKED_SHIFT_BY_EMPLOYEE_ID(int employeeID, string startDate, string endDate)
        {
            const string SPName = "SELECT EMPL_ID, SHFT_START_HOUR, SHFT_END_HOUR, SHFT_DATE, SHFT_ID FROM TN_EMPL_SHFT_XREF WHERE EMPL_ID = @P_EMPL_ID AND SHFT_DATE >= @P_START AND SHFT_DATE <= @P_END ORDER BY SHFT_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteEMPLOYEE_SHIFT_BY_SHIFT_ID(int shiftID)
        {
            const string SPName = "SELECT EMPL_ID, SHFT_START_HOUR, SHFT_END_HOUR, SHFT_DATE, SHFT_ID FROM TN_EMPL_SHFT_XREF WHERE SHFT_ID = @P_SHIFT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHIFT_ID", MySqlDbType.Int32, shiftID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteADMIN_LOGIN(string passwordType, string password)
        {
            const string SPName = "SELECT PWD_ID FROM TN_ADMN_DOMN WHERE PWD_TXT = @P_PASSWORD AND PWD_TYPE = @P_TYPE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, password));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, passwordType));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteMASSAGE_INFO_BY_DATA(string massageDate, string massageTime)
        {
            const string SPName = "SELECT MASSAGE_ID, MASSAGE_LENGTH, MASSAGE_DATE, MASSAGE_TIME, USER_ID, ACTV_IND FROM MASSAGE_LOG WHERE MASSAGE_DATE = @P_DATE AND MASSAGE_TIME = @P_TIME AND ACTV_IND = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, massageDate));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, massageTime));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteMASSAGE_TIME_TAKEN(string massageDate)
        {
            const string SPName = "SELECT MASSAGE_ID, MASSAGE_LENGTH, MASSAGE_DATE, MASSAGE_TIME, USER_ID, ACTV_IND FROM MASSAGE_LOG WHERE MASSAGE_DATE = @P_DATE AND ACTV_IND = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, massageDate));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_MASSAGES_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT MASSAGE_ID, MASSAGE_LENGTH, MASSAGE_DATE, MASSAGE_TIME, USER_ID, ACTV_IND FROM MASSAGE_LOG WHERE USER_ID = @P_USER_ID AND ACTV_IND = 1 ORDER BY MASSAGE_DATE DESC;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteCUSTOMER_MASSAGES_BY_MASSAGE_ID(int massageID)
        {
            const string SPName = "SELECT MASSAGE_ID, MASSAGE_LENGTH, MASSAGE_DATE, MASSAGE_TIME, USER_ID, ACTV_IND FROM MASSAGE_LOG WHERE MASSAGE_ID = @P_MASSAGE_ID AND ACTV_IND = 1 ORDER BY MASSAGE_DATE DESC;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_MASSAGE_ID", MySqlDbType.Int32, massageID));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_TIMES_BY_LOCATION_TYPE_DAY(string beginTime, string endTime, string location, string day, string type)
        {
            const string SPName = "UPDATE TN_STORE_TIME_INFO SET BEG_TIME = @P_BEG_TIME, END_TIME = @P_END_TIME WHERE TIME_LOC = @P_LOCATION AND TIME_DAY = @P_DAY AND TIME_TYPE = @P_TYPE;";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BEG_TIME", MySqlDbType.VarChar, beginTime));
            parameters.Add(makeInputParameter("P_END_TIME", MySqlDbType.VarChar, endTime));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_DAY", MySqlDbType.VarChar, day));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_NOTE_BY_NOTE_ID(int noteID, string noteText, int owesMoney, int owedProduct, int checkTransactions)
        {
            const string SPName = "UPDATE TN_CUST_NOTE_INFO SET NOTE_TXT = @P_NOTE_TEXT, NOTE_OWES = @P_NOTE_OWES, NOTE_OWED = @P_NOTE_OWED, NOTE_CHECK = @P_NOTE_CHECK WHERE NOTE_ID = @P_NOTE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NOTE_TEXT", MySqlDbType.VarChar, noteText));
            parameters.Add(makeInputParameter("P_NOTE_OWES", MySqlDbType.Int32, owesMoney));
            parameters.Add(makeInputParameter("P_NOTE_OWED", MySqlDbType.Int32, owedProduct));
            parameters.Add(makeInputParameter("P_NOTE_CHECK", MySqlDbType.Int32, checkTransactions));
            parameters.Add(makeInputParameter("P_NOTE_ID", MySqlDbType.Int32, noteID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_BED_BY_BED_ID(int bedID, string bedDescription, string shortDescription, string bedLocation, string bedType, int bedDisplayInternal, int bedDisplayExternal)
        {
            const string SPName = "UPDATE TN_TAN_BED_DOMN SET BED_LONG = @P_BED_DESC, BED_SHORT = @P_BED_SHORT, BED_LOC = @P_BED_LOC, BED_TYPE = @P_BED_TYPE, " + 
                "BED_DISP_INT = @P_BED_DISP_IN, BED_DISP_EXT = @P_BED_DISP_EX WHERE BED_ID = @P_BED_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_DESC", MySqlDbType.VarChar, bedDescription));
            parameters.Add(makeInputParameter("P_BED_SHORT", MySqlDbType.VarChar, shortDescription));
            parameters.Add(makeInputParameter("P_BED_LOC", MySqlDbType.VarChar, bedLocation));
            parameters.Add(makeInputParameter("P_BED_TYPE", MySqlDbType.VarChar, bedType));
            parameters.Add(makeInputParameter("P_BED_DISP_IN", MySqlDbType.Int32, bedDisplayInternal));
            parameters.Add(makeInputParameter("P_BED_DISP_EX", MySqlDbType.Int32, bedDisplayExternal));
            parameters.Add(makeInputParameter("P_BED_ID", MySqlDbType.Int32, bedID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_PACKAGE_BY_PACKAGE_ID(Int64 packageId, string shortName, string longName, string bedType, int packageLengthDays)
        {
            const string SPName = "UPDATE TN_PLAN_DOMN SET PLAN_LONG = @P_PLAN_LONG, PLAN_SHORT = @P_PLAN_SHORT, BED_TYPE = @P_PLAN_BED, " +
                "PLAN_LENGTH = @P_SPLAN_LENGTH WHERE PLAN_ID = @P_PLAN_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PLAN_LONG", MySqlDbType.VarChar, longName));
            parameters.Add(makeInputParameter("P_PLAN_SHORT", MySqlDbType.VarChar, shortName));
            parameters.Add(makeInputParameter("P_PLAN_BED", MySqlDbType.VarChar, bedType));
            parameters.Add(makeInputParameter("P_PLAN_LENGTH", MySqlDbType.Int32, packageLengthDays));
            parameters.Add(makeInputParameter("P_PLAN_ID", MySqlDbType.Int32, packageId));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_SPECIAL_BY_SPECIAL_ID(Int64 specialID, string specialName, string specialShortName, Int64 productID, int specialActive)
        {
            const string SPName = "UPDATE TN_SPEC_DOMN SET SPEC_NME = @P_SPECIAL_LONG, SPEC_SHORT_NME = @P_SPECIAL_SHORT, PROD_ID = @P_SPECIAL_PRODUCT, SPEC_ACTV = @P_SPECIAL_ACTIVE WHERE SPEC_ID = @P_SPECIAL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SPECIAL_LONG", MySqlDbType.VarChar, specialName));
            parameters.Add(makeInputParameter("P_SPECIAL_SHORT", MySqlDbType.VarChar, specialShortName));
            parameters.Add(makeInputParameter("P_SPECIAL_PRODUCT", MySqlDbType.Int32, productID));
            parameters.Add(makeInputParameter("P_SPECIAL_ACTIVE", MySqlDbType.Int32, specialActive));
            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int32, specialID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_SPECIAL_LEVEL_BY_SPECIAL_LEVEL_ID(int specialLevelID, Int64 specialID, string specialBed, int specialLength, int specialOrder)
        {
            const string SPName = "UPDATE TN_SPEC_INFO SET SPEC_ID = @P_SPECIAL_ID, SPEC_BED = @P_LEVEL_BED, SPEC_LENGTH = @P_LEVEL_LENGTH, SPEC_ORDER = @P_LEVEL_ORDER WHERE SPEC_LEVEL_ID = @P_LEVEL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LEVEL_BED", MySqlDbType.VarChar, specialBed));
            parameters.Add(makeInputParameter("P_LEVEL_LENGTH", MySqlDbType.Int32, specialLength));
            parameters.Add(makeInputParameter("P_LEVEL_ORDER", MySqlDbType.Int32, specialOrder));
            parameters.Add(makeInputParameter("P_LEVEL_ID", MySqlDbType.Int32, specialLevelID));
            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int32, specialID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_BY_CUSTOMER_ID(string FirstName, string LastName, int FitzNumber, string JoinDate, string RenewalDate, string Plan, 
            int OnSpecial, int SpecialLevelId, string SpecialRenewalDate, string Remarks, int Lotion, int Restrict, Int64 UserID)
        {
            const string SPName = "UPDATE TN_CUST_INFO SET CUST_FNAME = @P_FIRST_NAME, CUST_LNAME = @P_LAST_NAME, CUST_FPS = @P_FITZ, CUST_JOIN = @P_JOIN_DATE, "
            + "CUST_RENEWAL = @P_RENEWAL_DATE, CUST_REMARK = @P_REMARK, CUST_PLAN = @P_PLAN, CUST_LOTION = @P_LOTION, CUST_RESTRICT = @P_RESTRICT, " +
            " SPECIAL_FLAG = @P_ON_SPECIAL, SPECIAL_ID = @P_SPEC_LEVEL, SPECIAL_DATE = @P_SPEC_RENEW WHERE USER_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, UserID));
            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, LastName));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, FirstName));
            parameters.Add(makeInputParameter("P_JOIN_DATE", MySqlDbType.VarChar, JoinDate));
            parameters.Add(makeInputParameter("P_RENEWAL_DATE", MySqlDbType.VarChar, RenewalDate));
            parameters.Add(makeInputParameter("P_FITZ", MySqlDbType.Int32, FitzNumber));
            parameters.Add(makeInputParameter("P_PLAN", MySqlDbType.VarChar, Plan));
            parameters.Add(makeInputParameter("P_REMARK", MySqlDbType.VarChar, Remarks));
            parameters.Add(makeInputParameter("P_LOTION", MySqlDbType.Int32, Lotion));
            parameters.Add(makeInputParameter("P_RESTRICT", MySqlDbType.Int32, Restrict));
            parameters.Add(makeInputParameter("P_ON_SPECIAL", MySqlDbType.Int32, OnSpecial));
            parameters.Add(makeInputParameter("P_SPEC_LEVEL", MySqlDbType.Int32, SpecialLevelId));
            parameters.Add(makeInputParameter("P_SPEC_RENEW", MySqlDbType.VarChar, SpecialRenewalDate));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_LAST_LOGIN(Int64 customerID)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET LAST_LOGIN = CURRENT_TIMESTAMP WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_VERIFICATION_ID(string guid, Int64 customerID)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET VERIFY_ID = @P_GUID, VERIFY_TMST = CURRENT_TIMESTAMP WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_GUID", MySqlDbType.Int32, guid));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_ONLINE_STATUS(Int64 customerID, int isOnline)
        {
            const string SPName = "UPDATE TN_CUST_INFO SET CUST_ONLINE = @P_STATUS WHERE USER_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_STATUS", MySqlDbType.Int32, isOnline));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_PASSWORD(Int64 customerID, string newPassword)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET PASS_TXT = @P_PASSWORD WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int32, customerID));
            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, newPassword));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_DETAIL_INFO_BY_CUSTOMER_ID(long customerID, string phoneNumber, string birthdate)
        {
            const string SPName = "UPDATE TN_CUST_NEW_INFO SET CUST_DOB = @P_USER_BIRTH, CUST_PHONE = @P_USER_PHONE WHERE CUST_TAN_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_USER_BIRTH", MySqlDbType.VarChar, birthdate));
            parameters.Add(makeInputParameter("P_USER_PHONE", MySqlDbType.VarChar, phoneNumber));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_ONLINE_INFO_BY_CUSTOMER_ID(long customerID, string emailAddress, int specialsFlag)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET USER_MAIL = @P_USER_EMAIL, USER_SPECIAL = @P_SPECIALS WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_SPECIALS", MySqlDbType.Int32, specialsFlag));
            parameters.Add(makeInputParameter("P_USER_EMAIL", MySqlDbType.VarChar, emailAddress));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_AGREEMENT(string AgreementName, int Warned, long UserID)
        {
            const string SPName = "UPDATE TN_CUST_NEW_INFO SET CUST_WARN = @P_WARNED, CUST_WARN_TXT = @P_AGREEMENT_NAME WHERE CUST_TAN_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_WARNED", MySqlDbType.Int32, Warned));
            parameters.Add(makeInputParameter("P_AGREEMENT_NAME", MySqlDbType.VarChar, AgreementName));
            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, UserID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_CUSTOMER_VERIFICATION(Int64 customerID, int verificationStatus)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET VERIFY_IND = @P_VERIFY WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int32, customerID));
            parameters.Add(makeInputParameter("P_VERIFY", MySqlDbType.Int32, verificationStatus));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_TAN_BY_TAN_ID(int tanID, string tanBed, string tanDate, string tanTime, string tanLocation, int tanLength)
        {
            const string SPName = "UPDATE TN_TAN_LOG_INFO SET TAN_DATE = @P_DATE, TAN_TIME = @P_TIME, TAN_BED = @P_BED, TAN_LOC = @P_LOCATION, TAN_LENGTH = @P_LENGTH WHERE TAN_ID = @P_TAN_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TAN_ID", MySqlDbType.Int32, tanID));
            parameters.Add(makeInputParameter("P_LENGTH", MySqlDbType.Int32, tanLength));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, tanLocation));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, tanTime));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));
            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, tanBed));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_ADMIN_LOGIN(string passwordType, string newPassword)
        {
            const string SPName = "UPDATE TN_ADMN_DOMN SET PWD_TXT = @P_PASSWORD WHERE PWD_TYPE = @P_TYPE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, newPassword));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, passwordType));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_EMPLOYEE_BY_EMPLOYEE_ID(int employeeID, string firstName, string lastName)
        {
            const string SPName = "UPDATE TN_EMPL_DOMN SET EMPL_FNAME = @P_FIRST_NAME, EMPL_LNAME = @P_LAST_NAME WHERE EMPL_ID = @P_EMPL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_EMPLOYEE_PASSWORD_BY_EMPLOYEE_ID(int employeeID, string password)
        {
            const string SPName = "UPDATE TN_EMPL_DOMN SET EMPL_PWD = @P_PASSWORD WHERE EMPL_ID = @P_EMPL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, password));
            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_EMPLOYEE_LOGIN(int employeeID)
        {
            const string SPName = "UPDATE TN_EMPL_DOMN SET EMPL_LST_LGN = CURRENT_TIMESTAMP WHERE EMPL_ID = @P_EMPL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_EMPLOYEE_END_SHIFT(int shiftID, string endTime)
        {
            const string SPName = "UPDATE TN_EMPL_SHFT_XREF SET SHFT_END_HOUR = @P_END_TIME WHERE SHFT_ID = @P_SHIFT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHIFT_ID", MySqlDbType.Int32, shiftID));
            parameters.Add(makeInputParameter("P_END_TIME", MySqlDbType.VarChar, endTime));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_EMPLOYEE_SHIFT_BY_SHIFT_ID(int shiftID, string beginTimestamp, string endTimestamp)
        {
            const string SPName = "UPDATE TN_EMPL_SHFT_XREF SET SHFT_START_HOUR = @P_START, SHFT_END_HOUR = @P_END WHERE SHFT_ID = @P_SHIFT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHIFT_ID", MySqlDbType.Int32, shiftID));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, beginTimestamp));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endTimestamp));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_SITE_NOTIFY_BY_NOTIFY_ID(Int64 noticeID, string notificationText, string startDate, string endDate)
        {
            const string SPName = "UPDATE TN_SITE_NTFY_INFO SET NOTICE_TXT = @P_TEXT, NOTICE_START_DT = @P_START, NOTICE_END_DT = @P_END WHERE NOTICE_ID = @P_NOTICE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TEXT", MySqlDbType.VarChar, notificationText));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));
            parameters.Add(makeInputParameter("P_NOTICE_ID", MySqlDbType.Int32, noticeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_BILLING(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_CUST_HIST_INFO SET USER_ID = @P_TO WHERE USER_ID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_NOTES(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_CUST_NOTE_INFO SET USER_ID = @P_TO WHERE USER_ID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_TANS(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_TAN_LOG_INFO SET USER_ID = @P_TO WHERE USER_ID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_ONLINE_NEW(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_CUST_NEW_INFO SET CUST_TAN_ID = @P_TO WHERE CUST_TAN_ID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_ONLINE(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_CUST_ONLINE_INFO SET TAN_UID = @P_TO WHERE TAN_UID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_MAIN(string toCustomer)
        {
            const string SPName = "UPDATE TN_CUST_INFO SET CUST_ONLINE = '1', CUST_NEW_ONLINE = '1' WHERE USER_ID IN (@P_TO)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MASSAGE_BY_MASSAGE_ID(int massageID, string appointmentDate, string appointmentTime, int appointmentLength)
        {
            const string SPName = "UPDATE MASSAGE_LOG SET MASSAGE_LENGTH = @P_LENGTH, MASSAGE_DATE = @P_DATE, MASSAGE_TIME = @P_TIME WHERE MASSAGE_ID = @P_MASS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_MASS_ID", MySqlDbType.VarChar, massageID));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, appointmentDate));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, appointmentTime));
            parameters.Add(makeInputParameter("P_LENGTH", MySqlDbType.Int32, appointmentLength));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MASSAGE_BY_MASSAGE_ID(string password, string passwordType)
        {
            const string SPName = "UPDATE TN_ADMN_DOMN SET PWD_TXT = @P_PASSWORD WHERE PWD_TYPE = @P_TYPE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, password));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, passwordType));

            return modifyData(parameters, SPName);
        }
        #endregion

        #region INSERT
        public bool ExecuteINSERT_CUSTOMER_ONLINE(Int64 UserID, string UserName, string Password, string Email, int Specials)
        {
            const string SPName = "INSERT INTO TN_CUST_ONLINE_INFO (TAN_UID, USER_NAME, PASS_TXT, USER_MAIL, USER_SPECIAL) VALUES (@P_USER_ID,@P_USER_NAME,@P_PASSWORD,@P_EMAIL,@P_SPECIALS)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, UserID));
            parameters.Add(makeInputParameter("P_USER_NAME", MySqlDbType.VarChar, UserName));
            parameters.Add(makeInputParameter("P_PASSWORD", MySqlDbType.VarChar, Password));
            parameters.Add(makeInputParameter("P_EMAIL", MySqlDbType.VarChar, Email));
            parameters.Add(makeInputParameter("P_SPECIALS", MySqlDbType.VarChar, Specials));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_CUSTOMER_NOTE(Int64 userID, string noteText, int owesMoney, int owedProduct, int checkTransactions)
        {
            const string SPName = "INSERT INTO TN_CUST_NOTE_INFO (NOTE_TXT, NOTE_OWES, NOTE_OWED, NOTE_CHECK, USER_ID, NOTE_DISP) VALUES (@P_NOTE_TEXT, @P_NOTE_OWES, @P_NOTE_OWED, @P_NOTE_CHECK, @P_USER_ID, '1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NOTE_TEXT", MySqlDbType.VarChar, noteText));
            parameters.Add(makeInputParameter("P_NOTE_OWES", MySqlDbType.Int32, owesMoney));
            parameters.Add(makeInputParameter("P_NOTE_OWED", MySqlDbType.Int32, owedProduct));
            parameters.Add(makeInputParameter("P_NOTE_CHECK", MySqlDbType.Int32, checkTransactions));
            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, userID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_CUSTOMER_HISTORY(long customerId, string purchaseDate, string renewalDate, string packageName, long transactionId)
        {
            const string SPName = "INSERT INTO TN_CUST_HIST_INFO (USER_ID, TRNS_ID, TAN_PACKAGE, TAN_RENEWAL, TAN_PURCHASE) VALUES " +
                "(@P_USER_ID, @P_TRNS_ID, @P_PACKAGE, @P_RENEWAL, @P_PURCHASE);";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerId));
            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.VarChar, transactionId));
            parameters.Add(makeInputParameter("P_PACKAGE", MySqlDbType.VarChar, packageName));
            parameters.Add(makeInputParameter("P_RENEWAL", MySqlDbType.VarChar, renewalDate));
            parameters.Add(makeInputParameter("P_PURCHASE", MySqlDbType.VarChar, purchaseDate));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_PACKAGE(string shortName, string longName, string bedType, int packageLength, Int64 productID)
        {
            const string SPName = "INSERT INTO TN_PLAN_DOMN (PLAN_SHORT, PLAN_LONG, BED_TYPE, PLAN_LENGTH, PROD_ID, PLAN_ACTIVE) VALUES (@P_SHORT_NAME, @P_LONG_NAME, @P_BED, @P_LENGTH, @P_PRODUCT_ID, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHORT_NAME", MySqlDbType.VarChar, shortName));
            parameters.Add(makeInputParameter("P_LONG_NAME", MySqlDbType.VarChar, longName));
            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, bedType));
            parameters.Add(makeInputParameter("P_LENGTH", MySqlDbType.VarChar, packageLength));
            parameters.Add(makeInputParameter("P_PRODUCT_ID", MySqlDbType.Int64, productID));

            return returnLastInsert(parameters, SPName);
        }

        public long ExecuteINSERT_SPECIAL(string specialName, string specialShortName, Int64 productID)
        {
            const string SPName = "INSERT INTO TN_SPEC_DOMN (SPEC_NME, SPEC_SHORT_NME, PROD_ID, SPEC_ACTV) VALUES (@P_LONG_NAME, @P_SHORT_NAME, @P_PRODUCT_ID, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHORT_NAME", MySqlDbType.VarChar, specialShortName));
            parameters.Add(makeInputParameter("P_LONG_NAME", MySqlDbType.VarChar, specialName));
            parameters.Add(makeInputParameter("P_PRODUCT_ID", MySqlDbType.Int64, productID));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_SPECIAL_LEVEL(Int64 specialID, string specialBed, int specialLength, int specialOrder)
        {
            const string SPName = "INSERT INTO TN_SPEC_INFO (SPEC_ID, SPEC_BED, SPEC_LENGTH, SPEC_ORDER) VALUES (@P_SPECIAL_ID, @P_BED, @P_LENGTH, @P_ORDER)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, specialBed));
            parameters.Add(makeInputParameter("P_LENGTH", MySqlDbType.Int32, specialLength));
            parameters.Add(makeInputParameter("P_ORDER", MySqlDbType.Int32, specialOrder));
            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int64, specialID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_BED(string bedDescription, string shortDescription, string bedLocation, string bedType, int bedDisplayInternal, int bedDisplayExternal)
        {
            const string SPName = "INSERT INTO TN_TAN_BED_DOMN (BED_LONG, BED_SHORT, BED_LOC, BED_TYPE, BED_DISP_INT, BED_DISP_EXT, BED_ACTV) VALUES " + 
                "(@P_BED_DESC, @P_BED_SHORT, @P_BED_LOC, @P_BED_TYPE, @P_BED_DISP_INT, @P_BED_DISP_EXT, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_DESC", MySqlDbType.VarChar, bedDescription));
            parameters.Add(makeInputParameter("P_BED_SHORT", MySqlDbType.VarChar, shortDescription));
            parameters.Add(makeInputParameter("P_BED_LOC", MySqlDbType.VarChar, bedLocation));
            parameters.Add(makeInputParameter("P_BED_TYPE", MySqlDbType.VarChar, bedType));
            parameters.Add(makeInputParameter("P_BED_DISP_INT", MySqlDbType.Int32, bedDisplayInternal));
            parameters.Add(makeInputParameter("P_BED_DISP_EXT", MySqlDbType.Int32, bedDisplayExternal));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_TAN(long customerID, string tanBed, string tanDate, string tanTime, string tanLocation, int tanStore, int tanReminder)
        {
            const string SPName = "INSERT INTO TN_TAN_LOG_INFO (USER_ID, TAN_DATE, TAN_TIME, TAN_BED, TAN_LOC, TAN_LENGTH, TAN_ONLINE, TAN_REMIND) "
            + "VALUES (@P_USER_ID, @P_DATE, @P_TIME, @P_BED, @P_LOCATION, '0', @P_ONLINE, @P_REMINDER)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, tanLocation));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, tanTime));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, tanDate));
            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, tanBed));
            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));
            parameters.Add(makeInputParameter("P_REMINDER", MySqlDbType.Int32, tanReminder));
            parameters.Add(makeInputParameter("P_ONLINE", MySqlDbType.Int32, tanStore));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_CUSTOMER(string FirstName, string LastName, string JoinDate, int FitzNumber, string Plan, string RenewalDate, string Remark, 
            int OnlineUser, int NewOnline, int SpecialFlag, int SpecialId, string SpecialDate)
        {
            const string SPName = "INSERT INTO TN_CUST_INFO (CUST_LNAME, CUST_FNAME, CUST_JOIN, CUST_RENEWAL, CUST_PLAN, CUST_REMARK, CUST_FPS, "
            + "CUST_ONLINE, CUST_NEW_ONLINE, SPECIAL_FLAG, SPECIAL_ID, SPECIAL_DATE) "
            + "VALUES (@P_LAST_NAME, @P_FIRST_NAME, @P_JOIN_DATE, @P_RENEWAL_DATE, @P_PLAN, @P_REMARK, @P_FITZ, @P_ONLINE, @P_NEW_ONLINE, "
            + "@P_SPECIAL_FLAG, @P_SPECIAL_ID, @P_SPECIAL_DATE)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, LastName));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, FirstName));
            parameters.Add(makeInputParameter("P_JOIN_DATE", MySqlDbType.VarChar, JoinDate));
            parameters.Add(makeInputParameter("P_RENEWAL_DATE", MySqlDbType.VarChar, RenewalDate));
            parameters.Add(makeInputParameter("P_FITZ", MySqlDbType.Int32, FitzNumber));
            parameters.Add(makeInputParameter("P_PLAN", MySqlDbType.VarChar, Plan));
            parameters.Add(makeInputParameter("P_REMARK", MySqlDbType.VarChar, Remark));
            parameters.Add(makeInputParameter("P_ONLINE", MySqlDbType.VarChar, OnlineUser));
            parameters.Add(makeInputParameter("P_NEW_ONLINE", MySqlDbType.VarChar, NewOnline));
            parameters.Add(makeInputParameter("P_SPECIAL_FLAG", MySqlDbType.VarChar, SpecialFlag));
            parameters.Add(makeInputParameter("P_SPECIAL_ID", MySqlDbType.Int32, SpecialId));
            parameters.Add(makeInputParameter("P_SPECIAL_DATE", MySqlDbType.VarChar, SpecialDate));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_CUSTOMER_NEW(string FirstName, string LastName, string Address, string City, string State, string ZipCode, string PhoneNumber, 
            string DateOfBirth, int FitzNumber, int FamilyHistory, int SelfHistory, int WarningChecked, string WarningSignature, Int64 TanID)
        {
            const string SPName = "INSERT INTO TN_CUST_NEW_INFO (CUST_LNAME, CUST_FNAME, CUST_DOB, CUST_ADDR, CUST_CITY, CUST_ST, CUST_PHONE, CUST_ZIP, CUST_FHIST, "
            + "CUST_HIST, CUST_FITZ, CUST_WARN, CUST_WARN_TXT, CUST_TAN_ID) "
            + "VALUES (@P_LAST_NAME, @P_FIRST_NAME, @P_DATE_OF_BIRTH, @P_ADDRESS, @P_CITY, @P_STATE, @P_PHONE, @P_ZIP, @P_FAM_HIST, @P_SELF_HIST, @P_FITZ, @P_WARN_CHECK, @P_WARN_SIGN, @P_USER_ID)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, LastName));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, FirstName));
            parameters.Add(makeInputParameter("P_DATE_OF_BIRTH", MySqlDbType.VarChar, DateOfBirth));
            parameters.Add(makeInputParameter("P_ADDRESS", MySqlDbType.VarChar, Address));
            parameters.Add(makeInputParameter("P_CITY", MySqlDbType.VarChar, City));
            parameters.Add(makeInputParameter("P_STATE", MySqlDbType.VarChar, State));
            parameters.Add(makeInputParameter("P_PHONE", MySqlDbType.VarChar, PhoneNumber));
            parameters.Add(makeInputParameter("P_ZIP", MySqlDbType.VarChar, ZipCode));
            parameters.Add(makeInputParameter("P_FAM_HIST", MySqlDbType.Int32, FamilyHistory));
            parameters.Add(makeInputParameter("P_SELF_HIST", MySqlDbType.Int32, SelfHistory));
            parameters.Add(makeInputParameter("P_FITZ", MySqlDbType.Int32, FitzNumber));
            parameters.Add(makeInputParameter("P_WARN_CHECK", MySqlDbType.Int32, WarningChecked));
            parameters.Add(makeInputParameter("P_WARN_SIGN", MySqlDbType.VarChar, WarningSignature));
            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, TanID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_EMPLOYEE(int employeeID, string firstName, string lastName)
        {
            const string SPName = "INSERT INTO TN_EMPL_DOMN (EMPL_ID, EMPL_FNAME, EMPL_LNAME, EMPL_DISP) VALUES (@P_EMPL_ID, @P_FIRST_NAME, @P_LAST_NAME, 1)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_LAST_NAME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_FIRST_NAME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_SITE_NOTIFY(string notificationText, string startDate, string endDate)
        {
            const string SPName = "INSERT INTO TN_SITE_NTFY_INFO (NOTICE_TXT, NOTICE_START_DT, NOTICE_END_DT) VALUES (@P_TEXT, @P_START, @P_END)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TEXT", MySqlDbType.VarChar, notificationText));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_SITE_COMMENT(string commentEmail, string commentName, string commentAbout, string commentText)
        {
            const string SPName = "INSERT INTO TN_SITE_CMTS_INFO (COMMENT_EMAIL, COMMENT_NAME, COMMENT_ABOUT, COMMENT_TXT) VALUES (@P_EMAIL, @P_NAME, @P_ABOUT, @P_COMMENT)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMAIL", MySqlDbType.VarChar, commentEmail));
            parameters.Add(makeInputParameter("P_NAME", MySqlDbType.VarChar, commentName));
            parameters.Add(makeInputParameter("P_ABOUT", MySqlDbType.VarChar, commentAbout));
            parameters.Add(makeInputParameter("P_COMMENT", MySqlDbType.VarChar, commentText));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_EMPLOYEE_START_SHIFT(int employeeID, string shiftDate, string startTime)
        {
            const string SPName = "INSERT INTO TN_EMPL_SHFT_XREF (EMPL_ID, SHFT_START_HOUR, SHFT_END_HOUR, SHFT_DATE) VALUES (@P_EMPL_ID, @P_START_TIME, '00:00:00', @P_DATE)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, shiftDate));
            parameters.Add(makeInputParameter("P_START_TIME", MySqlDbType.VarChar, startTime));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_EMPLOYEE_NOTE(string noteText, string noteTo, string noteFrom)
        {
            const string SPName = "INSERT INTO TN_EMPL_NOTE_INFO (NOTE_TO, NOTE_FROM, NOTE_TXT, NOTE_DISP) VALUES (@P_TO, @P_FROM, @P_TEXT, '1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, noteTo));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, noteFrom));
            parameters.Add(makeInputParameter("P_TEXT", MySqlDbType.VarChar, noteText));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_EMPLOYEE_SHIFT(int employeeID, string shiftDate, string beginTimestamp, string endTimestamp)
        {
            const string SPName = "INSERT INTO TN_EMPL_SHFT_XREF (SHFT_START_HOUR, SHFT_END_HOUR, SHFT_DATE, EMPL_ID) VALUES (@P_SHIFT_START, @P_SHIFT_END, @P_SHIFT_DATE, @P_EMPL_ID)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SHIFT_START", MySqlDbType.VarChar, beginTimestamp));
            parameters.Add(makeInputParameter("P_SHIFT_END", MySqlDbType.VarChar, endTimestamp));
            parameters.Add(makeInputParameter("P_SHIFT_DATE", MySqlDbType.VarChar, shiftDate));
            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.VarChar, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_MASSAGE(Int64 customerID, string appointmentDate, string appointmentTime, int appointmentLength)
        {
            const string SPName = "INSERT INTO MASSAGE_LOG (MASSAGE_DATE, MASSAGE_TIME, MASSAGE_LENGTH, USER_ID, ACTV_IND) VALUES (@P_DATE, @P_TIME, @P_LENGTH, @P_USER_ID, '1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, appointmentDate));
            parameters.Add(makeInputParameter("P_TIME", MySqlDbType.VarChar, appointmentTime));
            parameters.Add(makeInputParameter("P_LENGTH", MySqlDbType.Int32, appointmentLength));
            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, customerID));

            return modifyData(parameters, SPName);
        }
        #endregion

        #region DELETE
        public bool ExecuteDELETE_NOTE_BY_NOTE_ID(int noteID)
        {
            const string SPName = "UPDATE TN_CUST_NOTE_INFO SET NOTE_DISP = 0 WHERE NOTE_ID = @P_NOTE_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NOTE_ID", MySqlDbType.Int32, noteID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_NOTES_BY_CUSTOMER_ID(long customerId)
        {
            const string SPName = "DELETE FROM TN_CUST_NOTE_INFO WHERE USER_ID = @P_CUST_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CUST_ID", MySqlDbType.Int64, customerId));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_CUSTOMER_NEW_ONLINE(Int64 userID)
        {
            const string SPName = "DELETE FROM TN_CUST_NEW_INFO WHERE CUST_TAN_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, userID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_CUSTOMER_ONLINE(Int64 userID)
        {
            const string SPName = "DELETE FROM TN_CUST_ONLINE_INFO WHERE TAN_UID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, userID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_CUSTOMER(Int64 userID)
        {
            const string SPName = "UPDATE TN_CUST_INFO SET CUST_ACTIVE = '0' WHERE USER_ID = @P_USER_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int64, userID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_TAN_BY_TAN_ID(Int32 tanID)
        {
            const string SPName = "UPDATE TN_TAN_LOG_INFO SET ACTV_IND = '0' WHERE TAN_ID = @P_TAN_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TAN_ID", MySqlDbType.Int32, tanID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_EMPLOYEE_BY_EMPLOYEE_ID(int employeeID)
        {
            const string SPName = "UPDATE TN_EMPL_DOMN SET EMPL_DISP = '0' WHERE EMPL_ID = @P_EMPL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_CUSTOMER(string customerID)
        {
            const string SPName = "UPDATE TN_CUST_INFO SET CUST_ACTIVE = '0' WHERE USER_ID IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.Int32, customerID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_MASSAGE(int massageID)
        {
            const string SPName = "UPDATE MASSAGE_LOG SET ACTV_IND = '0' WHERE MASSAGE_ID = @P_MASS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_MASS_ID", MySqlDbType.Int32, massageID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_BED_BY_BED_ID(int bedID)
        {
            const string SPName = "UPDATE TN_TAN_BED_DOMN SET BED_ACTV = 0 WHERE BED_ID = @P_BED_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED_ID", MySqlDbType.Int32, bedID));

            return modifyData(parameters, SPName);
        }

        #endregion
        #endregion
    }

    public class ProdSQL : SqlConnector
    {
        private static string pointOfSaleConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTProd"].ConnectionString;
        private static string pointOfSaleConnectionStringKey = "HOTProd";
        
        public ProdSQL(String connectionStringKey) : base(pointOfSaleConnectionStringKey) { }

        #region SQL Calls
        #region SELECT
        public DataTable ExecuteALL_ACTIVE_PRODUCTS()
        {
            const string SPName = "SELECT PROD_FILE_NAME, PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, PROD_DESC, PROD_COUNT, " +
                "PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV B ON A.PROD_ID = B.PROD_ID WHERE PROD_DISP = 1 " + 
                "AND B.PROD_LOC = 'W' ORDER BY PROD_TYPE, PROD_SUB_TYPE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePRODUCT_BY_PRODUCT_SUB_TYPE_ACTIVE_ONLINE(string productSubType)
        {
            const string SPName = "SELECT DISTINCT PROD_FILE_NAME, PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, C.PROD_LOC, PROD_DESC, " + 
                "PROD_COUNT, PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV C ON C.PROD_ID = A.PROD_ID WHERE PROD_TYPE = 'ACC' " + 
                "AND PROD_SUB_TYPE = @P_PROD_SUB_TYPE AND PROD_DISP = 1 AND PROD_DISP_ONLINE = 1 AND C.PROD_LOC = 'W' ORDER BY PROD_NAME ASC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_SUB_TYPE", MySqlDbType.VarChar, productSubType));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteLOTIONS_BY_PRODUCT_SUB_TYPE_ACTIVE_ONLINE(string productSubType)
        {
            const string SPName = "SELECT DISTINCT PROD_FILE_NAME, PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, A.PROD_LOC, PROD_DESC, " + 
                "PROD_COUNT, PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV C ON C.PROD_ID = A.PROD_ID WHERE PROD_TYPE = 'LTN' " + 
                "AND PROD_SUB_TYPE = @P_PROD_SUB_TYPE AND PROD_DISP = 1 AND PROD_DISP_ONLINE = 1 AND C.PROD_LOC IN ('B', 'W') ORDER BY PROD_NAME ASC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_SUB_TYPE", MySqlDbType.VarChar, productSubType));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePRODUCT_BY_BARCODE(string barCode)
        {
            const string SPName = "SELECT PROD_FILE_NAME, PROD_NAME, PROD_SALE_STORE, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, C.PROD_LOC, " + 
                "PROD_DESC, PROD_COUNT, PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX, A.PROD_DISP FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV C ON " + 
                "A.PROD_ID = C.PROD_ID WHERE A.PROD_DISP = 1 AND PROD_CODE = @P_PROD_BARCODE";
            
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_BARCODE", MySqlDbType.VarChar, barCode));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePRODUCT_BY_PRODUCT_ID(int productID)
        {
            const string SPName = "SELECT PROD_FILE_NAME, PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, C.PROD_LOC, PROD_DESC, PROD_COUNT, " +
                "PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX, PROD_SALE_STORE FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV C ON A.PROD_ID = C.PROD_ID " +
                "WHERE A.PROD_ID = @P_PRODUCT_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PRODUCT_ID", MySqlDbType.Int32, productID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePRODUCT_BY_PRODUCT_NAME(string productName)
        {
            const string SPName = "SELECT PROD_FILE_NAME, PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_PRICE, A.PROD_ID, C.PROD_LOC, PROD_DESC, PROD_COUNT, " +
                "PROD_FILE_NAME, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_TAX, PROD_SALE_STORE FROM TN_PROD_DOMN A INNER JOIN TN_PROD_INV C ON A.PROD_ID = C.PROD_ID " +
                "WHERE PROD_NAME LIKE @P_PRODUCT_NAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PRODUCT_NAME", MySqlDbType.VarChar, productName + "%"));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecutePLANS_BY_TYPE(string planBed)
        {
            const string SPName = "SELECT DISTINCT PROD_NAME, PROD_SALE_ONLINE, PROD_SALE_PRICE, PROD_DISP_ONLINE, PROD_PRICE, PROD_ID, PROD_SUB_TYPE, PROD_FILE_NAME, PROD_DISP_STORE FROM TN_PROD_DOMN "
            + "WHERE PROD_SUB_TYPE = @P_BED AND PROD_NAME NOT LIKE '%upgrade%' AND PROD_NAME NOT LIKE '%Single Tan%' "
            + "AND PROD_TYPE ='PKG' AND PROD_DISP = 1 AND PROD_DISP_ONLINE = 1 ORDER BY PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BED", MySqlDbType.VarChar, planBed));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTRANSACTION_BY_TRANSACTION_ID(int transactionID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_DATE, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH FROM TN_TRNS_DOMN " + 
                "WHERE TRNS_ID = @P_TRNS_ID ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTRANSACTION_ITEMS_BY_TRANSACTION_ID(int transactionID)
        {
            const string SPName = "SELECT XREF_ID, TRNS_ID, PROD_ID, PROD_QTY, PROD_NME, PROD_PRICE, PROD_TAX FROM TN_TRNS_XREF WHERE TRNS_ID = @P_TRNS_ID ORDER BY PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTRANSACTIONS_BY_CUSTOMER_ID(Int64 customerID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_DATE, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH FROM TN_TRNS_DOMN " +
                "WHERE TRNS_BGHT = @P_USER_ID ORDER BY TRNS_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int32, customerID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTRANSACTION_BY_BUYER_TOTAL_DATE(string Purchaser, string Date, string Total)
        {
            const string SPName = "SELECT TRNS_ID FROM TN_TRNS_DOMN WHERE TRNS_BGHT = @P_PURCHASER AND TRNS_TTL = @P_TOTAL AND TRNS_DATE = @P_DATE AND TRNS_VOID = 0";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PURCHASER", MySqlDbType.VarChar, Purchaser));
            parameters.Add(makeInputParameter("P_TOTAL", MySqlDbType.VarChar, Date));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, Total));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteTRANSACTION_BY_DATE_LOCATION(string TransactionDate, string Location)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID " +
                "FROM TN_TRNS_DOMN WHERE TRNS_DATE = @P_DATE AND TRNS_LOC = @P_LOCATION ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, TransactionDate));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, Location));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSALES_TOTALS_BY_EMPLOYEE_ID(int employeeID, string startDate, string endDate)
        {
            const string SPName = "SELECT TRNS_SELL, SUM(TRNS_TTL) As TotalSales, TRNS_DATE FROM TN_TRNS_DOMN WHERE TRNS_SELL = @P_EMPL_ID AND TRNS_VOID = 0 " +
                "AND TRNS_PAID = 1 AND TRNS_DATE >= @P_START AND TRNS_DATE <= @P_END GROUP BY TRNS_DATE ORDER BY TRNS_DATE DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));

            return getDataSet(parameters, SPName);
        }


        public DataTable ExecuteTRANSACTIONS_BY_EMPLOYEE_ID(int employeeID, string startDate, string endDate)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID FROM TN_TRNS_DOMN " + 
                "WHERE TRNS_SELL = @P_EMPL_ID AND TRNS_DATE >= @P_START AND TRNS_DATE <= @P_END ORDER BY TRNS_ID ASC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.Int32, employeeID));
            parameters.Add(makeInputParameter("P_START", MySqlDbType.VarChar, startDate));
            parameters.Add(makeInputParameter("P_END", MySqlDbType.VarChar, endDate));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_PRODUCT_BY_PRODUCT_ID(Int64 productID, string productName, string productDescription, string productType, string productSubType, string productBarcode,
            string productPrice, int productTax, string salePrice, int onSaleOnline, int onSaleInStore, int availableOnline, int availableInStore)
        {
            const string SPName = "UPDATE TN_PROD_DOMN SET PROD_NAME = @P_PROD_NAME, PROD_PRICE = @P_PROD_PRICE, PROD_TYPE = @P_PROD_TYPE, PROD_SUB_TYPE = @P_PROD_SUB_TYPE, "
            + "PROD_DESC = @P_PROD_DESC, PROD_SALE_PRICE = @P_PROD_SALE_PRICE, PROD_CODE = @P_PROD_BARCODE, PROD_LOC = 'W', PROD_TAX = @P_PROD_TAXED, PROD_DISP_STORE = @P_PROD_DISP_STORE, "
            + "PROD_DISP_ONLINE = @P_PROD_DISP_ONLINE, PROD_SALE_ONLINE = @P_PROD_SALE_ONLINE, PROD_SALE_STORE = @P_PROD_SALE_STORE WHERE PROD_ID = @P_PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_NAME", MySqlDbType.VarChar, productName));
            parameters.Add(makeInputParameter("P_PROD_PRICE", MySqlDbType.VarChar, productPrice));
            parameters.Add(makeInputParameter("P_PROD_TYPE", MySqlDbType.VarChar, productType));
            parameters.Add(makeInputParameter("P_PROD_SUB_TYPE", MySqlDbType.VarChar, productSubType));
            parameters.Add(makeInputParameter("P_PROD_DESC", MySqlDbType.VarChar, productDescription));
            parameters.Add(makeInputParameter("P_PROD_SALE_PRICE", MySqlDbType.VarChar, salePrice));
            parameters.Add(makeInputParameter("P_PROD_BARCODE", MySqlDbType.VarChar, productBarcode));
            parameters.Add(makeInputParameter("P_PROD_TAXED", MySqlDbType.Int32, productTax));
            parameters.Add(makeInputParameter("P_PROD_DISP_STORE", MySqlDbType.Int32, availableInStore));
            parameters.Add(makeInputParameter("P_PROD_DISP_ONLINE", MySqlDbType.Int32, availableOnline));
            parameters.Add(makeInputParameter("P_PROD_SALE_ONLINE", MySqlDbType.Int32, onSaleOnline));
            parameters.Add(makeInputParameter("P_PROD_SALE_STORE", MySqlDbType.Int32, onSaleInStore));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.VarChar, productID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_PRODUCT_DISPLAY_BY_PRODUCT_ID(Int64 productID, string productDisplay)
        {
            const string SPName = "UPDATE TN_PROD_DOMN SET PROD_DISP = @P_PROD_DISP WHERE PROD_ID = @P_PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_DISP", MySqlDbType.VarChar, productDisplay));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int64, productID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_PRODUCT_INVENTORY(Int64 productID, int itemCount, string location)
        {
            const string SPName = "UPDATE TN_PROD_INV SET PROD_COUNT = @P_PROD_COUNT, PROD_UPDT = CURRENT_TIMESTAMP WHERE PROD_ID = @P_PROD_ID AND PROD_LOC = @P_PROD_LOCATION";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_PROD_COUNT", MySqlDbType.Int32, itemCount));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, productID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_TRANSACTION_BY_TRANSACTION_ID(int transactionID, string seller, string total, string date, string payment, string isVoid, string isPaid)
        {
            const string SPName = "UPDATE TN_TRNS_DOMN SET TRNS_SELL = @P_SELLER, TRNS_TTL = @P_TOTAL, TRNS_DATE = @P_DATE, TRNS_PYMT = @P_PYMT, TRNS_VOID = @P_VOID, TRNS_PAID = @P_PAID WHERE TRNS_ID = @P_TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SELLER", MySqlDbType.VarChar, seller));
            parameters.Add(makeInputParameter("P_TOTAL", MySqlDbType.VarChar, total));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));
            parameters.Add(makeInputParameter("P_PYMT", MySqlDbType.VarChar, payment));
            parameters.Add(makeInputParameter("P_VOID", MySqlDbType.VarChar, isVoid));
            parameters.Add(makeInputParameter("P_PAID", MySqlDbType.VarChar, isPaid));
            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int32, transactionID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_MERGE_TRANSACTIONS(string toCustomer, string fromCustomer)
        {
            const string SPName = "UPDATE TN_TRNS_DOMN SET TRNS_BGHT = @P_TO WHERE TRNS_BGHT IN (@P_FROM)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TO", MySqlDbType.VarChar, toCustomer));
            parameters.Add(makeInputParameter("P_FROM", MySqlDbType.VarChar, fromCustomer));

            return modifyData(parameters, SPName);
        }
        #endregion

        #region INSERT
        public long ExecuteINSERT_PRODUCT(string productName, string productDescription, string productType, string productSubType, string productBarcode,
            string productPrice, int productTax, string salePrice, int onSaleOnline, int onSaleInStore, int availableOnline, int availableInStore)
        {
            const string SPName = "INSERT INTO TN_PROD_DOMN (PROD_NAME, PROD_PRICE, PROD_TYPE, PROD_SUB_TYPE, PROD_DESC, " +
                "PROD_SALE_PRICE, PROD_CODE, PROD_LOC, PROD_TAX, PROD_DISP_ONLINE, PROD_DISP_STORE, PROD_SALE_ONLINE, PROD_SALE_STORE, PROD_DISP) " + 
                "VALUES (@P_PROD_NAME, @P_PROD_PRICE, @P_PROD_TYPE, @P_PROD_SUB_TYPE, @P_PROD_DESC, @P_PROD_SALE_PRICE, " +
                "@P_PROD_BARCODE, 'W', @P_PROD_TAXED, @P_PROD_DISP_ONLINE, @P_PROD_DISP_STORE, @P_PROD_SALE_ONLINE, @P_PROD_SALE_STORE, '1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_NAME", MySqlDbType.VarChar, productName));
            parameters.Add(makeInputParameter("P_PROD_PRICE", MySqlDbType.VarChar, productPrice));
            parameters.Add(makeInputParameter("P_PROD_TYPE", MySqlDbType.VarChar, productType));
            parameters.Add(makeInputParameter("P_PROD_SUB_TYPE", MySqlDbType.VarChar, productSubType));
            parameters.Add(makeInputParameter("P_PROD_DESC", MySqlDbType.VarChar, productDescription));
            parameters.Add(makeInputParameter("P_PROD_SALE_PRICE", MySqlDbType.VarChar, salePrice));
            parameters.Add(makeInputParameter("P_PROD_BARCODE", MySqlDbType.VarChar, productBarcode));
            parameters.Add(makeInputParameter("P_PROD_TAXED", MySqlDbType.Int32, productTax));
            parameters.Add(makeInputParameter("P_PROD_DISP_ONLINE", MySqlDbType.Int32, availableOnline));
            parameters.Add(makeInputParameter("P_PROD_DISP_STORE", MySqlDbType.Int32, availableInStore));
            parameters.Add(makeInputParameter("P_PROD_SALE_ONLINE", MySqlDbType.Int32, onSaleOnline));
            parameters.Add(makeInputParameter("P_PROD_SALE_STORE", MySqlDbType.Int32, onSaleInStore));

            return returnLastInsert(parameters, SPName);
        }

        public long ExecuteINSERT_TRANSACTION(string Seller, string CartTotal, Int64 Purchaser, string Location, string PaymentType, string Date, string Tax, string PaidIndator, string OtherInfo)
        {
            const string SPName = "INSERT INTO TN_TRNS_DOMN (TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_DATE, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH) " + 
                "VALUES (@P_SELLER, @P_TOTAL, @P_PURCHASER, @P_LOCATION, @P_PAYMENT_TYPE, @P_DATE, @P_TAX, '0', @P_PAID_IND, @P_OTHER)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SELLER", MySqlDbType.VarChar, Seller));
            parameters.Add(makeInputParameter("P_TOTAL", MySqlDbType.VarChar, CartTotal));
            parameters.Add(makeInputParameter("P_PURCHASER", MySqlDbType.VarChar, Purchaser.ToString()));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, Location));
            parameters.Add(makeInputParameter("P_PAYMENT_TYPE", MySqlDbType.VarChar, PaymentType));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, Date));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.VarChar, Tax));
            parameters.Add(makeInputParameter("P_PAID_IND", MySqlDbType.VarChar, PaidIndator));
            parameters.Add(makeInputParameter("P_OTHER", MySqlDbType.VarChar, OtherInfo));

            return returnLastInsert(parameters, SPName);
        }

        public bool ExecuteINSERT_TRANSACTION_ITEM(Int64 transactionID, int itemID, int itemQuantity, string itemName, double itemPrice, int itemTaxes)
        {
            const string SPName = "INSERT INTO TN_TRNS_XREF (TRNS_ID, PROD_ID, PROD_QTY, PROD_NME, PROD_PRICE, PROD_TAX) VALUES (@P_TRNS_ID,@P_ITEM_ID,@P_ITEM_QTY"
            + ",@P_ITEM_NAME,@P_ITEM_PRICE,@P_ITEM_TAX)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ITEM_TAX", MySqlDbType.Int32, itemTaxes));
            parameters.Add(makeInputParameter("P_ITEM_PRICE", MySqlDbType.VarChar, itemPrice.ToString()));
            parameters.Add(makeInputParameter("P_ITEM_NAME", MySqlDbType.VarChar, itemName));
            parameters.Add(makeInputParameter("P_ITEM_QTY", MySqlDbType.Int32, itemQuantity));
            parameters.Add(makeInputParameter("P_ITEM_ID", MySqlDbType.Int32, itemID));
            parameters.Add(makeInputParameter("P_TRNS_ID", MySqlDbType.Int64, transactionID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_PRODUCT_INVENTORY(Int64 productID, int itemCount, string location)
        {
            const string SPName = "INSERT INTO TN_PROD_INV (PROD_ID, PROD_COUNT, PROD_LOC) VALUES (@P_PROD_ID, @P_PROD_COUNT, @P_PROD_LOCATION)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_LOCATION", MySqlDbType.VarChar, location));
            parameters.Add(makeInputParameter("P_PROD_COUNT", MySqlDbType.Int32, itemCount));
            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, productID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteINSERT_GIFT_CARD(Int64 customerID, string from, int employeeID, string amount, string boughtDate, string description)
        {
            const string SPName = "INSERT INTO TN_PROD_GIFT_INFO (USER_ID, CARD_BGHT_BY, CARD_SOLD_BY, CARD_AMT, CARD_DATE, CARD_DESC, CARD_USED) VALUES (@P_USER_ID, @P_BGHT, @P_EMPL_ID, @P_AMT, @P_DATE, @P_DESC, '1')";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_USER_ID", MySqlDbType.Int32, customerID));
            parameters.Add(makeInputParameter("P_BGHT", MySqlDbType.VarChar, from));
            parameters.Add(makeInputParameter("P_EMPL_ID", MySqlDbType.VarChar, employeeID));
            parameters.Add(makeInputParameter("P_AMT", MySqlDbType.VarChar, amount));
            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, boughtDate));
            parameters.Add(makeInputParameter("P_DESC", MySqlDbType.VarChar, description));

            return modifyData(parameters, SPName);
        }
        
        #endregion

        #region DELETE
        public bool ExecuteDELETE_TRANSACTION_ITEM_BY_ITEM_ID(Int32 TransactionItemID)
        {
            const string SPName = "DELETE FROM TN_TRNS_XREF WHERE XREF_ID = @P_TAN_ITEM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TAN_ITEM_ID", MySqlDbType.Int32, TransactionItemID));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_TRANSACTION_ITEMS_BY_TRANSACTION_ID(int transactionId)
        {
            const string SPName = "DELETE FROM TN_TRNS_XREF WHERE TRNS_ID = @P_TRANS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_TRANS_ID", MySqlDbType.Int32, transactionId));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteDELETE_TRANSACTIONS_BY_CUSTOMER_ID(long customerId)
        {
            const string SPName = "DELETE FROM TN_TRNS_DOMN WHERE TRNS_BGHT = @P_CUST_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_CUST_ID", MySqlDbType.Int64, customerId));

            return modifyData(parameters, SPName);
        }
        
        #endregion
        #endregion

        public Int64 GetLastValue()
        {
            object returnScalar;
            Int64 returnValue = 0;
            MySqlConnection valConnection = new MySqlConnection(pointOfSaleConnectionString);
            MySqlCommand valCommand = valConnection.CreateCommand();
            valCommand.CommandText = "SELECT LAST_INSERT_ID()";
            try
            {
                valConnection.Open();
                using (valConnection)
                {
                    returnScalar = valCommand.ExecuteScalar();
                    returnValue = (Int64)returnScalar;
                }
            }
            catch (Exception ex)
            {
                MailMessage objMessage = new MailMessage();
                objMessage.Subject = "Problem in:TansSQL.GetProdLastValue";
                objMessage.From = new MailAddress("lowlysacker@gmail.com");
                objMessage.To.Add("lowlysacker@gmail.com");
                objMessage.Body = "<b>SQL:</b>" + valCommand.CommandText + "<br><b>Message:</b>" + ex.Message + "<br><b>StackTrace:</b>" + ex.StackTrace;
                objMessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("lowlysacker@gmail.com", "onhnpqjlbqmakcno"); //*wS!UE8GXZFThwC
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(objMessage);
            }
            finally
            {
                if (valCommand != null)
                {
                    valCommand.Dispose();
                }
            }

            return returnValue;
        }
    }
}