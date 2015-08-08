using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class SpecialEdit : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Specials";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
            else
            {
                HOTBAL.Special specialDetail = sqlClass.GetSpecialByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"].ToString())));

                if (specialDetail.SpecialID != 0)
                {
                    HOTBAL.Product specialProductInfo = sqlClass.GetProductByID(specialDetail.SpecialProductID);

                    if (!String.IsNullOrEmpty(specialProductInfo.ProductName))
                    {
                        specialName.Text = specialDetail.SpecialName;
                        productID.Value = specialDetail.SpecialProductID.ToString();
                        specialAbbrName.Text = specialDetail.SpecialShortName;
                        specialPrice.Text = specialProductInfo.ProductPrice.ToString();
                        specialBarCode.Text = specialProductInfo.ProductCode;
                        displayInStore.Checked = specialProductInfo.IsAvailableInStore;
                        displayOnline.Checked = specialProductInfo.IsAvailableOnline;

                        List<HOTBAL.SpecialLevel> specialLevels = sqlClass.GetLevelsBySpecialID(specialDetail.SpecialID);
                        
                        foreach (HOTBAL.SpecialLevel sl in specialLevels)
                        {
                            switch(sl.SpecialLevelOrder)
                            {
                                case 1:
                                    specialLength1.Text = sl.SpecialLevelLength.ToString();
                                    specialLevelID1.Value = sl.SpecialLevelID.ToString();
                                    specialType1.Items.FindByValue(sl.SpecialLevelBed).Selected = true;
                                    break;
                                case 2:
                                    specialLength2.Text = sl.SpecialLevelLength.ToString();
                                    specialLevelID2.Value = sl.SpecialLevelID.ToString();
                                    specialType2.Items.FindByValue(sl.SpecialLevelBed).Selected = true;
                                    break;
                                case 3:
                                    specialLength3.Text = sl.SpecialLevelLength.ToString();
                                    specialLevelID3.Value = sl.SpecialLevelID.ToString();
                                    specialType3.Items.FindByValue(sl.SpecialLevelBed).Selected = true;
                                    break;
                                case 4:
                                    specialLength4.Text = sl.SpecialLevelLength.ToString();
                                    specialLevelID4.Value = sl.SpecialLevelID.ToString();
                                    specialType4.Items.FindByValue(sl.SpecialLevelBed).Selected = true;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        protected void editSpecial_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Add to Product
                bool updateProduct = sqlClass.EditProduct(Convert.ToInt64(productID.Value), functionsClass.CleanUp(specialName.Text), functionsClass.CleanUp(specialDescription.Text), "PKG", "OT",
                    functionsClass.CleanUp(specialBarCode.Text), functionsClass.InternalCleanUp(specialPrice.Text), false, "0.00", false, false, displayOnline.Checked, displayInStore.Checked, 0);

                if (updateProduct)
                {
                    // Add to Specials
                    bool updateSpecial = sqlClass.UpdateSpecial(Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), functionsClass.CleanUp(specialName.Text), functionsClass.CleanUp(specialAbbrName.Text), Convert.ToInt64(productID.Value), true);

                    int count = 1;
                    if (updateSpecial)
                    {
                        if (!String.IsNullOrEmpty(specialLength1.Text))
                        {
                            bool updateSpecialLevel = sqlClass.UpdateSpecialLevel(Convert.ToInt32(functionsClass.CleanUp(specialLevelID1.Value)), Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), specialType1.SelectedValue, Convert.ToInt32(specialLength1.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength2.Text))
                        {
                            bool updateSpecialLevel = sqlClass.UpdateSpecialLevel(Convert.ToInt32(functionsClass.CleanUp(specialLevelID2.Value)), Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), specialType2.SelectedValue, Convert.ToInt32(specialLength2.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength3.Text))
                        {
                            bool updateSpecialLevel = sqlClass.UpdateSpecialLevel(Convert.ToInt32(functionsClass.CleanUp(specialLevelID3.Value)), Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), specialType3.SelectedValue, Convert.ToInt32(specialLength3.Text), count);
                            count++;
                        }

                        if (!String.IsNullOrEmpty(specialLength4.Text))
                        {
                            bool updateSpecialLevel = sqlClass.UpdateSpecialLevel(Convert.ToInt32(functionsClass.CleanUp(specialLevelID4.Value)), Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), specialType4.SelectedValue, Convert.ToInt32(specialLength4.Text), count);
                            count++;
                        }
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                else
                {
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }
    }
}