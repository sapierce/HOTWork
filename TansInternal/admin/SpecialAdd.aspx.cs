using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class SpecialAdd : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Specials";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
        }

        protected void onClick_btnAddSpecial(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Add to Product
                Int64 addProduct = sqlClass.AddProduct(functionsClass.CleanUp(specialName.Text), functionsClass.CleanUp(specialDescription.Text), "PKG", "OT", "", 
                    functionsClass.InternalCleanUp(specialPrice.Text), false, "0.00", false, false, availableOnline.Checked, availableInStore.Checked);

                if (addProduct > 0)
                {
                    // Add to Specials
                    Int64 addSpecial = sqlClass.AddSpecial(functionsClass.CleanUp(specialName.Text), functionsClass.CleanUp(specialAbbrName.Text), addProduct);

                    int count = 1;
                    if (addSpecial > 0)
                    {
                        if (!String.IsNullOrEmpty(specialLength1.Text))
                        {
                            bool addSpecialLevel = sqlClass.AddSpecialLevel(addSpecial, specialType1.SelectedValue, Convert.ToInt32(specialLength1.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength2.Text))
                        {
                            bool addSpecialLevel = sqlClass.AddSpecialLevel(addSpecial, specialType2.SelectedValue, Convert.ToInt32(specialLength2.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength3.Text))
                        {
                            bool addSpecialLevel = sqlClass.AddSpecialLevel(addSpecial, specialType3.SelectedValue, Convert.ToInt32(specialLength3.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength4.Text))
                        {
                            bool addSpecialLevel = sqlClass.AddSpecialLevel(addSpecial, specialType4.SelectedValue, Convert.ToInt32(specialLength4.Text), count);
                            count++;
                        }
                    }
                    else
                    {
                        lblError.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                else
                {
                    lblError.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }
    }
}