using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerCombine : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Combine Customers";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            returnToAdmin.NavigateUrl = HOTBAL.TansConstants.ADMIN_INTERNAL_URL;
        }

        protected void combineCustomers_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            string mergeToCustomerID = keepId.Text;
            string mergeFromCustomerIDs = removeId.Text;

            if (!String.IsNullOrEmpty(mergeToCustomerID))
                {
                    if (!String.IsNullOrEmpty(mergeFromCustomerIDs))
                    {
                        string[] splitMergedIds = mergeFromCustomerIDs.Split(Convert.ToChar(","));
                        combinationResults.Text = String.Empty;
                        int customerCount = 1;

                        foreach (string customerId in splitMergedIds)
                        {
                            bool successfulUpdate = false;
                            combinationResults.Text += "Merging Customers (" + customerCount + " of " + splitMergedIds.Length + ")...<br />";
                            customerCount++;

                            // Merge History
                            successfulUpdate = sqlClass.MergeCustomerBillingHistory(mergeToCustomerID, customerId.Trim());

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Package/Billing History successfully merged.<br />";
                                successfulUpdate = false;
                                // Merge Notes
                                successfulUpdate = sqlClass.MergeCustomerNotes(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Package/Billing History not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Notes successfully merged.<br />";
                                successfulUpdate = false;
                                // Merge Transactions
                                successfulUpdate = sqlClass.MergeCustomerTransactions(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Notes not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Transactions successfully merged.<br />";
                                successfulUpdate = false;
                                // Merge Tan Log
                                successfulUpdate = sqlClass.MergeCustomerTanningLog(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Transactions not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Tanning log successfully merged.<br />";
                                successfulUpdate = false;
                                // Updating online sign up information
                                successfulUpdate = sqlClass.MergeCustomerOnlineSignUp(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Tanning log not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Online sign-up information successfully merged.<br />";
                                successfulUpdate = false;
                                // Update online logins
                                successfulUpdate = sqlClass.MergeCustomerLogins(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Online sign-up information not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Online logins successfully merged.<br />";
                                successfulUpdate = false;
                                // Updating main customer
                                successfulUpdate = sqlClass.MergeCustomerMain(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Online logins not successfully merged.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Main customer successfully updated.<br />";
                                successfulUpdate = false;
                                // Deactivating additional customers
                                successfulUpdate = sqlClass.MergeCustomerAdditional(mergeToCustomerID, customerId.Trim());
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Main customer not successfully updated.<br />";
                            }

                            if (successfulUpdate)
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Additional customer successfully deactivated.<br />";
                            }
                            else
                            {
                                combinationResults.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Additional customers not successfully deactivated.<br />";
                            }
                        }
                    }
                    else
                    {
                        errorLabel.Text = "Please enter a customer to remove.<br />";
                    }
                }
                else
                {
                    errorLabel.Text = "Please enter a customer to merge.<br />";
                }
        }

        protected void keepId_TextChanged(object sender, EventArgs e)
        {
            HOTBAL.Customer keepCustomer = sqlClass.GetCustomerInformationByID(Convert.ToInt64(keepId.Text));

            if (String.IsNullOrEmpty(keepCustomer.Error))
            {
                keepNames.Text = keepCustomer.FirstName + " " + keepCustomer.LastName;
            }
            else
                keepNames.Text = "Unable to locate customer.";
        }

        protected void removeId_TextChanged(object sender, EventArgs e)
        {
            string[] splitMergedIds = removeId.Text.Split(Convert.ToChar(","));
            removeNames.Text = String.Empty;

            foreach (string customerId in splitMergedIds)
            {
                HOTBAL.Customer removeCustomer = sqlClass.GetCustomerInformationByID(Convert.ToInt64(customerId.Trim()));

                if (String.IsNullOrEmpty(removeCustomer.Error))
                {
                    removeNames.Text += removeCustomer.FirstName + " " + removeCustomer.LastName + "<br />";
                }
                else
                    removeNames.Text += "Unable to locate customer.<br />";
            }
        }
    }
}