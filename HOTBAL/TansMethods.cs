using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using HOTDAL;
using System.Configuration;

namespace HOTBAL
{
    public class TansMethods
    {
        private static string tansConnectionStringKey = "HOTTans";
        private static string prodConnectionStringKey = "HOTProd";
        private TansSQL tansDataAccess = new TansSQL(tansConnectionStringKey);
        private ProdSQL prodDataAccess = new ProdSQL(prodConnectionStringKey);
        private TansFunctionsClass functionsClass = new TansFunctionsClass();

        /// <summary>
        /// Logs the error messages to the database
        /// </summary>
        /// <param name="errorMessage">Exception object</param>
        /// <param name="sqlCommand">Additional information</param>
        /// <param name="errorLocation">Where the error occurred</param>
        public void LogErrorMessage(Exception errorMessage, string sqlCommand, string errorLocation)
        {
            //try
            //{
            //    errorDataAccess.ExecuteTAN_ERROR_ADD(errorLocation, errorMessage.Message, (String.IsNullOrEmpty(errorMessage.StackTrace) ? "" : errorMessage.StackTrace), sqlCommand);
            //}
            //catch (Exception ex)
            //{
            functionsClass.SendErrorMail(errorLocation, errorMessage, sqlCommand);
            //}
        }

        #region Location Information

        /// <summary>
        /// Retrieves the begin and end times for each day based on location and type
        /// </summary>
        /// <param name="location">Location of times (W, H)</param>
        /// <param name="type">Type of times (T, M)</param>
        /// <returns></returns>
        public List<Time> GetLocationTimes(string location, string type)
        {
            List<Time> locationTimes = new List<Time>();
            try
            {
                // Get the times for the type and location
                DataTable timeTable = tansDataAccess.ExecuteTIMES_BY_LOCATION_TYPE(functionsClass.CleanUp(location), functionsClass.CleanUp(type));
                
                // Do we have records?
                if (timeTable.Rows.Count > 0)
                {
                    // Loop through returned records
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        // Build a new Time object
                        Time locationTime = new Time();

                        // Check the time to see if the store is closed
                        if (timeReader["BEG_TIME"].ToString().Trim() == "12:00 AM")
                        {
                            locationTime.BeginTime = "CLOSED";
                            locationTime.EndTime = "CLOSED";
                        }
                        else
                        {
                            locationTime.BeginTime = timeReader["BEG_TIME"].ToString().Trim();
                            locationTime.EndTime = timeReader["END_TIME"].ToString().Trim();
                        }

                        // Build out rest of Time object
                        locationTime.WebTimeDay = timeReader["TIME_WEB"].ToString().Trim();
                        locationTime.TimeDay = timeReader["TIME_DAY"].ToString().Trim();
                        locationTimes.Add(locationTime);
                    }
                }
                else
                {
                    // Log error
                    LogErrorMessage(new Exception("No Times"), location, "Methods: GetLocationTimes: NoTimes");
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, location, "Methods: GetLocationTimes");
            }

            return locationTimes;
        }

        /// <summary>
        /// Updates the begin and end times for a given location, day and type
        /// </summary>
        /// <param name="location">Location to update</param>
        /// <param name="beginTime">New begin time</param>
        /// <param name="endTime">New end time</param>
        /// <param name="date">Day to update</param>
        /// <param name="type">Type to update</param>
        /// <returns></returns>
        public bool UpdateLocationTimes(string location, string beginTime, string endTime, string date, string type)
        {
            try
            {
                // Update the begin and end times for the given location, day and type
                bool response = tansDataAccess.ExecuteUPDATE_TIMES_BY_LOCATION_TYPE_DAY(beginTime, endTime, functionsClass.CleanUp(location), date, functionsClass.CleanUp(type));
                return response;
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, location, "Methods: UpdateLocationTimes");
                return false;
            }
        }

        #endregion Location Information

        #region Bed Information

        /// <summary>
        /// Get all of the beds
        /// </summary>
        /// <returns></returns>
        public List<Bed> GetAllActiveBeds()
        {
            List<Bed> returnBeds = new List<Bed>();

            try
            {
                // Get the beds
                DataTable bedTable = tansDataAccess.ExecuteGET_ALL_ACTIVE_BEDS();

                // Do we have records?
                if (bedTable.Rows.Count > 0)
                {
                    // Loop through returned records
                    foreach (DataRow bedReader in bedTable.Rows)
                    {
                        // Build Bed object
                        Bed bedList = new Bed();
                        bedList.BedLong = bedReader["BED_LONG"].ToString().Trim();
                        bedList.BedShort = bedReader["BED_SHORT"].ToString().Trim();
                        bedList.BedType = bedReader["BED_TYPE"].ToString().Trim(); ;
                        bedList.BedID = Convert.ToInt32(bedReader["BED_ID"].ToString().Trim());
                        bedList.BedDisplayInternal = (bedReader["BED_DISP_INT"].ToString().Trim() == "True" ? true : false);
                        bedList.BedDisplayExternal = (bedReader["BED_DISP_EXT"].ToString().Trim() == "True" ? true : false);
                        bedList.BedActive = (bedReader["BED_ACTV"].ToString().Trim() == "True" ? true : false);
                        bedList.BedLocation = bedReader["BED_LOC"].ToString().Trim();
                        returnBeds.Add(bedList);
                    }
                }
                else
                {
                    // No active beds found
                    // Log error
                    LogErrorMessage(new Exception("No Beds Found"), "", "Methods: GetAllActiveBeds");
                //    Bed bedList = new Bed();
                //    returnBeds.Add(bedList);
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, "", "Methods: GetAllActiveBeds");
                //Bed bedList = new Bed();
                //returnBeds.Add(bedList);
            }

            return returnBeds;
        }

        /// <summary>
        /// Get the available beds based on the bed type
        /// </summary>
        /// <param name="bedType">Type of bed</param>
        /// <returns></returns>
        public List<Bed> GetBedsByType(string bedType)
        {
            List<Bed> returnBeds = new List<Bed>();

            try
            {
                // Get the beds based on the passed in bed type
                DataTable bedTable = tansDataAccess.ExecuteEXTERNAL_ACTIVE_BEDS_BY_BED_TYPE(bedType);

                // Do we have records?
                if (bedTable.Rows.Count > 0)
                {
                    // Loop through returned records
                    foreach (DataRow bedReader in bedTable.Rows)
                    {
                        // Build Bed object
                        Bed bedList = new Bed();
                        bedList.BedLong = bedReader["BED_LONG"].ToString().Trim();
                        bedList.BedShort = bedReader["BED_SHORT"].ToString().Trim();
                        bedList.BedType = bedType;
                        bedList.BedID = Convert.ToInt32(bedReader["BED_ID"].ToString().Trim());
                        bedList.BedActive = (bedReader["BED_ACTV"].ToString().Trim() == "True" ? true : false);
                        bedList.BedDisplayInternal = (bedReader["BED_DISP_INT"].ToString().Trim() == "True" ? true : false);
                        bedList.BedDisplayExternal = (bedReader["BED_DISP_EXT"].ToString().Trim() == "True" ? true : false);
                        bedList.BedLocation = bedReader["BED_LOC"].ToString().Trim();
                        returnBeds.Add(bedList);
                    }
                }
                else
                {
                    // No active beds of that type
                    Bed bedList = new Bed();
                    bedList.BedType = bedType;
                    returnBeds.Add(bedList);
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedType, "Methods: GetBedsByType");
                Bed bedList = new Bed();
                bedList.BedType = bedType;
                returnBeds.Add(bedList);
            }

            return returnBeds;
        }

        /// <summary>
        /// Get all beds at a given location
        /// </summary>
        /// <param name="bedLocation">Bed location</param>
        /// <returns></returns>
        public List<Bed> GetLocationActiveBeds(string bedLocation)
        {
            List<Bed> returnBeds = new List<Bed>();

            try
            {
                // Get beds at the given location
                DataTable bedTable = tansDataAccess.ExecuteBEDS_BY_LOCATION(functionsClass.CleanUp(bedLocation));
                if (bedTable.Rows.Count > 0)
                {
                    foreach (DataRow bedReader in bedTable.Rows)
                    {
                        // Build a bed object
                        Bed bedList = new Bed();
                        bedList.BedID = Convert.ToInt32(bedReader["BED_ID"].ToString());
                        bedList.BedLong = bedReader["BED_LONG"].ToString();
                        bedList.BedShort = bedReader["BED_SHORT"].ToString();
                        bedList.BedType = bedReader["BED_TYPE"].ToString();
                        bedList.BedActive = (bedReader["BED_ACTV"].ToString() == "True" ? true : false);
                        bedList.BedDisplayInternal = (bedReader["BED_DISP_INT"].ToString() == "True" ? true : false);
                        bedList.BedDisplayExternal = (bedReader["BED_DISP_EXT"].ToString() == "True" ? true : false);
                        bedList.BedLocation = functionsClass.CleanUp(bedLocation);
                        returnBeds.Add(bedList);
                    }
                }
                else
                {
                    // No active beds at this location
                    Bed bedList = new Bed();
                    bedList.BedLocation = functionsClass.CleanUp(bedLocation);

                    returnBeds.Add(bedList);
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedLocation, "Methods: GetLocationBeds");
                Bed bedList = new Bed();
                bedList.BedLocation = functionsClass.CleanUp(bedLocation);

                returnBeds.Add(bedList);
            }

            return returnBeds;
        }

        /// <summary>
        /// Gets information on a given bed
        /// </summary>
        /// <param name="bedID">ID of the bed</param>
        /// <returns></returns>
        public Bed GetBedInformationByID(int bedID)
        {
            Bed returnBed = new Bed();

            try
            {
                // Get the bed information for the given bed ID
                DataTable bedTable = tansDataAccess.ExecuteBEDS_BY_BED_ID(bedID);
                if (bedTable.Rows.Count > 0)
                {
                    foreach (DataRow bedReader in bedTable.Rows)
                    {
                        // Populate the bed object
                        returnBed.BedLong = bedReader["BED_LONG"].ToString().Trim();
                        returnBed.BedShort = bedReader["BED_SHORT"].ToString().Trim();
                        returnBed.BedType = bedReader["BED_TYPE"].ToString().Trim();
                        returnBed.BedActive = (bedReader["BED_ACTV"].ToString().Trim() == "True" ? true : false);
                        returnBed.BedDisplayInternal = (bedReader["BED_DISP_INT"].ToString().Trim() == "True" ? true : false);
                        returnBed.BedDisplayExternal = (bedReader["BED_DISP_EXT"].ToString().Trim() == "True" ? true : false);
                        returnBed.BedLocation = bedReader["BED_LOC"].ToString().Trim();
                        returnBed.BedID = Convert.ToInt32(bedReader["BED_ID"].ToString().Trim());
                    }
                }
                else
                {
                    // No active beds with that ID
                    returnBed.BedID = bedID;
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedID.ToString(), "Methods: GetBedInformationByID");
                returnBed.BedID = bedID;
            }

            return returnBed;
        }

        /// <summary>
        /// Adds a new bed
        /// </summary>
        /// <param name="bedDescription">Long description of bed</param>
        /// <param name="shortDescription">Short description of bed</param>
        /// <param name="bedLocation">Where the bed is located</param>
        /// <param name="bedType">Type of bed</param>
        /// <param name="bedDisplay">Is this bed active</param>
        /// <returns></returns>
        public bool AddNewBed(string bedDescription, string shortDescription, string bedLocation, string bedType, bool bedDisplayInternal, bool bedDisplayExternal)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_BED(functionsClass.CleanUp(bedDescription), functionsClass.CleanUp(shortDescription),
                    functionsClass.CleanUp(bedLocation), functionsClass.CleanUp(bedType), (bedDisplayInternal == true ? 1 : 0), (bedDisplayExternal == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedDescription, "Methods: AddNewBed");
                return false;
            }
        }

        /// <summary>
        /// Updates the bed information for the given bed ID
        /// </summary>
        /// <param name="bedID">ID of the bd</param>
        /// <param name="bedDescription">Long description of the bed</param>
        /// <param name="shortDescription">Short description of the bed</param>
        /// <param name="bedLocation">Location of the bed</param>
        /// <param name="bedType">Type of bed</param>
        /// <param name="bedDisplay">Is the bed active</param>
        /// <returns></returns>
        public bool UpdateBed(int bedID, string bedDescription, string shortDescription, string bedLocation, string bedType, bool bedDisplayInternal, bool bedDisplayExternal)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_BED_BY_BED_ID(bedID, functionsClass.CleanUp(bedDescription), functionsClass.CleanUp(shortDescription),
                    functionsClass.CleanUp(bedLocation), functionsClass.CleanUp(bedType), (bedDisplayInternal == true ? 1 : 0), (bedDisplayExternal == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedID.ToString(), "Methods: UpdateBed");
                return false;
            }
        }

        public bool DeleteBed(int bedID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_BED_BY_BED_ID(bedID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, bedID.ToString(), "Methods: DeleteBed");
                return false;
            }
        }

        #endregion Bed Information

        #region Customer Information

        /// <summary>
        /// Gets customer information by given customer ID
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <returns></returns>
        public Customer GetCustomerInformationByID(Int64 customerID)
        {
            Customer customerResponse = new Customer();
            try
            {
                DataTable infoTable = tansDataAccess.ExecuteCUSTOMER_INFO_BY_CUSTOMER_ID(customerID);

                // Were main information records returned?
                if (infoTable.Rows.Count > 0)
                {
                    foreach (DataRow infoReader in infoTable.Rows)
                    {
                        // Populate the customer object
                        customerResponse.ID = customerID;
                        customerResponse.IsActive = (infoReader["CUST_ACTIVE"].ToString().Trim() == "True" ? true : false);
                        customerResponse.FirstName = infoReader["CUST_FNAME"].ToString().Trim();
                        customerResponse.LastName = infoReader["CUST_LNAME"].ToString().Trim();
                        customerResponse.Address = infoReader["CUST_ADDR"].ToString().Trim();
                        customerResponse.City = infoReader["CUST_CITY"].ToString().Trim();
                        customerResponse.State = infoReader["CUST_ST"].ToString().Trim();
                        customerResponse.ZipCode = infoReader["CUST_ZIP"].ToString().Trim();
                        customerResponse.PhoneNumber = infoReader["CUST_PHONE"].ToString().Trim();
                        customerResponse.DateOfBirth = (String.IsNullOrEmpty(infoReader["CUST_DOB"].ToString().Trim()) ? DateTime.MinValue : Convert.ToDateTime(infoReader["CUST_DOB"].ToString().Trim()));
                        customerResponse.JoinDate = Convert.ToDateTime(infoReader["CUST_JOIN"].ToString().Trim());
                        customerResponse.RenewalDate = Convert.ToDateTime(infoReader["CUST_RENEWAL"].ToString().Trim());
                        customerResponse.Remarks = infoReader["CUST_REMARK"].ToString().Trim();
                        customerResponse.FitzPatrickNumber = Convert.ToInt32(infoReader["CUST_FPS"].ToString().Trim());
                        customerResponse.Email = infoReader["USER_MAIL"].ToString().Trim();
                        customerResponse.ReceiveSpecials = (infoReader["USER_SPECIAL"].ToString().Trim() == "True" ? true : false);
                        customerResponse.OnlineName = infoReader["USER_NAME"].ToString().Trim();
                        customerResponse.OnlineUser = (infoReader["CUST_ONLINE"].ToString().Trim() == "True" ? true : false);
                        customerResponse.NewOnlineCustomer = (infoReader["CUST_NEW_ONLINE"].ToString().Trim() == "True" ? true : false);
                        customerResponse.VerifiedEmail = (infoReader["VERIFY_IND"].ToString().Trim() == "True" ? true : false);
                        customerResponse.LotionWarning = (infoReader["CUST_LOTION"].ToString().Trim() == "True" ? true : false);
                        customerResponse.AcknowledgeWarning = (infoReader["CUST_WARN"].ToString().Trim() == "True" ? true : false);
                        customerResponse.AcknowledgeWarningText = infoReader["CUST_WARN_TXT"].ToString().Trim();
                        customerResponse.FamilyHistory = (infoReader["CUST_FHIST"].ToString().Trim() == "True" ? true : false);
                        customerResponse.PersonalHistory = (infoReader["CUST_HIST"].ToString().Trim() == "True" ? true : false);
                        customerResponse.OnlineRestriction = (infoReader["CUST_RESTRICT"].ToString().Trim() == "True" ? true : false);

                        // 
                        customerResponse = GetCustomerPlanDetails(customerResponse, infoReader["CUST_PLAN"].ToString().Trim().ToUpper());
                        
                        //
                        customerResponse.SpecialDate = Convert.ToDateTime(infoReader["SPECIAL_DATE"].ToString().Trim());
                        customerResponse.SpecialFlag = (infoReader["SPECIAL_FLAG"].ToString().Trim() == "True" ? true : false);
                        customerResponse.SpecialID = (infoReader["SPECIAL_ID"].ToString() == String.Empty ? 0 : Convert.ToInt32(infoReader["SPECIAL_ID"].ToString().Trim()));
                        
                    }
                }
                else
                {
                    // Set error
                    customerResponse.Error = "We're sorry, but we are currently unable to access customer information.<br>";
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving main customer information.  Please try again.<br>";
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Main");
            }

            try
            { 
                if (String.IsNullOrEmpty(customerResponse.Error))
                {
                    DataTable tanTable = tansDataAccess.ExecuteCUSTOMER_TANS_BY_CUSTOMER_ID(customerID);
                    List<Tan> customerTans = new List<Tan>();

                    try
                    {
                        // Were tan information records returned?
                        if (tanTable.Rows.Count > 0)
                        {
                            foreach (DataRow tanReader in tanTable.Rows)
                            {
                                // Build a Tan object
                                Tan tans = new Tan();
                                tans.TanID = Convert.ToInt32(tanReader["TAN_ID"].ToString().Trim());
                                tans.Date = functionsClass.FormatSlash(Convert.ToDateTime(tanReader["TAN_DATE"].ToString().Trim()));
                                tans.Time = tanReader["TAN_TIME"].ToString().Trim();
                                tans.Location = (tanReader["TAN_LOC"].ToString().Trim() == "W" ? "Waco" : "Hewitt");
                                tans.Length = Convert.ToInt32(tanReader["TAN_LENGTH"].ToString().Trim());
                                tans.Bed = tanReader["TAN_BED"].ToString().Trim();
                                tans.DeletedIndicator = (tanReader["ACTV_IND"].ToString().Trim() == "True" ? false : true);
                                tans.OnlineIndicator = (tanReader["TAN_ONLINE"].ToString().Trim() == "True" ? true : false);
                                customerTans.Add(tans);
                            }
                        }
                        customerResponse.Tans = customerTans;
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Tans");
                        customerResponse.Error += "Error getting tans.<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving customer tanning information.  Please try again.<br>";
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Tans");
            }

            try
            {
                if (String.IsNullOrEmpty(customerResponse.Error))
                {
                    if (ConfigurationManager.AppSettings["MassageEnabled"].ToString() == "Y")
                    {
                        DataTable massageTable = tansDataAccess.ExecuteCUSTOMER_MASSAGES_BY_CUSTOMER_ID(customerID);
                        List<Massage> customerMassages = new List<Massage>();

                        try
                        {
                            // Were massage records returned?
                            if (massageTable.Rows.Count > 0)
                            {
                                foreach (DataRow massageReader in massageTable.Rows)
                                {
                                    // Build a Massage object
                                    Massage massages = new Massage();
                                    massages.ID = Convert.ToInt32(massageReader["MASSAGE_ID"].ToString().Trim());
                                    massages.Date = Convert.ToDateTime(massageReader["MASSAGE_DATE"].ToString().Trim());
                                    massages.Time = massageReader["MASSAGE_TIME"].ToString().Trim();
                                    massages.Length = Convert.ToInt32(massageReader["MASSAGE_LENGTH"].ToString().Trim());
                                    massages.UserID = Convert.ToInt32(massageReader["USER_ID"].ToString().Trim());
                                    customerMassages.Add(massages);
                                }
                            }
                            customerResponse.Massages = customerMassages;
                        }
                        catch (Exception ex)
                        {
                            // Log error
                            LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Massages");
                            customerResponse.Error += "Error getting massages.<br>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving customer massage information.  Please try again.<br>";
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Massage");
            }

            try
            { 
                if (String.IsNullOrEmpty(customerResponse.Error))
                {
                    List<CustomerNote> customerNotes = new List<CustomerNote>();
                    DataTable noteTable = tansDataAccess.ExecuteCUSTOMER_NOTES_BY_CUSTOMER_ID(customerID);
                    
                    try
                    {
                        // Were note records returned?
                        if (noteTable.Rows.Count > 0)
                        {
                            foreach (DataRow noteReader in noteTable.Rows)
                            {
                                // Build a Note object
                                CustomerNote notes = new CustomerNote();
                                notes.NoteID = Convert.ToInt32(noteReader["NOTE_ID"].ToString().Trim());
                                notes.NoteText = noteReader["NOTE_TXT"].ToString().Trim();
                                notes.NeedsUpgrade = (noteReader["NOTE_CHECK"].ToString().Trim() == "True" ? true : false);
                                notes.OwedProduct = (noteReader["NOTE_OWED"].ToString().Trim() == "True" ? true : false);
                                notes.OwesMoney = (noteReader["NOTE_OWES"].ToString().Trim() == "True" ? true : false);
                                customerNotes.Add(notes);
                            }
                        }
                        customerResponse.Notes = customerNotes;
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: CustomerNotes");
                        customerResponse.Error += "Error getting notes.<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving customer note information.  Please try again.<br>";
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerInformationByID: Notes");
            }

            return customerResponse;
        }

        /// <summary>
        /// Gets minimal customer information by given customer ID
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <returns></returns>
        public Customer GetLimitedCustomerInformationByID(Int64 customerID)
        {
            Customer customerResponse = new Customer();
            try
            {
                DataTable infoTable = tansDataAccess.ExecuteCUSTOMER_INFO_BY_CUSTOMER_ID(customerID);

                // Were information records returned?
                if (infoTable.Rows.Count > 0)
                {
                    foreach (DataRow infoReader in infoTable.Rows)
                    {
                        // Populate the customer object
                        customerResponse.ID = customerID;
                        customerResponse.FirstName = infoReader["CUST_FNAME"].ToString().Trim();
                        customerResponse.LastName = infoReader["CUST_LNAME"].ToString().Trim();
                        customerResponse.JoinDate = Convert.ToDateTime(infoReader["CUST_JOIN"].ToString().Trim());
                        customerResponse.RenewalDate = Convert.ToDateTime(infoReader["CUST_RENEWAL"].ToString().Trim());
                        customerResponse.Remarks = infoReader["CUST_REMARK"].ToString().Trim();
                        customerResponse.FitzPatrickNumber = Convert.ToInt32(infoReader["CUST_FPS"].ToString().Trim());
                        customerResponse.Plan = PlanTranslation(infoReader["CUST_PLAN"].ToString().Trim().ToUpper());
                        customerResponse.Email = infoReader["USER_MAIL"].ToString().Trim();
                        customerResponse.LotionWarning = (infoReader["CUST_LOTION"].ToString().Trim() == "True" ? true : false);
                        customerResponse.SpecialDate = Convert.ToDateTime(infoReader["SPECIAL_DATE"].ToString().Trim());
                        customerResponse.SpecialFlag = (infoReader["SPECIAL_FLAG"].ToString().Trim() == "True" ? true : false);
                        customerResponse.SpecialID = (infoReader["SPECIAL_ID"].ToString() == String.Empty ? 0 : Convert.ToInt32(infoReader["SPECIAL_ID"].ToString().Trim()));
                    }
                }
                else
                {
                    // Set error
                    customerResponse.Error = "We're sorry, but we are currently unable to access this customer information.<br>";
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving customer information.  Please try again.<br>";
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetLimitedCustomerInformationByID: Main");
            }

            return customerResponse;
        }

        /// <summary>
        /// Get a list of customers containing first and last names
        /// </summary>
        /// <param name="firstName">First name of customer</param>
        /// <param name="lastName">Last name of customer</param>
        /// <param name="inStore">Is this search being done in store</param>
        /// <returns></returns>
        public List<Customer> GetCustomerByName(string firstName, string lastName, bool inStore)
        {
            List<Customer> customerReturn = new List<Customer>();

            try
            {
                DataTable userTable = tansDataAccess.ExecuteCUSTOMER_INFO_BY_CUSTOMER_NAME(functionsClass.CleanUp(firstName), functionsClass.CleanUp(lastName));
                
                // Were any customer records returned?
                if (userTable.Rows.Count > 0)
                {
                    // A single customer was found
                    if (userTable.Rows.Count == 1)
                    {
                        foreach (DataRow userReader in userTable.Rows)
                        {
                            // Set the Session variable
                            HttpContext.Current.Session["userID"] = userReader["USER_ID"].ToString().Trim();
                        }
                    }

                    // Single or multiple customers were found
                    foreach (DataRow userReader in userTable.Rows)
                    {
                        // Build a Customer object
                        Customer customerList = new Customer();
                        customerList.ID = Convert.ToInt32(userReader["USER_ID"].ToString().Trim());
                        customerList.LastName = userReader["CUST_LNAME"].ToString().Trim();
                        customerList.FirstName = userReader["CUST_FNAME"].ToString().Trim();
                        customerList.JoinDate = Convert.ToDateTime(userReader["CUST_JOIN"].ToString().Trim());
                        customerList.Plan = PlanTranslation(userReader["CUST_PLAN"].ToString().Trim());
                        customerList.OnlineUser = (userReader["CUST_ONLINE"].ToString().Trim() == "True" ? true : false);
                        customerReturn.Add(customerList);
                    }
                }
                else
                {
                    // Unable to locate a customer
                    Customer customerList = new Customer();
                    if (inStore)
                        customerList.Error = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL;
                    else
                        customerList.Error = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE;
                    customerReturn.Add(customerList);
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, firstName + " " + lastName, "Methods: GetCustomerIDByName");

                Customer customerList = new Customer();
                if (inStore)
                    customerList.Error = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL;
                else
                    customerList.Error = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE;
                customerReturn.Add(customerList);
            }

            return customerReturn;
        }

        /// <summary>
        /// Gets customer information by given transaction ID
        /// </summary>
        /// <param name="transactionId">Transaction ID</param>
        /// <returns></returns>
        public Customer GetCustomerNameByTransactionID(int transactionId)
        {
            Customer customerResponse = new Customer();
            try
            {
                DataTable infoTable = prodDataAccess.ExecuteTRANSACTION_BY_TRANSACTION_ID(transactionId);

                // Were main information records returned
                if (infoTable.Rows.Count > 0)
                {
                    string[] splitOther = infoTable.Rows[0]["TRNS_OTH"].ToString().Trim().Split(Convert.ToChar("-"));
                    string[] splitName;

                    if (splitOther[0].Length > 4)
                        splitName = splitOther[0].Split(Convert.ToChar(" "));
                    else
                        splitName = splitOther[1].Split(Convert.ToChar(" "));

                    // Build the customer object
                        customerResponse.ID = 0;
                        customerResponse.FirstName = splitName[0];
                        customerResponse.LastName = splitName[1];
                }
                else
                {
                    // Set error
                    customerResponse.Error = "We're sorry, but we are currently unable to access customer information.<br>";
                }
            }
            catch (Exception ex)
            {
                // Log error
                customerResponse.Error += "There was an error retrieving customer information.  Please try again.<br>";
                LogErrorMessage(ex, transactionId.ToString(), "Methods: GetCustomerNameByTransactionID");
            }

            return customerResponse;
        }

        public List<Customer> GetAllOnlineCustomers(bool onlyNewOnline)
        {
            List<Customer> onlineCustomerList = new List<Customer>();
            DataTable onlineCustomers = tansDataAccess.ExecuteONLINE_CUSTOMERS(onlyNewOnline);

            if (onlineCustomers.Rows.Count > 0)
            {
                foreach (DataRow customer in onlineCustomers.Rows)
                {
                    HOTBAL.Customer selectedCustomer = GetCustomerInformationByID(Convert.ToInt64(customer["USER_ID"]));

                    if (!String.IsNullOrEmpty(selectedCustomer.Error))
                    {
                        if (selectedCustomer.Error == "We're sorry, but we are currently unable to access customer information.<br>")
                        {
                            bool success = tansDataAccess.ExecuteDELETE_CUSTOMER_ONLINE(Convert.ToInt64(customer["USER_ID"]));
                            bool success2 = tansDataAccess.ExecuteDELETE_CUSTOMER_NEW_ONLINE(Convert.ToInt64(customer["USER_ID"]));
                        }
                    }
                    else
                    {
                        onlineCustomerList.Add(selectedCustomer);
                    }
                }
            }

            return onlineCustomerList;
        }

        /// <summary>
        /// Gets note information by a given note ID
        /// </summary>
        /// <param name="noteID">ID of note</param>
        /// <returns></returns>
        public CustomerNote GetNoteByNoteID(int noteID)
        {
            CustomerNote noteResponse = new CustomerNote();

            try
            {
                // Get note information
                DataTable noteTable = tansDataAccess.ExecuteCUSTOMER_NOTES_BY_NOTE_ID(noteID);
                
                // Were note records returned
                if (noteTable.Rows.Count > 0)
                {
                    foreach (DataRow noteReader in noteTable.Rows)
                    {
                        // Populate the note object
                        noteResponse.NoteID = Convert.ToInt32(noteReader["NOTE_ID"].ToString().Trim());
                        noteResponse.NoteText = noteReader["NOTE_TXT"].ToString().Trim();
                        noteResponse.NeedsUpgrade = (noteReader["NOTE_CHECK"].ToString().Trim() == "True" ? true : false);
                        noteResponse.OwedProduct = (noteReader["NOTE_OWED"].ToString().Trim() == "True" ? true : false);
                        noteResponse.OwesMoney = (noteReader["NOTE_OWES"].ToString().Trim() == "True" ? true : false);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, noteID.ToString(), "Methods: GetNoteByNoteID");
            }

            return noteResponse;
        }

        /// <summary>
        /// Adds a note to a customer record
        /// </summary>
        /// <param name="userID">Customer to which the note will be added</param>
        /// <param name="noteText">Text of the note</param>
        /// <param name="owesMoney">Is this a note about owing money</param>
        /// <param name="owedProduct">Is this a note about being owed a product</param>
        /// <param name="checkTransactions">Is this a note about needing to check transactions</param>
        /// <returns></returns>
        public bool AddCustomerNote(Int64 userID, string noteText, bool owesMoney, bool owedProduct, bool checkTransactions)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_CUSTOMER_NOTE(userID, noteText, (owesMoney == true ? 1 : 0), (owedProduct == true ? 1 : 0), (checkTransactions == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString() + "-" + noteText, "Methods: AddCustomerNote");
                return false;
            }
        }

        /// <summary>
        /// Edit a customer note
        /// </summary>
        /// <param name="noteID">ID of note</param>
        /// <param name="noteText">Text of note</param>
        /// <param name="owesMoney">Is this note about owing money</param>
        /// <param name="owedProduct">Is this note about being owed a product</param>
        /// <param name="checkTransactions">Is this a note to check transactions</param>
        /// <returns></returns>
        public bool EditCustomerNote(int noteID, string noteText, bool owesMoney, bool owedProduct, bool checkTransactions)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_NOTE_BY_NOTE_ID(noteID, functionsClass.LightCleanUp(noteText), 
                    (owesMoney == true ? 1 : 0), (owedProduct == true ? 1 : 0), (checkTransactions == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, noteID.ToString(), "Methods: EditCustomerNote");
                return false;
            }
        }

        /// <summary>
        /// Delete a customer note
        /// </summary>
        /// <param name="noteID">ID of note</param>
        /// <returns></returns>
        public bool DeleteCustomerNote(int noteID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_NOTE_BY_NOTE_ID(noteID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, noteID.ToString(), "Methods: DeleteCustomerNote");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool DeleteAllCustomerNotes(long customerId)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_NOTES_BY_CUSTOMER_ID(customerId);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerId.ToString(), "Methods: DeleteAllCustomerNotes");
                return false;
            }
        }

        /// <summary>
        /// Deletes a customer's online account
        /// </summary>
        /// <param name="customerID">ID of customer</param>
        /// <returns></returns>
        public bool DeleteCustomerOnlineAccount(Int64 customerID)
        {
            try
            {
                bool success = tansDataAccess.ExecuteDELETE_CUSTOMER_ONLINE(customerID);

                if (success)
                    // Mark their main account as no longer being online
                    success = UpdateOnlineStatus(customerID, false);

                return success;
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerID.ToString(), "Methods: DeleteCustomerOnlineAccount");
                return false;
            }
        }

        public List<long> DeleteInvalidOnlineAccounts()
        {
            try
            {
                List<long> deletedCustomers = new List<long>();

                // Get everyone who has signed up online
                DataTable newOnlineTable = tansDataAccess.ExecuteNEW_ONLINE_CUSTOMERS();

                // Loop through
                foreach (DataRow newOnline in newOnlineTable.Rows)
                {
                    long customerId = Convert.ToInt64(newOnline["CUST_TAN_ID"]);
                    // Get the customer information
                    HOTBAL.Customer customer = GetCustomerInformationByID(customerId);

                    // Was there an error?
                    if (!String.IsNullOrEmpty(customer.Error))
                    {
                        // Does the customer no longer exist?
                        if (customer.Error == "We're sorry, but we are currently unable to access customer information.<br>")
                        {
                            deletedCustomers.Add(customerId);
                            bool success = tansDataAccess.ExecuteDELETE_CUSTOMER_ONLINE(customerId);
                            bool success2 = tansDataAccess.ExecuteDELETE_CUSTOMER_NEW_ONLINE(customerId);
                        }
                    }
                }

                return deletedCustomers;
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, "", "Methods: DeleteInvalidOnlineAccounts");
                List<long> deletedCustomers = new List<long>();
                return deletedCustomers;
            }
        }

        /// <summary>
        /// Verifies a customer's log in information
        /// </summary>
        /// <param name="userName">Customer online user name</param>
        /// <param name="password">Customer online password</param>
        /// <returns></returns>
        public bool VerifyLogin(string userName, string password)
        {
            bool loginReturn = false;

            try
            {
                DataTable loginTable = tansDataAccess.ExecuteCUSTOMER_PUBLIC_LOGIN(functionsClass.LightCleanUp(userName), functionsClass.HashText(functionsClass.LightCleanUp(password)));
                
                // Were online account records found?
                if (loginTable.Rows.Count > 0)
                {
                    foreach (DataRow loginReader in loginTable.Rows)
                    {
                        // Set the session variable
                        HttpContext.Current.Session["userID"] = loginReader["TAN_UID"].ToString().Trim();
                    }

                    // Update the last logged in timestamp
                    if (UpdateTimestamp(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString().Trim())))
                        loginReturn = true;
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userName, "Methods: VerifyLogin");
            }

            return loginReturn;
        }

        /// <summary>
        /// Updates the last logged in timestamp
        /// </summary>
        /// <param name="userID">Customer ID</param>
        /// <returns></returns>
        private bool UpdateTimestamp(long userID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_LAST_LOGIN(userID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: UpdateTimestamp");
                return false;
            }
        }

        /// <summary>
        /// Updating only the password of online information
        /// </summary>
        /// <param name="customerID">ID of customer</param>
        /// <param name="newPassword">New password of customer</param>
        /// <returns></returns>
        public bool UpdatePasswordOnly(long customerID, string newPassword)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_PASSWORD(customerID, functionsClass.HashText(functionsClass.CleanUp(newPassword)));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerID.ToString(), "Methods: PasswordOnlyUpdate");
                return false;
            }
        }

        /// <summary>
        /// Updates the customer's online information
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="emailAddress">Customer e-mail address</param>
        /// <param name="specialsFlag">Does the customer want to be notified of specials</param>
        /// <returns></returns>
        public bool UpdateOnlineInfo(long customerID, string emailAddress, bool specialsFlag)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_ONLINE_INFO_BY_CUSTOMER_ID(customerID, emailAddress, (specialsFlag == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerID.ToString(), "Methods: UpdateOnlineInfo");
                return false;
            }
        }

        /// <summary>
        /// Checks if the online username is currently in use
        /// </summary>
        /// <param name="userName">Online username</param>
        /// <returns></returns>
        public bool UserNameCheck(string userName)
        {
            try
            {
                DataTable userReader = tansDataAccess.ExecuteCUSTOMER_TAN_ID_BY_USER_NAME(userName);

                if (userReader.Rows.Count > 0)
                {
                    // Someone already using that name
                    return false;
                }
                else
                {
                    // Free to use
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userName, "Methods: UserNameCheck");
                return false;
            }
        }

        /// <summary>
        /// Adds an online user account
        /// </summary>
        /// <param name="userID">Customer ID</param>
        /// <param name="userName">Username of customer</param>
        /// <param name="password">Password of customer</param>
        /// <param name="email">Email address of customer</param>
        /// <param name="specials">Does the customer want notification of specials</param>
        /// <returns></returns>
        public bool InsertOnlineUser(long userID, string userName, string password, string email, bool specials)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_CUSTOMER_ONLINE(userID, functionsClass.CleanUp(userName), 
                    functionsClass.HashText(functionsClass.CleanUp(password)), email, (specials == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: InsertOnlineUser");
                return false;
            }
        }

        /// <summary>
        /// Updates the customer's online status
        /// </summary>
        /// <param name="userID">Customer ID</param>
        /// <param name="isOnline">Is the customer online</param>
        /// <returns></returns>
        public bool UpdateOnlineStatus(long userID, bool isOnline)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_ONLINE_STATUS(userID, (isOnline ? 1 : 0));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: UpdateOnlineStatus");
                return false;
            }
        }

        /// <summary>
        /// Gets the customer ID by online username
        /// </summary>
        /// <param name="userName">Username of customer</param>
        /// <returns></returns>
        public long TanIDByUserName(string userName)
        {
            Int32 siteID = 0;

            try
            {
                DataTable userTable = tansDataAccess.ExecuteCUSTOMER_TAN_ID_BY_USER_NAME(userName);
                
                // Were any records returned for the username
                if (userTable.Rows.Count > 0)
                {
                    foreach (DataRow userReader in userTable.Rows)
                    {
                        siteID = Convert.ToInt32(userReader["TAN_UID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userName, "Methods: TanIDByUserName");
            }

            return siteID;
        }

        /// <summary>
        /// Adds a new customer
        /// </summary>
        /// <param name="firstName">First name of customer</param>
        /// <param name="lastName">Last name of customer</param>
        /// <param name="joinDate">Customer join date</param>
        /// <param name="fitzNumber">Fitzpatrick number of customer</param>
        /// <param name="plan">Customer tanning plan</param>
        /// <param name="renewalDate">Customer renewal date</param>
        /// <param name="remark">Remarks on customer</param>
        /// <param name="onlineUser">Is the customer an online user</param>
        /// <param name="newOnline">Did this customer originate online</param>
        /// <param name="specialFlag">Is this customer on a special</param>
        /// <param name="specialId">Id of the special</param>
        /// <param name="specialDate">Start date of the special</param>
        /// <returns></returns>
        public long InsertNewCustomer(string firstName, string lastName, DateTime joinDate, int fitzNumber, string plan, DateTime renewalDate, string remark, 
            bool onlineUser, bool newOnline, bool specialFlag, int specialId, DateTime specialDate)
        {
            long response = 0;

            try
            {
                response = tansDataAccess.ExecuteINSERT_CUSTOMER(functionsClass.LightCleanUp(firstName), functionsClass.LightCleanUp(lastName),
                    functionsClass.FormatDash(joinDate), fitzNumber, functionsClass.LightCleanUp(plan), functionsClass.FormatDash(renewalDate), 
                    functionsClass.CleanUp(remark), (onlineUser == true ? 1 : 0), (newOnline == true ? 1 :0), (specialFlag == true ? 1 :0),
                    specialId, functionsClass.FormatDash(specialDate));
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, functionsClass.CleanUp(firstName) + " " + functionsClass.CleanUp(lastName), "Methods: InsertNewCustomer");
            }

            return response;
        }

        public bool DeleteCustomerInformation(Int64 customerID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_CUSTOMER(customerID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerID.ToString(), "Methods: DeleteCustomerInformation");
                return false;
            }
        }

        /// <summary>
        /// Adds a new customer from the website
        /// </summary>
        /// <param name="firstName">Customer first name</param>
        /// <param name="lastName">Customer last name</param>
        /// <param name="address">Customer address</param>
        /// <param name="city">Customer city</param>
        /// <param name="state">Customer state</param>
        /// <param name="zipCode">Customer zip code</param>
        /// <param name="phoneNumber">Customer phone number</param>
        /// <param name="dateOfBirth">Customer date of birth</param>
        /// <param name="fitzNumber">Customer Fitzpatrick number</param>
        /// <param name="familyHistory">Does the customer have a family history of skin cancer</param>
        /// <param name="selfHistory">Does the customer have a history of skin cancer</param>
        /// <param name="tanID">Customer ID</param>
        /// <returns></returns>
        public bool InsertNewCustomerOnline(string firstName, string lastName, string address, string city, string state, string zipCode, string phoneNumber, DateTime dateOfBirth, int fitzNumber, bool familyHistory, bool selfHistory, long tanID)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_CUSTOMER_NEW(functionsClass.CleanUp(firstName), functionsClass.CleanUp(lastName), functionsClass.LightCleanUp(address), 
                    functionsClass.CleanUp(city), functionsClass.CleanUp(state), functionsClass.CleanUp(zipCode), functionsClass.LightCleanUp(phoneNumber), 
                    functionsClass.FormatDash(dateOfBirth), fitzNumber, (familyHistory == true ? 1 : 0), (selfHistory == true ? 1 : 0), 0, "", tanID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, firstName + " " + lastName, "Methods: InsertNewCustomerOnline");
                return false;
            }
        }

        /// <summary>
        /// Updates the customer's online/tanning agreement
        /// </summary>
        /// <param name="agreementName">Customer signed name</param>
        /// <param name="warned">Customer accepted warning</param>
        /// <param name="userID">Customer ID</param>
        /// <returns></returns>
        public bool UpdateCustomerAgreement(string agreementName, bool warned, long userID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_AGREEMENT(agreementName, (warned == true ? 1 : 0), userID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, agreementName, "Methods: UpdateCustomerAgreement");
                return false;
            }
        }

        public bool UpdateCustomerDetailInformation(long userID, string phoneNumber, string birthDate)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_DETAIL_INFO_BY_CUSTOMER_ID(userID, phoneNumber, birthDate);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: UpdateCustomerDetailInformation");
                return false;
            }
        }
        /// <summary>
        /// Updates customer information
        /// </summary>
        /// <param name="firstName">Customer first name</param>
        /// <param name="lastName">Customer last name</param>
        /// <param name="fitzNumber">Customer Fitzpatrick number</param>
        /// <param name="joinDate">Customer join date</param>
        /// <param name="renewalDate">Customer renewal date</param>
        /// <param name="plan">Customer tanning plan</param>
        /// <param name="remarks">Remarks about customer</param>
        /// <param name="lotionWarning">Does the customer use lotion that requires warning</param>
        /// <param name="restrictOnlinePayment">Is the customer allowed to make online payments</param>
        /// <param name="userID">Customer ID</param>
        /// <returns></returns>
        public bool UpdateCustomerInformation(string firstName, string lastName, int fitzNumber, DateTime joinDate, DateTime renewalDate,
            string plan, bool onSpecial, int specialLevelId, DateTime specialRenewalDate, string remarks, bool lotionWarning, bool restrictOnlinePayment, long userID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_BY_CUSTOMER_ID(functionsClass.CleanUp(firstName), functionsClass.CleanUp(lastName), 
                    fitzNumber, functionsClass.FormatDash(joinDate), functionsClass.FormatDash(renewalDate), plan, (onSpecial ? 1 : 0), specialLevelId, 
                    functionsClass.FormatDash(specialRenewalDate), functionsClass.CleanUp(remarks), (lotionWarning ? 1 : 0), (restrictOnlinePayment ? 1 : 0), userID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, functionsClass.CleanUp(firstName) + " " + functionsClass.CleanUp(lastName), "Methods: UpdateCustomerInformation");
                return false;
            }
        }

        /// <summary>
        /// Updates the customer's package renewal history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="transactionId"></param>
        /// <param name="purchaseDate"></param>
        /// <param name="renewalDate"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public bool UpdateCustomerHistory(long userId, long transactionId, string purchaseDate, string renewalDate, string packageName)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_CUSTOMER_HISTORY(userId, purchaseDate, renewalDate, packageName, transactionId);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userId.ToString(), "Methods: UpdateCustomerHistory");
                return false;
            }
        }

        /// <summary>
        /// Resets the online user password
        /// </summary>
        /// <param name="emailAddress">Email address of customer</param>
        /// <returns></returns>
        public string ResetUserPassword(string emailAddress)
        {
            string response = TansMessages.ERROR_GENERIC;
            Int64 userID = 0;

            try
            {
                DataTable emailTable = tansDataAccess.ExecuteCUSTOMER_BY_EMAIL(emailAddress);
                
                // Were online records found?
                if (emailTable.Rows.Count > 0)
                {
                    foreach (DataRow emailReader in emailTable.Rows)
                    {
                        userID = Convert.ToInt64(emailReader["TAN_UID"].ToString().Trim());
                    }

                    // Create new password
                    string newPassword = functionsClass.GeneratePassword(6);
                    bool resetResponse = tansDataAccess.ExecuteUPDATE_CUSTOMER_PASSWORD(userID, functionsClass.HashText(newPassword));

                    // Password reset, send e-mail notification
                    if (resetResponse)
                    {
                        functionsClass.SendMail(emailAddress, "password@hottropicaltans.net", "Password Request for HOTTropicalTans.net", "This e-mail is in response to a request on our site (HOTTropicalTans.net).  Your new password is " + newPassword + ".");
                        response = TansMessages.SUCCESS_MESSAGE;
                    }
                }
                else
                {
                    response = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE;
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: ResetUserPassword");
            }

            return response;
        }

        /// <summary>
        /// Save verification link information
        /// </summary>
        /// <param name="guid">Verification Guid</param>
        /// <param name="userID">Customer ID</param>
        /// <returns></returns>
        public bool SaveLinkInfo(string guid, long userID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_CUSTOMER_VERIFICATION_ID(guid.ToString(), userID);
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, userID.ToString(), "Methods: SaveLinkInfo");
                return false;
            }
        }

        /// <summary>
        /// Verify the e-mail address
        /// </summary>
        /// <param name="guid">Verification Guid</param>
        /// <param name="emailAddress">Customer e-mail address</param>
        /// <returns></returns>
        public string VerifyEmail(Guid guid, string emailAddress)
        {
            string response = TansMessages.ERROR_GENERIC;
            
            try
            {
                DataTable verifyTable = tansDataAccess.ExecuteCUSTOMER_EMAIL_VERIFICATION(emailAddress, guid.ToString());
                
                // Were verification records found?
                if (verifyTable.Rows.Count > 0)
                    {
                        foreach (DataRow verifyReader in verifyTable.Rows)
                        {
                            double expireDays = Double.Parse("5");
                            Int64 userID = Convert.ToInt64(verifyReader["TAN_UID"].ToString().Trim());
                            DateTime expireDate = Convert.ToDateTime(verifyReader["VERIFY_TMST"].ToString()).AddDays(expireDays);

                            // Has it been more than five days since the link was sent?
                            if (expireDate >= DateTime.Now)
                            {
                                // Flip verification flag in DB
                                bool success = tansDataAccess.ExecuteUPDATE_CUSTOMER_VERIFICATION(userID, 1);
                                if (success)
                                    response = TansMessages.SUCCESS_MESSAGE;
                                else
                                    response = TansMessages.ERROR_INVALID_VERIFY_LINK;
                            }
                            else
                            {
                                // Invalid link
                                response = TansMessages.ERROR_INVALID_VERIFY_LINK;
                            }
                        }
                    }
                    else
                    {
                        // Invalid link
                        response = TansMessages.ERROR_INVALID_VERIFY_LINK;
                    }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, guid.ToString(), "Methods: VerifyEmail");
            }

            return response;
        }

        /// <summary>
        /// Gets customer billing history information
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <returns></returns>
        public List<CustomerBilling> GetCustomerBillingByID(long customerID)
        {
            List<CustomerBilling> billingsResponse = new List<CustomerBilling>();
            
            try
            {
                DataTable transactionTable = tansDataAccess.ExecuteCUSTOMER_BILLING_BY_CUSTOMER_ID(customerID);
                
                // Were billing records found?
                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        // Build a CustomerBilling object
                        CustomerBilling transactionInfo = new CustomerBilling();
                        transactionInfo.PurchaseDate = Convert.ToDateTime(transactionReader["TAN_PURCHASE"].ToString());
                        transactionInfo.RenewalDate = Convert.ToDateTime(transactionReader["TAN_RENEWAL"].ToString());
                        transactionInfo.Package = transactionReader["TAN_PACKAGE"].ToString();
                        billingsResponse.Add(transactionInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetCustomerBillingByID");
            }

            return billingsResponse;
        }

        /// <summary>
        /// Merging customer data - billing
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerBillingHistory(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_BILLING(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerBillingHistory");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - notes
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerNotes(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_NOTES(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerNotes");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - transactions
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerTransactions(string toCustomer, string fromCustomer)
        {
            try
            {
                return prodDataAccess.ExecuteUPDATE_MERGE_TRANSACTIONS(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerTransactions");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - tanning
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerTanningLog(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_TANS(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerTanningLog");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - online accounts
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerOnlineSignUp(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_ONLINE_NEW(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerOnlineSignUp");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - login accounts
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerLogins(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_ONLINE(toCustomer, fromCustomer.Replace(",", "','"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerLogins");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - main information
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerMain(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MERGE_MAIN(toCustomer);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerMain");
                return false;
            }
        }

        /// <summary>
        /// Merging customer data - additional information
        /// </summary>
        /// <param name="toCustomer">Customer to which we are merging</param>
        /// <param name="fromCustomer">Customer(s) from which we are merging</param>
        /// <returns></returns>
        public bool MergeCustomerAdditional(string toCustomer, string fromCustomer)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_CUSTOMER(fromCustomer);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, toCustomer + "->" + fromCustomer, "Methods: MergeCustomerAdditional");
                return false;
            }
        }

        #endregion Customer Information

        #region Plan Information

        /// <summary>
        /// Translates the short plan name to the long plan name or special name
        /// </summary>
        /// <param name="planShortName">Short plan name</param>
        /// <returns></returns>
        public string PlanTranslation(string planShortName)
        {
            string returnName = "OTHER";
            if (functionsClass.CleanUp(planShortName).ToUpper().Trim() != returnName)
            {
                DataTable planTable = tansDataAccess.ExecutePLAN_BY_PLAN_NAME(functionsClass.CleanUp(planShortName).ToUpper().Trim());

                try
                {
                    if (planTable.Rows.Count > 0)
                    {
                        returnName = planTable.Rows[0]["PLAN_LONG"].ToString().Trim();
                    }
                    else
                    {
                        // This may be a Special
                        string[] planSplit = functionsClass.CleanUp(planShortName).ToUpper().Trim().Split(Convert.ToChar(" "));

                        if (planSplit[1] != null)
                        {
                            string[] specialSplit = planSplit[1].Split(Convert.ToChar("-"));

                            planTable = tansDataAccess.ExecuteSPECIALS_BY_SPECIAL_NAME(specialSplit[0].Trim());

                            if (planTable.Rows.Count > 0)
                            {
                                returnName = planTable.Rows[0]["SPEC_NME"].ToString().Trim();
                            }
                            else
                            {
                                returnName = specialSplit[0].Trim().ToUpper();
                            }
                        }
                        else
                        {
                            planTable = tansDataAccess.ExecuteSPECIALS_BY_SPECIAL_NAME(functionsClass.CleanUp(planShortName).ToUpper().Trim());

                            if (planTable.Rows.Count > 0)
                            {
                                returnName = planTable.Rows[0]["SPEC_NME"].ToString().Trim();
                            }
                            else
                            {
                                returnName = functionsClass.CleanUp(planShortName).ToUpper().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, functionsClass.CleanUp(planShortName).ToUpper().Trim(), "Methods: Plan Translation");
                }
            }

            return returnName;
        }

        /// <summary>
        /// Retrieves the plan information based on plan name
        /// </summary>
        /// <param name="planFullName">Short plan name</param>
        /// <returns></returns>
        public HOTBAL.Package PlanInformation(string planFullName)
        {
            HOTBAL.Package planInformation = new Package();
                         
            if (functionsClass.CleanUp(planFullName).ToUpper().Trim() != "OTHER")
            {
                DataTable planTable = tansDataAccess.ExecutePLAN_BY_PLAN_NAME(functionsClass.CleanUp(planFullName).ToUpper().Trim());

                try
                {
                    if (planTable.Rows.Count > 0)
                    {
                        planInformation.PackageID = Convert.ToInt32(planTable.Rows[0]["PLAN_ID"].ToString().Trim());
                        planInformation.PackageBed = planTable.Rows[0]["BED_TYPE"].ToString().Trim();
                        planInformation.PackageName = planTable.Rows[0]["PLAN_LONG"].ToString().Trim();
                        planInformation.PackageLength = Convert.ToInt32(planTable.Rows[0]["PLAN_LENGTH"].ToString().Trim());
                        planInformation.PackageNameShort = planTable.Rows[0]["PLAN_SHORT"].ToString().Trim();
                        planInformation.PackageTanCount = Convert.ToInt32(planTable.Rows[0]["PLAN_TAN_COUNT"].ToString().Trim());
                    }
                    else
                    {
                        // This may be a Special
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, functionsClass.CleanUp(planFullName).ToUpper().Trim(), "Methods: Plan Information (string)");
                }
            }

            return planInformation;
        }

        /// <summary>
        /// Retrieves the plan information based on plan name
        /// </summary>
        /// <param name="planFullName">Short plan name</param>
        /// <returns></returns>
        public HOTBAL.Package PlanInformation(int planId)
        {
            HOTBAL.Package planInformation = new Package();

            if (planId > 0)
            {
                DataTable planTable = tansDataAccess.ExecutePLAN_BY_PLAN_ID(planId);

                try
                {
                    if (planTable.Rows.Count > 0)
                    {
                        planInformation.PackageID = Convert.ToInt32(planTable.Rows[0]["PLAN_ID"].ToString().Trim());
                        planInformation.PackageBed = planTable.Rows[0]["BED_TYPE"].ToString().Trim();
                        planInformation.PackageName = planTable.Rows[0]["PLAN_LONG"].ToString().Trim();
                        planInformation.PackageLength = Convert.ToInt32(planTable.Rows[0]["PLAN_LENGTH"].ToString().Trim());
                        planInformation.PackageNameShort = planTable.Rows[0]["PLAN_SHORT"].ToString().Trim();
                        planInformation.PackageTanCount = Convert.ToInt32(planTable.Rows[0]["PLAN_TAN_COUNT"].ToString().Trim());
                    }
                    else
                    {
                        // This may be a Special
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, planId.ToString(), "Methods: Plan Information (int)");
                }
            }

            return planInformation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerInformation"></param>
        /// <returns></returns>
        public HOTBAL.Customer GetCustomerPlanDetails(HOTBAL.Customer customerInformation, string planName)
        {
            HOTBAL.Package planInformation = new Package();
            customerInformation.PlanId = 0;
            customerInformation.Plan = functionsClass.CleanUp(planName).ToUpper().Trim();

            if (functionsClass.CleanUp(planName).ToUpper().Trim() != "OTHER")
            {
                DataTable planTable = tansDataAccess.ExecutePLAN_BY_PLAN_NAME(functionsClass.CleanUp(planName).ToUpper().Trim());

                try
                {
                    if (planTable.Rows.Count > 0)
                    {
                        customerInformation.PlanId = Convert.ToInt32(planTable.Rows[0]["PLAN_ID"].ToString().Trim());
                        customerInformation.Plan = PlanTranslation(planName);
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, functionsClass.CleanUp(planName).ToUpper().Trim(), "Methods: GetCustomerPlanDetails (string)");
                }
            }

            return customerInformation;
        }

        /// <summary>
        /// Checks to see if the selected bed matches the customer's selected plan
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="bedType">Bed type the customer is requesting</param>
        /// <returns></returns>
        public string CheckCustomerPackage(Int64 customerID, string bedType)
        {
            string checkResponse = "";
            string planName = String.Empty;
            string planBed = String.Empty;
            string planLength = String.Empty;
            string[] planHold;
            string planBedType = String.Empty;
            
            try
            {
                DataTable infoTable = tansDataAccess.ExecuteCUSTOMER_INFO_BY_CUSTOMER_ID(customerID);
                //Get customer's plan
                if (infoTable.Rows.Count > 0)
                {
                    foreach (DataRow infoReader in infoTable.Rows)
                    {
                        planName = infoReader["CUST_PLAN"].ToString().Trim().ToUpper();
                        if (planName != "OTHER")
                        {
                            planHold = planName.Split(Convert.ToChar(" "));
                            planBed = planHold[0];
                            planLength = planHold[1];
                        }
                    }

                    try
                    {
                        //Get plan's bed
                        if (planName != "OTHER")
                        {
                            infoTable = tansDataAccess.ExecutePLAN_BY_PLAN_NAME(planName);

                            if (infoTable.Rows.Count > 0)
                            {
                                foreach (DataRow infoReader in infoTable.Rows)
                                {
                                    planBedType = infoReader["BED_TYPE"].ToString().Trim();
                                }

                                //Compare the bed type the selected with the bed type in their plan
                                if (bedType == planBedType)
                                {
                                    checkResponse = TansMessages.SUCCESS_MESSAGE;
                                }
                                else
                                {
                                    if (planBed == "BB")
                                    {
                                        //Has a Super Bed plan
                                        if (planLength == "SINGLE")
                                        {
                                            //On singles
                                            checkResponse = TansMessages.PLAN_SINGLES;
                                        }
                                        else if (bedType == "MY")
                                        {
                                            //Wants to do Mystic
                                            checkResponse = TansMessages.PLAN_MYSTIC;
                                        }
                                    }
                                    else if (planBed == "SB")
                                    {
                                        if (planLength == "SINGLE")
                                        {
                                            //On singles
                                            checkResponse = TansMessages.PLAN_SINGLES;
                                        }
                                        else if (bedType == "BB")
                                        {
                                            //Wants to do super bed
                                            checkResponse = TansMessages.PLAN_UPGRADE;
                                        }
                                        else if (bedType == "MY")
                                        {
                                            //Wants to do Mystic
                                            checkResponse = TansMessages.PLAN_MYSTIC;
                                        }
                                    }
                                    else if (planBed == "MY")
                                    {
                                        if (bedType != "MY")
                                        {
                                            //Wants to do something other than Mystic
                                            checkResponse = TansMessages.PLAN_SINGLES;
                                        }
                                    }
                                    else
                                    {
                                        // May be on specials - check
                                        checkResponse = TansMessages.PLAN_GENERIC;
                                    }
                                }
                            }
                            else
                            {
                                checkResponse = TansMessages.PLAN_GENERIC;
                            }
                        }
                        else
                        {
                            checkResponse = TansMessages.PLAN_GENERIC;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogErrorMessage(ex, customerID.ToString(), "Methods: CustomerPackageCheckPlan");
                        checkResponse = TansMessages.ERROR_GENERIC;
                    }
                }
                else
                {
                    checkResponse = TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, customerID.ToString(), "Methods: CustomerPackageCheck");
                checkResponse = TansMessages.ERROR_GENERIC;
            }

            return checkResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Special> GetAllSpecials()
        {
            List<Special> returnSpecials = new List<Special>();
            
            try
            {
                DataTable specialTable = tansDataAccess.ExecuteALL_ACTIVE_SPECIALS();
                if (specialTable.Rows.Count > 0)
                {
                    foreach (DataRow specialReader in specialTable.Rows)
                    {
                        Special specialList = new Special();
                        specialList.SpecialID = Convert.ToInt32(specialReader["SPEC_ID"].ToString().Trim());
                        specialList.SpecialName = specialReader["SPEC_NME"].ToString().Trim();
                        specialList.SpecialProductID = Convert.ToInt32(specialReader["PROD_ID"].ToString().Trim());
                        specialList.SpecialShortName = specialReader["SPEC_SHORT_NME"].ToString().Trim();
                        specialList.SpecialLength = Convert.ToInt32(specialReader["SPEC_LENGTH"].ToString().Trim());
                        specialList.SpecialActive = (specialReader["SPEC_ACTV"].ToString().Trim() == "True" ? true : false);
                        returnSpecials.Add(specialList);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetSpecials");
            }

            return returnSpecials;
        }

        public Special GetSpecialByID(int specialID)
        {
            Special returnSpecial = new Special();
            
            try
            {
                DataTable planTable = tansDataAccess.ExecuteSPECIALS_BY_SPECIAL_ID(specialID);
                if (planTable.Rows.Count > 0)
                {
                    foreach (DataRow planReader in planTable.Rows)
                    {
                        //SPEC_ID, SPEC_NME, SPEC_SHORT_NME, PROD_ID, SPEC_ACTV
                        returnSpecial.SpecialName = planReader["SPEC_NME"].ToString().Trim();
                        returnSpecial.SpecialID = specialID;
                        returnSpecial.SpecialActive = (planReader["SPEC_ACTV"].ToString().Trim() == "True" ? true : false);
                        returnSpecial.SpecialProductID = Convert.ToInt32(planReader["PROD_ID"].ToString().Trim());
                        returnSpecial.SpecialShortName = planReader["SPEC_SHORT_NME"].ToString().Trim();
                        returnSpecial.SpecialLength = Convert.ToInt32(planReader["SPEC_LENGTH"].ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialID.ToString(), "Methods: GetSpecialByID");
            }

            return returnSpecial;
        }

        public List<SpecialLevel> GetLevelsBySpecialID(int specialID)
        {
            List<SpecialLevel> returnSpecials = new List<SpecialLevel>();
            
            try
            {
                DataTable specialTable = tansDataAccess.ExecuteSPECIAL_LEVELS_BY_SPECIAL_ID(specialID);
                if (specialTable.Rows.Count > 0)
                {
                    foreach (DataRow specialReader in specialTable.Rows)
                    {
                        SpecialLevel specialList = new SpecialLevel();
                        specialList.SpecialID = Convert.ToInt32(specialReader["SPEC_ID"].ToString().Trim());
                        specialList.SpecialLevelID = Convert.ToInt32(specialReader["SPEC_LEVEL_ID"].ToString().Trim());
                        specialList.SpecialLevelBed = specialReader["SPEC_LEVEL_BED"].ToString().Trim();
                        specialList.SpecialLevelLength = Convert.ToInt32(specialReader["SPEC_LEVEL_LENGTH"].ToString().Trim());
                        specialList.SpecialLevelOrder = Convert.ToInt32(specialReader["SPEC_LEVEL_ORDER"].ToString().Trim());
                        returnSpecials.Add(specialList);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialID.ToString(), "Methods: GetLevelsBySpecialID");
            }

            return returnSpecials;
        }

        public SpecialLevel GetSpecialLevelByLevelID(int specialLevel)
        {
            SpecialLevel returnSpecial = new SpecialLevel();
            
            try
            {
                DataTable planTable = tansDataAccess.ExecuteSPECIAL_LEVEL_BY_LEVEL_ID(specialLevel);
                if (planTable.Rows.Count > 0)
                {
                    foreach (DataRow planReader in planTable.Rows)
                    {
                        returnSpecial.SpecialID = Convert.ToInt32(planReader["SPEC_ID"].ToString().Trim());
                        returnSpecial.SpecialLevelBed = planReader["SPEC_LEVEL_BED"].ToString().Trim();
                        returnSpecial.SpecialLevelID = Convert.ToInt32(planReader["SPEC_LEVEL_ID"].ToString().Trim());
                        returnSpecial.SpecialLevelLength = Convert.ToInt32(planReader["SPEC_LEVEL_LENGTH"].ToString().Trim());
                        returnSpecial.SpecialLevelOrder = Convert.ToInt32(planReader["SPEC_LEVEL_ORDER"].ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialLevel.ToString(), "Methods: GetSpecialLevelByLevelID");
            }

            return returnSpecial;
        }

        public List<Package> GetAllPackages()
        {
            List<Package> returnPackages = new List<Package>();
            
            try
            {
                DataTable packageTable = tansDataAccess.ExecuteALL_ACTIVE_PLANS();
                if (packageTable.Rows.Count > 0)
                {
                    foreach (DataRow packageReader in packageTable.Rows)
                    {
                        Package packagesList = new Package();
                        packagesList.PackageID = Convert.ToInt32(packageReader["PLAN_ID"].ToString());
                        packagesList.PackageBed = packageReader["BED_TYPE"].ToString();
                        packagesList.PackageName = packageReader["PLAN_LONG"].ToString();
                        packagesList.PackageLength = Convert.ToInt32(packageReader["PLAN_LENGTH"].ToString());
                        packagesList.PackageNameShort = packageReader["PLAN_SHORT"].ToString();
                        returnPackages.Add(packagesList);
                    }
                }
                else
                {
                    //No packages found
                    LogErrorMessage(new Exception("No Active Packages"), "", "Methods: GetPackages");
                    Package packagesList = new Package();
                    returnPackages.Add(packagesList);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetPackages");
                Package packagesList = new Package();
                returnPackages.Add(packagesList);
            }

            return returnPackages;
        }

        public Package GetPackageByPackageID(int packageID)
        {
            Package returnPackage = new Package();
            
            try
            {
                DataTable packageTable = tansDataAccess.ExecutePLAN_BY_PLAN_ID(packageID);
                if (packageTable.Rows.Count > 0)
                {
                    returnPackage.PackageID = Convert.ToInt32(packageTable.Rows[0]["PLAN_ID"].ToString().Trim());
                    returnPackage.ProductID = Convert.ToInt32(packageTable.Rows[0]["PROD_ID"].ToString().Trim());
                    returnPackage.PackageBed = packageTable.Rows[0]["BED_TYPE"].ToString().Trim();
                    returnPackage.PackageName = packageTable.Rows[0]["PLAN_LONG"].ToString().Trim();
                    returnPackage.PackageLength = Convert.ToInt32(packageTable.Rows[0]["PLAN_LENGTH"].ToString().Trim());
                    returnPackage.PackageNameShort = packageTable.Rows[0]["PLAN_SHORT"].ToString().Trim();
                    returnPackage.PackageTanCount = Convert.ToInt32(packageTable.Rows[0]["PLAN_TAN_COUNT"].ToString().Trim());

                    DataTable productTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_ID(Convert.ToInt32(packageTable.Rows[0]["PROD_ID"].ToString().Trim()));
                    if (productTable.Rows.Count > 0)
                    {
                        returnPackage.PackageBarCode = productTable.Rows[0]["PROD_CODE"].ToString().Trim();
                        returnPackage.PackagePrice = productTable.Rows[0]["PROD_PRICE"].ToString().Trim();
                        returnPackage.PackageAvailableInStore = (productTable.Rows[0]["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageAvailableOnline = (productTable.Rows[0]["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSaleOnline = (productTable.Rows[0]["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSaleStore = (productTable.Rows[0]["PROD_SALE_STORE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSalePrice = productTable.Rows[0]["PROD_SALE_PRICE"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, packageID.ToString(), "Methods: GetPackageByPackageID");
            }

            return returnPackage;
        }

        public Package GetPackageByProductID(int productId)
        {
            Package returnPackage = new Package();

            try
            {
                DataTable packageTable = tansDataAccess.ExecutePLAN_BY_PRODUCT_ID(productId);
                if (packageTable.Rows.Count > 0)
                {
                    returnPackage.PackageID = Convert.ToInt32(packageTable.Rows[0]["PLAN_ID"].ToString().Trim());
                    returnPackage.ProductID = Convert.ToInt32(packageTable.Rows[0]["PROD_ID"].ToString().Trim());
                    returnPackage.PackageBed = packageTable.Rows[0]["BED_TYPE"].ToString().Trim();
                    returnPackage.PackageName = packageTable.Rows[0]["PLAN_LONG"].ToString().Trim();
                    returnPackage.PackageLength = Convert.ToInt32(packageTable.Rows[0]["PLAN_LENGTH"].ToString().Trim());
                    returnPackage.PackageNameShort = packageTable.Rows[0]["PLAN_SHORT"].ToString().Trim();

                    DataTable productTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_ID(productId);
                    if (productTable.Rows.Count > 0)
                    {
                        returnPackage.PackageBarCode = productTable.Rows[0]["PROD_CODE"].ToString().Trim();
                        returnPackage.PackagePrice = productTable.Rows[0]["PROD_PRICE"].ToString().Trim();
                        returnPackage.PackageAvailableOnline = (productTable.Rows[0]["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageAvailableInStore = (productTable.Rows[0]["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSaleOnline = (productTable.Rows[0]["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSaleStore = (productTable.Rows[0]["PROD_SALE_STORE"].ToString().Trim() == "True" ? true : false);
                        returnPackage.PackageSalePrice = productTable.Rows[0]["PROD_SALE_PRICE"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productId.ToString(), "Methods: GetPackageByProductID");
            }

            return returnPackage;
        }

        public Int64 AddPackage(string shortName, string longName, string bedType, int packageLengthDays, Int64 productID)
        {
            Int64 productResponse = 0;

            try
            {
                productResponse = tansDataAccess.ExecuteINSERT_PACKAGE(functionsClass.CleanUp(shortName), functionsClass.CleanUp(longName), 
                    bedType, packageLengthDays, productID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, longName.ToString(), "Methods: AddPackage");
            }

            return productResponse;
        }

        public bool EditPackage(Int64 packageId, string shortName, string longName, string bedType, int packageLengthDays, Int64 productID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_PACKAGE_BY_PACKAGE_ID(packageId, functionsClass.CleanUp(shortName), functionsClass.CleanUp(longName), bedType, packageLengthDays);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, packageId.ToString(), "Methods: EditPackage");
                return false;
            }
        }

        public Int64 AddSpecial(string specialName, string specialShortName, Int64 productID)
        {
            Int64 productResponse = 0;

            try
            {
                productResponse = tansDataAccess.ExecuteINSERT_SPECIAL(functionsClass.CleanUp(specialName), functionsClass.CleanUp(specialShortName), productID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, functionsClass.CleanUp(specialName).ToString(), "Methods: AddSpecial");
            }

            return productResponse;
        }

        public bool AddSpecialLevel(Int64 specialID, string specialBed, int specialLength, int specialOrder)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_SPECIAL_LEVEL(specialID, specialBed, specialLength, specialOrder);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialBed.ToString(), "Methods: AddSpecialLevel");
                return false;
            }
        }

        public bool UpdateSpecial(Int64 specialID, string specialName, string specialShortName, Int64 productID, bool specialActive)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_SPECIAL_BY_SPECIAL_ID(specialID, functionsClass.CleanUp(specialName), 
                    functionsClass.CleanUp(specialShortName), productID, (specialActive == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialName.ToString(), "Methods: UpdateSpecial");
                return false;
            }
        }

        public bool UpdateSpecialLevel(int specialLevelID, Int64 specialID, string specialBed, int specialLength, int specialOrder)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_SPECIAL_LEVEL_BY_SPECIAL_LEVEL_ID(specialLevelID, specialID, specialBed, specialLength, specialOrder);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, specialBed.ToString(), "Methods: UpdateSpecialLevel");
                return false;
            }
        }

        #endregion Plan Information

        #region Appointment Information
        public string ScheduleAppointment(long customerID, string tanBed, string tanDate, string tanTime, string tanLocation, bool tanStore, bool tanReminder)
        {
            string responseString = String.Empty;

            // First, check the schedule
            if (ScheduleCheck(tanBed, tanDate, tanTime, tanLocation, 0))
            {
                // Bed/date/time/location available
                int tanID = SameDayTanCheck(customerID, tanDate, 0);

                if (tanID == 0)
                {
                    // No tan scheduled on same day
                    if (TwentyFourHourCheck(customerID, tanDate, tanTime))
                    {
                        // At least 24 hours between all appointments
                        if (tanStore)
                        {
                            // In-store schedule, don't do a package check and note
                        }
                        else
                        {
                            // Website schedule, check package and add notes as needed
                            string packageCheck = CheckCustomerPackage(customerID, tanBed);
                            bool noteResponse;

                            if (packageCheck == TansMessages.PLAN_UPGRADE)
                            {
                                // Add a note about upgrades
                                noteResponse = AddCustomerNote(customerID, TansMessages.NOTE_NEED_UPGRADE, false, false, true);
                            }
                            else if (packageCheck == TansMessages.PLAN_SINGLES)
                            {
                                // Add note about singles
                                noteResponse = AddCustomerNote(customerID, TansMessages.NOTE_NEED_SINGLES, false, false, true);
                            }
                            else if (packageCheck == TansMessages.PLAN_MYSTIC)
                            {
                                // Add note about mystic
                                noteResponse = AddCustomerNote(customerID, TansMessages.NOTE_NEED_MYSTIC, false, false, true);
                            }
                            else if (packageCheck == TansMessages.PLAN_GENERIC)
                            {
                                // Add note about checking package
                                noteResponse = AddCustomerNote(customerID, TansMessages.NOTE_NEED_PACKAGE_CHECK, false, false, true);
                            }
                        }

                        // Schedule appointment
                        try
                        {
                            bool success = tansDataAccess.ExecuteINSERT_TAN(customerID, tanBed, tanDate, tanTime, tanLocation, (tanStore == true ? 0 : 1), (tanReminder == true ? 1 : 0));
                            if (success)
                                responseString = TansMessages.SUCCESS_MESSAGE;
                        }
                        catch (Exception ex)
                        {
                            responseString = TansMessages.ERROR_GENERIC;
                            LogErrorMessage(ex, customerID.ToString(), "Methods: ScheduleAppointment");
                        }
                    }
                    else
                    {
                        // Doesn't have 24 hours between appointments
                        if (tanStore)
                        {
                            responseString = TansMessages.TAN_NOT24_STORE;
                        }
                        else
                        {
                            responseString = TansMessages.TAN_NOT24_SITE;
                        }
                    }
                }
                else
                {
                    // Already has a tan scheduled that day
                    if (tanStore)
                    {
                        responseString = TansMessages.TAN_SAME_DAY_STORE.Replace("@UserID", customerID.ToString()).Replace("@TanID", tanID.ToString());
                    }
                    else
                    {
                        responseString = TansMessages.TAN_SAME_DAY_SITE.Replace("@Date", tanDate);
                    }
                }
            }
            else
            {
                // Bed/date/time/location unavailable
                responseString = TansMessages.TAN_ALREADY_TAKEN.Replace("@Date", tanDate).Replace("@Time", tanTime).Replace("@Bed", tanBed);
            }

            return responseString;
        }

        public string UpdateAppointment(int tanID, long customerID, string tanBed, string tanDate, string tanTime, string tanLocation, int tanLength, bool swap)
        {
            string responseString = TansMessages.ERROR_GENERIC;

            // If we're swapping beds, don't do the checks
            if (swap)
            {
                bool success = tansDataAccess.ExecuteUPDATE_TAN_BY_TAN_ID(tanID, tanBed, tanDate, tanTime, tanLocation, tanLength);
                if (success)
                    responseString = TansMessages.SUCCESS_MESSAGE;
            }
            else
            {
                //First, check the schedule
                if (ScheduleCheck(tanBed, tanDate, tanTime, tanLocation, tanID))
                {
                    //Bed/date/time/location available
                    int sameDayTanID = 0;
                    sameDayTanID = SameDayTanCheck(customerID, tanDate, tanID);

                    if (sameDayTanID == 0)
                    {
                        //No tan scheduled on same day
                        if (TwentyFourHourCheck(customerID, tanDate, tanTime))
                        {
                            //At least 24 hours between all appointments
                            //Schedule appointment
                            try
                            {
                                bool success = tansDataAccess.ExecuteUPDATE_TAN_BY_TAN_ID(tanID, tanBed, tanDate, tanTime, tanLocation, tanLength);
                                if (success)
                                    responseString = TansMessages.SUCCESS_MESSAGE;
                            }
                            catch (Exception ex)
                            {
                                LogErrorMessage(ex, tanID.ToString(), "Methods: UpdateAppointment");
                            }
                        }
                        else
                        {
                            //Doesn't have 24 hours between appointments
                            responseString = TansMessages.TAN_NOT24_STORE;
                        }
                    }
                    else
                    {
                        //Already has a tan scheduled that day
                        responseString = TansMessages.TAN_SAME_DAY_STORE.Replace("@UserID", customerID.ToString()).Replace("@TanID", sameDayTanID.ToString());
                    }
                }
                else
                {
                    //Bed/date/time/location unavailable
                    responseString = TansMessages.TAN_ALREADY_TAKEN.Replace("@Date", tanDate).Replace("@Time", tanTime).Replace("@Bed", tanBed);
                }
            }

            return responseString;
        }

        public bool DeleteAppointment(int TanID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_TAN_BY_TAN_ID(TanID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TanID.ToString(), "Methods: DeleteAppointment");
                return false;
            }
        }

        public bool ScheduleCheck(string tanBed, string tanDate, string tanTime, string tanLocation, int tanID)
        {
            bool schedule = true;
            
            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTAN_TIME_TAKEN(tanBed, tanDate, tanLocation, tanTime);
                if (timeTable.Rows.Count > 0)
                {
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        if (timeReader["TAN_ID"].ToString() == tanID.ToString())
                        {
                            // Same tan as we were looking for, allow
                            schedule = true;
                        }
                        else
                        {
                            // Not the tan we were looking for, disallow
                            schedule = false;
                        }
                    }
                }
                else
                {
                    // Free to schedule
                    schedule = true;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, tanID.ToString(), "Methods: ScheduleCheck");
                schedule = false;
            }

            return schedule;
        }

        public int SameDayTanCheck(Int64 userID, string tanDate, int tanID)
        {
            int sameDayTanID = 0;

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteCUSTOMER_TAN_BY_CUSTOMER_ID_DATE(userID, tanDate);
                if (timeTable.Rows.Count > 0)
                {
                    //Customer already has a tan scheduled
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        if (Convert.ToInt32(timeReader["TAN_ID"].ToString()) == tanID)
                        {
                            sameDayTanID = 0;
                        }
                        else
                        {
                            sameDayTanID = Convert.ToInt32(timeReader["TAN_ID"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, tanID.ToString(), "Methods: SameDayTanCheck");
            }

            return sameDayTanID;
        }

        public bool TwentyFourHourCheck(Int64 userID, string tanDate, string tanTime)
        {
            bool hourCheck = true;

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteCUSTOMER_24_HOUR_CHECK(userID, functionsClass.FormatDash(Convert.ToDateTime(tanDate).AddDays(-1)));
                if (timeTable.Rows.Count > 0)
                {
                    // Had an appointment the day before requested, see if there are 24 hours between
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        DateTime requestedTime = Convert.ToDateTime(tanTime);
                        DateTime yesterdayTime = Convert.ToDateTime(timeReader["TAN_TIME"].ToString());

                        TimeSpan diff = requestedTime.Subtract(yesterdayTime);
                        //LogErrorMessage(new Exception("Previous"), diff.Hours.ToString() + ":" + diff.Minutes.ToString(), "Methods: TwentyFourHourCheck");
                
                        if ((diff.Hours < 0) || (diff.Minutes < 0))
                        {
                            // Is this appointment older than today?
                            if (requestedTime < DateTime.Now)
                            {
                                // Did they actually tan?
                                if (Convert.ToInt32(timeReader["TAN_LENGTH"]) > 0)
                                {
                                    // Hasn't been 24 hours, and kept appointment on previous day
                                    hourCheck = false;
                                    break;
                                }
                                else
                                {
                                    // Did not tan on previous date, and the previous date has already passed, can tan
                                }
                            }
                            else
                            {
                                // Hasn't been 24 hours and the dates are all future
                                hourCheck = false;
                                break;
                            }
                        }
                    }
                }

                timeTable.Clear();
                timeTable = tansDataAccess.ExecuteCUSTOMER_24_HOUR_CHECK(userID, functionsClass.FormatDash(Convert.ToDateTime(tanDate).AddDays(1)));
                if (timeTable.Rows.Count > 0)
                {
                    // Has an appointment the day after requested, see if there are 24 hours between
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        DateTime requestedTime = Convert.ToDateTime(tanTime);
                        DateTime tomorrowTime = Convert.ToDateTime(timeReader["TAN_TIME"].ToString());

                        TimeSpan diff = requestedTime.Subtract(tomorrowTime);
                        //LogErrorMessage(new Exception("Next"), diff.Hours.ToString() + ":" + diff.Minutes.ToString(), "Methods: TwentyFourHourCheck");

                        if ((diff.Hours > 0) || (diff.Minutes > 0))
                        {
                            // Hasn't been 24 hours
                            hourCheck = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, userID.ToString() + "-" + tanTime, "Methods: TwentyFourHourCheck");
                hourCheck = false;
            }

            return hourCheck;
        }

        #endregion Appointment Information

        #region Product Information

        public List<Product> GetAllProducts()
        {
            List<Product> productsList = new List<Product>();
            
            try
            {
                DataTable productTable = prodDataAccess.ExecuteALL_ACTIVE_PRODUCTS();
                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow productReader in productTable.Rows)
                    {
                        Product productResponse = new Product();
                        productResponse.ProductID = Convert.ToInt32(productReader["PROD_ID"].ToString().Trim());
                        productResponse.ProductName = productReader["PROD_NAME"].ToString().Trim();
                        productResponse.ProductDescription = productReader["PROD_DESC"].ToString().Trim();
                        productResponse.ProductAvailableOnline = (productReader["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductAvailableInStore = (productReader["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductPrice = Convert.ToDouble(productReader["PROD_PRICE"].ToString().Trim());
                        productResponse.ProductSaleOnline = (productReader["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSalePrice = Convert.ToDouble(productReader["PROD_SALE_PRICE"].ToString().Trim());
                        productResponse.ProductSubType = productReader["PROD_SUB_TYPE"].ToString().Trim();
                        productResponse.ProductCode = productReader["PROD_CODE"].ToString().Trim();
                        productResponse.ProductTaxable = (productReader["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductType = productReader["PROD_TYPE"].ToString().Trim();
                        productResponse.ProductFileName = productReader["PROD_FILE_NAME"].ToString().Trim();
                        productResponse.ProductCount = Convert.ToInt32(productReader["PROD_COUNT"].ToString().Trim());
                        productsList.Add(productResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetAllProducts");
            }

            return productsList;
        }

        public Product GetProductByID(Int32 productID)
        {
            Product productResponse = new Product();
            
            try
            {
                DataTable productTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_ID(productID);
                if (productTable.Rows.Count > 0)
                {
                    productResponse.ProductID = Convert.ToInt32(productTable.Rows[0]["PROD_ID"].ToString().Trim());
                        productResponse.ProductName = productTable.Rows[0]["PROD_NAME"].ToString().Trim();
                        productResponse.ProductDescription = productTable.Rows[0]["PROD_DESC"].ToString().Trim();
                        productResponse.ProductAvailableOnline = (productTable.Rows[0]["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductAvailableInStore = (productTable.Rows[0]["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductPrice = Convert.ToDouble(productTable.Rows[0]["PROD_PRICE"].ToString().Trim());
                        productResponse.ProductSaleInStore = (productTable.Rows[0]["PROD_SALE_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSaleOnline = (productTable.Rows[0]["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSalePrice = Convert.ToDouble(productTable.Rows[0]["PROD_SALE_PRICE"].ToString().Trim());
                        productResponse.ProductSubType = productTable.Rows[0]["PROD_SUB_TYPE"].ToString().Trim();
                        productResponse.ProductCode = productTable.Rows[0]["PROD_CODE"].ToString().Trim();
                        productResponse.ProductTaxable = (productTable.Rows[0]["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductType = productTable.Rows[0]["PROD_TYPE"].ToString().Trim();
                        productResponse.ProductFileName = productTable.Rows[0]["PROD_FILE_NAME"].ToString().Trim();
                        productResponse.ProductCount = Convert.ToInt32(productTable.Rows[0]["PROD_COUNT"].ToString().Trim());
                }
                else
                {
                    // No product of that ID
                    productResponse.ProductID = 0;
                    productResponse.ProductName = TansMessages.ERROR_PRODUCT_NO_INFO;
                    LogErrorMessage(new Exception("No product"), productID.ToString(), "Methods: GetProductByID");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productID.ToString(), "Methods: GetProductByID");
                productResponse.ProductID = 0;
                productResponse.ProductName = TansMessages.ERROR_PRODUCT_NO_INFO;
            }

            return productResponse;
        }

        public List<Product> GetProductByName(string productName)
        {
            List<Product> productsList = new List<Product>();

            try
            {
                DataTable productTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_NAME(productName);
                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in productTable.Rows)
                    {
                        Product productResponse = new Product();
                        productResponse.ProductID = Convert.ToInt32(dr["PROD_ID"].ToString().Trim());
                        productResponse.ProductName = dr["PROD_NAME"].ToString().Trim();
                        productResponse.ProductDescription = dr["PROD_DESC"].ToString().Trim();
                        productResponse.ProductAvailableOnline = (dr["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductAvailableInStore = (dr["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductPrice = Convert.ToDouble(dr["PROD_PRICE"].ToString().Trim());
                        productResponse.ProductSaleInStore = (dr["PROD_SALE_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSaleOnline = (dr["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSalePrice = Convert.ToDouble(dr["PROD_SALE_PRICE"].ToString().Trim());
                        productResponse.ProductSubType = dr["PROD_SUB_TYPE"].ToString().Trim();
                        productResponse.ProductCode = dr["PROD_CODE"].ToString().Trim();
                        productResponse.ProductTaxable = (dr["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductType = dr["PROD_TYPE"].ToString().Trim();
                        productResponse.ProductFileName = dr["PROD_FILE_NAME"].ToString().Trim();
                        productResponse.ProductCount = Convert.ToInt32(dr["PROD_COUNT"].ToString().Trim());
                        productsList.Add(productResponse);
                    }
                }
                else
                {
                    // No product by that name
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productName.ToString(), "Methods: GetProductByName");
                Product productResponse = new Product();
                productResponse.ProductName = TansMessages.ERROR_PRODUCT_NO_INFO;
                productsList.Add(productResponse);
            }

            return productsList;
        }

        public Int64 AddProduct(string productName, string productDescription, string productType, string productSubType, string productBarcode,
            string productPrice, bool productTax, string salePrice, bool onSaleOnline, bool onSaleInStore, bool availableOnline, bool availableInStore)
        {
            Int64 productResponse = 0;

            try
            {
                productResponse = prodDataAccess.ExecuteINSERT_PRODUCT(functionsClass.CleanUp(productName), functionsClass.CleanUp(productDescription), productType,
                    productSubType, productBarcode, productPrice, (productTax == true ? 1 : 0), salePrice, (onSaleOnline == true ? 1 : 0),
                    (onSaleInStore == true ? 1 : 0), (availableOnline == true ? 1 : 0), (availableInStore == true ? 1 : 0));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productName.ToString(), "Methods: AddProduct");
            }

            return productResponse;
        }

        public bool InsertProductInventory(Int64 productID, int itemCount, string location)
        {
            try
            {
                return prodDataAccess.ExecuteINSERT_PRODUCT_INVENTORY(productID, itemCount, location);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productID.ToString(), "Methods: InsertProductInventory");
                return false;
            }
        }

        public bool EditProduct(Int64 productID, string productName, string productDescription, string productType, string productSubType, string productBarcode,
            string productPrice, bool productTax, string salePrice, bool onSaleOnline, bool onSaleInStore, bool availableOnline, bool availableInStore, int productInventory)
        {
            try
            {
                bool isSuccessful = prodDataAccess.ExecuteUPDATE_PRODUCT_BY_PRODUCT_ID(productID, functionsClass.CleanUp(productName), functionsClass.CleanUp(productDescription),
                    productType, productSubType, productBarcode, productPrice, (productTax == true ? 1 : 0), salePrice, (onSaleOnline == true ? 1 : 0),
                    (onSaleInStore == true ? 1 : 0), (availableOnline == true ? 1 : 0), (availableInStore == true ? 1 : 0));

                if (isSuccessful)
                    return UpdateProductInventory(productID, productInventory, "W");
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productName.ToString(), "Methods: EditProduct");
                return false;
            }
        }

        public bool UpdateProductStatus(Int64 productID, bool isActive)
        {
            try
            {
                return prodDataAccess.ExecuteUPDATE_PRODUCT_DISPLAY_BY_PRODUCT_ID(productID, (isActive == true ? "1" : "0"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productID.ToString(), "Methods: UpdateProductStatus");
                return false;
            }
        }

        public bool UpdateProductInventory(Int64 productID, int itemCount, string location)
        {
            try
            {
                return prodDataAccess.ExecuteUPDATE_PRODUCT_INVENTORY(productID, itemCount, location);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, productID.ToString(), "Methods: UpdateProductInventory");
                return false;
            }
        }

        public List<Product> GetAccessoriesByType(string ProductSubType)
        {
            List<Product> productsList = new List<Product>();
            
            try
            {
                DataTable productTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_SUB_TYPE_ACTIVE_ONLINE(functionsClass.CleanUp(ProductSubType));
                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow productReader in productTable.Rows)
                    {
                        Product productResponse = new Product();
                        productResponse.ProductID = Convert.ToInt32(productReader["PROD_ID"].ToString().Trim());
                        productResponse.ProductName = productReader["PROD_NAME"].ToString().Trim();
                        productResponse.ProductDescription = productReader["PROD_DESC"].ToString().Trim();
                        productResponse.ProductAvailableOnline = (productReader["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductAvailableInStore = (productReader["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductPrice = Convert.ToDouble(productReader["PROD_PRICE"].ToString().Trim());
                        productResponse.ProductSaleOnline = (productReader["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSalePrice = Convert.ToDouble(productReader["PROD_SALE_PRICE"].ToString().Trim());
                        productResponse.ProductSubType = productReader["PROD_SUB_TYPE"].ToString().Trim();
                        productResponse.ProductCode = productReader["PROD_CODE"].ToString().Trim();
                        productResponse.ProductTaxable = (productReader["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductType = productReader["PROD_TYPE"].ToString().Trim();
                        productResponse.ProductFileName = productReader["PROD_FILE_NAME"].ToString().Trim();
                        productResponse.ProductCount = Convert.ToInt32(productReader["PROD_COUNT"].ToString().Trim());
                        productsList.Add(productResponse);
                    }
                }
                else
                {
                    LogErrorMessage(new Exception("No Product Type Found"), ProductSubType, "Methods: GetAccessoriesByType");
                    Product productResponse = new Product();
                    productResponse.ProductName = TansMessages.ERROR_NO_PRODUCT_TYPE;
                    productsList.Add(productResponse);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ProductSubType, "Methods: GetAccessoriesByType");
                Product productResponse = new Product();
                productResponse.ProductName = TansMessages.ERROR_NO_PRODUCT_TYPE;
                productsList.Add(productResponse);
            }

            return productsList;
        }

        public List<Product> GetLotionsByType(string ProductSubType)
        {
            List<Product> productsList = new List<Product>();
            
            try
            {
                DataTable productTable = prodDataAccess.ExecuteLOTIONS_BY_PRODUCT_SUB_TYPE_ACTIVE_ONLINE(functionsClass.CleanUp(ProductSubType));
                if (productTable.Rows.Count > 0)
                {
                    foreach (DataRow productReader in productTable.Rows)
                    {
                        Product productResponse = new Product();
                        productResponse.ProductID = Convert.ToInt32(productReader["PROD_ID"].ToString().Trim());
                        productResponse.ProductName = productReader["PROD_NAME"].ToString().Trim();
                        productResponse.ProductDescription = productReader["PROD_DESC"].ToString().Trim();
                        productResponse.ProductAvailableOnline = (productReader["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductAvailableInStore = (productReader["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductPrice = Convert.ToDouble(productReader["PROD_PRICE"].ToString().Trim());
                        productResponse.ProductSaleOnline = (productReader["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductSalePrice = Convert.ToDouble(productReader["PROD_SALE_PRICE"].ToString().Trim());
                        productResponse.ProductSubType = productReader["PROD_SUB_TYPE"].ToString().Trim();
                        productResponse.ProductCode = productReader["PROD_CODE"].ToString().Trim();
                        productResponse.ProductTaxable = (productReader["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        productResponse.ProductType = productReader["PROD_TYPE"].ToString().Trim();
                        productResponse.ProductFileName = productReader["PROD_FILE_NAME"].ToString().Trim();
                        productResponse.ProductCount = Convert.ToInt32(productReader["PROD_COUNT"].ToString().Trim());
                        productsList.Add(productResponse);
                    }
                }
                else
                {
                    LogErrorMessage(new Exception("No Lotion Type"), ProductSubType, "Methods: GetLotionsByType");
                    Product productResponse = new Product();
                    productResponse.ProductName = TansMessages.ERROR_NO_PRODUCT_TYPE;
                    productsList.Add(productResponse);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, ProductSubType, "Methods: GetLotionsByType");
                Product productResponse = new Product();
                productResponse.ProductName = TansMessages.ERROR_NO_PRODUCT_TYPE;
                productsList.Add(productResponse);
            }

            return productsList;
        }

        public Product GetProductByBarCode(string barCode)
        {
            Product itemResponse = new Product();
            
            try
            {
                DataTable itemTable = prodDataAccess.ExecutePRODUCT_BY_BARCODE(barCode);
                if (itemTable.Rows.Count > 0)
                {
                    if (itemTable.Rows.Count > 1)
                    {
                        LogErrorMessage(new Exception("Too many items"), barCode, "Methods: GetProductByBarCode");
                        itemResponse.ErrorMessage = "More than one product found with this barcode. Please check the code and try again.";
                    }
                    else
                    {
                        itemResponse.ProductID = Convert.ToInt32(itemTable.Rows[0]["PROD_ID"].ToString().Trim());
                        itemResponse.ProductSubType = itemTable.Rows[0]["PROD_SUB_TYPE"].ToString().Trim();
                        itemResponse.ProductCode = itemTable.Rows[0]["PROD_CODE"].ToString().Trim();
                        itemResponse.ProductName = itemTable.Rows[0]["PROD_NAME"].ToString().Trim();
                        itemResponse.ProductFileName = itemTable.Rows[0]["PROD_FILE_NAME"].ToString().Trim();
                        itemResponse.ProductDescription = itemTable.Rows[0]["PROD_DESC"].ToString().Trim();
                        itemResponse.ProductAvailableOnline = (itemTable.Rows[0]["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        itemResponse.ProductAvailableInStore = (itemTable.Rows[0]["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        itemResponse.ProductPrice = Convert.ToDouble(itemTable.Rows[0]["PROD_PRICE"].ToString().Trim());
                        itemResponse.ProductCount = Convert.ToInt32(itemTable.Rows[0]["PROD_COUNT"].ToString().Trim());
                        itemResponse.ProductSaleOnline = (itemTable.Rows[0]["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        itemResponse.ProductSaleInStore = (itemTable.Rows[0]["PROD_SALE_STORE"].ToString().Trim() == "True" ? true : false);
                        itemResponse.ProductSalePrice = Convert.ToDouble(itemTable.Rows[0]["PROD_SALE_PRICE"].ToString().Trim());
                        itemResponse.ProductTaxable = (itemTable.Rows[0]["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        itemResponse.ProductType = itemTable.Rows[0]["PROD_TYPE"].ToString().Trim();
                        itemResponse.Active = (itemTable.Rows[0]["PROD_DISP"].ToString().Trim() == "True" ? false : true);
                    }
                }
                else
                {
                    itemResponse.ErrorMessage = "Unable to locate product with given barcode. Please check the code and try again.";
                    LogErrorMessage(new Exception("Unable to find item"), barCode, "Methods: GetProductByBarCode");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, barCode, "Methods: GetProductByBarCode");
                itemResponse.ErrorMessage = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
            return itemResponse;
        }

        public List<Package> OnlinePackageList(string packageType)
        {
            List<Package> returnPackages = new List<Package>();
            
            try
            {
                DataTable packageReader = prodDataAccess.ExecutePLANS_BY_TYPE(functionsClass.CleanUp(packageType));
                if (packageReader.Rows.Count > 0)
                {
                    foreach (DataRow d in packageReader.Rows)
                    {
                        Package packagesList = new Package();
                        CultureInfo ci = new CultureInfo("en-us");
                        packagesList = new Package();
                        packagesList.PackageID = Convert.ToInt32(d["PROD_ID"].ToString().Trim());
                        packagesList.PackageBed = d["PROD_SUB_TYPE"].ToString().Trim();
                        packagesList.PackageName = d["PROD_NAME"].ToString().Trim();
                        packagesList.PackageAvailableOnline = (d["PROD_DISP_ONLINE"].ToString().Trim() == "True" ? true : false);
                        packagesList.PackageAvailableInStore = (d["PROD_DISP_STORE"].ToString().Trim() == "True" ? true : false);
                        packagesList.PackagePrice = Convert.ToDouble(d["PROD_PRICE"].ToString().Trim()).ToString("C", ci);
                        packagesList.PackageSaleOnline = (d["PROD_SALE_ONLINE"].ToString().Trim() == "True" ? true : false);
                        packagesList.PackageSalePrice = Convert.ToDouble(d["PROD_SALE_PRICE"].ToString().Trim()).ToString("C", ci);
                        returnPackages.Add(packagesList);
                    }
                }
                else
                {
                    //No packages of that type
                    Package packagesList = new Package();
                    returnPackages.Add(packagesList);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, packageType, "Methods: OnlinePackageList");
                Package packagesList = new Package();
                returnPackages.Add(packagesList);
            }

            return returnPackages;
        }

        #endregion Product Information

        #region Tan Information

        public ArrayList GetAllTanTimes(string tanDay, string tanBed, string tanLocation, bool internalFlag)
        {
            ArrayList tanTimes = new ArrayList();
            Int32 count = 0;

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTIMES_BY_LOCATION_DAY_TYPE(tanLocation, tanDay, "T");
                if (timeTable.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow timeReader in timeTable.Rows)
                        {
                            DateTime bTime = Convert.ToDateTime(timeReader["BEG_TIME"].ToString());
                            DateTime eTime = Convert.ToDateTime(timeReader["END_TIME"].ToString());
                            DateTime cTime = bTime;
                            count = 0;

                            //Response.Write(cTime & "-" & eTime & "<br>")
                            while (cTime < eTime)
                            {
                                if (count == 0)
                                {
                                    if ((tanBed == "9") || (tanBed == "10"))
                                    {
                                        cTime = cTime.AddMinutes(15);
                                    }
                                }
                                else
                                {
                                    if (((tanBed == "1") || (tanBed == "2")) || internalFlag)
                                    {
                                        cTime = cTime.AddMinutes(15);
                                    }
                                    else
                                    {
                                        cTime = cTime.AddMinutes(30);
                                    }
                                }

                                if (((tanBed == "1") || (tanBed == "2")) && !internalFlag)
                                {
                                    if (cTime.Minute != 45)
                                    {
                                        tanTimes.Add(cTime.ToShortTimeString());
                                    }
                                }
                                else
                                {
                                    tanTimes.Add(cTime.ToShortTimeString());
                                }

                                count++;
                                if ((cTime >= eTime.AddMinutes(-45) && !internalFlag) || count > 100)
                                    cTime = eTime;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //ERROR -
                        LogErrorMessage(ex, tanDay, "Methods: GetAllTanTimes: Output rows");
                    }
                }
                else
                {
                    //ERROR - No time set up for that day (not good)
                    LogErrorMessage(new Exception("NoTimes"), tanDay, "Methods: GetAllTanTimes: No Times");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, tanDay, "Methods: GetAllTanTimes");
            }

            return tanTimes;
        }

        public ArrayList GetAvailableTanTimes(string tanBed, string tanDate, ArrayList tanTimesArray)
        {
            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTAN_TIMES_TAKEN(tanBed, tanDate, "W");

                if (timeTable.Rows.Count > 0)
                {
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        //Remove taken times from list
                        tanTimesArray.Remove(timeReader["TAN_TIME"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetAvailableTanTimes");
            }
            return tanTimesArray;
        }

        public Tan GetTanInformationByData(string tanBed, string tanDate, string tanTime, string tanLocation)
        {
            Tan scheduleTan = new Tan();

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTAN_TIME_TAKEN(tanBed, tanDate, tanLocation, tanTime);

                if (timeTable.Rows.Count > 0)
                {
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        //Someone scheduled
                        scheduleTan.Bed = tanBed;
                        scheduleTan.CustomerID = Convert.ToInt32(timeReader["USER_ID"]);
                        scheduleTan.Date = tanDate;
                        scheduleTan.DeletedIndicator = (timeReader["ACTV_IND"].ToString() == "True" ? false : true);
                        scheduleTan.Length = Convert.ToInt32(timeReader["TAN_LENGTH"]);
                        scheduleTan.Location = tanLocation;
                        scheduleTan.OnlineIndicator = (timeReader["TAN_ONLINE"].ToString() == "True" ? true : false);
                        scheduleTan.TanID = Convert.ToInt32(timeReader["TAN_ID"]);
                        scheduleTan.Time = tanTime;
                    }
                }
                else
                {
                    //Free to schedule
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetTanInformationByData");
            }

            return scheduleTan;
        }

        public List<Tan> GetTanInformationByDate(string tanBeginDate, string tanEndDate)
        {
            List<Tan> tanList = new List<Tan>();

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTANS_BY_DATE(tanBeginDate, tanEndDate);

                if (timeTable.Rows.Count > 0)
                {
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        //Someone scheduled
                        Tan scheduleTan = new Tan();
                        scheduleTan.Bed = timeReader["TAN_BED"].ToString();
                        scheduleTan.CustomerID = Convert.ToInt32(timeReader["USER_ID"]);
                        scheduleTan.Date = timeReader["TAN_DATE"].ToString();
                        scheduleTan.DeletedIndicator = (timeReader["ACTV_IND"].ToString() == "True" ? false : true);
                        scheduleTan.Length = Convert.ToInt32(timeReader["TAN_LENGTH"]);
                        scheduleTan.Location = timeReader["TAN_LOC"].ToString();
                        scheduleTan.OnlineIndicator = (timeReader["TAN_ONLINE"].ToString() == "True" ? true : false);
                        scheduleTan.TanID = Convert.ToInt32(timeReader["TAN_ID"]);
                        scheduleTan.Time = timeReader["TAN_TIME"].ToString();
                        tanList.Add(scheduleTan);
                    }
                }
                else
                {
                    //Free to schedule
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetTanInformationByDate");
            }

            return tanList;
        }

        public Tan GetTanInformationByTanID(int tanID)
        {
            Tan scheduleTan = new Tan();

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTAN_BY_TAN_ID(tanID);

                if (timeTable.Rows.Count > 0)
                {
                    foreach (DataRow timeReader in timeTable.Rows)
                    {
                        // TAN_ID, TAN_BED, TAN_DATE, TAN_LOC, TAN_TIME, TAN_LENGTH, ACTV_IND, TAN_ONLINE, USER_ID
                        scheduleTan.Bed = timeReader["TAN_BED"].ToString();
                        scheduleTan.CustomerID = Convert.ToInt32(timeReader["USER_ID"]);
                        scheduleTan.Date = Convert.ToDateTime(timeReader["TAN_DATE"].ToString()).ToShortDateString();
                        scheduleTan.DeletedIndicator = (timeReader["ACTV_IND"].ToString() == "True" ? false : true);
                        scheduleTan.Length = Convert.ToInt32(timeReader["TAN_LENGTH"]);
                        scheduleTan.Location = timeReader["TAN_LOC"].ToString();
                        scheduleTan.OnlineIndicator = (timeReader["TAN_ONLINE"].ToString() == "True" ? true : false);
                        scheduleTan.TanID = Convert.ToInt32(timeReader["TAN_ID"]);
                        scheduleTan.Time = timeReader["TAN_TIME"].ToString();
                    }
                }
                else
                {
                    // Unable to locate the tan
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, tanID.ToString(), "Methods: GetTanInformationByTanID");
            }

            return scheduleTan;
        }

        public List<Tan> GetCustomerTansByID(int customerID)
        {
            List<Tan> custTans = new List<Tan>();

            //Get tan information
            try
            {
                DataTable tanTable = tansDataAccess.ExecuteCUSTOMER_TANS_BY_CUSTOMER_ID(customerID);

                if (tanTable.Rows.Count > 0)
                {
                    foreach (DataRow tanReader in tanTable.Rows)
                    {
                        Tan tans = new Tan();
                        tans.TanID = Convert.ToInt32(tanReader["TAN_ID"].ToString().Trim());
                        tans.CustomerID = customerID;
                        tans.Date = functionsClass.FormatSlash(Convert.ToDateTime(tanReader["TAN_DATE"].ToString().Trim()));
                        tans.Time = tanReader["TAN_TIME"].ToString().Trim();
                        tans.Location = (tanReader["TAN_LOC"].ToString().Trim() == "W" ? "Waco" : "Hewitt");
                        tans.Length = Convert.ToInt32(tanReader["TAN_LENGTH"].ToString().Trim());
                        tans.Bed = tanReader["TAN_BED"].ToString().Trim();
                        tans.DeletedIndicator = (tanReader["ACTV_IND"].ToString().Trim() == "True" ? false : true);
                        tans.OnlineIndicator = (tanReader["TAN_ONLINE"].ToString().Trim() == "True" ? true : false);
                        custTans.Add(tans);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetTans");
            }

            return custTans;
        }

        #endregion Tan Information

        #region EmployeeInformation

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeesReturn = new List<Employee>();
            
            //Get employee information
            
            try
            {
                DataTable employeeTable = tansDataAccess.ExecuteALL_ACTIVE_EMPLOYEES();
                if (employeeTable.Rows.Count > 0)
                {
                    foreach (DataRow empReader in employeeTable.Rows)
                    {
                        Employee employeeDetail = new Employee();
                        employeeDetail.EmployeeID = Convert.ToInt32(empReader["EMPL_ID"].ToString().Trim());
                        employeeDetail.FirstName = empReader["EMPL_FNAME"].ToString().Trim();
                        employeeDetail.LastName = empReader["EMPL_LNAME"].ToString().Trim();
                        employeesReturn.Add(employeeDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetAllEmployees");
            }

            return employeesReturn;
        }

        public List<Employee> GetEmployeeByID(int employeeID)
        {
            List<Employee> employeesReturn = new List<Employee>();

            //Get employee information
            if (employeeID != 9999)
            {
                try
                {
                    DataTable employeeTable = tansDataAccess.ExecuteEMPLOYEE_BY_EMPLOYEE_ID(employeeID);
                    if (employeeTable.Rows.Count > 0)
                    {
                        foreach (DataRow empReader in employeeTable.Rows)
                        {
                            Employee employeeReturn = new Employee();
                            employeeReturn.EmployeeID = Convert.ToInt32(empReader["EMPL_ID"].ToString().Trim());
                            employeeReturn.FirstName = empReader["EMPL_FNAME"].ToString().Trim();
                            employeeReturn.LastName = empReader["EMPL_LNAME"].ToString().Trim();
                            employeesReturn.Add(employeeReturn);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, employeeID.ToString(), "Methods: GetEmployeeByID");
                }
            }
            else
            {
                Employee employeeReturn = new Employee();
                employeeReturn.EmployeeID = 9999;
                employeeReturn.FirstName = "All";
                employeeReturn.LastName = "Employees";
                employeesReturn.Add(employeeReturn);
            }

            return employeesReturn;
        }

        public bool AddEmployee(int employeeID, string firstName, string lastName)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_EMPLOYEE(employeeID, firstName, lastName);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, firstName + " " + lastName + "-" + employeeID.ToString(), "Methods: AddEmployee");
                return false;
            }
        }

        public bool EditEmployee(int employeeID, string firstName, string lastName)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_EMPLOYEE_BY_EMPLOYEE_ID(employeeID, firstName, lastName);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, firstName + " " + lastName + "-" + employeeID.ToString(), "Methods: EditEmployee");
                return false;
            }
        }

        public bool DeleteEmployee(int employeeID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_EMPLOYEE_BY_EMPLOYEE_ID(employeeID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeID.ToString(), "Methods: DeleteEmployee");
                return false;
            }
        }

        public bool ResetEmployeePassword(int employeeID)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_EMPLOYEE_PASSWORD_BY_EMPLOYEE_ID(employeeID, "");
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeID.ToString(), "Methods: ResetEmployeePassword");
                return false;
            }
        }

        public List<EmployeeNote> GetNotesToEmployees()
        {
            List<EmployeeNote> employeesReturn = new List<EmployeeNote>();
            
            //Get employee note information
            try
            {
                DataTable employeeTable = tansDataAccess.ExecuteEMPLOYEE_NOTES_TO_EMPLOYEES();
                if (employeeTable.Rows.Count > 0)
                {
                    foreach (DataRow empReader in employeeTable.Rows)
                    {
                        EmployeeNote employeeDetail = new EmployeeNote();
                        employeeDetail.NoteFrom = Convert.ToInt32(empReader["NOTE_FROM"].ToString().Trim());
                        employeeDetail.NoteID = Convert.ToInt32(empReader["NOTE_ID"].ToString().Trim());
                        employeeDetail.NoteText = empReader["NOTE_TXT"].ToString().Trim();
                        employeeDetail.NoteTime = Convert.ToDateTime(empReader["NOTE_DATE"].ToString().Trim());
                        employeeDetail.NoteTo = Convert.ToInt32(empReader["NOTE_TO"].ToString().Trim());
                        employeesReturn.Add(employeeDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetNotesToEmployees");
            }

            return employeesReturn;
        }

        public List<EmployeeNote> GetNotesFromEmployees()
        {
            List<EmployeeNote> employeesReturn = new List<EmployeeNote>();
            
            //Get employee note information
            try
            {
                DataTable employeeTable = tansDataAccess.ExecuteEMPLOYEE_NOTES_FROM_EMPLOYEES();
                if (employeeTable.Rows.Count > 0)
                {
                    foreach (DataRow empReader in employeeTable.Rows)
                    {
                        EmployeeNote employeeDetail = new EmployeeNote();
                        employeeDetail.NoteFrom = Convert.ToInt32(empReader["NOTE_FROM"].ToString().Trim());
                        employeeDetail.NoteID = Convert.ToInt32(empReader["NOTE_ID"].ToString().Trim());
                        employeeDetail.NoteText = empReader["NOTE_TXT"].ToString().Trim();
                        employeeDetail.NoteTime = Convert.ToDateTime(empReader["NOTE_DATE"].ToString().Trim());
                        employeeDetail.NoteTo = Convert.ToInt32(empReader["NOTE_TO"].ToString().Trim());
                        employeesReturn.Add(employeeDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetNotesFromEmployees");
            }

            return employeesReturn;
        }

        public bool EmployeeLogin(int employeeNumber, string employeePassword)
        {
            try
            {
                DataTable loginTable = tansDataAccess.ExecuteEMPLOYEE_LOGIN(employeeNumber,
                    (employeePassword == "" ? "" : functionsClass.HashText(functionsClass.CleanUp(employeePassword))));
                if (loginTable.Rows.Count > 0)
                {
                    return tansDataAccess.ExecuteUPDATE_EMPLOYEE_LOGIN(employeeNumber);
                }
                else
                {
                    // See if this is an override account
                    DataTable adminTable = tansDataAccess.ExecuteADMIN_LOGIN("Override", employeePassword);

                    if (adminTable.Rows.Count > 0)
                        {
                            return true;
                        }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeLogin");
                return false;
            }
        }

        public int EmployeeShiftCheck(int employeeNumber)
        {
            int shiftID = 0;
            
            try
            {
                DataTable loginTable = tansDataAccess.ExecuteEMPLOYEE_CURRENT_SHIFT_BY_EMPLOYEE_ID(employeeNumber);
                if (loginTable.Rows.Count > 0)
                {
                    shiftID = Convert.ToInt32(loginTable.Rows[0]["SHFT_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeShiftCheck");
            }

            return shiftID;
        }

        public DataTable EmployeeSchedule(int employeeNumber, string startDate, string endDate)
        {
            DataTable scheduleTable = new DataTable();
            try
            {
                scheduleTable = tansDataAccess.ExecuteEMPLOYEE_SCHEDULE_BY_EMPLOYEE_ID(employeeNumber, startDate, endDate);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeSchedule");
            }
            return scheduleTable;
        }

        public DataTable EmployeeWorked(int employeeNumber, string startDate, string endDate)
        {
            DataTable scheduleTable = new DataTable();
            try
            {
                scheduleTable = tansDataAccess.ExecuteEMPLOYEE_WORKED_SHIFT_BY_EMPLOYEE_ID(employeeNumber, startDate, endDate);
            }
            catch(Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeWorked");
            }
            return scheduleTable;
        }

        public DataTable EmployeeSales(int employeeNumber, string startDate, string endDate)
        {
            DataTable salesTable = new DataTable();
            try
            {
                salesTable = prodDataAccess.ExecuteSALES_TOTALS_BY_EMPLOYEE_ID(employeeNumber, startDate, endDate);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeSales");
            }
            return salesTable;
        }

        public List<Transaction> EmployeeTanningTransactions(int employeeNumber, string startDate, string endDate)
        {
            List<Transaction> returnTransactions = new List<Transaction>();
            try
            {
                DataTable salesTable = prodDataAccess.ExecuteTRANSACTIONS_BY_EMPLOYEE_ID(employeeNumber, startDate, endDate);
                
                if (salesTable.Rows.Count > 0)
                {
                    foreach (DataRow row in salesTable.Rows)
                    {
                        Transaction employeeSales = new Transaction();
                        employeeSales.Date = Convert.ToDateTime(row["TRNS_DATE"].ToString().Trim());
                        employeeSales.ID = Convert.ToInt32(row["TRNS_ID"].ToString().Trim());
                        employeeSales.Location = row["TRNS_LOC"].ToString().Trim();
                        employeeSales.Other = row["TRNS_OTH"].ToString().Trim();
                        employeeSales.Paid = (row["TRNS_PAID"].ToString().Trim() == "True" ? true : false);
                        employeeSales.Payment = row["TRNS_PYMT"].ToString().Trim();
                        employeeSales.Seller = row["TRNS_SELL"].ToString().Trim();
                        employeeSales.CustomerID = Convert.ToInt32(row["TRNS_BGHT"].ToString().Trim());
                        employeeSales.Tax = Convert.ToDouble(row["TRNS_TAX"].ToString().Trim());
                        employeeSales.Total = Convert.ToDouble(row["TRNS_TTL"].ToString().Trim());
                        employeeSales.Void = (row["TRNS_VOID"].ToString().Trim() == "True" ? true : false);
                        returnTransactions.Add(employeeSales);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeTanningTransactions");
            }

            return returnTransactions;
        }

        public void EmployeeClockIn(int employeeNumber, string startDate)
        {
            try
            {
                string startTime = (DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString())
                    + ":" + (DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString())
                    + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString());

                bool isSuccessful = tansDataAccess.ExecuteINSERT_EMPLOYEE_START_SHIFT(employeeNumber, startDate, startTime);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: EmployeeClockIn");
            }
        }

        public void EmployeeClockOut(int shiftID)
        {
            try
            {
                string endTime = (DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString())
                    + ":" + (DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString())
                    + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString());

                bool isSuccessful = tansDataAccess.ExecuteUPDATE_EMPLOYEE_END_SHIFT(shiftID, endTime);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, shiftID.ToString(), "Methods: EmployeeClockOut");
            }
        }

        public bool AddEmployeeNote(string noteText, string noteTo, string noteFrom)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_EMPLOYEE_NOTE(functionsClass.CleanUp(noteText), functionsClass.CleanUp(noteTo), functionsClass.CleanUp(noteFrom));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, noteText, "Methods: AddEmployeeNote");
                return false;
            }
        }

        public bool UpdateEmployeePassword(int employeeNumber, string newPassword)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_EMPLOYEE_PASSWORD_BY_EMPLOYEE_ID(employeeNumber, functionsClass.HashText(functionsClass.CleanUp(newPassword)));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeNumber.ToString(), "Methods: UpdateEmployeePassword");
                return false;
            }
        }

        public DataTable GetEmployeeShift(int shiftID)
        {
            DataTable shiftTable = new DataTable();

            try
            {
                shiftTable = tansDataAccess.ExecuteEMPLOYEE_SHIFT_BY_SHIFT_ID(shiftID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, shiftID.ToString(), "Methods: GetEmployeeShift");
            }
            return shiftTable;
        }

        public bool AddEmployeeHours(int employeeID, string shiftDate, DateTime beginTimestamp, DateTime endTimestamp)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_EMPLOYEE_SHIFT(employeeID, shiftDate, beginTimestamp.ToString(), endTimestamp.ToString());
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, employeeID.ToString(), "Methods: AddEmployeeHours");
                return false;
            }
        }

        public bool EditEmployeeHours(int shiftID, DateTime beginTimestamp, DateTime endTimestamp)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_EMPLOYEE_SHIFT_BY_SHIFT_ID(shiftID, beginTimestamp.ToString(), endTimestamp.ToString());
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, shiftID.ToString(), "Methods: EditEmployeeHours");
                return false;
            }
        }

        #endregion EmployeeInformation

        #region PointOfSale/Transaction Information

        public Transaction GetCustomerTransaction(int TransactionID)
        {
            Transaction transactionResponse = new Transaction();
            
            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTION_BY_TRANSACTION_ID(TransactionID);
                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        transactionResponse.Date = Convert.ToDateTime(transactionReader["TRNS_DATE"].ToString().Trim());
                        transactionResponse.ID = Convert.ToInt32(transactionReader["TRNS_ID"].ToString().Trim());
                        transactionResponse.Location = transactionReader["TRNS_LOC"].ToString().Trim();
                        transactionResponse.Other = transactionReader["TRNS_OTH"].ToString().Trim();
                        transactionResponse.Paid = (transactionReader["TRNS_PAID"].ToString().Trim() == "True" ? true : false);
                        transactionResponse.Payment = transactionReader["TRNS_PYMT"].ToString().Trim();
                        transactionResponse.Seller = transactionReader["TRNS_SELL"].ToString().Trim();
                        transactionResponse.CustomerID = Convert.ToInt32(transactionReader["TRNS_BGHT"].ToString().Trim());
                        transactionResponse.Tax = Convert.ToDouble(transactionReader["TRNS_TAX"].ToString().Trim());
                        transactionResponse.Total = Convert.ToDouble(transactionReader["TRNS_TTL"].ToString().Trim());
                        transactionResponse.Void = (transactionReader["TRNS_VOID"].ToString().Trim() == "True" ? true : false);
                    }
                }
                else
                {
                    transactionResponse.Error = TansMessages.ERROR_GENERIC;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionID.ToString(), "Methods: GetCustomerTransaction");
                transactionResponse.Error = TansMessages.ERROR_GENERIC;
            }

            return transactionResponse;
        }

        public List<TransactionItem> GetCustomerTransactionItems(int TransactionID)
        {
            List<TransactionItem> transactionItemsResponse = new List<TransactionItem>();
            
            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTION_ITEMS_BY_TRANSACTION_ID(TransactionID);
                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        TransactionItem transactionInfo = new TransactionItem();
                        transactionInfo.ID = Convert.ToInt32(transactionReader["XREF_ID"].ToString().Trim());
                        transactionInfo.Price = Convert.ToDouble(transactionReader["PROD_PRICE"].ToString().Trim());
                        transactionInfo.ProductID = Convert.ToInt32(transactionReader["PROD_ID"].ToString().Trim());
                        transactionInfo.ProductName = transactionReader["PROD_NME"].ToString().Trim();
                        transactionInfo.Quantity = Convert.ToInt32(transactionReader["PROD_QTY"].ToString().Trim());
                        transactionInfo.Tax = (transactionReader["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        transactionInfo.TransactionID = Convert.ToInt32(TransactionID);
                        transactionItemsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    TransactionItem transactionInfo = new TransactionItem();
                    transactionInfo.ID = 0;
                    transactionItemsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionID.ToString(), "Methods: GetCustomerTransactionItems");
                TransactionItem transactionInfo = new TransactionItem();
                transactionInfo.ID = 0;
                transactionInfo.Error = TansMessages.ERROR_GENERIC;
                transactionItemsResponse.Add(transactionInfo);
            }

            return transactionItemsResponse;
        }

        public List<Transaction> GetAllCustomerTransactions(Int64 customerID)
        {
            List<Transaction> transactionsResponse = new List<Transaction>();
            
            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTIONS_BY_CUSTOMER_ID(customerID);
                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        Transaction transactionInfo = new Transaction();
                        transactionInfo.Date = Convert.ToDateTime(transactionReader["TRNS_DATE"].ToString().Trim());
                        transactionInfo.ID = Convert.ToInt32(transactionReader["TRNS_ID"].ToString().Trim());
                        transactionInfo.Location = transactionReader["TRNS_LOC"].ToString().Trim();
                        transactionInfo.Other = transactionReader["TRNS_OTH"].ToString().Trim();
                        transactionInfo.Paid = (transactionReader["TRNS_PAID"].ToString().Trim() == "True" ? true : false);
                        transactionInfo.Payment = transactionReader["TRNS_PYMT"].ToString().Trim();
                        transactionInfo.Seller = transactionReader["TRNS_SELL"].ToString().Trim();
                        transactionInfo.CustomerID = Convert.ToInt32(transactionReader["TRNS_BGHT"].ToString().Trim());
                        transactionInfo.Tax = Convert.ToDouble(transactionReader["TRNS_TAX"].ToString().Trim());
                        transactionInfo.Total = Convert.ToDouble(transactionReader["TRNS_TTL"].ToString().Trim());
                        transactionInfo.Void = (transactionReader["TRNS_VOID"].ToString().Trim() == "True" ? true : false);
                        transactionsResponse.Add(transactionInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, customerID.ToString(), "Methods: GetAllCustomerTransactions");
                Transaction transactionInfo = new Transaction();
                transactionInfo.ID = 0;
                transactionInfo.Error = TansMessages.ERROR_GENERIC;
                transactionsResponse.Add(transactionInfo);
            }

            return transactionsResponse;
        }

        public Int64 InsertTransaction(List<CartItem> cart, int Seller, string CartTotal, Int64 Purchaser, string Location, string PaymentType, string Date, string Tax, string PaidIndator, string OtherInfo)
        {
            Int64 transactionID = 0;
            try
            {
                string translatedSeller = Seller.ToString();

                if (Seller == 99)
                    translatedSeller = "PAYPAL";
                if (Seller == 0)
                    translatedSeller = "ONLINE";

                transactionID = prodDataAccess.ExecuteINSERT_TRANSACTION(translatedSeller, CartTotal, Purchaser, Location, PaymentType, Date, Tax, PaidIndator, OtherInfo);

                if (transactionID > 0)
                {
                    //Add in the items for the transaction
                    foreach (CartItem item in cart)
                    {
                        bool addResponse = prodDataAccess.ExecuteINSERT_TRANSACTION_ITEM(transactionID, item.ItemID, item.ItemQuantity, item.ItemName, item.ItemPrice, (item.ItemTaxed == true ? 1 : 0));

                        if (addResponse)
                        {
                            //Get current product count
                            DataTable transactionTable = prodDataAccess.ExecutePRODUCT_BY_PRODUCT_ID(item.ItemID);

                            if (transactionTable.Rows.Count > 0)
                            {
                                int itemCount = 0;

                                itemCount = Convert.ToInt32(transactionTable.Rows[0]["PROD_COUNT"].ToString());

                                //UpdateInventory
                                bool isSuccessful = prodDataAccess.ExecuteUPDATE_PRODUCT_INVENTORY(item.ItemID, (itemCount - 1), "W");

                                if (!isSuccessful)
                                    LogErrorMessage(new Exception("InventoryNotUpdated"), item.ItemID.ToString(), "Methods: InsertTransaction: UpdateInventory");
                                else
                                    LogErrorMessage(new Exception("InventoryUpdated"), item.ItemID.ToString(), "Methods: InsertTransaction: UpdateInventory");
                            }
                            else
                                LogErrorMessage(new Exception("NoProduct"), item.ItemID.ToString(), "Methods: InsertTransaction: GetProduct");

                            if ((item.ItemType == "PKG") && (Purchaser != 0) && (!item.ItemName.ToLower().Contains("upgrade")))
                            {
                                // Update the customer renewal date
                                int packageLength = 0;
                                int levelLength = 0;
                                string packageName = "Other";
                                string packageLongName = "Other";
                                int levelId = 0;
                                bool updateRenewal = false;
                                bool isSpecial = false;

                                transactionTable = tansDataAccess.ExecutePLAN_BY_PRODUCT_ID(item.ItemID);

                                if (transactionTable.Rows.Count > 0)
                                {
                                    packageLength = Convert.ToInt32(transactionTable.Rows[0]["PLAN_LENGTH"].ToString());
                                    packageName = transactionTable.Rows[0]["PLAN_SHORT"].ToString();
                                    packageLongName = item.ItemSubType + " " + item.ItemName;
                                    updateRenewal = true;
                                }
                                else
                                {
                                    // Is this a special?
                                    transactionTable = tansDataAccess.ExecuteSPECIAL_BY_PRODUCT_ID(item.ItemID);
                                    if (transactionTable.Rows.Count > 0)
                                    {
                                        isSpecial = true;
                                        packageLength = Convert.ToInt32(transactionTable.Rows[0]["SPEC_LENGTH"].ToString());
                                        levelLength = Convert.ToInt32(transactionTable.Rows[0]["SPEC_LEVEL_LENGTH"].ToString());
                                        levelId = Convert.ToInt32(transactionTable.Rows[0]["SPEC_LEVEL_ID"].ToString());
                                        packageName = transactionTable.Rows[0]["SPEC_SHORT_NME"].ToString();
                                        packageLongName = transactionTable.Rows[0]["SPEC_NME"].ToString();
                                        updateRenewal = true;
                                    }
                                    else
                                        LogErrorMessage(new Exception("No Plan/Special Found"), item.ItemID.ToString(), item.ItemName);
                                }

                                if ((packageLength > 0) && (!String.IsNullOrEmpty(packageName)))
                                {
                                    // Get the current renewal date
                                    Customer transactionBuyer = GetCustomerInformationByID(Convert.ToInt32(Purchaser));
                                    DateTime newPlanRenewalDate = DateTime.Now.AddDays(packageLength);
                                    DateTime newSpecialRenewalDate = DateTime.Now.AddDays(levelLength);
                                    string[] splitPackageName = packageName.Split(Convert.ToChar(" "));
                                        
                                    // Is the current renewal date further out than the new renewal date?
                                    if (transactionBuyer.RenewalDate > newPlanRenewalDate)
                                    {
                                        // Check if this is an upgrade or a single
                                        if (splitPackageName.Length > 1)
                                        {
                                            if ((splitPackageName[1] == "SINGLE") || 
                                                (splitPackageName[1] == "SINGLE-L1") ||
                                                (splitPackageName[1] == "SINGLE-L2") ||
                                                (splitPackageName[1] == "SINGLE-L3") ||
                                                (splitPackageName[1] == "UPGRADE"))
                                            {
                                                // They're just buying a single or an upgrade while their current
                                                //  package is active; don't change the renewal.
                                                updateRenewal = false;
                                            }
                                        }
                                    }

                                    if (updateRenewal)
                                    {
                                        // Check if this is a single
                                        if (splitPackageName.Length > 1)
                                        {
                                            if ((splitPackageName[1] == "SINGLE") ||
                                                (splitPackageName[1] == "SINGLE-L1") ||
                                                (splitPackageName[1] == "SINGLE-L2") ||
                                                (splitPackageName[1] == "SINGLE-L3"))
                                            {
                                                // This person is on singles; set the renewal to current date.
                                                newPlanRenewalDate = DateTime.Now;
                                                newSpecialRenewalDate = Convert.ToDateTime("2001-01-01");
                                            }
                                        }
                                    }

                                    if (updateRenewal)
                                    {
                                        bool customerUpdate = UpdateCustomerInformation(transactionBuyer.FirstName, transactionBuyer.LastName, transactionBuyer.FitzPatrickNumber,
                                            transactionBuyer.JoinDate, newPlanRenewalDate, packageName, isSpecial, levelId, newSpecialRenewalDate, transactionBuyer.Remarks, 
                                            transactionBuyer.LotionWarning, transactionBuyer.OnlineRestriction, Convert.ToInt32(Purchaser));

                                        if (!customerUpdate)
                                            LogErrorMessage(new Exception("CustomerNotUpdated"), Purchaser.ToString() + "-" + transactionID.ToString() + "-" + item.ItemID.ToString(), "Methods: InsertTransaction: UpdateExpiration");
                                        //else
                                        //    LogErrorMessage(new Exception("CustomerUpdated"), Purchaser.ToString(), "Methods: InsertTransaction: UpdateExpiration");

                                        bool customerHistory = UpdateCustomerHistory(transactionBuyer.ID, transactionID, functionsClass.FormatDash(Convert.ToDateTime(Date)),
                                            functionsClass.FormatDash(newPlanRenewalDate), packageLongName);

                                        if (!customerUpdate)
                                            LogErrorMessage(new Exception("CustomerHistoryNotUpdated"), Purchaser.ToString() + "-" + transactionID.ToString() + "-" + item.ItemID.ToString(), "Methods: InsertTransaction: UpdateHistory");
                                        //else
                                        //    LogErrorMessage(new Exception("CustomerHistoryUpdated"), Purchaser.ToString(), "Methods: InsertTransaction: UpdateHistory");
                                    }
                                }
                            }
                            //else
                            //    LogErrorMessage(new Exception("TransactionNotPackage"), item.ItemType, "Methods: InsertTransaction: UpdateExpiration");
                        }
                        else
                            LogErrorMessage(new Exception("TransactionItemNotSaved"), transactionID.ToString() + "-" + item.ItemID.ToString(), "Methods: InsertTransaction: InsertTransactionItem");
                    }
                }
                else
                    LogErrorMessage(new Exception("TransactionNotSaved"), transactionID.ToString(), "Methods: InsertTransaction: InsertTransaction");
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, Purchaser.ToString(), "Methods: InsertTransaction");
            }

            return transactionID;
        }

        public string GetTransactionByBuyerDateAmount(string Purchaser, string Date, string Total)
        {
            string response = TansMessages.ERROR_GENERIC;
            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTION_BY_BUYER_TOTAL_DATE(Purchaser, Total, Date);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        response = transactionReader["TRNS_ID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: GetTransactionByBuyerDateAmount");
            }

            return response;
        }

        public List<TransactionItem> GetTanningTransactionItems(int TransactionID)
        {
            List<TransactionItem> transactionItemsResponse = new List<TransactionItem>();

            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTION_ITEMS_BY_TRANSACTION_ID(TransactionID);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        TransactionItem transactionInfo = new TransactionItem();
                        transactionInfo.ID = Convert.ToInt32(transactionReader["XREF_ID"].ToString().Trim());
                        transactionInfo.Price = Convert.ToDouble(transactionReader["PROD_PRICE"].ToString().Trim());
                        transactionInfo.ProductID = Convert.ToInt32(transactionReader["PROD_ID"].ToString().Trim());
                        transactionInfo.ProductName = transactionReader["PROD_NME"].ToString().Trim();
                        transactionInfo.Quantity = Convert.ToInt32(transactionReader["PROD_QTY"].ToString().Trim());
                        transactionInfo.Tax = (transactionReader["PROD_TAX"].ToString().Trim() == "True" ? true : false);
                        transactionInfo.TransactionID = Convert.ToInt32(TransactionID);
                        transactionItemsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    TransactionItem transactionInfo = new TransactionItem();
                    transactionInfo.ID = 0;
                    transactionInfo.Price = 0.00;
                    transactionInfo.ProductID = 0;
                    transactionInfo.ProductName = "Unknown";
                    transactionInfo.Quantity = 0;
                    transactionInfo.Tax = false;
                    transactionInfo.TransactionID = Convert.ToInt32(TransactionID);
                    transactionItemsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionID.ToString(), "Methods: GetTanningTransactionItems");
                TransactionItem transactionInfo = new TransactionItem();
                transactionInfo.Error = TansMessages.ERROR_GENERIC;
                transactionItemsResponse.Add(transactionInfo);
            }

            return transactionItemsResponse;
        }

        public bool DeleteTanningTransactionItems(int TransactionItemID)
        {
            try
            {
                return prodDataAccess.ExecuteDELETE_TRANSACTION_ITEM_BY_ITEM_ID(TransactionItemID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionItemID.ToString(), "Methods: RemoveTanningTransactionItems");
                return false;
            }
        }

        public bool UpdateTransaction(int transactionID, string seller, string total, string date, string payment, bool isVoid, bool isPaid)
        {
            try
            {
                return prodDataAccess.ExecuteUPDATE_TRANSACTION_BY_TRANSACTION_ID(transactionID, seller, total, date, payment, (isVoid ? "1" : "0"), (isPaid ? "1" : "0"));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, transactionID.ToString(), "Methods: UpdateTransaction");
                return false;
            }
        }

        public List<Transaction> GetAllTanningTransactions(string TransactionDate, string Location)
        {
            List<Transaction> transactionsResponse = new List<Transaction>();

            try
            {
                DataTable transactionTable = prodDataAccess.ExecuteTRANSACTION_BY_DATE_LOCATION(TransactionDate, Location);

                if (transactionTable.Rows.Count > 0)
                {
                    foreach (DataRow transactionReader in transactionTable.Rows)
                    {
                        Transaction transactionInfo = new Transaction();
                        transactionInfo.Date = Convert.ToDateTime(transactionReader["TRNS_DATE"].ToString().Trim());
                        transactionInfo.ID = Convert.ToInt32(transactionReader["TRNS_ID"].ToString().Trim());
                        transactionInfo.Location = transactionReader["TRNS_LOC"].ToString().Trim();
                        transactionInfo.Other = transactionReader["TRNS_OTH"].ToString().Trim();
                        transactionInfo.Paid = (transactionReader["TRNS_PAID"].ToString().Trim() == "True" ? true : false);
                        transactionInfo.Payment = transactionReader["TRNS_PYMT"].ToString().Trim();
                        transactionInfo.Seller = transactionReader["TRNS_SELL"].ToString().Trim();
                        transactionInfo.CustomerID = Convert.ToInt32(transactionReader["TRNS_BGHT"].ToString().Trim());
                        transactionInfo.Tax = Convert.ToDouble(transactionReader["TRNS_TAX"].ToString().Trim());
                        transactionInfo.Total = Convert.ToDouble(transactionReader["TRNS_TTL"].ToString().Trim());
                        transactionInfo.Void = (transactionReader["TRNS_VOID"].ToString().Trim() == "True" ? true : false);
                        transactionsResponse.Add(transactionInfo);
                    }
                }
                else
                {
                    Transaction transactionInfo = new Transaction();
                    transactionInfo.ID = 0;
                    transactionsResponse.Add(transactionInfo);
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, TransactionDate, "Methods: GetAllTanningTransactions");
                Transaction transactionInfo = new Transaction();
                transactionInfo.Error = TansMessages.ERROR_GENERIC;
                transactionsResponse.Add(transactionInfo);
            }

            return transactionsResponse;
        }

        public bool DeleteAllTanningTransactions(long customerId)
        {
            try
            {
                return prodDataAccess.ExecuteDELETE_TRANSACTIONS_BY_CUSTOMER_ID(customerId);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, customerId.ToString(), "Methods: DeleteAllTanningTransactions");
                return false;
            }
        }

        public bool AddGiftCard(Int64 customerID, string from, int employeeID, double amount, DateTime boughtDate, string description)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_GIFT_CARD(customerID, from, employeeID, amount.ToString(), functionsClass.FormatDash(boughtDate), description);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Methods: AddGiftCard");
                return false;
            }
        }

        #endregion PointOfSale/Transaction Information

        #region Site Information

        public SiteNotification GetSiteNotification()
        {
            SiteNotification returnNotification = new SiteNotification();
            
            try
            {
                DataTable noticeTable = tansDataAccess.ExecuteALL_ACTIVE_SITE_NOTICES();
                if (noticeTable.Rows.Count > 0)
                {
                    foreach (DataRow noticeReader in noticeTable.Rows)
                    {
                        returnNotification.NoticeID = Convert.ToInt32(noticeReader["NOTICE_ID"].ToString());
                        returnNotification.NoticeText = noticeReader["NOTICE_TXT"].ToString();
                        returnNotification.StartDate = Convert.ToDateTime(noticeReader["NOTICE_START_DT"].ToString());
                        returnNotification.EndDate = Convert.ToDateTime(noticeReader["NOTICE_END_DT"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Method: GetSiteNotification");
            }

            return returnNotification;
        }

        public bool InsertSiteNotification(string notificationText, DateTime startDate, DateTime endDate)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_SITE_NOTIFY(notificationText, functionsClass.FormatDash(startDate), functionsClass.FormatDash(endDate));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Method: InsertSiteNotification");
                return false;
            }
        }

        public bool UpdateSiteNotification(Int64 noticeID, string notificationText, DateTime startDate, DateTime endDate)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_SITE_NOTIFY_BY_NOTIFY_ID(noticeID, notificationText, functionsClass.FormatDash(startDate), functionsClass.FormatDash(endDate));
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, noticeID.ToString(), "Method: UpdateSiteNotification");
                return false;
            }
        }

        public bool AddComment(string commentEmail, string commentName, string commentAbout, string commentText)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_SITE_COMMENT(commentEmail, commentName, commentAbout, commentText);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, commentEmail, "Method: AddComment");
                return false;
            }
        }

        #endregion Site Information

        #region Massage Information

        public bool AddMassageAppointment(Int64 customerID, DateTime appointmentDate, string appointmentTime, int appointmentLength)
        {
            try
            {
                return tansDataAccess.ExecuteINSERT_MASSAGE(customerID, functionsClass.FormatDash(appointmentDate), appointmentTime, appointmentLength);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "", "Method: AddMassageAppointment");
                return false;
            }
        }

        public bool UpdateMassageAppointment(int massageID, DateTime appointmentDate, string appointmentTime, int appointmentLength)
        {
            try
            {
                return tansDataAccess.ExecuteUPDATE_MASSAGE_BY_MASSAGE_ID(massageID, functionsClass.FormatDash(appointmentDate), appointmentTime, appointmentLength);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, massageID.ToString(), "Method: UpdateMassageAppointment");
                return false;
            }
        }

        public bool DeleteMassageAppointment(int massageID)
        {
            try
            {
                return tansDataAccess.ExecuteDELETE_MASSAGE(massageID);
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, massageID.ToString(), "Method: DeleteMassageAppointment");
                return false;
            }
        }

        public Massage GetMassageByData(string massageDate, string massageTime)
        {
            Massage massageData = new Massage();

            try
            {
                DataTable massageTable = tansDataAccess.ExecuteMASSAGE_INFO_BY_DATA(massageDate, massageTime);

                if (massageTable.Rows.Count > 0)
                {
                    foreach (DataRow row in massageTable.Rows)
                    {
                        massageData.Date = Convert.ToDateTime(row["MASSAGE_DATE"].ToString().Trim());
                        massageData.ID = Convert.ToInt32(row["MASSAGE_ID"].ToString().Trim());
                        massageData.UserID = Convert.ToInt64(row["USER_ID"].ToString().Trim());
                        massageData.Length = Convert.ToInt32(row["MASSAGE_LENGTH"].ToString().Trim());
                        massageData.Time = row["MASSAGE_TIME"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, massageDate.ToString(), "Method: GetMassageByData");
            }

            return massageData;
        }

        public List<Massage> GetMassageByCustomerID(int customerID)
        {
            List<Massage> customerMassages = new List<Massage>();
            DataTable massageTable = tansDataAccess.ExecuteCUSTOMER_MASSAGES_BY_CUSTOMER_ID(customerID);

            if (massageTable.Rows.Count > 0)
            {
                foreach (DataRow row in massageTable.Rows)
                {
                    Massage massageEntry = new Massage();
                    massageEntry.Date = Convert.ToDateTime(row["MASSAGE_DATE"].ToString());
                    massageEntry.ID = Convert.ToInt32(row["MASSAGE_ID"].ToString());
                    massageEntry.Length = Convert.ToInt32(row["MASSAGE_LENGTH"].ToString());
                    massageEntry.Time = row["MASSAGE_TIME"].ToString();
                    massageEntry.UserID = Convert.ToInt64(row["USER_ID"].ToString());
                    customerMassages.Add(massageEntry);
                }
            }

            return customerMassages;
        }

        public Massage GetMassageInformationByMassageID(int massageID)
        {
            Massage customerMassage = new Massage();
            DataTable massageTable = tansDataAccess.ExecuteCUSTOMER_MASSAGES_BY_MASSAGE_ID(massageID);

            if (massageTable.Rows.Count > 0)
            {
                foreach (DataRow row in massageTable.Rows)
                {
                    customerMassage.Date = Convert.ToDateTime(row["MASSAGE_DATE"].ToString().Trim());
                    customerMassage.ID = Convert.ToInt32(row["MASSAGE_ID"].ToString().Trim());
                    customerMassage.Length = Convert.ToInt32(row["MASSAGE_LENGTH"].ToString().Trim());
                    customerMassage.Time = row["MASSAGE_TIME"].ToString().Trim();
                    customerMassage.UserID = Convert.ToInt64(row["USER_ID"].ToString().Trim());
                }
            }

            return customerMassage;
        }

        public ArrayList GetAllMassageTimes(string massageDay)
        {
            ArrayList massageTimes = new ArrayList();

            try
            {
                DataTable timeTable = tansDataAccess.ExecuteTIMES_BY_LOCATION_DAY_TYPE("W", massageDay, "M");
                if (timeTable.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow timeReader in timeTable.Rows)
                        {
                            DateTime bTime = Convert.ToDateTime(timeReader["BEG_TIME"].ToString());
                            DateTime eTime = Convert.ToDateTime(timeReader["END_TIME"].ToString());
                            DateTime cTime = bTime;

                            //Response.Write(cTime & "-" & eTime & "<br>")
                            while (cTime < eTime)
                            {
                                cTime = cTime.AddMinutes(30);

                                massageTimes.Add(cTime.ToShortTimeString());

                                if (cTime >= eTime.AddMinutes(-45))
                                    cTime = eTime;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //ERROR -
                        LogErrorMessage(ex, massageDay, "Methods: GetAllMassageTimes: Output rows");
                    }
                }
                else
                {
                    //ERROR - No time set up for that day (not good)
                    LogErrorMessage(new Exception("NoTimes"), massageDay, "Methods: GetAllMassageTimes: No Times");
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, massageDay, "Methods: GetAllMassageTimes");
            }

            return massageTimes;
        }

        public ArrayList GetAvailableMassageTimes(DateTime date, ArrayList massageTimesArray)
        {
            DataTable massageTable = tansDataAccess.ExecuteMASSAGE_TIME_TAKEN(functionsClass.FormatDash(date));

            if (massageTable.Rows.Count > 0)
            {
                foreach (DataRow row in massageTable.Rows)
                {
                    //Remove taken times from list
                    massageTimesArray.Remove(row["MASSAGE_TIME"].ToString());
                }
            }

            return massageTimesArray;
        }

        #endregion Massage Information

        #region Other Information

        public bool AdministrationCheck(string Password, string PasswordType)
        {
            bool response = false;
            DataTable adminTable = tansDataAccess.ExecuteADMIN_LOGIN(PasswordType, functionsClass.CleanUp(Password));

            try
            {
                if (adminTable.Rows.Count > 0)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, Password, "Methods: AdministrationCheck");
            }

            return response;
        }

        public string AdministrationUpdate(string CurrentPassword, string NewPassword, string PasswordType)
        {
            string response = String.Empty;
            DataTable adminTable = tansDataAccess.ExecuteADMIN_LOGIN(PasswordType, functionsClass.CleanUp(CurrentPassword));

            try
            {
                if (adminTable.Rows.Count > 0)
                {
                    // Valid current password, update to new password
                    bool success = tansDataAccess.ExecuteUPDATE_ADMIN_LOGIN(PasswordType, functionsClass.CleanUp(NewPassword));
                    if (success)
                        response = "Password successfully updated.";
                }
                else
                {
                    response = "Sorry, the entered current password does not match what is on record. Please try again.";
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, PasswordType, "Methods: AdministrationUpdate");
                response = TansMessages.ERROR_GENERIC_INTERNAL;
            }

            return response;
        }

        #endregion Other Information
    }
}