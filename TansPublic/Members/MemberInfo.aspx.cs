using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Net.Mail;

namespace PublicWebsite.MembersArea
{
    public partial class MemberInfo : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        int tanCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Title + " - Member Information";
            if (!functionsClass.isLoggedIn())
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
            }
            else
            {
                //Get customer information
                HOTBAL.Customer  user = new HOTBAL.Customer ();
                user = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

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
                    currentPlan.Text = user.Plan;
                    if (user.SpecialFlag)
                    {
                        // User is on a Special, see what level
                        HOTBAL.SpecialLevel specialLevel = sqlClass.GetSpecialLevelByLevelID(user.SpecialID);
                        currentPlan.Text += " - " + specialLevel.SpecialLevelBed;
                    }
                    renewalDate.Text = functionsClass.FormatSlash(user.RenewalDate);

                    //if (user.VerifiedEmail)
                    //{
                    //    isVerified.Text = "<img src='/images/verified.gif' alt='E-mail verified!' />";
                    //}
                    //else
                    //{
                    //    isNotVerified.Text = "<br /><a href='MemberVerify.aspx'>Verify your e-mail address</a>";
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
                                    previousTans.Text += "<tr><td>" + i.Date +
                                        "</td><td>" + i.Time + "</td><td align='center'>" +
                                        i.Bed + "</td><td align='center'>" +
                                        i.Length + "</td></tr>";
                                    tanCount++;
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(previousTans.Text))
                        {
                            previousTans.Text = "<tr><td colspan=5>No previous tans found.</td></tr>";
                        }
                    }
                    else
                    {
                        previousTans.Text = "<tr><td colspan=5>No previous tans found.</td></tr>";
                    }

                    if (user.Tans != null)
                    {
                        user.Tans.Sort(delegate(HOTBAL.Tan n1, HOTBAL.Tan n2)
                        {
                            return n1.Date.CompareTo(n2.Date);
                        });

                        foreach (HOTBAL.Tan i in user.Tans)
                        {
                            if ((i.Length == 0) && (Convert.ToDateTime(i.Date + " " + i.Time) > DateTime.Now.AddHours(-1)) && (i.DeletedIndicator == false))
                            {
                                //Pending tans
                                upcomingTans.Text += "<tr><td>" + i.Date +
                                    "</td><td>" + i.Time + "</td><td align='center'>" +
                                    i.Bed + "</td><td><a href='" + HOTBAL.TansConstants.DELETE_APPT_PUBLIC_URL + "?ID=" +
                                    i.TanID + "'>Delete</a></td></tr>";
                            }
                        }

                        if (String.IsNullOrEmpty(upcomingTans.Text))
                        {
                            upcomingTans.Text = "<tr><td colspan=5>No scheduled tans found. <a href='" + HOTBAL.TansConstants.ADD_APPT_PUBLIC_URL + "' class='center'>Schedule one today!</a></td></tr>";
                        }
                    }
                    else
                    {
                        upcomingTans.Text = "<tr><td colspan=5>No scheduled tans found. <a href='" + HOTBAL.TansConstants.ADD_APPT_PUBLIC_URL + "' class='center'>Schedule one today!</a></td></tr>";
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = user.Error;
                }
            }
        }
    }
}
