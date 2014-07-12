using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTPOS
{
    public partial class _Default : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods tansSqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.POSConstants.INTERNAL_NAME;

            if (!Page.IsPostBack)
            {
                transactionDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void submitPOS_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lastName.Text))
            {
                if (notACustomer.Checked)
                {
                    Response.Redirect(HOTBAL.POSConstants.CART_URL + "?ID=0&FN=" + firstName.Text + "&LN=" + lastName.Text + "&Action=");
                }
                else
                {
                    List<HOTBAL.Customer> customerName = tansSqlClass.GetCustomerByName(firstName.Text, lastName.Text, true);

                    if (customerName != null)
                    {
                        if (String.IsNullOrEmpty(customerName[0].Error))
                        {
                            if (customerName.Count > 1)
                            {
                                customerResults.Text = "<table class='tanning'>";
                                customerResults.Text += "<thead><tr><th>Customer Results</th></tr></thead><tbody>";
                                foreach (HOTBAL.Customer c in customerName)
                                {
                                    customerResults.Text += "<tr><td><a href='" + HOTBAL.POSConstants.CART_URL + "?ID=" + c.ID.ToString() + "&Action='>" + c.LastName + ", " + c.FirstName + "</a></td></tr>";
                                }
                                customerResults.Text += "</tbody></table>";
                            }
                            else
                            {
                                Response.Redirect(HOTBAL.POSConstants.CART_URL + "?ID=" + customerName[0].ID.ToString() + "&Action=");
                            }
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = customerName[0].Error;
                        }
                    }
                }
            }
        }

        protected void endOfShift_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(employeeNumber.Text))
            {
                Response.Redirect(HOTBAL.POSConstants.TRANSACTION_LOG_URL + "?ID=" + employeeNumber.Text + "&StartDate=" + transactionDate.Text + "&EndDate=" + transactionDate.Text + "&Totals=False");
            }
        }
    }
}