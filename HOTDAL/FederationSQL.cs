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
    public class FederationSQL : SqlConnector
    {
        private static string federationConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTSDA"].ConnectionString;
        private static string federationConnectionStringKey = "HOTSDA";

        #region SQLCalls
        public FederationSQL(String connectionStringKey) : base(federationConnectionStringKey) { }

        #region SELECT
        public DataTable ExecuteSTUDENT_INFORMATION_BY_SCHOOL_ID(int schoolID)
        {
            const string SPName = "SELECT * FROM STDT_INFO WHERE STDT_SCHOOL_ID = @P_SCHOOL_ID AND STDT_ACTIVE = 1 ORDER BY STDT_LNAME, STDT_FNAME";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSCHOOL_INFORMATION_BY_SCHOOL_ID(int schoolID)
        {
            const string SPName = "SELECT * FROM SCHOOL_DOMN WHERE SCHOOL_ID = @P_SCHOOL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSCHOOL_INFORMATION()
        {
            const string SPName = "SELECT * FROM SCHOOL_DOMN";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteSCHOOL_LOGIN(int schoolID)//, string schoolPassword)
        {
            const string SPName = "SELECT * FROM SCHOOL_DOMN WHERE SCHOOL_ID = @P_SCHOOL_ID";// AND SCHOOL_PASSWORD = @P_SCHOOL_PWD";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));
            //parameters.Add(makeInputParameter("P_SCHOOL_PWD", MySqlDbType.VarChar, schoolPassword));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteART_INFORMATION_BY_SCHOOL_ID(int schoolID)
        {
            const string SPName = "SELECT * FROM ART_DOMN WHERE ART_SCHOOL_ID = @P_SCHOOL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_SCHOOL_BY_SCHOOL_ID(int schoolID, string schoolName)
        {
            const string SPName = "UPDATE SCHOOL_DOMN SET SCHOOL_NAME = @P_SCHOOL_NAME WHERE SCHOOL_ID = @P_SCHOOL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));
            parameters.Add(makeInputParameter("P_SCHOOL_NAME", MySqlDbType.VarChar, schoolName));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_SCHOOL_PASSWORD_BY_SCHOOL_ID(int schoolID, string schoolPassword)
        {
            const string SPName = "UPDATE SCHOOL_DOMN SET SCHOOL_PASSWORD = @P_SCHOOL_PWD WHERE SCHOOL_ID = @P_SCHOOL_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));
            parameters.Add(makeInputParameter("P_SCHOOL_PWD", MySqlDbType.VarChar, schoolPassword));

            return modifyData(parameters, SPName);
        }

        public bool ExecuteUPDATE_STUDENT_BY_ID(int studentID, string firstName, string lastName, string address, string city, string state, string zipCode, string emergencyContact, string birthDate, string studentNote)
        {
            const string SPName = "UPDATE STDT_INFO SET STDT_LNAME = @P_LST_NME, STDT_FNAME = @P_FRST_NME, STDT_ADDR = @P_ADDR, STDT_CITY = @P_CITY," +
            "STDT_STATE = @P_STATE, STDT_ZIP = @P_ZIP, STDT_EMER = @P_EMG_CNTC, STDT_BRTH_DATE = @P_BRTH_DT, " +
            "STDT_NOTE = @P_NOTE WHERE STDT_ID = @P_STDT_ID";

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
            parameters.Add(makeInputParameter("P_NOTE", MySqlDbType.VarChar, studentNote));

            return modifyData(parameters, SPName);
        }

        #endregion

        #region INSERT
        public bool ExecuteINSERT_SCHOOL(string schoolName)//, string schoolPassword)
        {
            //const string SPName = "INSERT INTO SCHOOL_DOMN (SCHOOL_NAME, SCHOOL_PASSWORD) VALUE (@P_SCHOOL_NAME, @P_SCHOOL_PWD)";
            const string SPName = "INSERT INTO SCHOOL_DOMN (SCHOOL_NAME) VALUE (@P_SCHOOL_NAME)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_SCHOOL_NAME", MySqlDbType.VarChar, schoolName));
            //parameters.Add(makeInputParameter("P_SCHOOL_PWD", MySqlDbType.VarChar, schoolPassword));

            return modifyData(parameters, SPName);
        }

        public long ExecuteINSERT_STUDENT(string federationID, string firstName, string lastName, string address, string city, string state, string zipCode, string birthDate, string emergencyContact, int schoolID)
        {
            const string SPName = "INSERT INTO STDT_INFO (STDT_REG_ID, STDT_LNAME, STDT_FNAME, STDT_ADDR, STDT_CITY, STDT_STATE, STDT_ZIP, STDT_EMER, STDT_BRTH_DATE, STDT_PYMT_DT, STDT_PYMT_PLAN, STDT_PYMT_AMT, STDT_NOTE, STDT_PASS, STDT_PAID, STDT_ACTIVE, STDT_SCHOOL_ID) " +
            "VALUES (@P_REG_ID, @P_LST_NME, @P_FRST_NME, @P_ADDR, @P_CITY, @P_STATE, @P_ZIP, @P_EMG_CNTC, @P_BRTH_DT, '9999-12-31', 'None', '0.00', '', '1','1','1',@P_SCHOOL_ID)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_REG_ID", MySqlDbType.VarChar, federationID));
            parameters.Add(makeInputParameter("P_LST_NME", MySqlDbType.VarChar, lastName));
            parameters.Add(makeInputParameter("P_FRST_NME", MySqlDbType.VarChar, firstName));
            parameters.Add(makeInputParameter("P_ADDR", MySqlDbType.VarChar, address));
            parameters.Add(makeInputParameter("P_CITY", MySqlDbType.VarChar, city));
            parameters.Add(makeInputParameter("P_STATE", MySqlDbType.VarChar, state));
            parameters.Add(makeInputParameter("P_ZIP", MySqlDbType.VarChar, zipCode));
            parameters.Add(makeInputParameter("P_EMG_CNTC", MySqlDbType.VarChar, emergencyContact));
            parameters.Add(makeInputParameter("P_BRTH_DT", MySqlDbType.VarChar, birthDate));
            parameters.Add(makeInputParameter("P_SCHOOL_ID", MySqlDbType.Int32, schoolID));

            return returnLastInsert(parameters, SPName);
        }

        public long ExecuteINSERT_STUDENT_ART(long studentID, int artID)
        {
            const string SPName = "INSERT INTO STDT_ART_XREF (STDT_ID, ART_ID) VALUES (@P_STDT_ID, @P_ART_ID)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ART_ID", MySqlDbType.Int32, artID));
            parameters.Add(makeInputParameter("P_STDT_ID", MySqlDbType.Int32, studentID));

            return returnLastInsert(parameters, SPName);
        }

        #endregion

        #region DELETE
        #endregion

        #endregion
    }

    public class FederationProdSQL : SqlConnector
    {
        private static string pointOfSaleConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HOTSDAPOS"].ConnectionString;
        private static string sdaPointOfSaleConnectionStringKey = "HOTSDAPOS";

        public FederationProdSQL(String connectionStringKey) : base(sdaPointOfSaleConnectionStringKey) { }

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

        public DataTable ExecuteGET_ALL_FEDERATION_ITEMS()
        {
            const string SPName = "SELECT PROD_ID, PROD_TYPE, PROD_SUB_TYPE, PROD_CODE, PROD_NAME, PROD_PRICE, PROD_TAX FROM FED_PROD_DOMN WHERE PROD_DISP = 1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ITEM_BY_ITEM_ID(int itemID)
        {
            const string SPName = "SELECT PROD_NAME,PROD_PRICE,PROD_TYPE,PROD_TAX,PROD_FILE_NAME,PROD_DESC,PROD_SALE_ONLINE,PROD_SALE_STORE,PROD_SALE_PRICE,PROD_CODE,"
            + "A.PROD_LOC, A.PROD_ID,PROD_SUB_TYPE,PROD_DISP_ONLINE,PROD_DISP_STORE,PROD_DISP,PROD_CNT FROM PROD_DOMN A INNER JOIN PROD_INV C ON A.PROD_ID = C.PROD_ID "
            + "WHERE A.PROD_DISP = 1 AND A.PROD_ID = @P_ITEM_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_ITEM_ID", MySqlDbType.Int32, itemID));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_ITEM_BY_BARCODE(string barcode)
        {
            const string SPName = "SELECT * FROM FED_PROD_DOMN WHERE PROD_DISP = 1 AND PROD_CODE = @P_BAR_CODE";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, barcode));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE(string date)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID FROM TRNS_DOMN WHERE TRNS_DATE = @P_DATE ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));

            return getDataSet(parameters, SPName);
        }

        public DataTable ExecuteGET_STUDENT_TRANSACTIONS_BY_DATE_EMPLOYEE(string date, int employeeID)
        {
            const string SPName = "SELECT TRNS_ID, TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_TAX, TRNS_VOID, TRNS_DATE, TRNS_OTH, TRNS_PAID FROM TRNS_DOMN WHERE TRNS_DATE = @P_DATE AND TRNS_SELL = @P_EMP_ID ORDER BY TRNS_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_DATE", MySqlDbType.VarChar, date));
            parameters.Add(makeInputParameter("P_EMP_ID", MySqlDbType.Int32, employeeID));

            return getDataSet(parameters, SPName);
        }
        #endregion

        #region UPDATE
        public bool ExecuteUPDATE_ITEM_INVENTORY(int itemID, int itemCount)
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

        public bool ExecuteUPDATE_ITEM(int productID, string name, string price, string type, int tax, string fileName, string description, int saleOnline, int saleStore, string salePrice, string barCode, string location)
        {
            const string SPName = "UPDATE PROD_DOMN SET PROD_NAME = @P_NAME,PROD_PRICE = @P_PRICE,PROD_TYPE = @P_TYPE,PROD_TAX = @P_TAX,PROD_FILE_NAME = @P_FILE_NAME,PROD_DESC = @P_DESC"
            + ",PROD_SALE_ONLINE = @P_SALE_ONLINE,PROD_SALE_STORE = @P_SALE_STORE,PROD_SALE_PRICE = @P_SALE_PRICE,PROD_CODE = @P_BAR_CODE,PROD_LOC = @P_LOCATION WHERE PROD_ID = @P_PROD_ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_PROD_ID", MySqlDbType.Int32, productID));
            parameters.Add(makeInputParameter("P_NAME", MySqlDbType.VarChar, name));
            parameters.Add(makeInputParameter("P_PRICE", MySqlDbType.VarChar, price));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, type));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.Int32, tax));
            parameters.Add(makeInputParameter("P_FILE_NAME", MySqlDbType.VarChar, fileName));
            parameters.Add(makeInputParameter("P_DESC", MySqlDbType.VarChar, description));
            parameters.Add(makeInputParameter("P_SALE_ONLINE", MySqlDbType.Int32, saleOnline));
            parameters.Add(makeInputParameter("P_SALE_STORE", MySqlDbType.Int32, saleStore));
            parameters.Add(makeInputParameter("P_SALE_PRICE", MySqlDbType.VarChar, salePrice));
            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, barCode));
            parameters.Add(makeInputParameter("P_LOCATION", MySqlDbType.VarChar, location));

            return modifyData(parameters, SPName);
        }

        #endregion

        #region INSERT
        public long ExecuteINSERT_TRANSACTION(int employeeID, string cartTotal, int studentID, string location, string paymentType, string date, string tax, string note)
        {
            const string SPName = "INSERT INTO FED_TRNS_DOMN (TRNS_SELL, TRNS_TTL, TRNS_BGHT, TRNS_LOC, TRNS_PYMT, TRNS_DATE, TRNS_TAX, TRNS_VOID, TRNS_PAID, TRNS_OTH) "
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
            const string SPName = "INSERT INTO FED_TRNS_XREF (TRNS_ID, PROD_ID, PROD_QTY, PROD_NME, PROD_PRICE, PROD_TAX) VALUES (@P_TRNS_ID,@P_ITEM_ID,@P_ITEM_QTY,@P_ITEM_NME,@P_ITEM_PRICE,@P_ITEM_TAX)";

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

        public bool ExecuteINSERT_PRODUCT(string Name, string Price, string Type, int Tax, string FileName, string Description, int SaleOnline, int SaleStore, string SalePrice, string Barcode, string Location)
        {
            const string SPName = "INSERT INTO PROD_DOMN (PROD_NAME,PROD_PRICE,PROD_TYPE,PROD_TAX,PROD_FILE_NAME,PROD_DESC,PROD_SALE_ONLINE,PROD_SALE_STORE,PROD_SALE_PRICE,PROD_CODE,PROD_LOC)"
            + "VALUES (@P_NAME,@P_PRICE,@P_TYPE,@P_TAX,@P_FILE_NAME,@P_DESC,@P_SALE_ONLINE,@P_SALE_STORE,@P_SALE_PRICE,@P_BAR_CODE,@P_LOC)";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(makeInputParameter("P_NAME", MySqlDbType.VarChar, Name));
            parameters.Add(makeInputParameter("P_PRICE", MySqlDbType.VarChar, Price));
            parameters.Add(makeInputParameter("P_TYPE", MySqlDbType.VarChar, Type));
            parameters.Add(makeInputParameter("P_TAX", MySqlDbType.Int32, Tax));
            parameters.Add(makeInputParameter("P_FILE_NAME", MySqlDbType.VarChar, FileName));
            parameters.Add(makeInputParameter("P_DESC", MySqlDbType.VarChar, Description));
            parameters.Add(makeInputParameter("P_SALE_ONLINE", MySqlDbType.Int32, SaleOnline));
            parameters.Add(makeInputParameter("P_SALE_STORE", MySqlDbType.Int32, SaleStore));
            parameters.Add(makeInputParameter("P_SALE_PRICE", MySqlDbType.VarChar, SalePrice));
            parameters.Add(makeInputParameter("P_BAR_CODE", MySqlDbType.VarChar, Barcode));
            parameters.Add(makeInputParameter("P_LOC", MySqlDbType.VarChar, Location));

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