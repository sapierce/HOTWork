using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using HOTDAL;

namespace HOTBAL
{
    public class FederationMethods : SDAMethods
    {
        private static string federationConnectionStringKey = "HOTSDA";
        private static string prodConnectionStringKey = "HOTSDAPOS";
        private FederationSQL fedDataAccess = new FederationSQL(federationConnectionStringKey);
        private FederationProdSQL prodDataAccess = new FederationProdSQL(prodConnectionStringKey);
        private TansFunctionsClass functionsClass = new TansFunctionsClass();

        #region School Information
        public List<School> GetAllSchools()
        {
            List<School> schools = new List<School>();
            try
            {
                // Get the schools
                DataTable schoolTable = fedDataAccess.ExecuteSCHOOL_INFORMATION();

                // Do we have records?
                if (schoolTable.Rows.Count > 0)
                {
                    // Loop through returned records
                    foreach (DataRow schoolReader in schoolTable.Rows)
                    {
                        // Build a new School object
                        School schoolInformation = new School();
                        schoolInformation.SchoolID = Convert.ToInt32(schoolReader["SCHOOL_ID"].ToString());
                        schoolInformation.SchoolName = schoolReader["SCHOOL_NAME"].ToString().Trim();
                        schools.Add(schoolInformation);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, "", "FederationMethods: GetAllSchools");
            }

            return schools;
        }

        public School GetSchoolBySchoolID(int schoolID)
        {
            School schoolInformation = new School();
            try
            {
                // Get the schools
                DataTable schoolTable = fedDataAccess.ExecuteSCHOOL_INFORMATION_BY_SCHOOL_ID(schoolID);

                // Do we have records?
                if (schoolTable.Rows.Count > 0)
                {
                    // Loop through returned records
                    foreach (DataRow schoolReader in schoolTable.Rows)
                    {
                        // Build a new School object
                        schoolInformation.SchoolID = Convert.ToInt32(schoolReader["SCHOOL_ID"].ToString());
                        schoolInformation.SchoolName = schoolReader["SCHOOL_NAME"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, schoolID.ToString(), "FederationMethods: GetSchoolBySchoolID");
            }

            return schoolInformation;
        }

        public bool GetSchoolByLogin(int schoolID)//, string schoolPassword)
        {
            try
            {
                // Get the schools
                DataTable schoolTable = fedDataAccess.ExecuteSCHOOL_LOGIN(schoolID);//, functionsClass.HashText(schoolPassword));

                // Do we have records?
                if (schoolTable.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, schoolID.ToString(), "FederationMethods: GetSchoolBySchoolID");
                return false;
            }
        }

        public bool UpdateSchoolInformation(int schoolID, string schoolName)
        {
            try
            {
                return fedDataAccess.ExecuteUPDATE_SCHOOL_BY_SCHOOL_ID(schoolID, schoolName);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, schoolID.ToString(), "FederationMethods: UpdateSchoolInformation");
                return false;
            }
        }

        public bool UpdateSchoolPassword(int schoolID, string schoolPassword)
        {
            try
            {
                return fedDataAccess.ExecuteUPDATE_SCHOOL_PASSWORD_BY_SCHOOL_ID(schoolID, functionsClass.HashText(schoolPassword));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, schoolID.ToString(), "FederationMethods: UpdateSchoolPassword");
                return false;
            }
        }

        public bool AddSchoolInformation(string schoolName)//, string schoolPassword)
        {
            try
            {
                return fedDataAccess.ExecuteINSERT_SCHOOL(schoolName);//, functionsClass.HashText(schoolPassword));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, schoolName, "FederationMethods: AddSchoolInformation");
                return false;
            }
        }
        #endregion

        #region Art Information
        public List<Art> GetArtsBySchoolID(int schoolID)
        {
            List<Art> artResponse = new List<Art>();

            try
            {
                DataTable artTable = fedDataAccess.ExecuteART_INFORMATION_BY_SCHOOL_ID(schoolID);

                if (artTable.Rows.Count > 0)
                {
                    foreach (DataRow row in artTable.Rows)
                    {
                        Art arts = new Art();
                        arts.ID = Convert.ToInt32(row["ART_ID"].ToString());
                        arts.Title = row["ART_TITLE"].ToString();
                        artResponse.Add(arts);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "FederationMethods: GetArtsBySchoolID");
                Art arts = new Art();
                arts.Error = SDAMessages.ERROR_GENERIC;
                artResponse.Add(arts);
            }

            return artResponse;
        }
        #endregion

        #region Student Information
        public List<Student> GetStudentsBySchoolID(int schoolID)
        {
            List<Student> schoolStudents = new List<Student>();
            try
            {
                DataTable studentsTable = fedDataAccess.ExecuteSTUDENT_INFORMATION_BY_SCHOOL_ID(schoolID);

                if (studentsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Student student = new Student();
                        student.ID = Convert.ToInt32(row["STDT_ID"].ToString());
                        if (Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim()) == 1)
                            student.RegistrationID = row["STDT_ID"].ToString().Trim();
                        else
                            student.RegistrationID = row["STDT_REG_ID"].ToString().Trim();
                        student.FirstName = row["STDT_FNAME"].ToString();
                        student.LastName = row["STDT_LNAME"].ToString();
                        student.Address = row["STDT_ADDR"].ToString();
                        student.City = row["STDT_CITY"].ToString();
                        student.State = row["STDT_STATE"].ToString();
                        student.ZipCode = row["STDT_ZIP"].ToString();
                        student.EmergencyContact = row["STDT_EMER"].ToString();
                        student.PaymentDate = Convert.ToDateTime(row["STDT_PYMT_DT"].ToString());
                        student.PaymentPlan = row["STDT_PYMT_PLAN"].ToString();
                        student.PaymentAmount = Convert.ToDouble(row["STDT_PYMT_AMT"].ToString());
                        student.Paid = (row["STDT_PAID"].ToString() == "True" ? true : false);
                        student.Pass = (row["STDT_PASS"].ToString() == "True" ? true : false);
                        student.BirthDate = Convert.ToDateTime(row["STDT_BRTH_DATE"].ToString());
                        student.Note = row["STDT_NOTE"].ToString();
                        student.Active = (row["STDT_ACTIVE"].ToString() == "True" ? true : false);
                        student.School = Convert.ToInt32(row["STDT_SCHOOL_ID"].ToString().Trim());
                        schoolStudents.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, "", "FederationMethods: GetStudentsBySchoolID");
            }

            return schoolStudents;
        }

        public long AddStudentInformation(string federationID, string firstName, string lastName, string address, string city, string state, string zipCode, DateTime birthDate, string emergencyContact, int artID, int beltID, int schoolID)
        {
            long studentID = 0;
            long xrefID = 0;
            bool response = false;
            try
            {
                studentID = fedDataAccess.ExecuteINSERT_STUDENT(federationID, firstName, lastName, address, city, state, zipCode, functionsClass.FormatDash(birthDate), emergencyContact, schoolID);

                if (studentID > 0)
                {
                    xrefID = fedDataAccess.ExecuteINSERT_STUDENT_ART(studentID, artID);
                    if (xrefID > 0)
                    {
                        response = UpdateStudentArt(studentID, xrefID, artID, beltID, 0, 0, DateTime.Now, "New", "U");
                    }
                    else
                        LogErrorMessage(new Exception("NoXREFId"), firstName + " " + lastName, "FederationMethods: AddStudentInformation: No XREF Id");
                }
                else
                    LogErrorMessage(new Exception("NoStudentId"), firstName + " " + lastName, "FederationMethods: AddStudentInformation: No Student Id");
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, firstName + " " + lastName, "FederationMethods: AddStudentInformation");
            }

            return studentID;
        }
        
        public bool UpdateStudent(int ID, string FirstName, string LastName, string Address, string City, string State, string ZipCode, string EmergencyContact, DateTime BirthDate, string Note)
        {
            bool addResponse = false;

            try
            {
                addResponse = fedDataAccess.ExecuteUPDATE_STUDENT_BY_ID(ID, FirstName, LastName, Address, City, State, ZipCode,
                    EmergencyContact, functionsClass.FormatDash(BirthDate), Note);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, FirstName + " " + LastName, "SDAMethods: UpdateStudent");
            }

            return addResponse;
        }

        #endregion

        #region Product Information
        public List<Product> GetFederationItems()
        {
            List<Product> itemsResponse = new List<Product>();
            
            try
            {
                DataTable productTable = prodDataAccess.ExecuteGET_ALL_FEDERATION_ITEMS();

                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow row in productTable.Rows)
                    {
                        Product items = new Product();
                        items.ProductID = Convert.ToInt32(row["PROD_ID"].ToString());
                        items.ProductType = row["PROD_TYPE"].ToString();
                        items.ProductCode = row["PROD_CODE"].ToString();
                        items.ProductName = row["PROD_NAME"].ToString();
                        items.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        items.ProductCount = 0;
                        items.ProductTaxable = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        items.ProductSubType = row["PROD_SUB_TYPE"].ToString();
                        itemsResponse.Add(items);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "FederationMethods: GetFederationItems");
                Product items = new Product();
                //items[0].Error = SDAMessages.ERROR_GENERIC;
                itemsResponse.Add(items);
            }

            return itemsResponse;
        }

        public Product GetFederationItemByBarCode(string barCode)
        {
            Product itemResponse = new Product();

            try
            {
                DataTable productTable = prodDataAccess.ExecuteGET_ITEM_BY_BARCODE(barCode);

                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow row in productTable.Rows)
                    {
                        itemResponse.ProductID = Convert.ToInt32(row["PROD_ID"].ToString());
                        itemResponse.ProductType = row["PROD_TYPE"].ToString();
                        itemResponse.ProductCode = row["PROD_CODE"].ToString();
                        itemResponse.ProductName = row["PROD_NAME"].ToString();
                        itemResponse.ProductDescription = row["PROD_DESC"].ToString();
                        itemResponse.ProductPrice = Convert.ToDouble(row["PROD_PRICE"].ToString());
                        itemResponse.ProductTaxable = (row["PROD_TAX"].ToString() == "True" ? true : false);
                        itemResponse.ProductSubType = row["PROD_SUB_TYPE"].ToString();
                        itemResponse.Active = (row["PROD_DISP"].ToString() == "True" ? false : true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, barCode, "FedMethods: GetItemByBarCode");
            }

            return itemResponse;
        }

        public bool AddTransaction(List<CartItem> cartSummary, int employeeID, string cartTotal, int studentID, string location, string paymentType, string date, string tax, string note)
        {
            bool response = false;
            long transactionID = 0;

            try
            {
                transactionID = prodDataAccess.ExecuteINSERT_TRANSACTION(employeeID, cartTotal, studentID, location, paymentType, date, tax, note);

                //Add in the items for the transaction
                foreach (CartItem item in cartSummary)
                {
                    response = prodDataAccess.ExecuteINSERT_TRANSACTION_ITEM(transactionID, item.ItemID, item.ItemQuantity, item.ItemName, item.ItemPrice.ToString(), (item.ItemTaxed ? 1 : 0));

                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, studentID.ToString(), "FedMethods: AddTransaction");
            }

            return response;
        }

        
        #endregion
    }
}
