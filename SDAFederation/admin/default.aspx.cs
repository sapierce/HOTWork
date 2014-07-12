using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class _default : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                federationSchools.Items.AddRange(functionsClass.GetSchoolList());
                artSchool.Items.AddRange(functionsClass.GetSchoolList());
                beltSchool.Items.AddRange(functionsClass.GetSchoolList());

                List<Product> productList = methodsClass.GetFederationItems();
                foreach (Product product in productList)
                {
                    federationProducts.Items.Add(new ListItem(product.ProductName, product.ProductID.ToString()));
                }
            }
        }

        protected void editSchool_Click(object sender, EventArgs e)
        {
            Response.Redirect(FederationConstants.SCHOOL_UPDATE_URL + "?ID=" + federationSchools.SelectedValue);
        }

        protected void editArt_Click(object sender, EventArgs e)
        {
            Response.Redirect(FederationConstants.ART_UPDATE_URL + "?ID=" + artArts.SelectedValue);
        }

        protected void editBelt_Click(object sender, EventArgs e)
        {
            Response.Redirect(FederationConstants.BELT_UPDATE_URL + "?ID=" + beltBelts.SelectedValue);
        }

        protected void editProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect(FederationConstants.PRODUCT_UPDATE_URL + "?ID=" + federationProducts.SelectedValue);
        }

        protected void artSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            artArts.Items.Clear();
            artArts.Items.AddRange(functionsClass.GetArtList(Convert.ToInt32(artSchool.SelectedValue)));
        }

        protected void beltSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            beltArts.Items.Clear();
            beltArts.Items.AddRange(functionsClass.GetArtList(Convert.ToInt32(artSchool.SelectedValue)));
        }

        protected void beltArts_SelectedIndexChanged(object sender, EventArgs e)
        {
            beltBelts.Items.Clear();
            beltBelts.Items.AddRange(functionsClass.GetBeltList(Convert.ToInt32(beltArts.SelectedValue)));
        }
    }
}