using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace HOTBAL
{
    public class HOTFunctions
    {
        public void SendErrorMail(string ErrorClass, Exception ErrorReceived, string ErrorSQL)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.hottropicaltans.com");

            objMessage.Subject = "Problem in:" + ErrorClass;
            objMessage.From = new MailAddress("hotproblems@hottropicaltans.com");
            objMessage.To.Add("HOTTans@hottropicaltans.com");

            objMessage.Body = "<b>SQL:</b>" + ErrorSQL + "<br /><b>Location:</b>" + ErrorClass + "<br />";

            if (ErrorReceived != null)
            {
                objMessage.Body += "<b>Message:</b>" + ErrorReceived.Message +
                "<br /><b>StackTrace:</b>" + ErrorReceived.StackTrace;

                if (ErrorReceived.InnerException != null)
                {
                    objMessage.Body += "<br /><b>Inner Message:</b>" + ErrorReceived.InnerException.Message +
                    "<br /><b>Inner Stack Trace:</b>" + ErrorReceived.InnerException.StackTrace;
                }
            }
            objMessage.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("hotproblems@hottropicaltans.com", "H0tTans.");
            smtp.Send(objMessage);
        }

        public void SendMail(string mailTo, string mailFrom, string mailSubject, string mailBody)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.hottropicaltans.com");

            objMessage.Subject = mailSubject;
            objMessage.From = new MailAddress(mailFrom);
            objMessage.To.Add(mailTo);
            objMessage.Body = mailBody;
            objMessage.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("hotproblems@hottropicaltans.com", "H0tTans.");
            smtp.Send(objMessage);
        }

        public string CleanUp(string inputString)
        {
            inputString = inputString.Replace("\\", "");
            inputString = inputString.Replace("/", "");
            inputString = inputString.Replace("*", "");
            inputString = inputString.Replace("'", "");
            inputString = inputString.Replace("=", "");
            inputString = inputString.Replace("-", "");
            inputString = inputString.Replace("#", "");
            inputString = inputString.Replace(";", "");
            inputString = inputString.Replace("<", "");
            inputString = inputString.Replace(">", "");
            inputString = inputString.Replace("+", "");
            inputString = inputString.Replace("%", "");
            inputString = inputString.Replace("@", "");
            inputString = inputString.Replace("1=1", "");
            inputString = inputString.Replace("$", "");
            inputString = inputString.Replace("&", "");

            return inputString;
        }

        public string LightCleanUp(string inputString)
        {
            inputString = inputString.Replace("\\", "");
            inputString = inputString.Replace("/", "");
            inputString = inputString.Replace("*", "");
            inputString = inputString.Replace("'", "''");
            inputString = inputString.Replace("=", "");
            inputString = inputString.Replace("#", "");
            inputString = inputString.Replace(";", "");
            inputString = inputString.Replace("<", "");
            inputString = inputString.Replace(">", "");
            inputString = inputString.Replace("+", "");
            inputString = inputString.Replace("%", "");
            inputString = inputString.Replace("1=1", "");
            inputString = inputString.Replace("&", "");

            return inputString;
        }

        public string HashText(string password)
        {
            string textToHash = password;
            //string saltAsString = "H0T";
            Byte[] byteRepresentation = System.Text.UnicodeEncoding.UTF8.GetBytes(textToHash); //+ saltAsString);
            Byte[] hashedTextInBytes = null;
            MD5CryptoServiceProvider myMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            hashedTextInBytes = myMD5.ComputeHash(byteRepresentation);
            string hashedText = Convert.ToBase64String(hashedTextInBytes);

            return hashedText;
        }

        public string FormatDash(DateTime formatDate)
        {
            string dashFormattedDate = String.Format("{0:yyyy-MM-dd}", formatDate);

            return dashFormattedDate;
        }

        public string FormatSlash(DateTime formatDate)
        {
            string slashFormattedDate = String.Format("{0:MM/dd/yyyy}", formatDate);

            return slashFormattedDate;
        }
    }

    public class SDAFunctionsClass : HOTFunctions
    {
        private FederationMethods methodsClass = new FederationMethods();
        private int count = 1;

        public bool isAdmin()
        {
            if (HttpContext.Current.Session["admin"] != null)
                if (HttpContext.Current.Session["admin"].ToString() == "Yes")
                    return true;
                else
                    return false;
            else
                return false;
        }

        public bool SchoolSelected()
        {
            if (HttpContext.Current.Session["school"] != null)
                if (HttpContext.Current.Session["school"].ToString() != "")
                    return true;
                else
                    return false;
            else
                return false;
        }

        public int SchoolID()
        {
            return Convert.ToInt32(HttpContext.Current.Session["school"].ToString());
        }

        public ListItem[] GetSchoolList()
        {
            count = 1;
            try
            {
                List<HOTBAL.School> studentSchools = methodsClass.GetAllSchools();
                ListItem[] schoolList = new ListItem[studentSchools.Count + 1];

                schoolList[0] = new ListItem("-SELECT-", "0");
                foreach (School school in studentSchools)
                {
                    schoolList[count] = new ListItem(school.SchoolName, school.SchoolID.ToString());
                    count++;
                }

                return schoolList;
            }
            catch (Exception ex)
            {
                methodsClass.LogErrorMessage(ex, "", "SDAFunctions: GetSchoolList");
                ListItem[] schoolList = new ListItem[0];
                return schoolList;
            }
        }

        public ListItem[] GetArtList(int SchoolId)
        {
            count = 1;
            try
            {
                List<Art> studentArts = methodsClass.GetArtsBySchoolID(SchoolId);
                ListItem[] artList = new ListItem[studentArts.Count + 1];
                artList[0] = new ListItem("-SELECT-", "0");
                foreach (Art art in studentArts)
                {
                    artList[count] = new ListItem(art.Title, art.ID.ToString());
                    count++;
                }
                return artList;
            }
            catch (Exception ex)
            {
                methodsClass.LogErrorMessage(ex, SchoolId.ToString(), "SDAFunctions: GetArtList");
                ListItem[] artList = new ListItem[0];
                return artList;
            }
        }

        public ListItem[] GetBeltList(int ArtId)
        {
            count = 1;
            try
            {
                List<Belt> studentBelts = methodsClass.GetArtBelts(ArtId);
                ListItem[] beltList = new ListItem[studentBelts.Count + 1];
                beltList[0] = new ListItem("-SELECT-", "0");
                foreach (Belt belt in studentBelts)
                {
                    beltList[count] = new ListItem(belt.Title, belt.ID.ToString());
                    count++;
                }
                return beltList;
            }
            catch (Exception ex)
            {
                methodsClass.LogErrorMessage(ex, ArtId.ToString(), "SDAFunctions: GetBeltList");
                ListItem[] beltList = new ListItem[0];
                return beltList;
            }
        }
    }

    public class TansFunctionsClass : HOTFunctions
    {
        public string GeneratePassword(int length)
        {
            //Get the GUID
            String guidResult = System.Guid.NewGuid().ToString();

            //Remove the hyphens
            guidResult = guidResult.Replace("-", String.Empty);

            //Make sure length is valid
            if ((length <= 0) || (length > guidResult.Length))
            {
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);
            }

            //Return the first length bytes
            return guidResult.Substring(0, length);
        }

        public EmailVerificationInfo BuildEmailVerificationInfo()
        {
            EmailVerificationInfo returnVerify = new EmailVerificationInfo();
            returnVerify.guid = new Guid();
            returnVerify.userName = "";
            returnVerify.emailAddress = "";
            returnVerify.requestSent = DateTime.Now;

            return returnVerify;
        }

        public EmailVerificationInfo BuildEmailVerificationInfo(Guid guid, string memberNumber, string userName, string emailAddress, DateTime requestSent)
        {
            EmailVerificationInfo returnVerify = new EmailVerificationInfo();
            returnVerify.guid = guid;
            returnVerify.userName = userName;
            returnVerify.emailAddress = emailAddress;
            returnVerify.requestSent = requestSent;

            return returnVerify;
        }

        public static EmailVerificationInfo DecodeDelimited(string encodedLine)
        {
            EmailVerificationInfo evInfo = new EmailVerificationInfo();

            try
            {
                // Parse verification info
                string[] lineSplit = encodedLine.Split(';');
                evInfo.memberNumber = lineSplit[0];
                evInfo.guid = new Guid(lineSplit[1]);
                evInfo.emailAddress = lineSplit[2];
                evInfo.userName = lineSplit[3];
                evInfo.requestSent = DateTime.Parse(lineSplit[4]);
            }
            catch (Exception ex)
            {
                // Unable to parse line, send back the object with default initialization
                //throw new Exception(ex.Message + " \n\n" + ex.StackTrace);
                TansMethods functionsClass = new TansMethods();
                functionsClass.LogErrorMessage(ex, "", "Functions: DecodeDelimited");
            }
            return evInfo;
        }

        public Literal GetLiteral(string text)
        {
            Literal rv = new Literal();
            rv.Text = text;
            return rv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public Customer PlanRenewalCheck(Customer customerInfo)
        {
            TansMethods sqlClass = new TansMethods();
            if (customerInfo.Plan.Trim().ToUpper() != "OTHER")
            {
                if (customerInfo.SpecialFlag)
                {
                    // This customer is on a special
                    int specialLevelID = customerInfo.SpecialID;
                    DateTime specialEndDate = Convert.ToDateTime(customerInfo.SpecialDate);
                    DateTime specialLevelEndDate = Convert.ToDateTime(customerInfo.SpecialDate);
                    SpecialLevel specialLevelInfo = sqlClass.GetSpecialLevelByLevelID(specialLevelID);

                    // Get all of the levels for the special + count levels
                    Special specialInfo = sqlClass.GetSpecialByID(specialLevelInfo.SpecialID);
                    List<SpecialLevel> specialLevelsInfo = sqlClass.GetLevelsBySpecialID(specialLevelInfo.SpecialID);

                    int specialTotalDuration = specialInfo.SpecialLength;
                    
                    if (specialLevelEndDate <= DateTime.Now)
                    {
                        // Special level is over
                        if (specialEndDate <= DateTime.Now)
                        {
                            // Special is over
                            customerInfo.RenewalDate = DateTime.Now;
                            customerInfo.SpecialDate = DateTime.MinValue;
                            customerInfo.SpecialFlag = false;
                            customerInfo.SpecialID = 0;
                        }
                        else
                        {
                            // Needs to be prompted to the next level
                            bool isLevel = false;
                            foreach (SpecialLevel sLevel in specialLevelsInfo)
                            {
                                if (isLevel)
                                {
                                    // This is their new level; get the new information
                                    SpecialLevel newSpecialLevel = sqlClass.GetSpecialLevelByLevelID(sLevel.SpecialLevelID);
                                    
                                    // Set the new information and break the loop.
                                    customerInfo.SpecialID = sLevel.SpecialLevelID;
                                    customerInfo.Plan = specialInfo.SpecialName + "-" + newSpecialLevel.SpecialLevelBed;
                                    customerInfo.SpecialDate = DateTime.Now.AddDays(sLevel.SpecialLevelLength);                                    
                                    break;
                                }

                                if (sLevel.SpecialLevelID == specialLevelID)
                                    // This is the current level, so the next will be the new
                                    isLevel = true;
                            }

                            // Update the customer information with the new level and date
                            sqlClass.UpdateCustomerInformation(customerInfo.FirstName, customerInfo.LastName, customerInfo.FitzPatrickNumber, customerInfo.JoinDate, customerInfo.RenewalDate,
                                customerInfo.Plan, true, customerInfo.SpecialID, customerInfo.SpecialDate, customerInfo.Remarks, customerInfo.LotionWarning, customerInfo.OnlineRestriction, customerInfo.ID);
                        }
                    }
                }
                else
                {
                    Package customerPlan = sqlClass.PlanInformation(customerInfo.PlanId);

                    if (customerPlan.PackageTanCount > 0)
                    {
                        DateTime dateLimit = customerInfo.RenewalDate.AddDays((customerPlan.PackageLength * -1));
                        int tanCount = 0;
                        foreach (Tan t in customerInfo.Tans)
                        {
                            if (Convert.ToDateTime(t.Date) >= dateLimit)
                                tanCount++;
                        }

                        if (tanCount >= customerPlan.PackageTanCount)
                        {
                            customerInfo.RenewalDate = DateTime.Now;
                            sqlClass.UpdateCustomerInformation(customerInfo.FirstName, customerInfo.LastName, customerInfo.FitzPatrickNumber, customerInfo.JoinDate, DateTime.Now,
                                customerPlan.PackageNameShort, false, customerInfo.SpecialID, customerInfo.SpecialDate, customerInfo.Remarks, customerInfo.LotionWarning, customerInfo.OnlineRestriction, customerInfo.ID);
                        }
                        else
                            customerInfo.Plan = customerInfo.Plan + " (" + (customerPlan.PackageTanCount - tanCount).ToString() + " tans left)";
                    }
                }
            }

            return customerInfo;
        }

        public bool isLoggedIn()
        {
            if (HttpContext.Current.Session["userID"] != null)
                return true;
            else
                return false;
        }

        public bool isAdmin()
        {
            if (HttpContext.Current.Session["admin"] != null)
                if (HttpContext.Current.Session["admin"].ToString() == "Yes")
                    return true;
                else
                    return false;
            else
                return false;
        }

        public void buildAppointmentDatesList(DropDownList appointmentDates)
        {
            appointmentDates.Items.Add(new ListItem("-Choose-", ""));
            appointmentDates.Items.Add(new ListItem(FormatSlash(DateTime.Now), FormatDash(DateTime.Now)));
            appointmentDates.Items.Add(new ListItem(FormatSlash(DateTime.Now.AddDays(1)), FormatDash(DateTime.Now.AddDays(1))));
            appointmentDates.Items.Add(new ListItem(FormatSlash(DateTime.Now.AddDays(2)), FormatDash(DateTime.Now.AddDays(2))));
            appointmentDates.Items.Add(new ListItem(FormatSlash(DateTime.Now.AddDays(3)), FormatDash(DateTime.Now.AddDays(3))));
            appointmentDates.Items.Add(new ListItem(FormatSlash(DateTime.Now.AddDays(4)), FormatDash(DateTime.Now.AddDays(4))));
        }

        public void buildBedTypeList(DropDownList bedTypes)
        {
            bedTypes.Items.Add(new ListItem("-Choose-", ""));
            bedTypes.Items.Add(new ListItem("Super Bed", "BB"));
            bedTypes.Items.Add(new ListItem("Regular Bed", "SB"));
            bedTypes.Items.Add(new ListItem("Mystic", "MY"));
        }
    }
}