using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class PackageAdd : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Package";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
        }

        protected void packageLength_TextChanged(object sender, EventArgs e)
        {
            if (unlimitedTans.Checked)
            {
                // Unlimited tans for duration
                switch (packageLength.Text)
                {
                    case "7":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Week";
                        packageShort.Text = packageType.SelectedValue.ToString() + " WEEK";
                        break;
                    case "13":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Two Week";
                        packageShort.Text = packageType.SelectedValue.ToString() + " 2WEEK";
                        break;
                    case "14":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Two Week";
                        packageShort.Text = packageType.SelectedValue.ToString() + " 2WEEK";
                        break;
                    case "29":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Month";
                        packageShort.Text = packageType.SelectedValue.ToString() + " MONTH";
                        break;
                    case "30":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Month";
                        packageShort.Text = packageType.SelectedValue.ToString() + " MONTH";
                        break;
                    case "90":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Three Months";
                        packageShort.Text = packageType.SelectedValue.ToString() + " 3MONTH";
                        break;
                    case "120":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Semester";
                        packageShort.Text = packageType.SelectedValue.ToString() + " SEMESTER";
                        break;
                    case "180":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Six Months";
                        packageShort.Text = packageType.SelectedValue.ToString() + " 6MONTH";
                        break;
                    case "365":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " One Year";
                        packageShort.Text = packageType.SelectedValue.ToString() + " YEAR";
                        break;
                    default:
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " " + packageLength.Text + " Days";
                        packageShort.Text = packageType.SelectedValue.ToString() + " " + packageLength.Text + " DAYS";
                        break;
                }
            }
            else
            {
                // Specific tan count for duration
                packageName.Text = packageType.SelectedItem.Text.ToString() + " " + packageTanCount.Text + " Tans";
                packageShort.Text = packageType.SelectedValue.ToString() + " " + packageTanCount.Text + " TANS";
            }
        }

        protected void addPackage_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                // Add to PROD_DOMN
                Int64 productID = sqlClass.AddProduct(packageShort.Text, "", "PKG", packageType.SelectedValue, "", packagePrice.Text, false, 
                    salePrice.Text, saleOnlineOnly.Checked, saleInStoreOnly.Checked, availableOnline.Checked, availableInStore.Checked);

                // Add to PLAN_DOMN
                if (productID > 0)
                {
                    Int64 packageID = sqlClass.AddPackage(packageShort.Text, packageName.Text, packageType.SelectedValue, Convert.ToInt32(packageLength.Text), productID);

                    if (packageID > 0)
                        Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }
    }
}