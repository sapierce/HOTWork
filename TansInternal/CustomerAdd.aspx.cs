using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerAdd : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Customer";
            if (!Page.IsPostBack)
            {
                PopulatePlans();
                PopulateSpecials();
                joinDate.Text = functionsClass.FormatSlash(DateTime.Now);
            }
        }

        protected void PopulatePlans()
        {
            List<HOTBAL.Package> availablePlans = sqlClass.GetAllPackages();

            if (availablePlans != null)
            {
                plans.Items.Clear();
                plans.Items.Add(new ListItem("-Choose-", ""));

                foreach (HOTBAL.Package p in availablePlans)
                {
                    plans.Items.Add(new ListItem(p.PackageName, p.PackageID.ToString() + ":" + p.PackageLength.ToString()));
                }
            }
        }

        protected void PopulateSpecials()
        {
            List<HOTBAL.Special> availableSpecials = sqlClass.GetAllSpecials();

            if (availableSpecials != null)
            {
                specials.Items.Clear();
                specials.Items.Add(new ListItem("-Choose-", ""));

                foreach (HOTBAL.Special s in availableSpecials)
                {
                    specials.Items.Add(new ListItem(s.SpecialName, s.SpecialID.ToString() + ":" + s.SpecialLength.ToString()));
                }
            }
        }

        protected void plans_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] splitPlan = plans.SelectedValue.Split(Convert.ToChar(":"));

            DateTime calculateDate = DateTime.Now.AddDays(Convert.ToDouble(splitPlan[1]));

            renewalDate.Text = functionsClass.FormatSlash(calculateDate);
        }

        protected void specials_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] splitSpecial = specials.SelectedValue.Split(Convert.ToChar(":"));

            DateTime calculateDate = DateTime.Now.AddDays(Convert.ToDouble(splitSpecial[1]));

            renewalDate.Text = functionsClass.FormatSlash(calculateDate);
        }

        protected void addNewCustomer_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            try
            {
                string planName = "Other";
                int specialId = 0, specialLevelId = 0;
                bool isSpecial = false;

                if (!String.IsNullOrEmpty(plans.SelectedValue.ToString().Trim()))
                {
                    planName = getPackageName();
                }

                if (!String.IsNullOrEmpty(specials.SelectedValue.ToString().Trim()))
                {
                    planName = getSpecialName();
                    specialId = getSpecialId();
                    List<HOTBAL.SpecialLevel> getSpecialLevelInfo = sqlClass.GetLevelsBySpecialID(specialId);

                    foreach (HOTBAL.SpecialLevel level in getSpecialLevelInfo)
                    {
                        if (level.SpecialLevelOrder == 1)
                        {
                            specialLevelId = level.SpecialLevelID;
                            isSpecial = true;
                            break;
                        }
                    }
                }

                Int64 insertCustomer = sqlClass.InsertNewCustomer(firstName.Text,
                    lastName.Text, Convert.ToDateTime(joinDate.Text),
                    Convert.ToInt32(fitzpatrickNumber.SelectedValue.ToString().Trim()),
                    planName, Convert.ToDateTime(renewalDate.Text),
                    remarks.Text, false, false, isSpecial, specialLevelId, 
                    (isSpecial ? DateTime.Now : Convert.ToDateTime("2001-01-01")));

                if (insertCustomer > 0)
                    Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + insertCustomer.ToString());
                else
                {
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_ADD_CUSTOMER + "<br />";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, plans.SelectedValue + "/" + specials.SelectedValue, "Internal: Add Customer: addCustomer_Click");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
            }
        }

        private string getPackageName()
        {
            string[] splitPlan = plans.SelectedValue.Split(Convert.ToChar(":"));

            if (splitPlan.Length > 1)
            {
                HOTBAL.Package getPlanInfo = sqlClass.GetPackageByPackageID(Convert.ToInt32(splitPlan[0]));
                return getPlanInfo.PackageNameShort;
            }
            else
            {
                return String.Empty;
            }
        }

        private string getSpecialName()
        {
            string[] splitSpecial = specials.SelectedValue.ToString().Trim().Split(Convert.ToChar(":"));
            if (splitSpecial.Length > 1)
            {
                HOTBAL.Special getSpecialInfo = sqlClass.GetSpecialByID(Convert.ToInt32(splitSpecial[0]));
                return "OT-" + getSpecialInfo.SpecialShortName;
            }
            else
            {
                return String.Empty;
            }
        }

        private int getSpecialId()
        {
            string[] splitSpecial = specials.SelectedValue.ToString().Trim().Split(Convert.ToChar(":"));
            if (splitSpecial.Length > 1)
            {
                HOTBAL.Special getSpecialInfo = sqlClass.GetSpecialByID(Convert.ToInt32(splitSpecial[0]));
                return getSpecialInfo.SpecialID;
            }
            else
            {
                return 0;
            }
        }
    }
}