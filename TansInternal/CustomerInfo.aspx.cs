using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace HOTTropicalTans
{
    public partial class CustomerInfo : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Customer Information";

            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string customerId = Request.QueryString["ID"].ToString();

                if (ConfigurationManager.AppSettings["MassageEnabled"].ToString() == "Y")
                {
                    addMassageAppointment.Visible = true;
                }
                else
                {
                    addMassageAppointment.Visible = false;
                }

                HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt64(customerId));

                if (String.IsNullOrEmpty(customerInfo.Error))
                {
                    customerInfo = functionsClass.PlanRenewalCheck(customerInfo);

                    customerID.Text = customerId;
                    customerName.Text = customerInfo.FirstName + " " + customerInfo.LastName;
                    if (!customerInfo.IsActive)
                    {
                        customerID.Text += " - INACTIVE";
                        customerName.Text += " - INACTIVE";
                    }

                    fitzpatrickNumber.Text = customerInfo.FitzPatrickNumber.ToString();
                    joinDate.Text = functionsClass.FormatSlash(customerInfo.JoinDate);
                    renewalDate.Text = functionsClass.FormatSlash(customerInfo.RenewalDate);
                    remarks.Text = customerInfo.Remarks;
                    emailAddress.Text = customerInfo.Email;

                    if (customerInfo.SpecialID > 0)
                    {
                        HOTBAL.SpecialLevel levelInfo = sqlClass.GetSpecialLevelByLevelID(customerInfo.SpecialID);

                        HOTBAL.Special specialInfo = sqlClass.GetSpecialByID(levelInfo.SpecialID);
                        planName.Text = specialInfo.SpecialName + "-" + levelInfo.SpecialLevelBed;
                        specialRenewalInformation.Text = "<tr><td class='rightAlignHeader'>Special Level Renewal Date:</td><td>"
                            + functionsClass.FormatSlash(customerInfo.SpecialDate)
                            + "</td><td><br /></td>";
                    }
                    else
                        planName.Text = customerInfo.Plan;

                    if (customerInfo.OnlineUser)
                    {
                        onlineUser.Checked = true;
                        onlineUserInfo.Text = "<a href='" + HOTBAL.TansConstants.CUSTOMER_ONLINE_INFO_INTERNAL_URL +
                            "?ID=" + customerId + "'>View Account Info</a>";
                    }

                    if (customerInfo.NewOnlineCustomer)
                        signUpOnline.Checked = true;

                    int count = 0;

                    if (customerInfo.Notes != null)
                    {
                        if (customerInfo.Notes.Count > 0)
                        {
                            foreach (HOTBAL.CustomerNote n in customerInfo.Notes)
                            {
                                if (count == 0)
                                {
                                    count++;
                                    notes.Text += "<tr><td class='rightAlignHeader'>Special Notes:</td><td class='standardField'>";
                                    notes.Text += n.NoteText + "</td>";
                                    notes.Text += "<td class='standardField'><a href='" + HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL +
                                        "?ID=" + customerId + "&NID=" + n.NoteID +
                                        "&Act=Edit'>Edit</a> | <a href='" + HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL +
                                        "?ID=" + customerId +
                                        "&NID=" + n.NoteID + "&Act=Delete'>Delete</a></td></tr>";
                                }
                                else
                                {
                                    notes.Text += "<tr><td class='rightAlignHeader'><br /></td><td class='standardField'>";
                                    notes.Text += n.NoteText + "</td>";
                                    notes.Text += "<td class='standardField'><a href='" + HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL +
                                        "?ID=" + customerId + "&NID=" + n.NoteID +
                                        "&Act=Edit'>Edit</a> | <a href='" + HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL +
                                        "?ID=" + customerId + "&NID=" + n.NoteID + "&Act=Delete'>Delete</a></td></tr>";
                                }
                            }
                            notes.Text += "<tr><td class='rightAlignHeader'><br /></td><td class='standardField'><a href='" +
                                HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL + "?ID=" + customerId +
                                "'>Add a note</a></td><td class='standardField'><br /></td></tr>";
                        }
                        else
                        {
                            notes.Text = "<tr><td class='rightAlignHeader'>Special Notes:</td><td class='standardField'><a href='" +
                                HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL + "?ID=" + customerId +
                                "'>Add a note</a></td><td class='standardField'><br /></td></tr>";
                        }
                    }
                    else
                    {
                        notes.Text = "<tr><td class='rightAlignHeader'>Special Notes:</td><td class='standardField'><a href='" +
                            HOTBAL.TansConstants.CUSTOMER_NOTES_INTERNAL_URL + "?ID=" + customerId +
                            "'>Add a note</a></td><td class='standardField'><br /></td></tr>";
                    }

                    count = 0;
                    if (customerInfo.Tans != null)
                    {
                        if (customerInfo.Tans.Count > 0)
                        {
                            foreach (HOTBAL.Tan t in customerInfo.Tans)
                            {
                                if (count < 10)
                                {
                                    if (!t.DeletedIndicator)
                                    {
                                        count++;
                                        tanLog.Text += "<tr><td class='standardField'>" + count.ToString()
                                            + "</td><td class='standardField' align='right'>" + t.Date
                                            + "</td><td class='standardField' align='center'>" + t.Time
                                            + "</td><td class='standardField' align='center'>" + t.Bed
                                            + "</td><td class='standardField' align='center'>" + t.Length.ToString()
                                            + "</td>"
                                            + "<td class='standardField' align='center'><a href='" + HOTBAL.TansConstants.EDIT_APPT_INTERNAL_URL + "?TanID=" + t.TanID.ToString() + "'>Edit</a></td>"
                                            + "<td class='standardField' align='center'><a href='" + HOTBAL.TansConstants.DELETE_APPT_INTERNAL_URL + "?TanID=" + t.TanID.ToString() + "'>Delete</a></td></tr>";
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }

                    if (ConfigurationManager.AppSettings["MassageEnabled"].ToString() == "Y")
                    {
                        count = 0;
                        if (customerInfo.Massages != null)
                        {
                            if (customerInfo.Massages.Count > 0)
                            {
                                foreach (HOTBAL.Massage m in customerInfo.Massages)
                                {
                                    if (count < 10)
                                    {
                                        count++;
                                        massageLog.Text += "<tr><td class='standardField'>" + count.ToString()
                                            + "</td><td class='standardField' align='right'>" + m.Date.ToShortDateString()
                                            + "</td><td class='standardField' align='center'>" + m.Time
                                            + "</td><td class='standardField' align='center'>" + m.Length.ToString()
                                            + "</td>"
                                            + "<td class='standardField' align='center'><a href='" + HOTBAL.TansConstants.EDIT_APPT_MASSAGE_INTERNAL_URL + "?MassageID=" + m.ID.ToString() + "'>Edit</a></td>"
                                            + "<td class='standardField' align='center'><a href='" + HOTBAL.TansConstants.DELETE_APPT_MASSAGE_INTERNAL_URL + "?MassageID=" + m.ID.ToString() + "'>Delete</a></td></tr>";
                                    }
                                    else
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = customerInfo.Error;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE;
            }
        }

        protected void editCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_EDIT_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
        }

        protected void addTanningAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.ADD_APPT_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
        }

        protected void addMassageAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.ADD_APPT_MASSAGE_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
        }

        protected void transactionList_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_TRANS_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
        }
    }
}