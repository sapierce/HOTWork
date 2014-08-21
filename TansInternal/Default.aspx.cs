using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class _Default : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Tanning Schedule";

            if (String.IsNullOrEmpty(enterDate.Text))
            {
                scheduleOutput.Text = WacoTanningSchedule(functionsClass.FormatDash(DateTime.Now));
                Page.Title += " for " + functionsClass.FormatSlash(DateTime.Now);
            }
            else
            {
                scheduleOutput.Text = WacoTanningSchedule(functionsClass.FormatDash(Convert.ToDateTime(enterDate.Text)));
                Page.Title += " for " + functionsClass.FormatSlash(Convert.ToDateTime(enterDate.Text));
            }
        }

        protected string WacoTanningSchedule(string inDate)
        {
            string scheduleTable = String.Empty;
            string dayOfDate = System.Convert.ToDateTime(inDate).ToString("ddd");
            bool isClosed = false;

            try
            {
                List<HOTBAL.Time> scheduleTimes = sqlClass.GetLocationTimes("W", "T");
                List<HOTBAL.Bed> scheduleBeds = sqlClass.GetLocationActiveBeds("W");

                if ((scheduleTimes != null) && (scheduleBeds != null) && (!String.IsNullOrEmpty(inDate)) && (!String.IsNullOrEmpty(dayOfDate.Trim())))
                {
                    int bedCount = 0;
                    foreach (HOTBAL.Bed b in scheduleBeds)
                    {
                        if (b.BedDisplayInternal)
                            bedCount++;
                    }


                    scheduleTable = "<thead><tr><th colspan='" + (bedCount + 2).ToString() + "'>Schedule for "
                        + functionsClass.FormatSlash(Convert.ToDateTime(inDate)) + "</th></tr><tr><td class='centerAlignHeader'><br /></td>";
                    foreach (HOTBAL.Bed b in scheduleBeds)
                    {
                        if (b.BedDisplayInternal)
                            scheduleTable = scheduleTable + "<td class='centerAlignHeader'>BED " + b.BedShort + "</td>";
                    }
                    scheduleTable = scheduleTable + "<td class='centerAlignHeader'><br /></td></tr></thead><tbody>";

                    DateTime beginTime = DateTime.MinValue;
                    DateTime endTime = DateTime.MaxValue;

                    foreach (HOTBAL.Time t in scheduleTimes)
                    {
                        if (t != null)
                        {
                            if (t.TimeDay == dayOfDate.Substring(0, 3).ToUpper())
                            {
                                if (t.BeginTime == "CLOSED")
                                {
                                    scheduleTable = scheduleTable + "<tr><td class='centerAlignHeader' colspan='" + (bedCount + 2).ToString() + "'>" + t.BeginTime + "</td></tr>";
                                    isClosed = true;
                                }
                                else
                                {
                                    beginTime = Convert.ToDateTime(t.BeginTime);
                                    endTime = Convert.ToDateTime(t.EndTime);
                                }
                            }
                        }
                    }

                    if (!isClosed)
                    {
                        DateTime currentTime = beginTime;
                        int hourCount = 0;

                        while (currentTime < endTime)
                        {
                            if (hourCount == 20)
                            {
                                scheduleTable = scheduleTable + "<tr><td class='centerAlignHeader'><br /></td>";
                                foreach (HOTBAL.Bed b in scheduleBeds)
                                {
                                    if (b.BedDisplayInternal)
                                        scheduleTable = scheduleTable + "<td class='centerAlignHeader'>BED " + b.BedShort + "</td>";
                                }
                                scheduleTable = scheduleTable + "<td class='centerAlignHeader'><br /></td></tr>";
                                hourCount = 0;
                            }
                            else
                            {
                                scheduleTable = scheduleTable + "<tr><td class='rightAlignHeader'>" + currentTime.ToShortTimeString() + "</td>";
                                foreach (HOTBAL.Bed b in scheduleBeds)
                                {
                                    if (b.BedDisplayInternal)
                                    {
                                        HOTBAL.Tan tanInformation = sqlClass.GetTanInformationByData(b.BedShort, inDate, currentTime.ToShortTimeString(), "W");

                                        if ((tanInformation != null) && (tanInformation.TanID != 0))
                                        {
                                            bool needsProduct = false, needsRenewal = false, needsUpgrade = false, owesMoney = false;
                                            string noteText = String.Empty;

                                            if (!tanInformation.DeletedIndicator)
                                            {
                                                // Taken tan, look up the user information
                                                HOTBAL.Customer tanCustomer = sqlClass.GetCustomerInformationByID(tanInformation.CustomerID);

                                                // Build the tan cell
                                                if (String.IsNullOrEmpty(tanCustomer.Error))
                                                {
                                                    if (tanCustomer.RenewalDate <= Convert.ToDateTime(inDate))
                                                    {
                                                        needsRenewal = true;
                                                    }
                                                    else
                                                    {
                                                        if (tanCustomer.Notes != null)
                                                        {
                                                            if (tanCustomer.Notes.Count > 0)
                                                            {
                                                                foreach (HOTBAL.CustomerNote n in tanCustomer.Notes)
                                                                {
                                                                    if (n.NeedsUpgrade)
                                                                    {
                                                                        needsUpgrade = true;
                                                                    }
                                                                    else if (n.OwesMoney)
                                                                    {
                                                                        owesMoney = true;
                                                                    }
                                                                    else if (n.OwedProduct)
                                                                    {
                                                                        needsProduct = true;
                                                                    }

                                                                    noteText += n.NoteText.Replace(Convert.ToChar("'"), Convert.ToChar("\'")) + "<br />";
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (tanInformation.Length > 0)
                                                    {
                                                        scheduleTable = scheduleTable + "<td class='tanned'>";
                                                    }
                                                    else
                                                    {
                                                        if ((needsProduct) || (needsRenewal) || (tanCustomer.LotionWarning) || (owesMoney) || (needsUpgrade))
                                                        {
                                                            if (needsRenewal)
                                                            {
                                                                scheduleTable = scheduleTable + "<td class='expired'>";
                                                            }
                                                            else if (tanCustomer.LotionWarning)
                                                            {
                                                                scheduleTable = scheduleTable + "<td class='lotion'>";
                                                            }
                                                            else if (owesMoney)
                                                            {
                                                                scheduleTable = scheduleTable + "<td class='owes'>";
                                                            }
                                                            else if (needsUpgrade)
                                                            {
                                                                scheduleTable = scheduleTable + "<td class='check'>";
                                                            }
                                                            else if (needsProduct)
                                                            {
                                                                scheduleTable = scheduleTable + "<td class='owed'>";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            scheduleTable = scheduleTable + "<td>";
                                                        }
                                                    }

                                                    scheduleTable = scheduleTable + "<a href=\"" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID="
                                                    + tanInformation.CustomerID.ToString()
                                                    + "\" title=\"" + tanCustomer.FirstName.Replace(Convert.ToChar("'"), Convert.ToChar("\'")) + " "
                                                    + tanCustomer.LastName.Replace(Convert.ToChar("'"), Convert.ToChar("\'")) + "<br/>"
                                                    + "<strong>Package: </strong>" + tanCustomer.Plan + "<br/>"
                                                    + "<strong>Expiration: </strong>" + functionsClass.FormatSlash(tanCustomer.RenewalDate);

                                                    if (tanCustomer.Tans != null)
                                                        if (tanCustomer.Tans.Count >= 1)
                                                        {
                                                            foreach (HOTBAL.Tan t in tanCustomer.Tans)
                                                            {
                                                                if (Convert.ToDateTime(t.Date) < DateTime.Now)
                                                                    if (t.Length > 0)
                                                                    {
                                                                        scheduleTable = scheduleTable + "<br/>" + "<strong>Last Tan: </strong>" + t.Date + " " + t.Time;
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                    scheduleTable = scheduleTable + "\" class=\"taken\">"
                                                    + tanCustomer.LastName + ", "
                                                    + tanCustomer.FirstName.Substring(0, 1) + "</a>";

                                                    if (!String.IsNullOrEmpty(noteText))
                                                    {
                                                        scheduleTable = scheduleTable + "&nbsp;<a href='#' title=\"" + noteText.Trim() + "\" class=\"note\">*</a>";
                                                    }
                                                    scheduleTable = scheduleTable + "</td>";
                                                }
                                                else
                                                {
                                                    scheduleTable = scheduleTable + "<td><a href=\"" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID="
                                                   + tanInformation.CustomerID.ToString()
                                                   + "\" title=\"Unknown Customer\" class=\"taken\">Unknown</a></td>";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Available tan spot
                                            scheduleTable = scheduleTable + "<td><a href=\"" + HOTBAL.TansConstants.ADD_APPT_INTERNAL_URL + "?Date="
                                                   + inDate
                                                   + "&Time="
                                                   + currentTime.ToShortTimeString()
                                                   + "&Bed="
                                                   + b.BedShort
                                                   + "\">------</a></td>";
                                        }
                                    }
                                }
                                scheduleTable = scheduleTable + "<td class='leftAlignHeader'>" + currentTime.ToShortTimeString() + "</td></tr>";
                                currentTime = currentTime.AddMinutes(15);

                                hourCount++;
                            }
                        }
                    }
                    scheduleTable = scheduleTable + "</tbody><tfoot><tr><td><br /></td>";
                    foreach (HOTBAL.Bed b in scheduleBeds)
                    {
                        if (b.BedDisplayInternal)
                            scheduleTable = scheduleTable + "<td>BED " + b.BedShort + "</td>";
                    }
                    scheduleTable = scheduleTable + "<td><br /></td></tr></tfoot>";
                }
                else
                {
                    scheduleTable = "<tbody><tr><td>" + HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "</td></tr></tbody>";
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, dayOfDate.Substring(0, 3).ToUpper(), "Schedule: Build");
            }

            return scheduleTable;
        }

        protected void changeDate_Click(object sender, EventArgs e)
        {
            string goToDate = enterDate.Text.Trim();
            if (String.IsNullOrEmpty(goToDate))
            {
                goToDate = DateTime.Now.ToShortDateString();
            }

            Label currentDate = (Label)Master.FindControl("currentDate");
            currentDate.Text = functionsClass.FormatSlash(Convert.ToDateTime(goToDate));
            scheduleOutput.Text = WacoTanningSchedule(functionsClass.FormatDash(Convert.ToDateTime(goToDate)));
        }
    }
}