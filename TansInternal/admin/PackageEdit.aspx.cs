using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class PackageEdit : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Package";

            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (!Page.IsPostBack)
            {
                try
                {
                    HOTBAL.Package editPackage = sqlClass.GetPackageByPackageID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (editPackage != null)
                    {
                        if (editPackage.ProductID != 0)
                        {
                            packageBarcode.Text = editPackage.PackageBarCode;
                            packageLength.Text = editPackage.PackageLength.ToString();
                            packageName.Text = editPackage.PackageName;
                            packageShortName.Text = editPackage.PackageNameShort;
                            packagePrice.Text = String.Format("{0:F2}", editPackage.PackagePrice);
                            packageType.Items.FindByValue(editPackage.PackageBed).Selected = true;
                            packageTanCount.Text = editPackage.PackageTanCount.ToString();

                            if (editPackage.PackageTanCount == 0)
                            {
                                unlimitedTans.Checked = true;
                                packageTanCount.Enabled = false;
                            }

                            availableInStore.Checked = editPackage.PackageAvailableInStore;
                            availableOnline.Checked = editPackage.PackageAvailableOnline;
                            saleInStoreOnly.Checked = editPackage.PackageSaleStore;
                            saleOnlineOnly.Checked = editPackage.PackageSaleOnline;
                            salePrice.Text = String.Format("{0:F2}", editPackage.PackageSalePrice);
                            productID.Value = editPackage.ProductID.ToString();
                        }
                        else
                            errorLabel.Text += HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                    }
                    else
                        errorLabel.Text += HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                }
                catch (Exception ex)
                {
                    sqlClass.LogErrorMessage(ex, Request.QueryString["ID"].ToString(), "Admin: ProductEdit");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }

        protected void packageLength_TextChanged(object sender, EventArgs e)
        {
            if (unlimitedTans.Checked)
            {
                // Unlimited tans for duration
                switch (packageLength.Text)
                {
                    case "7":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " One Week";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " 1WEEK";
                        break;
                    case "13":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Two Week";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " 2WEEK";
                        break;
                    case "14":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Two Week";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " 2WEEK";
                        break;
                    case "29":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Month";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " MONTH";
                        break;
                    case "30":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Month";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " MONTH";
                        break;
                    case "90":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Three Months";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " 3MONTH";
                        break;
                    case "120":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Semester";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " SEMESTER";
                        break;
                    case "180":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " Six Months";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " 6MONTH";
                        break;
                    case "365":
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " One Year";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " YEAR";
                        break;
                    default:
                        packageName.Text = packageType.SelectedItem.Text.ToString() + " " + packageLength.Text + " Days";
                        packageShortName.Text = packageType.SelectedValue.ToString() + " " + packageLength.Text + " DAYS";
                        break;
                }
            }
            else
            {
                // Specific tan count for duration
                packageName.Text = packageType.SelectedItem.Text.ToString() + " " + packageTanCount.Text + " Tans";
                packageShortName.Text = packageType.SelectedValue.ToString() + " " + packageTanCount.Text + " TANS";
            }
        }

        protected void editPackage_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                bool isSuccessful = sqlClass.EditProduct(Convert.ToInt64(productID.Value), packageName.Text, packageShortName.Text, "PKG", packageType.Text, packageBarcode.Text, packagePrice.Text,
                    false, salePrice.Text, saleOnlineOnly.Checked, saleInStoreOnly.Checked, availableOnline.Checked, availableInStore.Checked, 0);

                if (isSuccessful)
                {
                    isSuccessful = sqlClass.EditPackage(Convert.ToInt64(Request.QueryString["ID"]), packageShortName.Text, packageName.Text, packageType.SelectedValue, Convert.ToInt32(packageLength.Text), Convert.ToInt64(productID.Value));
                }

                if (isSuccessful)
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
                else
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }

        protected void deletePackage_Click(object sender, EventArgs e)
        {
            // TODO: Delete PROD_DOMN
            // TODO: Delete PLAN_DOMN
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccess = sqlClass.UpdateProductStatus(Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"])), false);

            if (isSuccess)
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
            else
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }
    }
}