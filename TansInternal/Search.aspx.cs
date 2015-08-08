using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace HOTTropicalTans
{
    public partial class Search : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mainSearch.Visible = true;
                searchResults.Visible = false;
            }
        }

        /// <summary>
        /// Searching by customer first/last name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customerNameSearch_Click(Object sender, System.EventArgs e)
        {
            mainSearch.Visible = false;
            searchResults.Visible = true;
            outputResults.Text = String.Empty;

            List<HOTBAL.Customer> customerArray = sqlClass.GetCustomerByName(firstName.Text, lastName.Text, true);

            if (customerArray.Count > 0)
            {
                if (customerArray.Count == 1)
                {
                    Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerArray[0].ID);
                }
                else
                {
                    try
                    {
                        foreach (HOTBAL.Customer c in customerArray)
                        {
                            outputResults.Text += "<tr><td><a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + 
                                "?ID=" + c.ID + "'>" + c.LastName + ", " + c.FirstName + "</a></td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, firstName.Text + " " + lastName.Text, "Search: OutputNames");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "Error getting names<br />";
                    }
                }
            }
            else
            {
                outputResults.Text = "<tr><td colspan='2'>Unable to locate customer " + firstName.Text + " " + lastName.Text +
                    ".  Check the name and <a href='" + HOTBAL.TansConstants.SEARCH_INTERNAL_URL + "'>try again</a> or <a href='" +
                    HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL + "'>add a new user</a>.</td></tr>";
            }
        }

        public void customerLetter_Click(Object sender, System.EventArgs e)
        {
            mainSearch.Visible = false;
            searchResults.Visible = true;
            outputResults.Text = String.Empty;
            LinkButton lastLetter = (LinkButton)sender;

            List<HOTBAL.Customer> customerArray = sqlClass.GetCustomerByName("", lastLetter.Text, true);
            int rowCount = customerArray.Count;

            if (rowCount > 0)
            {
                if (rowCount == 1)
                {
                    Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerArray[0].ID);
                }
                else
                {
                    try
                    {
                        foreach (HOTBAL.Customer c in customerArray)
                        {
                            outputResults.Text += "<tr><td><a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + 
                                c.ID + "'>" + c.LastName + ", " + c.FirstName + "</a></td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, lastLetter.Text, "Search: LastNameLetter");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "Error getting names<br />";
                    }
                }
            }
            else
            {
                outputResults.Text = "<tr><td colspan='2'>Unable to locate customers with a last name beginning with " + 
                    lastLetter.Text + ".  <a href='" + HOTBAL.TansConstants.SEARCH_INTERNAL_URL + 
                    "'>Try again</a> or <a href='" + HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL + "'>add a new user</a>.</td></tr>";
            }
        }

        public void productSearch_Click(Object sender, System.EventArgs e)
        {
            mainSearch.Visible = false;
            searchResults.Visible = true;
            outputResults.Text = String.Empty;

            List<HOTBAL.Product> productArray = sqlClass.GetProductByName(productName.Text);
            int rowCount = productArray.Count;

            if (productArray.Count > 0)
            {
                if (productArray.Count == 1)
                {
                    Response.Redirect(HOTBAL.TansConstants.PRODUCT_INFO_INTERNAL_URL + "?ID=" + productArray[0].ProductId);
                }
                else
                {
                    try
                    {
                        foreach (HOTBAL.Product p in productArray)
                        {
                            outputResults.Text += "<tr><td><a href='" + HOTBAL.TansConstants.PRODUCT_INFO_INTERNAL_URL + "?ID=" +
                                p.ProductId + "'>" + p.ProductName + "</a></td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, productName.Text, "Search: Product");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "Error getting products<br />";
                    }
                }
            }
            else
            {
                outputResults.Text = "<tr><td colspan='2'>Unable to locate products containing " +
                    productName.Text + ".  <a href='" + HOTBAL.TansConstants.SEARCH_INTERNAL_URL +
                    "'>Try again</a> or <a href='" + HOTBAL.TansConstants.ADMIN_ADD_PRODUCT_URL + "'>add a new product</a>.</td></tr>";
            }
        }

        protected void tanLog_Click(object sender, EventArgs e)
        {
            mainSearch.Visible = false;
            searchResults.Visible = true;
            outputResults.Text = String.Empty;

            List<HOTBAL.Tan> tanArray = sqlClass.GetTanInformationByDate(functionsClass.FormatDash(Convert.ToDateTime(beginDate.Text)), functionsClass.FormatDash(Convert.ToDateTime(endDate.Text)));
            int rowCount = tanArray.Count;

            if (rowCount > 0)
            {
                if (rowCount == 1)
                {
                    Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + tanArray[0].CustomerID);
                }
                else
                {
                    try
                    {
                        foreach (HOTBAL.Tan t in tanArray)
                        {
                            HOTBAL.Customer tanCustomer = sqlClass.GetCustomerInformationByID(Convert.ToInt64(t.CustomerID));
                            string customerName = String.Empty;
                            if (String.IsNullOrEmpty(tanCustomer.Error))
                                customerName = tanCustomer.LastName + ", " + tanCustomer.FirstName;
                            else
                                customerName = "UNKNOWN";

                            outputResults.Text += "<tr><td><a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" +
                                    t.CustomerID + "'>" + customerName + "</a> on " + functionsClass.FormatSlash(Convert.ToDateTime(t.Date)) + " at " + t.Time + " in " + t.Bed + "</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, beginDate.Text, "Search: TanLog");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "Error getting tanning log<br />";
                    }
                }
            }
            else
            {
                outputResults.Text = "<tr><td colspan='2'>Unable to locate tans between " + beginDate.Text + " and " +
                    endDate.Text + ".  <a href='" + HOTBAL.TansConstants.SEARCH_INTERNAL_URL +
                    "'>Try again</a> or <a href='" + HOTBAL.TansConstants.ADD_APPT_INTERNAL_URL + "'>add a new tan</a>.</td></tr>";
            }
        }
    }
}