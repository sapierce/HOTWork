using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class MemberInfo : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        int tanCount = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!functionsClass.isLoggedIn())
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + HOTBAL.TansMessages.SESSION_EXPIRED_MOBILE + "<br />";
            }
            else
            {
                //Get customer information
                HOTBAL.Customer  user = new HOTBAL.Customer();
                user = sqlClass.GetCustomerInformationByID(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()));

                if (String.IsNullOrEmpty(user.Error))
                {
                    TimeSpan ts = Convert.ToDateTime(user.RenewalDate) - DateTime.Now;

                    if (ts.Days < 0)
                    {
                        renewalPrompt.Visible = true;
                    }
                    else
                    {
                        renewalPrompt.Visible = false;
                    }

                    emailAddress.Text = user.Email;
                    joinDate.Text = functionsClass.FormatSlash(user.JoinDate);
                    customerName.Text = user.FirstName + " " + user.LastName;
                    customerPlan.Text = user.Plan;
                    renewalDate.Text = functionsClass.FormatSlash(user.RenewalDate);

                    //if (user.VerifiedEmail)
                    //{
                    //    lblVerified.Text = "<img src='/images/verified.gif' width='20' height='18' alt='E-mail verified!' />";
                    //}
                    //else
                    //{
                    //    lblNonVerified.Text = "<br /><a href='MemberVerify.aspx'>Verify your e-mail address</a>";
                    //}

                    if (user.Tans != null)
                    {
                        foreach (HOTBAL.Tan i in user.Tans)
                        {
                            if (tanCount < 6)
                            {
                                if ((i.Length > 0) && (Convert.ToDateTime(i.Date + " " + i.Time) < DateTime.Now.AddHours(-1)) && (i.DeletedIndicator == false))
                                {
                                    //Previous used tans
                                    lblPreviousTans.Text += "<tr><td class='tableCellNoBorder'>" + i.Date +
                                        "</td><td class='tableCellNoBorder'>" + i.Time + "</td><td class='tableCellNoBorder'>" +
                                        i.Bed + "</td><td class='tableCellNoBorder'>" +
                                        i.Length + "</td></tr>";
                                    tanCount++;
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(lblPreviousTans.Text))
                        {
                            lblPreviousTans.Text = "<tr><td colspan=4 class='tableCellNoBorder'>No previous tans found</td></tr>";
                        }
                    }
                    else
                    {
                        lblPreviousTans.Text = "<tr><td colspan=4 class='tableCellNoBorder'>No previous tans found</td></tr>";
                    }

                    if (user.Tans != null)
                    {
                        foreach (HOTBAL.Tan i in user.Tans)
                        {
                            if ((i.Length == 0) && (Convert.ToDateTime(i.Date + " " + i.Time) > DateTime.Now.AddHours(-1)) && (i.DeletedIndicator == false))
                            {
                                //Pending tans
                                lblUpcomingTans.Text += "<tr><td class='tableCellNoBorder'>" + i.Date +
                                    "</td><td class='tableCellNoBorder'>" + i.Time + "</td><td class='tableCellNoBorder'>" +
                                    i.Bed + "</td><td class='tableCellNoBorder'><a href='" + HOTBAL.TansConstants.DELETE_APPT_MOBILE_URL + "?ID=" +
                                    i.TanID + "'>Delete</a></td></tr>";
                            }
                        }

                        if (String.IsNullOrEmpty(lblUpcomingTans.Text))
                        {
                            lblUpcomingTans.Text = "<tr><td colspan=4 class='tableCellNoBorder'>No scheduled tans found.</td></tr>";
                        }
                    }
                    else
                    {
                        lblUpcomingTans.Text = "<tr><td colspan=4 class='tableCellNoBorder'>No scheduled tans found.</td></tr>";
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + user.Error + "<br />";
                }
            }
        }
    }
}