using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Customer Information";

            try
            {
                if (!Page.IsPostBack)
                {
                    List<HOTBAL.Package> packageList = sqlClass.GetAllPackages();

                    if (packageList.Count > 0)
                    {
                        foreach (HOTBAL.Package p in packageList)
                        {
                            planList.Items.Add(new ListItem(p.PackageName, p.PackageNameShort));
                        }
                    }

                    List<HOTBAL.Special> specialsList = sqlClass.GetAllSpecials();

                    specialList.Items.Add(new ListItem("Other", "0"));
                    if (specialsList.Count > 0)
                    {
                        foreach (HOTBAL.Special s in specialsList)
                        {
                            specialList.Items.Add(new ListItem(s.SpecialName, s.SpecialID.ToString()));
                        }
                    }

                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt32(Request.QueryString["ID"]));

                        if (String.IsNullOrEmpty(customerInfo.Error))
                        {
                            firstName.Text = customerInfo.FirstName;
                            lastName.Text = customerInfo.LastName;
                            fitzNumber.Items.FindByText(customerInfo.FitzPatrickNumber.ToString()).Selected = true;
                            joinDate.Text = functionsClass.FormatSlash(customerInfo.JoinDate);
                            renewalDate.Text = functionsClass.FormatSlash(customerInfo.RenewalDate);
                            expirationLabel.Text = functionsClass.FormatSlash(customerInfo.RenewalDate);

                            if (customerInfo.SpecialFlag)
                            {
                                planList.Enabled = false;
                                specialList.Enabled = true;
                                HOTBAL.SpecialLevel specialLevel = sqlClass.GetSpecialLevelByLevelID(customerInfo.SpecialID);

                                HOTBAL.Special specialName = sqlClass.GetSpecialByID(specialLevel.SpecialID);
                                specialList.Items.FindByText(specialName.SpecialName).Selected = true;


                                List<HOTBAL.SpecialLevel> levelList = sqlClass.GetLevelsBySpecialID(specialLevel.SpecialID);

                                specialLevelsList.Items.Add(new ListItem("Other", "0"));
                                if (levelList.Count > 0)
                                {
                                    foreach (HOTBAL.SpecialLevel s in levelList)
                                    {
                                        specialLevelsList.Items.Add(new ListItem(s.SpecialLevelBed, s.SpecialLevelID.ToString()));
                                    }
                                }

                                specialLevelsList.Items.FindByValue(customerInfo.SpecialID.ToString()).Selected = true;
                                specialDate.Text = functionsClass.FormatSlash(customerInfo.SpecialDate);
                            }
                            else
                            {
                                specialDate.Text = functionsClass.FormatSlash(customerInfo.SpecialDate);
                                planList.Enabled = true;
                                specialList.Enabled = false;
                                specialLevelsList.Enabled = false;
                                specialLevelsList.Items.Add(new ListItem("Other", "0"));
                                specialDate.Enabled = false;
                                planList.Items.FindByText(customerInfo.Plan).Selected = true;
                            }
                            remarkInfo.Text = customerInfo.Remarks;
                            lotionCheck.Checked = customerInfo.LotionWarning;
                            onlineCheck.Checked = customerInfo.OnlineRestriction;
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
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL;
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, "", "Internal: Customer Edit");
            }
        }

        protected void editCustomer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    bool updateResponse = false;
                    if (Convert.ToDateTime(renewalDate.Text) != Convert.ToDateTime(expirationLabel.Text))
                    {
                        if (String.IsNullOrEmpty(changeExpiration.Text))
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = "Validation is required to change the expiration date.";
                        }
                        else
                        {
                            if ((sqlClass.AdministrationCheck(changeExpiration.Text, "Renewal")) || (sqlClass.AdministrationCheck(changeExpiration.Text, "Override"))) // == "mgrimes2")
                            {
                                updateResponse = sqlClass.UpdateCustomerInformation(firstName.Text, lastName.Text, Convert.ToInt32(fitzNumber.SelectedValue),
                                    Convert.ToDateTime(joinDate.Text), Convert.ToDateTime(renewalDate.Text), planList.SelectedValue,
                                    specialLevelsList.Enabled, Convert.ToInt32(specialLevelsList.SelectedValue), Convert.ToDateTime(specialDate.Text), 
                                    remarkInfo.Text, lotionCheck.Checked, onlineCheck.Checked, Convert.ToInt64(Request.QueryString["ID"]));
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = "Validation is invalid.  Please try again.";
                            }
                        }
                    }
                    else
                    {
                        updateResponse = sqlClass.UpdateCustomerInformation(firstName.Text, lastName.Text, Convert.ToInt32(fitzNumber.SelectedValue),
                            Convert.ToDateTime(joinDate.Text), Convert.ToDateTime(renewalDate.Text), planList.SelectedValue, 
                            specialLevelsList.Enabled, Convert.ToInt32(specialLevelsList.SelectedValue), Convert.ToDateTime(specialDate.Text), 
                            remarkInfo.Text, lotionCheck.Checked, onlineCheck.Checked, Convert.ToInt64(Request.QueryString["ID"]));
                    }

                    if (!updateResponse)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += HOTBAL.TansMessages.ERROR_EDIT_CUSTOMER;
                    }
                    else
                    {
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
                    }
                }
                catch (Exception ex)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "Internal: Customer Edit Click");
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "<br />";
            }
        }

        protected void deleteCustomer_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            if (Request.QueryString["ID"] != null)
            {
                long customerId = Convert.ToInt64(Request.QueryString["ID"].ToString());
                bool success = sqlClass.DeleteCustomerOnlineAccount(customerId);

                if (success)
                    success = sqlClass.DeleteCustomerInformation(customerId);

                if (success)
                    Response.Redirect(HOTBAL.TansConstants.MAIN_INTERNAL_URL, false);
                else
                {
                    sqlClass.LogErrorMessage(new Exception("CannotDeleteCustomer"), Request.QueryString["ID"].ToString(), "Internal: Delete Customer");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_DELETE_CUSTOMER + "<br />";
                }
            }
            else
            {
                sqlClass.LogErrorMessage(new Exception("NoCustomerId"), "", "Internal: Delete Customer");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL + "<br />";
            }
        }
    }
}